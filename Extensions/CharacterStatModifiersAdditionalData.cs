using System;
using System.Runtime.CompilerServices;
using HarmonyLib;


namespace DopeBoys.Extensions
{
    // ADD FIELDS TO CHARACTERSTATMODIFIERS
    [Serializable]
    public class CharacterStatModifiersAdditionalData
    {
        public float wiggle;

        public float headhunter;
        public CharacterStatModifiersAdditionalData()
        {
            wiggle = 0;
            headhunter = 0;
        }
    }
    public static class CharacterStatModifiersExtension
    {
        public static readonly ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData> data =
            new ConditionalWeakTable<CharacterStatModifiers, CharacterStatModifiersAdditionalData>();

        public static CharacterStatModifiersAdditionalData GetAdditionalData(this CharacterStatModifiers characterstats)
        {
            return data.GetOrCreateValue(characterstats);
        }

        public static void AddData(this CharacterStatModifiers characterstats, CharacterStatModifiersAdditionalData value)
        {
            try
            {
                data.Add(characterstats, value);
            }
            catch (Exception) { }
        }

    }
    // reset additional CharacterStatModifiers when ResetStats is called
    [HarmonyPatch(typeof(CharacterStatModifiers), "ResetStats")]
    class CharacterStatModifiersPatchResetStats
    {
        private static void Prefix(CharacterStatModifiers __instance)
        {
            __instance.GetAdditionalData().wiggle = 0f;
            __instance.GetAdditionalData().headhunter = 0f;

        }
    }
}