using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

namespace ET
{
	[FriendClass(typeof(DlgServerList)), FriendClass(typeof(ServerInfosComponent))]
	[FriendClass(typeof(Scroll_Item_ServerToggle))]
	[FriendClassAttribute(typeof(ET.ServerInfo))]
	public static class DlgServerListSystem
	{
		public static void RegisterUIEvent(this DlgServerList self)
		{
			//Mark:据说一定要用Lambda表达式包裹，不然热更新不生效？
			self.View.ELoopScrollList_ServerlistLoopVerticalScrollRect.AddItemRefreshListener(self.OnLoopItemRefresh);
		}

		public static async void ShowWindow(this DlgServerList self, Entity contextData = null)
		{
			var zoneScene = self.ZoneScene();
			int error = await LoginHelper.GetServerInfos(zoneScene);
			if (error != ErrorCode.ERR_Success)
			{
				Log.Error(error.ToString());
				return;
            }
            var serverInfosCmp = self.ZoneScene().GetComponent<ServerInfosComponent>();
			var toggleDict = self.GetToggleDict();
            self.AddUIScrollItems(ref toggleDict, serverInfosCmp.servers.Count);
            self.View.ELoopScrollList_ServerlistLoopVerticalScrollRect.SetVisible(true, serverInfosCmp.servers.Count);

            //显示窗口时重置已选择的服务器编号
            serverInfosCmp.currentServerId = ServerInfosComponent.UnSelectId;
			//self.View.ELoopScrollList_ServerlistLoopVerticalScrollRect.RefreshCells();
        }

		static Dictionary<int, Scroll_Item_ServerToggle> GetToggleDict(this DlgServerList self)
        {
            var paramsCmp = self.GetComponent<DlgParamsComponent>();
            return paramsCmp.GetValue<Dictionary<int, Scroll_Item_ServerToggle>>("serverToggleDict");
        }

		static ToggleGroup GetToggleGroup(this DlgServerList self)
        {
            var paramsCmp = self.GetComponent<DlgParamsComponent>();
			return paramsCmp.GetValue<ToggleGroup>("toggleGroup");
        }

        public static void HideWindow(this DlgServerList self)
        {
            var toggleDict = self.GetToggleDict();
            self.RemoveUIScrollItems(ref toggleDict);
        }

		static void OnLoopItemRefresh(this DlgServerList self, Transform trans, int index)
        {
			//加载显示区服信息
			var infosManager = self.ZoneScene().GetComponent<ServerInfosComponent>();
            var info = infosManager.servers[index];
			if (!self.GetToggleDict().TryGetValue(index, out var item))
                return;

			var uiCmp = self.ZoneScene().GetComponent<UIComponent>();
            item.uiTransform = trans;
            item.E_LabelText.text = info.serverName;
			item.E_EnterButton.AddListener(() =>
			{
				infosManager.currentServerId = index + 1;   //区号比编号大1

				//TODO:获取账号物品信息
					//TODO:无物品信息时显示创建角色引导

				//TODO:获取、显示该频道房间列表
				uiCmp.HideWindow(WindowID.WindowID_ServerList);

            });

			var toogleGroup = self.GetToggleGroup();
            var toggle = trans.GetComponent<Toggle>();
			toggle.isOn = false;
			toggle.group = toogleGroup;
        }

        public class DlgServerListSystemAwakeSystem : AwakeSystem<DlgServerList>
        {
            public override void Awake(DlgServerList self)
            {
				var paramsCmp = self.AddComponent<DlgParamsComponent>();
				paramsCmp.SetValue("serverToggleDict", new Dictionary<int, Scroll_Item_ServerToggle>());
				paramsCmp.SetValue("toggleGroup", self.View.ELoopScrollList_ServerlistLoopVerticalScrollRect.content.GetComponent<ToggleGroup>());
            }
        }
    }
}
