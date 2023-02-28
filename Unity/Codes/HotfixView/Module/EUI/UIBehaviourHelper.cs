using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;

namespace ET
{
    public static class UIBehaviourHelper
    {
        public static void SetTouchable(this Button self) => self.interactable = true;
        public static void SetUntouchable(this Button self) => self.interactable = false;

        public static void SetTouchable(this Toggle self) => self.interactable = true;
        public static void SetUntouchable(this Toggle self) => self.interactable = false;
    }
}
