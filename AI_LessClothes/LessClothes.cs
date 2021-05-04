using AIChara;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using KKAPI;
using System.Collections.Generic;
using UnityEngine;

namespace AI_LessClothes
{
    [BepInPlugin(GUID, "Less Clothes", Version)]
    [BepInProcess("AI-Syoujyo")]
    [BepInDependency(KoikatuAPI.GUID, KoikatuAPI.VersionConst)]

    public partial class LessClothes : BaseUnityPlugin
    {
        public const string GUID = "LessClothes";
        public const string Version = "1.0";

        public new static ManualLogSource Logger;

        private static ConfigEntry<int> TopTotal { get; set; }
        private static ConfigEntry<int> BottomToTop { get; set; }
        private static ConfigEntry<int> BottomToTopTotal { get; set; }
        private static ConfigEntry<int> BottomTotal { get; set; }

        private void Start()
        {
            Logger = base.Logger;
            TopTotal = Config.Bind(
                "All",
                "Fully remove tops",
                0,
                new ConfigDescription("0 = normal behavior, 100 = always remove fully.", new AcceptableValueRange<int>(0, 100)));
            BottomToTop = Config.Bind(
                "All",
                "Remove tops if removing bottoms",
                0,
                new ConfigDescription("0 = normal behavior, 100 = always remove bottoms.", new AcceptableValueRange<int>(0, 100)));
            BottomToTopTotal = Config.Bind(
                "All",
                "Fully remove tops if above triggers",
                0,
                new ConfigDescription("0 = normal behavior, 100 = always remove bottoms fully.", new AcceptableValueRange<int>(0, 100)));
            BottomTotal = Config.Bind(
                "All",
                "Fully remove bottoms",
                0,
                new ConfigDescription("0 = normal behavior, 100 = always remove fully.", new AcceptableValueRange<int>(0, 100)));
            HarmonyLib.Harmony.CreateAndPatchAll(typeof(Hooks));
        }

        public static bool CheckRandom(int percent)
        {
            return Random.Range(0, 100) < percent;
        }

        private static bool OriginalModifyClothes(ChaControl _female, bool _isUpper, int _state, bool _isForce = false)
        {
            if (_female == null)
            {
                return true;
            }
            if (_state < 0)
            {
                _state = 0;
            }
            List<int> list = new List<int>();
            if (_isUpper)
            {
                list.Add(0);
                list.Add(2);
            }
            else
            {
                list.Add(1);
                list.Add(3);
                list.Add(5);
            }

            foreach (int item in list)
            {
                if (_female.IsClothesStateKind(item) && (_female.fileStatus.clothesState[item] < _state || _isForce))
                {
                    _female.SetClothesState(item, (byte)_state);
                }
            }

            return true;
        }
    }
}
