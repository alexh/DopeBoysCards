using System.Collections.Generic;
using UnboundLib;
using UnityEngine;
using System.Linq;
using UnboundLib.Networking;
using System.Reflection;
using ModdingUtils.MonoBehaviours;
using DopeBoys.Extensions;
using System;

namespace DopeBoys.MonoBehaviours
{
    public class TouchTipsEffect : MonoBehaviour
    {
        private CharacterData data;

        private Player player;

        private readonly float range = 1.75f;

        void Awake()
        {
        }

        void Start()
        {
            data = GetComponentInParent<CharacterData>();
            this.player = data.player;
        }

        void Update()
        {
            // if any player (friendlies included) is touched (i.e. within a very small range) turn them into gold
            if (PlayerStatus.PlayerAliveAndSimulated(this.player))
            {
                // get all alive players that are not this player
                List<Player> otherPlayers = PlayerManager.instance.players.Where(player => PlayerStatus.PlayerAliveAndSimulated(player) && (player.playerID != this.player.playerID)).ToList();

                Vector2 displacement;

                foreach (Player otherPlayer in otherPlayers)
                {
                    displacement = otherPlayer.transform.position - this.player.transform.position;
                    if (displacement.magnitude <= this.range)
                    {
                
                        otherPlayer.data.playerVel.AddForce(displacement * 25 * (float)player.data.playerVel.GetFieldValue("mass") * (float) player.data.stats.GetAdditionalData().touchtips, ForceMode2D.Impulse);
                        player.data.playerVel.AddForce(displacement * 7 * (float)player.data.playerVel.GetFieldValue("mass") * (float)player.data.stats.GetAdditionalData().touchtips, ForceMode2D.Impulse);
                    }

                }
            }

        }
        public void OnDestroy()
        {
        }
        public void Destroy()
        {
            UnityEngine.Object.Destroy(this);
        }

    }
}