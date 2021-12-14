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
        private bool _colFlg;


        void Start()
        {
            _parentObj = transform.parent.gameObject;
            _capCol = _parentObj.GetComponent<CapsuleCollider2D>();
            _sceneChange = GameObject.Find("SceneManager").GetComponent<ISceneChange>();
        }
        void Update()
        {
            ExceptionDead();
        }


        private void ExceptionDead()
        {
            if (_debugFlg)
            {
                if (Input.GetKeyDown(KeyCode.Alpha1))
                {
                    _colFlg = true;
                    Debug.Log(_colFlg);
                }
                else if (Input.GetKeyDown(KeyCode.Alpha2))
                {
                    _colFlg = false;
                    Debug.Log(_colFlg);
                }
            }

            if (_colFlg)
            {
                _capCol.enabled = false;

                if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("���񂾁I");
                    _sceneChange.ReloadScene();
                    _colFlg = true;
                }
            }
            else _capCol.enabled = true;
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            _colFlg = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _colFlg = true;
        }
    }

}
