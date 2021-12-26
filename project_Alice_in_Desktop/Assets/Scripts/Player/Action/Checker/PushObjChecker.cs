using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtility
{
    public class PushObjChecker : MonoBehaviour
    {
#pragma warning disable 649

        // �ǔ��菈��

        [SerializeField, Tooltip("���C�̒���(��)")] private float raylength_Under = 1f;
        [SerializeField, Tooltip("���C�̒���(��)")] private float raylength_Width = 1f;
        [SerializeField, Tooltip("�n��(��)�̃��C���[")] private LayerMask groundLayer;

        private Rigidbody2D rb2d;


        private void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }


        // �����I�u�W�F�N�g���胁�\�b�h(���C�̏���)
        public bool PushObjWidthChecker(CapsuleCollider2D col)
        {
            bool hit;                                                 // �����������̔���ϐ�
            float colHalfHeight = col.size.y / 3f;                    // Ray��Y���W�����ʒu
            float colHalfHeight2 = col.size.y / 15f;                  // Ray��Y���W�Ԋu
            Vector3 lineLength = transform.right * raylength_Width;   // ���C���΂������ƒ���
            Vector3 checkPos = transform.position;                    // �v���C���[�̍��W

            const int MAX_LOOP = 3;                                   // ���[�v�̉񐔁i���C�̖{���j


            // checkPos�̏����ʒu
            checkPos.y += colHalfHeight;


            // ���C���΂�
            hit = true;
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                //Debug.DrawLine(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, Color.green);// �f�o�b�O�Ń��C��\��
                hit &= Physics2D.Linecast(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, groundLayer);
                checkPos.y -= colHalfHeight2;// ���W��--���Ă���
            }

            return hit;
        }


        // ���n���胁�\�b�h(���C�̏���)
        public bool PushObjOnChecker(CapsuleCollider2D col)
        {
            bool hit;                                               // �����������̔���ϐ�
            float colHalfWidth = col.size.x / 3.5f;                 // X����Ray�̈ʒu
            Vector3 checkPos = transform.position;                  // �v���C���[�̍��W
            Vector3 lineLength = transform.up * raylength_Under;    // ���C�̒���(�v����)

            const float JUMPUP_CHECK_SPEED = 1f;                    // �㏸��ԕϐ�
            const int MAX_LOOP = 3;                                 // ���[�v�̉񐔁i���C�̖{���j


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
                //Debug.DrawLine(checkPos + transform.up * 0.1f, checkPos - lineLength, Color.green);// �f�o�b�O�Ń��C��\��
                hit = Physics2D.Linecast(checkPos + transform.up * 0.1f, checkPos - lineLength, groundLayer);
                if (hit) return true;
                checkPos.x += colHalfWidth;// ���W��++���Ă���
            }

            return false;
        }

    }
}
