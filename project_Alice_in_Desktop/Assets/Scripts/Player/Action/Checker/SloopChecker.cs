using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtility
{
    public class SloopChecker : MonoBehaviour
    {
#pragma warning disable 649

        // �n�ʔ��菈��

        [SerializeField, Tooltip("���C�̒���")] private float raylength = 1f;
        [SerializeField, Tooltip("�n�ʂ̃��C���[")] private LayerMask sloopLayer;

        private Rigidbody2D rb2d;


        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }


        // ���n���胁�\�b�h(���C�̏���)
        public bool CheckIsGround(CapsuleCollider2D col)
        {
            bool hit;                                       // �����������̔���ϐ�
            float colHalfWidth = col.size.x / 3.5f;         // X����Ray�̈ʒu
            Vector3 checkPos = transform.position;          // �v���C���[�̍��W
            Vector3 lineLength = transform.up * raylength;  // ���C�̒���(�v����)

            const float JUMPUP_CHECK_SPEED = 1f;            // �㏸��ԕϐ�
            const int MAX_LOOP = 3;                         // ���[�v�̉񐔁i���C�̖{���j


            // �㏸���͉������Ȃ�
            if (rb2d.velocity.y > JUMPUP_CHECK_SPEED)
            {
                return false;
            }


            // checkPos�̈ʒu�����[�Ɉړ�
            checkPos.x -= colHalfWidth;


            // ���C������
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.up * 0.1f, checkPos - lineLength, Color.blue);// �f�o�b�O�Ń��C��\��
                hit = Physics2D.Linecast(checkPos + transform.up * 0.1f, checkPos - lineLength, sloopLayer);
                if (hit) return true;
                checkPos.x += colHalfWidth;// ���W��++���Ă���
            }

            return false;
        }

    }



}
