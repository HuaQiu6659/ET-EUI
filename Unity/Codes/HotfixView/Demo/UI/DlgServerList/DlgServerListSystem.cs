using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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
			self.View.EButton_DecidedButton.AddListener(self.OnServerSelectDecideButtonClick);
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

            //显示窗口时重置已选择的服务器编号
            zoneScene.GetComponent<ServerInfosComponent>().currentServerId = ServerInfosComponent.UnSelectId;
			self.View.ELabel_SelectServerText.text = string.Empty;
			//self.View.ELoopScrollList_ServerlistLoopVerticalScrollRect.RefreshCells();
        }

		public static void HideWindow(this DlgServerList self)
		{
			self.RemoveUIScrollItems(ref self.serverToggleDict);
		}

		static void OnLoopItemRefresh(this DlgServerList self, Transform trans, int index)
        {
			//加载显示区服信息
			var infosManager = self.ZoneScene().GetComponent<ServerInfosComponent>();
            var info = infosManager.serverInfos[index];
			var item = self.serverToggleDict[index];

			item.uiTransform = trans;
            item.ELabelText.text = info.serverName;

			var selectedLabel = self.View.ELabel_SelectServerText;

			var toggle = trans.GetComponent<Toggle>();
			toggle.isOn = false;
			toggle.group = self.toggleGroup;
			toggle.onValueChanged.RemoveAllListeners();
			toggle.onValueChanged.AddListener(isOn =>
			{
				if (!isOn)
				{
					if (!self.toggleGroup.AnyTogglesOn())
					{
						infosManager.currentServerId = ServerInfosComponent.UnSelectId;
                        self.View.EButton_DecidedButton.interactable = false;
						selectedLabel.text = string.Empty;
                    }
                    return;
                }

				infosManager.currentServerId = index + 1;	//区号比编号大1
                self.View.EButton_DecidedButton.interactable = true;
                selectedLabel.text = info.serverName;
            });
        }

		static void OnServerSelectDecideButtonClick(this DlgServerList self)
		{
			var uiCmp = self.DomainScene().GetComponent<UIComponent>();
			uiCmp.HideWindow(WindowID.WindowID_ServerList);
			uiCmp.ShowWindow(WindowID.WindowID_RoleList);
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
