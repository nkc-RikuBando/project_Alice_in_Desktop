using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;
using Gimmicks;

namespace Player
{
    public class PlayerCollisionDetection : MonoBehaviour
    {
        // Player���ړ�����M�~�b�N�ɒǏ]���鏈��


        // �G�ꂽ��
        private void OnTriggerEnter2D(Collider2D collision)
        {
            var moveFloor = collision.GetComponent<MoveFloor>();

            // �e�I�u�W�F�N�g�؂�ւ�����
            if (moveFloor != null)
            {
                transform.SetParent(collision.transform);
            }
        }


        // ���ꂽ��
        private void OnTriggerExit2D(Collider2D collision)
        {
            var moveFloor = collision.GetComponent<MoveFloor>();

            // �e�I�u�W�F�N�g�؂�ւ�����
            if (moveFloor != null)
            {
                transform.SetParent(null);
            }
        }
    }
}
