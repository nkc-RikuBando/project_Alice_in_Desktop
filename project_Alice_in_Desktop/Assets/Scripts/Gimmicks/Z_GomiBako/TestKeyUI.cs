using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameSystem
{
    public class TestKeyUI : MonoBehaviour/*, IKeyCount*/
    {
        ////����
        ////public float high;
        ////�I�u�W�F�N�g�Ԃ̕�
        //public float width;
        ////�ォ�猩�ďc�AZ���̃I�u�W�F�N�g�̗�
        ////public int vertical;
        ////�ォ�猩�ĉ��AX���̃I�u�W�F�N�g�̗�
        //public int horizontal;

        ////Prefab�����闓�����
        //public GameObject cube;

        ////�ʒu������ϐ�
        //Vector3 pos;

        [SerializeField] private float width; //�I�u�W�F�N�g�Ԃ̕�
        [SerializeField] private GameObject geneKeyUI; // ��������UI
        private Vector3 uiPos; // UI�̐����ʒu��ۑ�
        private int geneKeyNum;

        void Start()
        {
            //Debug.Log(geneKeyNum);
        }

        void Test()
        {
            //// ���̃X�N���v�g����ꂽ�I�u�W�F�N�g�̈ʒu
            //pos = transform.position;

            ////Z����vertical�̐��������ׂ�
            ////for (int vi = 0; vi < vertical; vi++)
            //{
            //    //X����horizontal�̐��������ׂ�
            //    for (int hi = 0; hi < horizontal; hi++)
            //    {
            //        //Prefab��Cube�𐶐�����
            //        GameObject copy
            //            = Instantiate(cube, new Vector3(pos.x + horizontal * width / 2 - hi * width - width / 2, pos.y), Quaternion.identity);
            //    }
            //}
        }

        //public void keyCount(int num)
        //{
        //    geneKeyNum = num;
        //}

        void KeyCountUI()
        {
            // ���̃X�N���v�g����ꂽ�I�u�W�F�N�g�̈ʒu
            uiPos = transform.position;

            //X����horizontal�̐��������ׂ�
            for (int hi = 0; hi < geneKeyNum; hi++)
            {
                Vector3 genePos = new Vector3(uiPos.x + geneKeyNum * width / 2 - hi * width - width / 2, uiPos.y);
                //Prefab��Cube�𐶐�����
                GameObject copy = Instantiate(geneKeyUI, genePos, Quaternion.identity);
            }
        }
    }
}
