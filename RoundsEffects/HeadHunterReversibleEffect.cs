using DopeBoys.Extensions;
using ModdingUtils.MonoBehaviours;
using System;
using DopeBoys.Extensions;
using static System.Net.Mime.MediaTypeNames;
using UnboundLib;
using UnityEngine;


namespace DopeBoys.RoundsEffects
{
    internal class HeadHunterReversibleEffect : ReversibleEffect

    {
        private readonly float value = 65f;

        private readonly float valueStepAmount = 10f;

        private readonly float gunDmgMultiplier = 0.5f;

        private readonly float saturation = 100f;

        private readonly float hue = 356f;

        private Color color;


        public int heads = 0;

        public ReversibleColorEffect colorEffect;

        private ColorFlash colorFlash = null;

        private readonly float colorFlashDur = 0.1f;
        private readonly int colorFlashNumFlashes = 1;

        public override void OnAwake()
        {
            this.colorEffect = this.player.gameObject.AddComponent<ReversibleColorEffect>();
            this.colorEffect.SetColor(this.color);
        }

        public override void OnUpdate()
        {
            this.gunStatModifier.bulletDamageMultiplier_mult += gunDmgMultiplier * heads + 1 * player.data.stats.GetAdditionalData().headhunter + 1;
            UnityEngine.Debug.Log($"[{DopeBoys.ModInitials}] updating headhunter {this.color} {this.gunStatModifier.bulletDamageMultiplier_mult} num heads {heads}");
        }

        public override void OnOnDestroy()
        {
            base.OnOnDestroy();
            this.colorEffect.Destroy();
        }
    }
}
