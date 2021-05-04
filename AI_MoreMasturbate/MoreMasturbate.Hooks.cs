using AIProject;
using AIProject.Definitions;
using HarmonyLib;
using UnityEngine;

namespace AI_MoreMasturbate
{
    public partial class MoreMasturbate
    {
        private static class Hooks
        {
            [HarmonyPrefix]
            [HarmonyPatch(typeof(AgentActor), "ChangeBehavior", typeof(Desire.ActionType))]
            private static void ChangeBehavior(ref Desire.ActionType type, AgentActor __instance)
            {
                if (type == Desire.ActionType.SearchH && __instance.CanMasturbation && Random.Range(0, 100) < Rate.Value)
                {
                    type = Desire.ActionType.SearchMasturbation;
                }
            }
        }
    }
}
