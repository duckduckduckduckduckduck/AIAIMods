using AIProject;
using AIProject.Definitions;
using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace AI_LessSurprise
{
    public partial class LessSurprise
    {
        private static class Hooks
        {
            public static Dictionary<int, float> surpriseTimes = new Dictionary<int, float>();

            [HarmonyPostfix]
            [HarmonyPatch(typeof(AgentActor), "UpdateEncounter")]
            private static void HookEncounterPeep(AgentActor __instance)
            {
                if (__instance.Mode == Desire.ActionType.FoundPeeping && __instance.ReservedMode == Desire.ActionType.Normal)
                {
                    __instance.ReservedMode = __instance.PrevMode;
                    surpriseTimes[__instance.charaID] = Time.realtimeSinceStartup;
                }
            }

            [HarmonyPostfix]
            [HarmonyPatch(typeof(AIProject.Masturbation), "OnStart")]
            [HarmonyPatch(typeof(AIProject.Bath), "OnStart")]
            [HarmonyPatch(typeof(AIProject.Toilet), "OnStart")]
            private static void MasturbationStart(AIProject.Masturbation __instance)
            {
                AgentActor agent = (AgentActor)typeof(AIProject.Masturbation).GetProperty("Agent", BindingFlags.FlattenHierarchy | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance).GetValue(__instance);
                if (surpriseTimes.TryGetValue(agent.charaID, out var lastSurprised)) {
                    if (lastSurprised < Time.realtimeSinceStartup + 60 * 10)
                    {
                        agent.SurprisePoseID = null;
                    }
                }
            }

            [HarmonyPrefix]
            [HarmonyPatch(typeof(AIProject.Surprise), "Complete")]
            private static bool SkipSurpriseEnd()
            {
                return false;
            }
        }
    }
}
