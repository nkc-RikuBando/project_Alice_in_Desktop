using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Player;
using GameSystem;

namespace Gimmicks
{
    public class InputKeySwitch : MonoBehaviour, ISetToranpuSoldier
    {
        private GameObject player; // �v���C���[���擾
        private IPlayerAction _IActionKey; // ���̓C���^�[�t�F�[�X��ۑ�
        private Animator animator;
        //private GameObject gimmick;
        [SerializeField] private GameObject switchUI;
        private bool addSwitch;
        private bool stayFlg;

        [Header("�A�^�b�`�s�v")]
        [SerializeField] private List<GameObject> toranpuSoldier = new List<GameObject>();
        //[SerializeField] private List<ISetSwitch> iSetSwitch = new List<ISetSwitch>();
        private LayerChange layerChange;

        //[Range(1, 5)]
        //[SerializeField] private int seNum = 1;

        void Start()
        {
            player = GetGameObject.playerObject;
            _IActionKey = player.GetComponent<IPlayerAction>(); // ���̓C���^�[�t�F�[�X���擾
            animator = GetComponent<Animator>();
            //gimmick = GetGameObject.GimmickObj;
            switchUI.SetActive(false);
            addSwitch = false;
            stayFlg = false;
            layerChange = GetComponent<LayerChange>();
        }

        private void Update()
        {
            if(StayInput())
            {
                animator.SetTrigger("Turn");
                //AudioManager.Instance.SeAction("�J�[�h�X�C�b�`_" + seNum);
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
            // �v���C���[�������ė�����
            if (collision.gameObject == player && layerChange.OutFlg == false)
            {
                stayFlg = true;
                switchUI.SetActive(true);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player)
            {
                stayFlg = false;
                switchUI.SetActive(false);
            }
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���AQ�L�[������
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
