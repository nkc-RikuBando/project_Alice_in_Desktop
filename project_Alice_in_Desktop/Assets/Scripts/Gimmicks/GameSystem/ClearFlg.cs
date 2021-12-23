using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using Animation;

namespace GameSystem
{
    public class ClearFlg : MonoBehaviour, IGetKey
    {// 鍵を知っている。

        [SerializeField] private List<GameObject> keyList = new List<GameObject>();
 
        private GameObject player;
        [SerializeField] private GameObject inputUI;
        private IPlayerAction _IActionKey;                 // 入力インターフェースを保存
        private Animator animator;                  // アニメーターの保存

        private ClearEffect clearEffect;            // クリアエフェクトの保存
        private bool clearFlg;
        private bool stayFlg;

        //private GameObject canvas; // キャンバスの保存
        //public float high;
        //public float width;
        //public int horizontal;
        //private Vector3 pos;
        //public GameObject cube;

        void Awake()
        {

        }

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // 入力インターフェースを取得
            animator = GetComponent<Animator>();  // アニメーターを取得
            clearEffect = GetComponent<ClearEffect>(); // クリアエフェクトを取得
            clearFlg = false;
            inputUI.SetActive(false);
            if (keyList.Count <= 0) Clear();

            //canvas = GameObject.Find("Canvas");   // キャンバスの取得
            //for (int i = 0; i < keyList.Count; i++)
            //{
            //    GameObject ui = Instantiate(cube, canvas.transform);
            //    ui.transform.position = new Vector3(pos.x + keyList.Count * width / 2 - i * width - width / 2, 0, 0);
            //}
        }

        void Update()
        {
            animator.SetBool("Locked", true);
            if (IsSceceMove())
            {
                if (clearFlg == true)
                {
                    animator.SetTrigger("Action");
                    clearEffect.StartClearEffect();
                }
                else
                    animator.SetTrigger("Action");
            }
            if(clearFlg == true)
                animator.SetBool("Locked", false);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                stayFlg = true;
                inputUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                stayFlg = false;
                inputUI.SetActive(false);
            }
        }

        public void AddKey(GameObject set)
        {
            keyList.Add(set);
        }

        // 鍵が消えたらリストから消す
        public void GetKey(GameObject get)
        {
            keyList.Remove(get); // リストから鍵を消す
            if (keyList.Count <= 0) Clear(); // クリアメソッドを呼ぶ

            //Destroy(get); // リストから消えたら鍵自身を消す
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
            return _IActionKey.ActionKey_Down() && stayFlg == true;
        }
    }
}
