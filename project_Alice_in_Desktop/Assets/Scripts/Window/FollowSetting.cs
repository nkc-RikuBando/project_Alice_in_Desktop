using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Window
{
    // �E�B���h�E�ɒǏ]����I�u�W�F�N�g�̐ݒ�
    // �E�B���h�E����������Ƃ��ɁA���̕����ɒǏ]���Ȃ��I�u�W�F�N�g�͐F���Â�����

    enum RENDERER_TYPE
    {
        SINGLE_SPRITE,
        TILEMAP,
        BONE_ANIMATION
    }

    public class FollowSetting : MonoBehaviour
    {
        [Header("�{�[���A�j���[�V�����̂���I�u�W�F�N�g�͎q�I�u�W�F�N�g�̃X�v���C�g�����ׂăA�^�b�`")]
        [SerializeField] List<SpriteRenderer> animationSprites;

        private WindowManager windowManager;

        [SerializeField] private bool followUpWall = false;
        [SerializeField] private bool followRightWall = false;
        [SerializeField] private bool followDownWall = false;
        [SerializeField] private bool followLeftWall = false;

        private float movementX, movementY;
        private int moveObjNum;

        private SpriteRenderer sr;
        private Tilemap tilemap;

        [SerializeField] Color32 shadowColor = new Color32(60, 60, 60, 255);
        private Color32 defaultColor;

        private int rendererTipe = 3;

        private void Start()
        {
            windowManager = GameObject.Find("WindowManager").GetComponent<WindowManager>();

            // ���ʂ̃I�u�W�F�N�g�ƃ^�C���}�b�v�̏ꍇ�ŕ�����
            if (GetComponent<SpriteRenderer>() != null)
            {
                sr = GetComponent<SpriteRenderer>();
                defaultColor = sr.color;
                rendererTipe = (int)RENDERER_TYPE.SINGLE_SPRITE;
            }

            if (GetComponent<Tilemap>() != null)
            {
                tilemap = GetComponent<Tilemap>();
                defaultColor = tilemap.color;
                rendererTipe = (int)RENDERER_TYPE.TILEMAP;
            }

            if (animationSprites.Count != 0) rendererTipe = (int)RENDERER_TYPE.BONE_ANIMATION;

        }

        private void FixedUpdate()
        {
            Follow();
        }


        private void Follow()
        {

            // �ǂ̃E�B���h�E��������Ă��邩
            moveObjNum = windowManager.GetObjNum();
            ColorChange();

            if (moveObjNum == 0 || moveObjNum == 1 || moveObjNum == 2 || moveObjNum == 9)
            {
                if (followUpWall)
                {
                    movementY = windowManager.GetMovement().y;
                    ColorReSet();
                }
            }

            if (moveObjNum == 2 || moveObjNum == 3 || moveObjNum == 4 || moveObjNum == 9)
            {
                if (followRightWall)
                {
                    movementX = windowManager.GetMovement().x;
                    ColorReSet();
                }
            }

            if (moveObjNum == 4 || moveObjNum == 5 || moveObjNum == 6 || moveObjNum == 9)
            {
                if (followDownWall)
                {
                    movementY = windowManager.GetMovement().y;
                    ColorReSet();
                }
            }

            if (moveObjNum == 6 || moveObjNum == 7 || moveObjNum == 0 || moveObjNum == 9)
            {
                if (followLeftWall)
                {
                    movementX = windowManager.GetMovement().x;
                    ColorReSet();
                }
            }

            if (moveObjNum == 8)
            {
                movementX = 0;
                movementY = 0;
                ColorReSet();
            }

            transform.position += new Vector3(movementX, movementY, 0);
        }

        private void ColorChange()
        {
            if (rendererTipe==(int)RENDERER_TYPE.SINGLE_SPRITE)
            {
                sr.color = shadowColor;
            }
            else if(rendererTipe==(int)RENDERER_TYPE.TILEMAP)
            {
                tilemap.color = shadowColor;
            }
            else if(rendererTipe==(int)RENDERER_TYPE.BONE_ANIMATION)
            {
                for (int i = 0; i < animationSprites.Count; ++i)
                {
                    animationSprites[i].color = shadowColor;
                }
            }
        }

        private void ColorReSet()
        {
            if (rendererTipe == (int)RENDERER_TYPE.SINGLE_SPRITE)
            {
                sr.color = defaultColor;
            }
            else if (rendererTipe == (int)RENDERER_TYPE.TILEMAP)
            {
                tilemap.color = defaultColor;
            }
            else if (rendererTipe == (int)RENDERER_TYPE.BONE_ANIMATION)
            {
                for (int i = 0; i < animationSprites.Count; ++i)
                {
                    animationSprites[i].color = Color.white;
                }
            }

        }
    }
}