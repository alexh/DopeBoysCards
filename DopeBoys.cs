using BepInEx;
using UnboundLib;
using UnboundLib.Cards;
using DopeBoys.Cards;
using HarmonyLib;
using CardChoiceSpawnUniqueCardPatch.CustomCategories;
using static DeckSmithUtil;
using UnityEngine;


namespace DopeBoys
{
    // These are the mods required for our mod to work
    [BepInDependency("com.willis.rounds.unbound", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.moddingutils", BepInDependency.DependencyFlags.HardDependency)]
    [BepInDependency("pykess.rounds.plugins.cardchoicespawnuniquecardpatch", BepInDependency.DependencyFlags.HardDependency)]
    // Declares our mod to Bepin
    [BepInPlugin(ModId, ModName, Version)]
    // The game our mod is associated with
    [BepInProcess("Rounds.exe")]
    public class DopeBoys : BaseUnityPlugin
    {
        private const string ModId = "com.phalex.rounds.DopeBoys";
        private const string ModName = "DopeBoys";
        public const string Version = "0.1.0";
        public const string ModInitials = "DB";

        public static DopeBoys instance { get; private set; }

        void Awake()
        {
            // Use this to call any harmony patch files your mod may have
            var harmony = new Harmony(ModId);
            harmony.PatchAll();
        }
        void Start()
        {
            instance = this;
            CustomCard.BuildCard<StinkMaster>();
            CustomCard.BuildCard<WiggleWiggle>();
            CustomCard.BuildCard<SamTurret>();
            CustomCard.BuildCard<Smoot>();
            CustomCard.BuildCard<HeadHunter>();
        }
    }
}
