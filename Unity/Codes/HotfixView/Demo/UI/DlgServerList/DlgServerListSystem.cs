using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	[FriendClass(typeof(DlgServerList)), FriendClass(typeof(ServerInfosComponent))]
	[FriendClass(typeof(Scroll_Item_SeverToggle))]
	[FriendClassAttribute(typeof(ET.ServerInfo))]
	public static class DlgServerListSystem
	{

		public static void RegisterUIEvent(this DlgServerList self)
		{
			//Mark:据说一定要用Lambda表达式包裹，不然热更新不生效
			self.View.ELoopScrollList_ServerlistLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopItemRefresh);
		}

		public static async void ShowWindow(this DlgServerList self, Entity contextData = null)
		{
			self.View.EButton_DecidedButton.interactable = false;
			var zoneScene = self.ZoneScene();
			int error = await LoginHelper.GetServerInfos(zoneScene);
			if (error != ErrorCode.ERR_Success)
			{
				Log.Error(error.ToString());
				return;
            }
            var serverInfosCmp = self.ZoneScene().GetComponent<ServerInfosComponent>();
            self.AddUIScrollItems(ref self.serverToggleDict, serverInfosCmp.serverInfos.Count);
            self.View.ELoopScrollList_ServerlistLoopVerticalScrollRect.SetVisible(true, serverInfosCmp.serverInfos.Count);
            //self.View.ELoopScrollList_ServerlistLoopVerticalScrollRect.RefreshCells();
		}

		public static void HideWindow(this DlgServerList self)
		{
			self.RemoveUIScrollItems(ref self.serverToggleDict);
		}

		static void OnLoopItemRefresh(this DlgServerList self, Transform trans, int index)
        {
			//加载显示区服信息
			var info = self.ZoneScene().GetComponent<ServerInfosComponent>().serverInfos[index];
			var item = self.serverToggleDict[index];

			item.uiTransform = trans;
            item.ELabelText.text = info.serverName;

			var toggle = trans.GetComponent<Toggle>();
			toggle.isOn = false;
			toggle.group = self.toggleGroup;
			toggle.onValueChanged.RemoveAllListeners();
			toggle.onValueChanged.AddListener(isOn =>
			{
				if (!isOn)
                    return;

                self.View.EButton_DecidedButton.interactable = true;
			});
        }
	}

	public class DlgServerListSystemAwakeSystem : AwakeSystem<DlgServerList>
	{
		public override void Awake(DlgServerList self)
		{
			self.serverToggleDict = new Dictionary<int, Scroll_Item_SeverToggle>();

			self.toggleGroup = self.View.ELoopScrollList_ServerlistLoopVerticalScrollRect.content.GetComponent<ToggleGroup>();
		}
	}
}
