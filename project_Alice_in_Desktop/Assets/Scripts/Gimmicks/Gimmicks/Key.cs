using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class Key : MonoBehaviour
    {
        private GameObject player;
        private GameObject goal;   // ゴールを取得
        private IGetKey iGetKey;
        private Animator animator;
        private BoxCollider2D boxCol;
        // 鍵のUIリスト
        //[SerializeField] private List<GameObject> keyCountUI = new List<GameObject>();
        [Header("NotKeyImageをアタッチ")]
        [SerializeField] private GameObject notKeyUI;

        //[SerializeField] private string seName;

        void Awake()
        {
            goal = GameObject.Find("Door_Complete");
            iGetKey = goal.GetComponent<IGetKey>();
            iGetKey.AddKey(gameObject);
        }

        void Start()
        {
            player = GetGameObject.playerObject;
            animator = GetComponent<Animator>();
            boxCol = GetComponent<BoxCollider2D>();
            //keyUIObj = GetGameObject.KeyUI;
            //keyCountUI.Add(keyUIObj);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == player)
            {
                //AudioManager.Instance.SeAction("KeyGet");
                GetComponent<AudioSource>().Play();
                animator.SetTrigger("Get");
                iGetKey.GetKey(gameObject);
                //keyCountUI.Remove(keyUIObj);
                boxCol.enabled = false;
                Destroy(notKeyUI);
            }
        }
    }
}
