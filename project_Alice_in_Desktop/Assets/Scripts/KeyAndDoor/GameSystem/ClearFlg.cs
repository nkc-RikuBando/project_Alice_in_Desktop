using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    public class ClearFlg : MonoBehaviour, IGetKey
    {// 鍵を知っている。

        [SerializeField] private List<GameObject> keyList = new List<GameObject>();

        //[System.NonSerialized] public bool clearFlg; 
        private GameObject player;
        [SerializeField] private string playerName; // プレイヤーの名前を取得

        private bool clearFlg;
        [SerializeField] private string sceneName;        // シーン移動先の名前
        [SerializeField] private float fadeTime;     // フェードする時間

        void Start()
        {
            player = GameObject.Find(playerName); // プレイヤーオブジェクトを取得
            clearFlg = false;
            if (keyList.Count <= 0) Clear();
        }

        public void AddKey(GameObject set)
        {
            keyList.Add(set);
        }

        // 鍵が消えたらリストから消す
        public void GetKey(GameObject get)
        {
            keyList.Remove(get); // リストから鍵を消す
            if (keyList.Count <= 0)
            {
                Clear(); // クリアメソッドを呼ぶ
            }
            Destroy(get); // リストから消えたら鍵自身を消す
        }

        public void Clear()
        {
            clearFlg = true; // クリアフラグをtrueにする
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが触れた、クリアフラグがtrueのとき
            if (collision.gameObject == player && clearFlg == true)
            {
                // クリアシーンに移動
                FadeManager.Instance.LoadScene(sceneName, fadeTime);
            }
        }
    }
}
