using HarmonyLib;
using AIProject;
using BepInEx.Logging;

[HarmonyPatch]
public static class NoDoorHook_BAK
{
    private static ManualLogSource _logger;

    public static void SetLogger(ManualLogSource logger) => _logger = logger;

    public static System.Reflection.MethodBase TargetMethod()
    {
        // 取得 setter
        return AccessTools.PropertySetter(typeof(AgentActor), "EventKey");
    }

    public static void Prefix(ref EventType value,ref bool __runOriginal)
    {
        if (value == EventType.DoorOpen)
        {
            // 把 DoorOpen 換成 DoorClose，避免遊戲找不到 DoorOpen 的資料
            value = EventType.Move;

            UnityEngine.Debug.Log("[NoDoorHook] Changed DoorOpen to DoorClose in EventKey.");
        }
    }
}
