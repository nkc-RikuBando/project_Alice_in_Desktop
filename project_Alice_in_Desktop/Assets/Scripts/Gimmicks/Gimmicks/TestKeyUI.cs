using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestKeyUI : MonoBehaviour
{
    
    //����
    public float high;
    //�I�u�W�F�N�g�Ԃ̕�
    public float width;
    //�ォ�猩�ďc�AZ���̃I�u�W�F�N�g�̗�
    public int vertical;
    //�ォ�猩�ĉ��AX���̃I�u�W�F�N�g�̗�
    public int horizontal;

    //Prefab�����闓�����
    public GameObject cube;

    //�ʒu������ϐ�
    Vector3 pos;


    void Start()
    {
        //for(int i = 0; i < )

        Test();

        //GameObject sqObj = GameObject.Find("TestImage"); // �ړI�̃X�v���C�g�̃I�u�W�F�N�g���擾
        //SpriteRenderer sqSr = sqObj.GetComponent<SpriteRenderer>();//�ړI�̃X�v���C�g��SpriteRenderer���擾

        //GameObject sqObj2 = GameObject.Find("TestImage (1)"); // �ړI�̃X�v���C�g�̃I�u�W�F�N�g���擾
        ////Debug.Log("���S�̍��W�� " + sqSr.bounds.center + " �ł�");//���S�̍��W�� (-0.5, 0.2, 0.0) �ł�
        //sqObj2.transform.position = sqSr.bounds.center * -1;
    }

    void Test()
    {
        /*// ���̃X�N���v�g����ꂽ�I�u�W�F�N�g�̈ʒu
        pos = transform.position;

        //Z����vertical�̐��������ׂ�
        for (int vi = 0; vi < vertical; vi++)
        {
            //X����horizontal�̐��������ׂ�
            for (int hi = 0; hi < horizontal; hi++)
            {
                //Prefab��Cube�𐶐�����
                GameObject copy = Instantiate(cube,
                    //�����������̂�z�u����ʒu
                    new Vector3(
                        //X��
                        pos.x + horizontal * width / 2 - hi * width - width / 2,
                        //Y��
                        pos.z + vertical * width / 2 - vi * width - width / 2), Quaternion.identity);
                //Z��
            }
        }*/
    }
}
