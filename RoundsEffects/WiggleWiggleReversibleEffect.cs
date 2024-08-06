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
            this.duration = this.stats.GetAdditionalData().wiggle * 2f;
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
            damagedPlayer.data.playerVel.AddForce(damage * 4 * (float)player.data.playerVel.GetFieldValue("mass"), ForceMode2D.Impulse);
            damagedPlayer.data.playerVel.AddForce(-damage * 4 * (float)player.data.playerVel.GetFieldValue("mass"), ForceMode2D.Impulse);
            damagedPlayer.data.playerVel.AddForce(damage * 4 * (float)player.data.playerVel.GetFieldValue("mass"), ForceMode2D.Impulse);
            damagedPlayer.data.playerVel.AddForce(-damage * 4 * (float)player.data.playerVel.GetFieldValue("mass"), ForceMode2D.Impulse);
            damagedPlayer.data.playerVel.AddForce(damage * 4 * (float)player.data.playerVel.GetFieldValue("mass"), ForceMode2D.Impulse);
            damagedPlayer.data.playerVel.AddForce(-damage * 4 * (float)player.data.playerVel.GetFieldValue("mass"), ForceMode2D.Impulse);
            damagedPlayer.data.playerVel.AddForce(damage * 4 * (float)player.data.playerVel.GetFieldValue("mass"), ForceMode2D.Impulse);
            damagedPlayer.data.playerVel.AddForce(-damage * 4 * (float)player.data.playerVel.GetFieldValue("mass"), ForceMode2D.Impulse);

        }

        public override void OnOnDestroy()
        {
            base.OnOnDestroy();
            this.colorEffect.Destroy();
        }
    }
}
