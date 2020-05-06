using MelonLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using VRC.Core;

namespace GoHome
{
    public static class BuildInfo
    {
        public const string Name = "Fast Go Home"; // Name of the Mod.  (MUST BE SET)
        public const string Author = "404#0004"; // Author of the Mod.  (Set as null if none)
        public const string Company = "I am not a company -Kappa-"; // Company that made the Mod.  (Set as null if none)
        public const string Version = "1.0.0"; // Version of the Mod.  (MUST BE SET)
        public const string DownloadLink = "https://github.com/l-404-l"; // Download Link for the Mod.  (Set as null if none)
    }
    public class Main : MelonMod
    {
        public override void VRChat_OnUiManagerInit()
        {
            var buttont = QuickMenu.prop_QuickMenu_0.transform.Find("ShortcutMenu/GoHomeButton");
            var button = buttont.GetComponentInChildren<UnityEngine.UI.Button>();
            var deflt = button.onClick;
            button.onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            button.onClick.AddListener(new Action(() => {
                if (Input.GetKey(KeyCode.LeftShift))
                    VRCFlowManager.field_Private_Static_VRCFlowManager_0.Method_Public_Virtual_New_Void_2();
                else
                    deflt.Invoke();
            }));
        }
    }
}
