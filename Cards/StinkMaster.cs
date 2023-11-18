using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnboundLib;
using UnboundLib.Cards;
using UnityEngine;


namespace DopeBoys.Cards
{
    class StinkMaster : CustomCard
    {

        public static GameObject toxicObj;
        public override void SetupCard(CardInfo cardInfo, Gun gun, ApplyCardStats cardStats, CharacterStatModifiers statModifiers, Block block)
        {
            //Edits values on card itself, which are then applied to the player in `ApplyCardStats`
            block.forceToAdd = -8f;
            statModifiers.health = 1.2f;
            block.cdAdd = 0.25f;
        }
        public override void OnAddCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Edits values on player when card is selected

            // load toxic cloud effect from Toxic cloud card
            GameObject? toxicCloud = (GameObject)Resources.Load("0 cards/Toxic cloud");
            toxicObj = toxicCloud.GetComponent<Gun>().objectsToSpawn[0].effect;

            
            var objToSpawn = new List<GameObject>
            {
                toxicObj,
                toxicObj,
                toxicObj
            };
            block.objectsToSpawn = objToSpawn;
        }
        public override void OnRemoveCard(Player player, Gun gun, GunAmmo gunAmmo, CharacterData data, HealthHandler health, Gravity gravity, Block block, CharacterStatModifiers characterStats)
        {
            //Run when the card is removed from the player
            if (block.objectsToSpawn.Contains(toxicObj))    //It should contain it, but just to avoid errors
            {
                block.objectsToSpawn.Remove(toxicObj);
            }
            else
            {
                UnityEngine.Debug.LogWarning("No Toxic Objects Found even though Stink Master was removed");
            }
        }


        protected override string GetTitle()
        {
            return "Stink Master";
        }
        protected override string GetDescription()
        {
            return "Hello I'm stinkmaster!\nAdds a noxious cloud to your block";
        }
        protected override GameObject GetCardArt()
        {
            return DeckSmithUtil.Instance.GetArtFromUrl("https://raw.githubusercontent.com/alexh/DopeBoysCards/main/Assets/Cards/StinkMaster2.png");
        }
        protected override CardInfo.Rarity GetRarity()
        {
            return CardInfo.Rarity.Rare;
        }
        protected override CardInfoStat[] GetStats()
        {
            return new CardInfoStat[]
            {
                 new CardInfoStat()
                {
                    positive = true,
                    stat = "Block Cooldown",
                    amount = "+0.25s",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                },
                  new CardInfoStat()
                {
                    positive = true,
                    stat = "Health",
                    amount = "+20%",
                    simepleAmount = CardInfoStat.SimpleAmount.notAssigned
                }
   
            };
        }
        protected override CardThemeColor.CardThemeColorType GetTheme()
        {
            return CardThemeColor.CardThemeColorType.PoisonGreen;
        }
        public override string GetModName()
        {
            return DopeBoys.ModInitials;
        }
    }
}
