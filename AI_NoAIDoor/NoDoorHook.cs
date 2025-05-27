using AIProject;
using BehaviorDesigner.Runtime.Tasks;
using BepInEx.Logging;
using HarmonyLib;

public static class NoDoorHook
{
    private static ManualLogSource _logger;

    public static void SetLogger(ManualLogSource logger) => _logger = logger;

    [HarmonyPrefix]
    [HarmonyPatch(typeof(AIProject.IsMatchEventType), "OnUpdate")]
    public static bool Before_OnUpdate(ref TaskStatus __result, AIProject.IsMatchEventType __instance)
    {
        if (__instance._targetKey == EventType.DoorOpen)
        {
            // 攔截特定條件：不要讓 AI 判斷通過這個節點
            __result = TaskStatus.Failure;
            Debug.Log("[NoAIDoor] Prevented AI from evaluating DoorOpen behavior tree node.");
            return false; // ❌ 阻止原始 OnUpdate 執行
        }

        return true; // ✅ 其他情況正常執行
    }
}