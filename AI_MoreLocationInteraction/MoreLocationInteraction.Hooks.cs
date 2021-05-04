using AIProject;
using AIProject.Definitions;
using HarmonyLib;

namespace AI_MoreLocationInteraction
{
    public partial class MoreLocationInteraction
    {
        private static class Hooks
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(AgentActor), "GetAddRate", typeof(int))]
            private static void GetAddRate(ref float __result, int key, AgentActor  __instance)
            {
                if (key == Desire.GetDesireKey(Desire.Type.Game) && InteractionRate.Value != 0)
                {
                    __result = InteractionRate.Value;
                }
            }
        }
    }
}
