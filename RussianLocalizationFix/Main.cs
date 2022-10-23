using System.Reflection;
using HarmonyLib;
using PavonisInteractive.TerraInvicta;
using TMPro;
using UnityEngine;
using UnityModManagerNet;

namespace RussianLocalizationFix
{
    public class Main
    {
        public static bool enabled;

        public static UnityModManager.ModEntry mod;

        static bool Load(UnityModManager.ModEntry modEntry)
        {
            var harmony = new Harmony(modEntry.Info.Id);
            harmony.PatchAll();

            return true;
        }

        [HarmonyPatch()]
        public class HabModuleUIElementController_Patch
        {
            static MethodInfo TargetMethod()
            {
                return typeof(HabModuleUIElementController).GetMethod("OnEnable",
                    BindingFlags.Instance | BindingFlags.NonPublic);
            }

            static bool Prefix(HabModuleUIElementController __instance)
            {
                __instance.moduleDisplayName.font = Loc.CurrentBodyFont;
                return true;
            }
            
            
        }

        [HarmonyPatch(typeof(MarkerController), nameof(MarkerController.Initialize))]
        public class MarkerController_Patch
        {
            static void Postfix(MarkerController __instance)
            {
                TMP_FontAsset en_font = Resources.Load<TMP_FontAsset>("All Fonts/" + TemplateManager.Find<TILocalizationTemplate>("en", false).bodyTextFontPath);
                __instance.toHitText_Centered.font = en_font;
                __instance.toHitText_Low.font = en_font;
                __instance.toHitText_Lowest.font = en_font;
            }
        }
    }
}