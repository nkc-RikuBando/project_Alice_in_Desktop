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


        // Start is called before the first frame update
        void Start()
        {
            rb2d = GetComponent<Rigidbody2D>();
        }


        // ���n���胁�\�b�h(���C�̏���)
        public bool CheckIsGround(CapsuleCollider2D col)
        {
            const float JUMPUP_CHECK_SPEED = 1f;
            bool hit;                                       // �����������̔���ϐ�
            const int MAX_LOOP = 3;                         // ���[�v�̉񐔁i���C�̖{���j
            Vector3 checkPos = transform.position;          // �v���C���[�̍��W
            float colHalfWidth = col.size.x / 3.5f;         // �v���C���[�̔����̃R���C�_�[�̑傫��(�v����)
            Vector3 lineLength = transform.up * raylength;  // ���C�̒���(�v����)


            // �㏸���͉������Ȃ�
            if (rb2d.velocity.y > JUMPUP_CHECK_SPEED)
            {
                return false;
            }


            // checkPos�̈ʒu�����[�Ɉړ�
            checkPos.x -= colHalfWidth;

            // ���C������(3�{)
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.up * 0.1f, checkPos - lineLength, Color.blue);// �f�o�b�O�Ń��C��\��
                hit = Physics2D.Linecast(checkPos + transform.up * 0.1f, checkPos - lineLength, sloopLayer);
                if (hit) return true;
                checkPos.x += colHalfWidth;// ���W���{�P���Ă���
            }

            return false;
        }

    }



}
