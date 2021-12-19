using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Connector.Inputer;

namespace GameSystem
{
    public class ClearFlg : MonoBehaviour, IGetKey
    {// 鍵を知っている。

        [SerializeField] private List<GameObject> keyList = new List<GameObject>();

        //[System.NonSerialized] public bool clearFlg; 
        private GameObject player;
        [SerializeField] private string playerName; // プレイヤーの名前を取得
        private ITestKey _ITestKey;                 // 入力インターフェースを保存
        private Animator animator;                  // アニメーターの保存

        private bool clearFlg;
        private bool stayFlg;
        [SerializeField] private string sceneName;   // シーン移動先の名前
        [SerializeField] private float fadeTime;     // フェードする時間

        void Start()
        {
            player = GameObject.Find(playerName); // プレイヤーオブジェクトを取得
            _ITestKey = GetComponent<ITestKey>(); // 入力インターフェースを取得
            animator = GetComponent<Animator>();  // アニメーターを取得
            clearFlg = false;
            if (keyList.Count <= 0) Clear();
        }

        void Update()
        {
            animator.SetBool("Locked", true);
            if (IsSceceMove())
            {
                if (clearFlg == true)
                    FadeManager.Instance.LoadScene(sceneName, fadeTime);
                else 
                    animator.SetTrigger("Action");
            }
            if(clearFlg == true)
                animator.SetBool("Locked", false);
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
                Clear(); // クリアメソッドを呼ぶ

            Destroy(get); // リストから消えたら鍵自身を消す
        }

        public void Clear()
        {
            clearFlg = true; // クリアフラグをtrueにする
        }

        /// <summary>
        /// シーン移動する条件
        /// </summary>
        /// <returns></returns>
        bool IsSceceMove()
        {
            return _ITestKey.EventKey() && stayFlg == true;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject == player) stayFlg = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player) stayFlg = false;
        }
    }
}
