using AIProject;
using AIProject.Definitions;
using BepInEx.Logging;
using HarmonyLib;
using System.Text.Json;

public static class NoDoorHook_BAK2
{
    private static ManualLogSource _logger;

    public static void SetLogger(ManualLogSource logger) => _logger = logger;

    [HarmonyPrefix]
    [HarmonyPatch(typeof(AgentActor), "EnableEntity")]
    public static void EnableEntityPrefix(AgentActor __instance, ref bool __runOriginal)
    {
        _logger.LogInfo($"[EnableEntityPrefix] {JsonSerializer.Serialize(__instance)}");
        if (__instance.EventKey == EventType.DoorOpen)
        {
            __instance.StopNavMeshAgent();
            __instance.BehaviorResources.ChangeMode(Desire.ActionType.WaitForCalled);
            __instance.EventKey = EventType.Move;   // 或 (EventType)0
            __instance.CurrentPoint = null;
            __instance.TargetInSightActionPoint = null;
            __instance.Animation.ResetDefaultAnimatorController();
            __runOriginal = false;
            return;
        }

        __runOriginal = true;
        return;
    }
}
