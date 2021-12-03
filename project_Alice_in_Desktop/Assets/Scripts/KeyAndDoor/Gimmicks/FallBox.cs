using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBox : MonoBehaviour
{
    private Rigidbody2D rigid; // ���W�b�h�{�f�B�̕ۑ�
    [SerializeField] private GameObject hideKey; // ���I�u�W�F�N�g���擾

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>(); // ���W�b�h�{�f�B�̎擾
        hideKey.SetActive(false); // �����A�N�e�B�u�ɂ���
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���g���n�ʂɓ���������
        if (collision.gameObject.layer == 6 && rigid.velocity.y <= 0)
        {
            hideKey.SetActive(true);         // �����A�N�e�B�u�ɂ���
            hideKey.transform.parent = null; // �����q�I�u�W�F�N�g����͂���
            Destroy(gameObject);             // ���g��j������
        }
    }
}
