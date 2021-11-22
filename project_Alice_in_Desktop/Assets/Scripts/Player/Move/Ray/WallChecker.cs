using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtility
{
    public class WallChecker : MonoBehaviour
    {
#pragma warning disable 649

        // �ǔ��菈��

        [SerializeField, Tooltip("���C�̒���")]     private float raylength = 1f;
        [SerializeField, Tooltip("�n�ʂ̃��C���[")] private LayerMask groundLayer;


        // ���n���胁�\�b�h(���C�̏���)
        public bool CheckIsGround(CapsuleCollider2D col)
        {
            bool hit;                                               // �����������̔���ϐ�
            const int MAX_LOOP = 3;                                 // ���[�v�̉񐔁i���C�̖{���j
            Vector3 checkPos = transform.position;                  // �v���C���[�̍��W
            float colHalfWidth = col.size.y / 3.5f;                 // �v���C���[�̔����̃R���C�_�[�̑傫��(�v����)
            Vector3 lineLength = transform.right * raylength;       // ���C�̒���(�v����)


            // checkPos�̈ʒu�����[�Ɉړ�
            checkPos.y -= colHalfWidth;


            // ���C������(3�{)
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, Color.red);// �f�o�b�O�Ń��C��\��
                hit = Physics2D.Linecast(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, groundLayer);
                if (hit) return true;
                checkPos.y += colHalfWidth;// ���W���{�P���Ă���
            }

            return false;
        }

    }
}
