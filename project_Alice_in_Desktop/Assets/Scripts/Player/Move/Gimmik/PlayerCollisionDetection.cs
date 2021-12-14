using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Player
{
    public class PlayerCollisionDetection : MonoBehaviour
    {
        // Player���ړ�����M�~�b�N�ɒǏ]���鏈��

        [SerializeField] private string[] _objName;


        // �G�ꂽ��
        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �e�I�u�W�F�N�g�؂�ւ�����
            if (collision.gameObject.name == _objName[0])
            {
                transform.SetParent(collision.transform);
            }
        }


        // ���ꂽ��
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == _objName[0])
            {
                transform.SetParent(null);
            }
        }
    }
}
