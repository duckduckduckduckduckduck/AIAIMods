using System;
using System.Collections.Generic;
using System.Linq;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Harmony;
using BepInEx.Logging;
using KKAPI;
using KKAPI.Chara;
using KKAPI.Maker;
using KKAPI.Maker.UI;
using UnityEngine;

namespace AI_MoreMasturbate
{
    [BepInPlugin(GUID, "More Masturbate", Version)]
    [BepInProcess("AI-Syoujyo")]
    [BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]

    public partial class MoreMasturbate : BaseUnityPlugin
    {
        public const string GUID = "MoreMasturbate";
        public const string Version = "1.0";

        public new static ManualLogSource Logger;

        private static ConfigEntry<int> Rate { get; set; }

        private void Start()
        {
            Logger = base.Logger;
            Rate = Config.Bind("All", "Masturbate Rate", 0, new ConfigDescription("0 = normal behavior, 100 = always masturbate.", new AcceptableValueRange<int>(0, 100)));
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(Hooks));
        }
    }
}
