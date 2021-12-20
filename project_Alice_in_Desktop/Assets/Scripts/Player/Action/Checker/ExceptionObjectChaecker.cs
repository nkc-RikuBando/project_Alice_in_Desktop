using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.MySceneManager;

namespace Player
{
    public class ExceptionObjectChaecker : MonoBehaviour
    {
        // ��O�I�u�W�F�N�g����

        [SerializeField, Tooltip("Debug�pFlg")] private bool _debugFlg;

        private ISceneChange _sceneChange;
        private CapsuleCollider2D _capCol;
        private GameObject _parentObj;
        private bool _colHitFlg;


        void Start()
        {
            _parentObj   = transform.parent.gameObject;
            _capCol �@�@ = _parentObj.GetComponent<CapsuleCollider2D>();
            _sceneChange = GameObject.Find("SceneManager").GetComponent<ISceneChange>();
        }
        void Update()
        {
            ExceptionDead();
        }


        private void ExceptionDead()
        {
            // �f�o�b�O�p
            if (_debugFlg)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _colHitFlg = true;
                    Debug.Log(_colHitFlg);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _colHitFlg = false;
                    Debug.Log(_colHitFlg);
                }
            }


            // �G���[�I�u�W�F�N�g�ɓ������Ă���ꍇ
            if (_colHitFlg)
            {
                // ���ꂢ������H
                //_capCol.enabled = false;

                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("���񂾁I");
                    _sceneChange.ReloadScene();
                    _colHitFlg = true;
                }
            }
            //else _capCol.enabled = true;
        }


        // ����������
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _colHitFlg = true;
        }

        // ���ꂽ��
        private void OnTriggerExit2D(Collider2D collision)
        {
            _colHitFlg = false;
        }
    }

}
