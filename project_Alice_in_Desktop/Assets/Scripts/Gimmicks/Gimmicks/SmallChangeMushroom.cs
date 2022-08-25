using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;
using Connector.Player;

namespace Gimmicks
{
    public class SmallChangeMushroom : MonoBehaviour
    {
        // 小さくなるキノコの処理

        [SerializeField] private AudioClip se;

        private GameObject            player;
        private IPlayerStatusSentable iStatusSentable;
        private AudioSource           audioSource;
        private int                   sizeChangeCount;

        void Start()
        {
            player          = GetGameObject.playerObject;
            iStatusSentable = player.GetComponent<IPlayerStatusSentable>();
            audioSource     = GetComponent<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (iStatusSentable.GetIsWindowTouch()) return;

            // collisionがPlayerの場合のみ
            var player = collision.GetComponent<PlayerSet>();

            if (player != null)
            {
                // Playerのコライダーが二つついてる
                sizeChangeCount++;

                if (sizeChangeCount % 2 == 0)
                {
                    // Playerのサイズを変更
                    iStatusSentable.PlayerSizeChange(0.5f);

                    if (player.transform.localScale.y <= 0.5f) return;
                    iStatusSentable.PlayerBiggerAnimation();
                    audioSource.PlayOneShot(se);

                    sizeChangeCount = 0;
                }
            }
        }
    }
}
