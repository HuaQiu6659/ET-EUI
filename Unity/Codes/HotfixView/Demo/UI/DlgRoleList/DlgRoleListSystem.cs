using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

namespace ET
{
    [FriendClass(typeof(DlgRoleList))]
    [FriendClassAttribute(typeof(ET.ServerInfosComponent))]
    [FriendClassAttribute(typeof(ET.RoleInfosComponent))]
    [FriendClassAttribute(typeof(ET.Scroll_Item_RoleToggle))]
    [FriendClassAttribute(typeof(ET.RoleInfo))]
    public static class DlgRoleListSystem
    {
        public static void RegisterUIEvent(this DlgRoleList self)
        {
            self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.AddItemRefreshListener(self.OnLoopItemRefresh);
            self.View.EInputField_NameInputInputField.onValueChanged.AddListener(name=> self.View.EButton_CreateRoleButton.interactable = name.Length >= 2);
            self.View.EButton_CreateRoleButton.AddListener(self.OnCreateRoleButtonClick);
            self.View.EButton_DeleteRoleButton.AddListener(self.OnDeleteRoleButtonClick);
        }

        public static async void ShowWindow(this DlgRoleList self, Entity contextData = null)
        {
            self.View.EButton_CreateRoleButton.interactable = self.View.EButton_DeleteRoleButton.interactable = self.View.EButton_StartGameButton.interactable = false;
            self.View.EInputField_NameInputInputField.text = string.Empty;

            var domainScene = self.DomainScene();
            //请求角色列表
            var session = domainScene.GetComponent<SessionComponent>().Session;
            var accountInfo = domainScene.GetComponent<AccountInfoComponent>();
            var serverInfos = domainScene.GetComponent<ServerInfosComponent>();

            A2C_GetRolesResponse response = null;
            try
            {
                response = (A2C_GetRolesResponse)await session.Call(new C2A_GetRolesRequest() 
                { 
                    AccountId = accountInfo.AccountId, 
                    ServerId = serverInfos.currentServerId, 
                    Token = accountInfo.Token 
                });
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }

            if (response.Error != ErrorCode.ERR_Success)
            {
                Log.Error(response.Error.ToString());
                return;
            }

            var rolesCmp = self.DomainScene().GetComponent<RoleInfosComponent>();
            rolesCmp.ClearRoles();
            if (response.Roles?.Count > 0)
            {
                foreach (var proto in response.Roles)
                {
                    var newRole = new RoleInfo();
                    newRole.FromMessage(proto);
                    rolesCmp.roles.Add(newRole);
                }

                self.AddUIScrollItems(ref self.rolesToggleDict, response.Roles.Count);
                self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.SetVisible(true, response.Roles.Count);
            }
        }

        public static void HideWindow(this DlgRoleList self)
        {
            self.RemoveUIScrollItems(ref self.rolesToggleDict);
        }

        public static void OnLoopItemRefresh(this DlgRoleList self, Transform trans, int index)
        {
            var startGameBtn = self.View.EButton_StartGameButton;
            var delRoleBtn = self.View.EButton_DeleteRoleButton;

            var infoManager = self.ZoneScene().GetComponent<RoleInfosComponent>();
            var info = infoManager.roles[index];
            var item = self.rolesToggleDict[index];

            item.uiTransform = trans;
            item.ELabel_NameText.text = info.name;

            var toggle = trans.GetComponent<Toggle>();
            toggle.isOn = false;
            toggle.group = self.toggleGroup;
            toggle.AddListener(isOn =>
            {
                if (!isOn)
                {
                    if (!self.toggleGroup.AnyTogglesOn())
                        delRoleBtn.interactable = startGameBtn.interactable = false;

                    return;
                }

                infoManager.currentRoleId = index;
                delRoleBtn.interactable = startGameBtn.interactable = true;
            });
        }

        static async void OnCreateRoleButtonClick(this DlgRoleList self)
        {
            var createBtn = self.View.EButton_CreateRoleButton;

            var inputField = self.View.EInputField_NameInputInputField;
            string name = inputField.text;
            inputField.text = string.Empty;
            createBtn.interactable = false;
            int result = await LoginHelper.CreateRole(self.ZoneScene(), name);
            createBtn.interactable = true;
            if (result == ErrorCode.ERR_Success)
            {
                self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.totalCount += 1;
                self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.RefreshCells();
            }
        }

        static async void OnDeleteRoleButtonClick(this DlgRoleList self)
        {
            var deleteBtn = self.View.EButton_DeleteRoleButton;
            deleteBtn.interactable = false;
            if (!self.DomainScene().GetComponent<RoleInfosComponent>().GetCurrentRole(out RoleInfo role))
                return;

            int result = await LoginHelper.DeleteRole(self.ZoneScene(), role.name);
            if (result != ErrorCode.ERR_Success)
                return;

            self.ZoneScene().GetComponent<RoleInfosComponent>().RemoveRole(role.name);
            self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.totalCount -= 1;
            self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.RefreshCells();
        }
    }

    public class DlgRoleListAwakeSystem : AwakeSystem<DlgRoleList>
    {
        public override void Awake(DlgRoleList self)
        {
            self.rolesToggleDict = new Dictionary<int, Scroll_Item_RoleToggle>();
            self.toggleGroup = self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.content.GetComponent<ToggleGroup>();
        }
    }
}
