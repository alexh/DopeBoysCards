using DopeBoys.Extensions;
using ModdingUtils.RoundsEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib;
using UnityEngine;
using static ObjectsToSpawn;



namespace DopeBoys.RoundsEffects
{
    internal class SmootEffect : HitEffect
    {

        public int CardAmount { get; set; } = 0;

        public override void DealtDamage(Vector2 damage, bool selfDamage, Player damagedPlayer)
        {
            if (this == null || CardAmount == 0)
            {
                return;
            }
            UnityEngine.Debug.Log($"[{DopeBoys.ModInitials}] smooting player {damagedPlayer.playerID} x {CardAmount} times ");
            damagedPlayer.data.movement.Move(-damage * 1.2f * CardAmount + Vector2.up * .2f * CardAmount);

        }
    }
}
