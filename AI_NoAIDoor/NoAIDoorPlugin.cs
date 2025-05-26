using AIProject;
using AIProject.Player;
using BepInEx;
using BepInEx.Logging;
using HarmonyLib;

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
            NoDoorHook.SetLogger(_logger);
            Harmony.CreateAndPatchAll(typeof(NoDoorHook), GUID);
            _logger.LogInfo("NoAIDoorPlugin initialized.");
        }
    }
}