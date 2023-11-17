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
    internal class WiggleWiggleEffect : HitEffect
    {

        public int CardAmount { get; set; } = 0;

        public override void DealtDamage(Vector2 damage, bool selfDamage, Player damagedPlayer)
        {
            if (this == null)
            {
                return;
            }
            UnityEngine.Debug.Log($"[{DopeBoys.ModInitials}] wiggling player {damagedPlayer.playerID} {CardAmount} times.");
            for (int i = 0; i < CardAmount; i++)
            {
                damagedPlayer.data.movement.Move((damage + Vector2.up) * 1f);
                damagedPlayer.data.movement.Move((-damage + Vector2.up) * 1f);
                Unbound.Instance.ExecuteAfterFrames(6, () => {
                    damagedPlayer.data.movement.Move((-damage + Vector2.up) * 1f);
                });

            }
        }
    }
}
