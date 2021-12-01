using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFollowObject : MonoBehaviour
    {
        // Player���ړ�����M�~�b�N�ɒǏ]���鏈��

        [SerializeField] private string _objName;


        // �e�I�u�W�F�N�g�؂�ւ�����
        // �G�ꂽ��
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == _objName)
            {
                Debug.Log("��������");
                transform.SetParent(collision.transform);
            }
        }
        // ���ꂽ��
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == _objName)
            {
                transform.SetParent(null);
            } 
        }
    }

}
