using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Gimmicks
{
    public class WarpHole : MonoBehaviour
    {
        private GameObject player;                     // �v���C���[�I�u�W�F�N�g��ۑ�
        private ITestKey _ITestKey;
        [SerializeField] private GameObject warpPoint; // ���[�v��I�u�W�F�N�g��ۑ�
        private bool stayFlg = false;                  // �؍݂��Ă��邩�t���O

        void Start()
        {
            player = GameObject.Find("PlayerTest"); // �v���C���[�I�u�W�F�N�g���擾
            _ITestKey = GetComponent<ITestKey>();
        }

        void Update()
        {
            if (StayInput())
            {
                Warp();
            }
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player) stayFlg = true; // �؍݃t���O��true
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player) stayFlg = false; // �؍݃t���O��false
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���AQ�L�[������
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventKey();
        }

        /// <summary>
        /// ���[�v��Ƀv���C���[���ړ�������
        /// </summary>
        void Warp()
        {
            player.transform.position = warpPoint.transform.position;
        }
    }
}
