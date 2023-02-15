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
            self.View.EButton_StartGameButton.AddListener(self.OnStartGameButtonClick);
        }

        public static async void ShowWindow(this DlgRoleList self, Entity contextData = null)
        {
            self.View.EButton_CreateRoleButton.interactable = self.View.EButton_DeleteRoleButton.interactable = self.View.EButton_StartGameButton.interactable = false;
            self.View.EInputField_NameInputInputField.text = string.Empty;

            var zoneScene = self.ZoneScene();
            int result = await LoginHelper.GetRoles(zoneScene);
            if (result != ErrorCode.ERR_Success)
                return;

            var roles = zoneScene.GetComponent<RoleInfosComponent>().roles;
            self.AddUIScrollItems(ref self.rolesToggleDict, roles.Count);
            self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.SetVisible(true, roles.Count);
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

            self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.totalCount -= 1;
            self.View.ELoopScrollList_RolesLoopHorizontalScrollRect.RefreshCells();
        }

        static async void OnStartGameButtonClick(this DlgRoleList self)
        {
            Scene zoneScene = self.ZoneScene();
            int result = await LoginHelper.GetRealmKey(zoneScene);
            if (result != ErrorCode.ERR_Success)
                return;

            result = await LoginHelper.EnterGame(zoneScene);
            if (result != ErrorCode.ERR_Success)
                return;
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
