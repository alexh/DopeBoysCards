using DopeBoys.Extensions;
using ModdingUtils.MonoBehaviours;
using System;
using DopeBoys.Extensions;
using static System.Net.Mime.MediaTypeNames;
using UnboundLib;
using UnityEngine;


namespace DopeBoys.RoundsEffects
{
    internal class WiggleWiggleReversibleEffect : ReversibleEffect

    {

        private readonly Color color = Color.magenta;

        public float duration;

        public Vector2 damage;

        public ReversibleColorEffect colorEffect;
        public override void OnAwake()
        {
            this.duration = this.stats.GetAdditionalData().wiggle * 1f;
            this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
            this.colorEffect.SetColor(this.color);
            //UnityEngine.Debug.Log($"[{DopeBoys.ModInitials}] wiggling player {this.player.playerID} for {duration} seconds");
        }

        public override void OnUpdate()
        {
            duration -= TimeHandler.deltaTime;
            if (duration <= 0f)
            {
                Destroy();
            }
            var damagedPlayer = this.player;
            damagedPlayer.data.movement.Move((damage + Vector2.up) * 1f);
            damagedPlayer.data.movement.Move((-damage + Vector2.up) * 1f);

        }

        public override void OnOnDestroy()
        {
            base.OnOnDestroy();
            this.colorEffect.Destroy();
        }
    }
}
