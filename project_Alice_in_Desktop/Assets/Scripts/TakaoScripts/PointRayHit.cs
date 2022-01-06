using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRayHit : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;
    [SerializeField, Tooltip("�ڐG�I�u�W�F�N�g�̃��C���[")] private LayerMask layerMasks;

    // �n�ʔ���p�̕ϐ�
    [SerializeField] private float raylength = 30f;  // ���C�̒���

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

        //Debug.Log(hit);
        return hit;
    }
}
