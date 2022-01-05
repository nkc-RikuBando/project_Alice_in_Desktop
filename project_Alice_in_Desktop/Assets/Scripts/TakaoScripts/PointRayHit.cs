using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRayHit : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;
    public int distance = 10;
    [SerializeField, Tooltip("�ڐG�I�u�W�F�N�g�̃��C���[")] private LayerMask layerMasks;

    // �n�ʔ���p�̕ϐ�
    [SerializeField] private float raylength = 1f;  // ���C�̒���

    // Start is called before the first frame update
    void Start()
    {
        // �R���|�[�l���g���擾
        rb2d = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit();
        CheakIsCollisionObj(box2d);
    }

    public bool CheakIsCollisionObj(BoxCollider2D col)
    {
        bool hit = false;
        Vector3 checkPos = transform.position;          // �v���C���[�̍��W
        float colHalfWidth = col.size.y / 0.5f;         // �v���C���[�̔����̃R���C�_�[�̑傫��(�v����)
        Vector3 lineLength = transform.up * raylength;  // ���C�̒���(�v����)

        // checkPos�̈ʒu�����[�Ɉړ�
        checkPos.y += colHalfWidth;

        Debug.DrawLine(checkPos + transform.up, checkPos - lineLength, Color.red);// �f�o�b�O�Ń��C��\��
        hit = Physics2D.Linecast(checkPos + transform.up , checkPos - lineLength,layerMasks);

        Debug.Log(hit);
        return hit;
    }
    private void RaycastHit()
    {
        //RaycastHit2D raycastHit = Physics2D.Raycast(gameObject.transform.position, transform.up);
        ////Ray�̍쐬�@�@�@�@�@�@�@��Ray���΂����_�@�@�@��Ray���΂�����
        ////Ray ray = new Ray(gameObject.transform.position, transform.up);

        ////Ray�����������I�u�W�F�N�g�̏������锠
        //RaycastHit hit;

        ////Ray�̔�΂��鋗��
        ////int distance = 0;

        ////Ray�̉���  ��Ray�̌��_�@��Ray�̕����@��Ray�̐F
        //Debug.DrawRay(gameObject.transform.position, transform.up, Color.red);
    }
}
