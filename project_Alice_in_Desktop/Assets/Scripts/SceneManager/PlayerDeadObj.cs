using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace MyUtility 
{
    public class PlayerDeadObj : MonoBehaviour
    {
        // 画面外に行くとPlayerが死亡する処理

        [SerializeField] private GameObject _fadeObj;
        private FadeEffect _fadeEffect;

        private void Start()
        {
            _fadeEffect = _fadeObj.GetComponent<FadeEffect>();
        }

        // 当たったら
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // Playerを取得
            var playerCheck = collision.gameObject.GetComponent<PlayerStatus>();
            var rabbitCheck = collision.gameObject.GetComponent<RabbitHit>();

            bool isHit = playerCheck != null || rabbitCheck != null;

            // 当たったのがPlayerの場合
            if (isHit)
            {
                // シーンをリロード
                _fadeEffect.StartOutsideEffect();
            }
        }
    }

}
