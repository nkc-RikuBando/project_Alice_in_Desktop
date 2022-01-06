using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
{
    public class InputKeySwitch : MonoBehaviour, ISetToranpuSoldier
    {
        private GameObject player; // プレイヤーを取得
        private IPlayerAction _IActionKey; // 入力インターフェースを保存
        private Animator animator;
        //private GameObject gimmick;
        [SerializeField] private GameObject switchUI;
        private bool addSwitch;
        private bool stayFlg;

        [Header("アタッチ不要")]
        [SerializeField] private List<GameObject> toranpuSoldier = new List<GameObject>();
        //[SerializeField] private List<ISetSwitch> iSetSwitch = new List<ISetSwitch>();

        //void Awake()
        //{
        //    for (int i = 0; i < toranpuSoldier.Count; i++)
        //    {
        //        //toranpuSoldier[i].GetComponent<IHitSwitch>();
        //        iSetSwitch.Add(toranpuSoldier[i].GetComponent<ISetSwitch>());
        //        iSetSwitch[i].AddSwitch(gameObject);
        //    }
        //}

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // 入力インターフェースを取得
            animator = GetComponent<Animator>();
            //gimmick = GetGameObject.GimmickObj;
            switchUI.SetActive(false);
            addSwitch = false;
            stayFlg = false;
        }

        private void Update()
        {
            if(StayInput())
            {
                animator.SetTrigger("Turn");
                addSwitch = addSwitch ? false : true;
                for (int i = 0; i < toranpuSoldier.Count; i++)
                {
                    toranpuSoldier[i].GetComponent<IHitSwitch>().Switch();
                }
                //if (hitGimmick != null || hitGimmick1 != null)
                    //{
                    //    //addSwitch = addSwitch ? false : true;
                    //    hitGimmick.Switch(addSwitch);
                    //    hitGimmick1.Switch(addSwitch);
                    //}
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // プレイヤーが入って来たら
            if (collision.gameObject == player)
            {
                stayFlg = true;
                switchUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // プレイヤーが出て行ったら
            if (collision.gameObject == player)
            {
                stayFlg = false;
                switchUI.SetActive(false);
            }
        }

        /// <summary>
        /// プレイヤーが触れている、かつ、Qキーを押す
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _IActionKey.ActionKey_Down();
        }

        public void AddSoldier(GameObject set)
        {
            toranpuSoldier.Add(set);
        }
    }
}
