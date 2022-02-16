using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using Connector.Player;
using Player;

namespace Gimmicks
{
    public class BigChangeMushroom : MonoBehaviour
    {
        [SerializeField] private AudioClip se;

        private GameObject player;
        private IPlayerStatusSentable iStatusSentable;
        private PlayerAnimation playerAnimation;
        private AudioSource audioSource;

        private int sizeChangeCount = 0;

        void Start()
        {
            player = GetGameObject.playerObject;
            iStatusSentable = player.GetComponent<IPlayerStatusSentable>();
            playerAnimation = player.GetComponent<PlayerAnimation>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // collisionÇ™PlayerÇÃèÍçáÇÃÇ›
            var player = collision.GetComponent<PlayerSet>();

            if (player != null)
            {
                sizeChangeCount++;

                if (sizeChangeCount % 2 == 0)
                {
                    // AnimationÇçƒê∂Ç∑ÇÈ
                    iStatusSentable.PlayerSizeChange(1.5f);

                    if (player.transform.localScale.y >= 1.5f) return;
                    iStatusSentable.PlayerBiggerAnimation();
                    audioSource.PlayOneShot(se);

                    sizeChangeCount = 0;
                }
            }
        }
    }
}
