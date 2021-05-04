using AIChara;
using HarmonyLib;

namespace AI_LessClothes
{
    public partial class LessClothes
    {
        private static class Hooks
        {
            [HarmonyPostfix]
            [HarmonyPatch(typeof(GlobalMethod), "SetAllClothState")]
            private static bool ModifyClothes(ChaControl _female, bool _isUpper, int _state, bool _isForce = false)
            {
                if (_state == 0)
                {
                    OriginalModifyClothes(_female, _isUpper, _state, _isForce);
                }
                else
                {
                    if (_isUpper)
                    {
                        if (CheckRandom(TopTotal.Value))
                        {
                            OriginalModifyClothes(_female, _isUpper, 2, _isForce);
                        } 
                        else
                        {
                            OriginalModifyClothes(_female, _isUpper, _state, _isForce);
                        }
                    }
                    else
                    {
                        if (CheckRandom(BottomTotal.Value))
                        {
                            OriginalModifyClothes(_female, _isUpper, 2, _isForce);
                        }
                        else
                        {
                            OriginalModifyClothes(_female, _isUpper, _state, _isForce);
                        }
                        if (CheckRandom(BottomToTop.Value))
                        {
                            if (CheckRandom(BottomToTopTotal.Value))
                            {
                                OriginalModifyClothes(_female, true, 2, _isForce);
                            }
                            else
                            {
                                OriginalModifyClothes(_female, true, _state, _isForce);
                            }
                        }
                    }
                }
                return true;
            }
        }
    }
}
