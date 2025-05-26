using AIProject;
using BepInEx.Logging;
using HarmonyLib;

public static class NoDoorHook
{
    private static ManualLogSource _logger;

    public static void SetLogger(ManualLogSource logger) => _logger = logger;

    [HarmonyPrefix]
    [HarmonyPatch(typeof(Actor), nameof(Actor.EventKey))]
    public static bool PreventAIDoorOpen(Actor __instance, EventType type)
    {
        if (type == EventType.DoorOpen)
        {
            _logger?.LogInfo($"[NoAIDoor] Blocked {__instance.CharaName} from opening door.");
            return false;
        }

        return true;
    }
}