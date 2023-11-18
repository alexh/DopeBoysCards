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

        public override void DealtDamage(Vector2 damage, bool selfDamage, Player damagedPlayer)
        {
            if (this == null)
            {
                return;
            }
            WiggleWiggleReversibleEffect effect = damagedPlayer.gameObject.GetOrAddComponent<WiggleWiggleReversibleEffect>();
            effect.damage = damage;
        }
    }
}
