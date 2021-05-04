using BepInEx;
using BepInEx.Logging;
using KKAPI;

namespace AI_LessSurprise
{
    [BepInPlugin(GUID, "Less Surprise", Version)]
    [BepInProcess("AI-Syoujyo")]
    [BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]

    public partial class LessSurprise : BaseUnityPlugin
    {
        public const string GUID = "LessSurprise";
        public const string Version = "1.0";

        public new static ManualLogSource Logger;

        private void Start()
        {
            Logger = base.Logger;
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(Hooks));
        }
    }
}
