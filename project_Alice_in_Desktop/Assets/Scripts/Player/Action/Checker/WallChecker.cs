using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyUtility
{
    public class WallChecker : MonoBehaviour
    {
#pragma warning disable 649

        // �ǔ��菈��

        [SerializeField, Tooltip("���C�̒���")]     �@�@private float raylength = 1f;
        [SerializeField, Tooltip("�n��(��)�̃��C���[")] private LayerMask groundLayer;


        // ���n���胁�\�b�h(���C�̏���)
        public bool CheckIsGround(CapsuleCollider2D col)
        {
            bool hit;                                               // �����������̔���ϐ�
            float rayInitialPos  = col.size.y;                      // Ray��Y���W�����ʒu
            float colHalfHeight2 = col.size.y / 3.2f;               // Ray��Y���W�Ԋu
            Vector3 lineLength = transform.right * raylength;       // ���C���΂������ƒ���
            Vector3 checkPos = transform.position;                  // �v���C���[�̍��W

            const int MAX_LOOP = 3;                                 // ���[�v�̉񐔁i���C�̖{���j


            // checkPos�̏����ʒu
            checkPos.y += rayInitialPos;


            // ���C���΂�
            hit = true;
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, Color.red);// �f�o�b�O�Ń��C��\��
                hit &= Physics2D.Linecast(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, groundLayer);
                checkPos.y -= colHalfHeight2;// ���W��--���Ă���
            }

            return hit;
        }

    }
}
