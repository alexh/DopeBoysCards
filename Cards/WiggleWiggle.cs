using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib.Cards;
using UnityEngine;
using DopeBoys.RoundsEffects;
using DopeBoys.Extensions;

namespace DopeBoys.Cards
{
    internal class WiggleWiggle : CustomCard
    {

        public const float CD_TIME_SEC = 4f;

        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            gun.attackSpeedMultiplier = 1.25f;
            //UnityEngine.Debug.Log($"[{DopeBoys.ModInitials}][Card] {GetTitle()} has been setup.");
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected
            //UnityEngine.Debug.Log($"[{DopeBoys.ModInitials}][Card] {GetTitle()} has been added to player {player.playerID}.");

            WiggleWiggleEffect mono = player.gameObject.AddComponent<WiggleWiggleEffect>();
            characterStats.GetAdditionalData().wiggle++;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            characterStats.GetAdditionalData().wiggle--;
            //UnityEngine.Debug.Log($"[{DopeBoys.ModInitials}][Card] {GetTitle()} has been removed from player {player.playerID}.");
        }


        protected override string GetTitle()
        {
            return "Wiggle Wiggle";
        }
        protected override string GetDescription()
        {
            return "<color=#FC0FC0>WWPL</color>\n Make your opponent <color=#FC0FC0>wiggle</color> when you hit them";
        }
        protected override GameObject GetCardArt()
        {
            return DeckSmithUtil.Instance.GetArtFromUrl("https://raw.githubusercontent.com/alexh/DopeBoysCards/main/Assets/Cards/WiggleWiggle.png");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Uncommon;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                new CardInfoStat()
                {
                    positive = false,
                    stat = "ATK SPD",
                    amount = "-25%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.MagicPink;
        }
        public override string GetModName()
        {
            return DopeBoys.ModInitials;
        }
    }
}
