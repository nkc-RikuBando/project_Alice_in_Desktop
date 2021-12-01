using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerFollowObject : MonoBehaviour
    {
        // Player���ړ�����M�~�b�N�ɒǏ]���鏈��

        [SerializeField] private string _objName;



        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.name == _objName)
            {
                Debug.Log("��������");
                transform.SetParent(collision.transform);//�e�I�u�W�F�N�g�؂�ւ�
            }

        }

        private void OnTriggerExit2D(Collider2D collision)//���ꂽ��
        {
            if (collision.gameObject.name == _objName)
            {
                transform.SetParent(null);//�e�I�u�W�F�N�g�؂�ւ�
            } 
        }


        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    if (collision.gameObject.name == _objName)
        //    {
        //        Debug.Log("��������");
        //        transform.SetParent(collision.transform);//�e�I�u�W�F�N�g�؂�ւ�
        //    }

        //}

        //private void OnCollisionExit2D(Collision2D collision)
        //{
        //    if (collision.gameObject.name == _objName)
        //    {
        //        transform.SetParent(null);//�e�I�u�W�F�N�g�؂�ւ�
        //    }

        //}
    }

}
