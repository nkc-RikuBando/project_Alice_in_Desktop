using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Connector.Player;

namespace GameSystem
{
    public class WaitTimeUI : MonoBehaviour, IHitPlayer
    {
        private GameObject player;
        private IPlayerAction _ActionKey; // 入力インターフェースの保存
        private Image waitTime; // UIの保存
        public static bool gaugeMaxFlg;
        private GameObject _parent; // 親オブジェクトの保存
        private const float UP_DOWN_NUM = 0.01f; // ゲージの増減量

        void Start()
        {
            player = GetGameObject.playerObject;
            _ActionKey = player.GetComponent<IPlayerAction>();  // 入力インターフェースの取得
            waitTime = GetComponent<Image>();      // UIの取得
            gaugeMaxFlg = false;
            waitTime.fillAmount = default;         // ゲージの初期値０
            _parent = transform.parent.gameObject; // 一つ上の親オブジェクトを取得
        }

        void Update()
        {
            if (_ActionKey.ActionKey()) // キーを長押し
                waitTime.fillAmount += UP_DOWN_NUM; // ゲージが増える
                
            else // キーを離す
                waitTime.fillAmount -= UP_DOWN_NUM; // ゲージが減る

            if (waitTime.fillAmount >= 1) // ゲージが溜まったら
            {
                gaugeMaxFlg = true;
                //Destroy(_parent);        // 一つ上の親オブジェクトの削除
            }
        }

        public void IsHitPlayer()
        {
            _parent.SetActive(true);
        }

        public void NonHitPlayer()
        {
            _parent.SetActive(false);
            waitTime.fillAmount = default; // ゲージの初期値０
        }
    }
}
