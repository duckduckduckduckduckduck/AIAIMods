using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using KKAPI;

namespace AI_MoreLocationInteraction
{
    [BepInPlugin(GUID, "More Location Interaction", Version)]
    [BepInProcess("AI-Syoujyo")]
    [BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]
    public partial class MoreLocationInteraction : BaseUnityPlugin
    {
        public const string GUID = "MoreLocationInteraction";
        public const string Version = "1.0";

        public new static ManualLogSource Logger;

        private static ConfigEntry<int> InteractionRate { get; set; }

        private void Start()
        {
            Logger = base.Logger;
            InteractionRate = Config.Bind("All", "Interaction Rate", 0, new ConfigDescription("0 is disabled (original behavior), 5 is around normal, 200 is guaranteed constant location actions.", new AcceptableValueRange<int>(0, 200)));
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(Hooks));
        }
    }
}
