using MelonLoader;
using System;
using UnityEngine;
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
        public static bool IsWorld(string id) // Made for other mods
        {
            return VRCFlowManager.field_Private_Static_VRCFlowManager_0.Method_Public_Boolean_String_Boolean_0(id, true);
        }
        public static bool InLobby() // Made for other mods
        {
            var wi = RoomManagerBase.field_Internal_Static_ApiWorldInstance_0;
            var w = RoomManagerBase.field_Internal_Static_ApiWorld_0;
            return (wi != null && w != null);
        }
        public static bool IsHome()
        {
            string homew;
            if (APIUser.CurrentUser != null && !string.IsNullOrEmpty(APIUser.CurrentUser.homeLocation))
            {
                homew = APIUser.CurrentUser.homeLocation;
            }
            else homew = RemoteConfig.GetString("homeWorldId");
            return IsWorld(homew);
        }
        public static void GoHome() // Had to remake this because VRChats code SUCKKSSSSSSSSSSS
        {
            string homew;
            if (!string.IsNullOrEmpty(APIUser.CurrentUser.homeLocation))
            {
                homew = APIUser.CurrentUser.homeLocation;
            }
            else homew = RemoteConfig.GetString("homeWorldId");

            if (!InLobby())
                return;

            VRCFlowManager.field_Private_Static_VRCFlowManager_0.Method_Public_Void_ObjectPublicObDiVoDiPoVoStDiObDiUnique_0(new ObjectPublicObDiVoDiPoVoStDiObDiUnique(ObjectPublicObDiVoDiPoVoStDiObDiUnique.EnumNPublicSealedvaNoDeMePoMoSD7vUnique.Menu, "QuickMenu_GoHome"));
            VRCFlowManager.field_Private_Static_VRCFlowManager_0.prop_String_0 = homew;
            VRCFlowManager.field_Private_Static_VRCFlowManager_0.field_Protected_AccessType_0 = ApiWorldInstance.AccessType.InviteOnly;
        }

        public override void OnUpdate()
        {
            if (!InLobby())
                return;

            if (Input.GetKeyDown(KeyCode.Home))
            {
                GoHome();
            }
        }
        public override void VRChat_OnUiManagerInit()
        {
            var buttont = QuickMenu.prop_QuickMenu_0.transform.Find("ShortcutMenu/GoHomeButton");
            var button = buttont.GetComponentInChildren<UnityEngine.UI.Button>();
            var deflt = button.onClick;
            button.onClick = new UnityEngine.UI.Button.ButtonClickedEvent();
            button.onClick.AddListener(new Action(() =>
            {
                var wi = RoomManagerBase.field_Internal_Static_ApiWorldInstance_0;
                var w = RoomManagerBase.field_Internal_Static_ApiWorld_0;
                if (IsHome() && (wi.InstanceType == ApiWorldInstance.AccessType.InviteOnly | wi.InstanceType == ApiWorldInstance.AccessType.InvitePlus))
                {
                    VRCUiManager.prop_VRCUiManager_0.prop_VRCUiPopupManager_0.Method_Public_Void_String_String_Single_0("Prevented Freeze", "In Home World", 3f);
                    return; // Prevents semi game freeze (vrchats shit coding)
                }

                if (Input.GetKey(KeyCode.LeftShift))
                    GoHome();
                else
                {
                    deflt.Invoke();
                }
            }));
        }
    }
}
