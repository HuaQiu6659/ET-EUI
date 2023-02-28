using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
	public static class DlgHelperSystem
	{
        #region ------- 响应事件 -------

        public static void RegisterUIEvent(this DlgHelper self)
        {
			self.View.E_CloseButton.AddListener(self.OnCloseBtnClick);

        }

		static void OnCloseBtnClick(this DlgHelper self)
		{
			self.DomainScene().GetComponent<UIComponent>().HideWindow(WindowID.WindowID_Helper);
        }

        #endregion

        public static void ShowWindow(this DlgHelper self, Entity contextData = null)
		{
			var view = self.View;
			view.E_NoteText.text = DlgHelper.message;
		}

        public static void HideWindow(this DlgHelper self)
		{

		}
	}
}
