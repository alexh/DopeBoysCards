using DopeBoys.Extensions;
using ModdingUtils.RoundsEffects;
using System;
using System.Collections.Generic;
using System.Text;
using UnboundLib;
using UnboundLib.Extensions;
using UnityEngine;
using static ObjectsToSpawn;



namespace DopeBoys.RoundsEffects
{
    internal class HeadHunterEffect : HitEffect
    {

        public int CardAmount { get; set; } = 0;

        public int Heads { get; set; } = 0;
        public Player Owner { get; set; }

        public override void DealtDamage(Vector2 damage, bool selfDamage, Player damagedPlayer)
        {
            if (this == null)
            {
                return;
            }
            DopeBoys.instance.ExecuteAfterFrames(2, () =>
            {
                if (damagedPlayer.data.dead)
                {
                    HeadHunterReversibleEffect hhre = Owner.gameObject.GetOrAddComponent<HeadHunterReversibleEffect>();
                    hhre.heads++;
                    UnityEngine.Debug.Log($"[{DopeBoys.ModInitials}] head acquired by {Owner.playerID} num heads {hhre.heads++}");
                }
            });
        }
    }
}
