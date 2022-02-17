using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using Connector.Player;
using Player;
using Window;

namespace Gimmicks
{
    public class BigChangeMushroom : MonoBehaviour, IWindowTouch, IWindowLeave
    {
        [SerializeField] private AudioClip se;

        private GameObject player;
        private IPlayerStatusSentable iStatusSentable;
        private PlayerAnimation playerAnimation;
        private AudioSource audioSource;

        private int sizeChangeCount = 0;
        private bool isWindowTouch;

        void Start()
        {
            player = GetGameObject.playerObject;
            iStatusSentable = player.GetComponent<IPlayerStatusSentable>();
            playerAnimation = player.GetComponent<PlayerAnimation>();
            audioSource = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (isWindowTouch) return;

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

        void IWindowTouch.WindowTouchAction()
        {
            isWindowTouch = true;
            Debug.Log(isWindowTouch);
        }

        void IWindowLeave.WindowLeaveAction()
        {
            isWindowTouch = false;
            Debug.Log(isWindowTouch);
        }
    }
}
