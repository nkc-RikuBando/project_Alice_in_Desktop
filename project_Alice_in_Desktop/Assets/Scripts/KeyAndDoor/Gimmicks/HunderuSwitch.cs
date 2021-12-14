using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class HunderuSwitch : MonoBehaviour
    {
        [SerializeField] private GameObject player; // �v���C���[���擾
        [SerializeField] private GameObject gimmick; // �M�~�b�N���擾
        private bool addSwitch; // �X�C�b�`��ON�AOFF
        private bool stayFlg;   // �؍݂��Ă��邩���Ȃ���

        void Start()
        {
            addSwitch = false;
            stayFlg = false;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player)
            {
                stayFlg = true; // �؍݂��Ă���
                if (stayFlg == true)
                {
                    IHitSwitch hitGimmick = gimmick.GetComponent<IHitSwitch>();
                    if (hitGimmick != null)
                    {
                        addSwitch = true; // �X�C�b�`ON
                        hitGimmick.Switch(addSwitch); // Switch���\�b�h��false��n��
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player)
            {
                stayFlg = false; // �؍݂��Ă��Ȃ�
                if (stayFlg == false)
                {
                    IHitSwitch hitGimmick = gimmick.GetComponent<IHitSwitch>();
                    if (hitGimmick != null)
                    {
                        addSwitch = false; // �X�C�b�`OFF
                        hitGimmick.Switch(addSwitch); // Switch���\�b�h��false��n��
                    }
                }
            }
        }
    }
}
