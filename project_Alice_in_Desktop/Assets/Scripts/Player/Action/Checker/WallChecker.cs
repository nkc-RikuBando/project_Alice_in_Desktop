using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

namespace MyUtility
{
    public class WallChecker : MonoBehaviour
    {
    #pragma warning disable 649

        // �ǔ��菈��

        [SerializeField, Tooltip("���C�̒���")]     �@�@private float raylength = 1f;
        [SerializeField, Tooltip("�n��(��)�̃��C���[")] private LayerMask groundLayer;

        private PlayerStatus _playerStatus;


        void Start()
        {
            _playerStatus = GetComponent<PlayerStatus>();
        }

        // ���n���胁�\�b�h(���C�̏���)
        public bool CheckIsWall(CapsuleCollider2D col)
        {
            bool hit;                                               // �����������̔���ϐ�
            float rayInitialPos  = col.size.y + RayPostionAdj();    // Ray��Y���W�����ʒu
            float colHalfHeight  = col.size.y / RayInterval();      // Ray��Y���W�Ԋu
            Vector3 lineLength = transform.right * raylength;       // ���C���΂������ƒ���
            Vector3 checkPos = transform.position;                  // �v���C���[�̍��W

            const int MAX_LOOP = 3;                                 // ���[�v�̉񐔁i���C�̖{���j

            // �ǔ���t���O
            if (!_playerStatus._WallJudge) return false;

            // checkPos�̏����ʒu
            checkPos.y += rayInitialPos;


            // ���C���΂�
            hit = true;
            for (int loop = 0; loop < MAX_LOOP; ++loop)
            {
                Debug.DrawLine(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, Color.red);// �f�o�b�O�Ń��C��\��
                hit &= Physics2D.Linecast(checkPos + transform.right * 0.1f, checkPos - lineLength * -transform.localScale.x, groundLayer);
                checkPos.y -= colHalfHeight;// ���W��--���Ă���
            }

            return hit;
        }

        // Ray���m�̊Ԋu�ύX���\�b�h(Player�̃T�C�Y�ɂ���ĕύX)
        private float RayInterval()
        {
            float scaleY = transform.localScale.y;
            float rayInterval_defaltSize = 2.4f;
            float rayInterval_BigSize    = 1.8f;
            float rayInterval_SmallSize  = 6.4f;

            if (scaleY > 1) return rayInterval_BigSize;
            if (scaleY < 1) return rayInterval_SmallSize;
            return rayInterval_defaltSize;
        }

        // Ray�̈ʒu�������\�b�h(Player�̃T�C�Y�ɂ���ĕύX)
        private float RayPostionAdj() 
        {
            float scaleY = transform.localScale.y;
            float rayPostionAdj_defaltSize = -0.2f;
            float rayPostionAdj_BigSize    =  1.3f;
            float rayPostionAdj_SmallSize  =   -2f;

            if (scaleY > 1) return rayPostionAdj_BigSize;
            if (scaleY < 1) return rayPostionAdj_SmallSize;
            return rayPostionAdj_defaltSize;
        }
    }
}
