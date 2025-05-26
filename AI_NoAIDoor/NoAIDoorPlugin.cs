using AIChara;
using AIProject;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using KKAPI.Chara;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_NoAIDoor
{
    [BepInPlugin(GUID, "No AI Door Open", Version)]
    public class NoAIDoorPlugin : BaseUnityPlugin
    {
        private static ManualLogSource _logger;

        public const string GUID = "hakin.NoAIDoor";
        public const string Version = "1.0";
        private void Awake()
        {
            _logger = base.Logger;

             var h = Harmony.CreateAndPatchAll(typeof(NoAIDoorPlugin), GUID);
            Logger.LogInfo("NoAIDoorPlugin initialized.");
        }


        private static class Patches
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(Actor), "SendEvent")]
            static bool PreventAIDoorOpen(ref Actor __instance)
            {
                if (__instance.EventKey == EventType.DoorOpen)
                {
                    _logger.LogInfo($"Prevented AI {__instance.name} from opening door.");
                    __instance.EventKey = EventType.DoorClose;
                    return false; // 阻止原始事件發送
                }

                return true; // 允許其他事件通過
            }
        }
    }
}
