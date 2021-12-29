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
        //private IKeyCount iKeyCount;
        private Animator animator;                  // アニメーターの保存

        private ClearEffect clearEffect;            // クリアエフェクトの保存
        private bool clearFlg;
        private bool stayFlg;

        [SerializeField] private float width; //オブジェクト間の幅
        [SerializeField] private GameObject geneKeyUI; // 生成するUI
        private Vector3 uiPos; // UIの生成位置を保存
        private GameObject frame;
        //private int geneKeyNum;

        private void Awake()
        {
            //iKeyCount = gameObject.GetComponent<IKeyCount>();
            //iKeyCount.keyCount(keyList.Count);
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

            frame = GetGameObject.FrameObject;
            //KeyCountUI();
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
            if (clearFlg == true)
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

        void KeyCountUI()
        {
            // このスクリプトを入れたオブジェクトの位置
            uiPos = transform.position;

            //X軸にhorizontalの数だけ並べる
            for (int i = 0; i < keyList.Count; i++)
            {
                Vector3 genePos = new Vector3(-33+(uiPos.x + keyList.Count * width / 2 - i * width - width / 2), 7);
                //PrefabのCubeを生成する
                GameObject copy = Instantiate(geneKeyUI, genePos, Quaternion.identity);
                copy.transform.SetParent(frame.transform);
            }
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
