using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Gimmicks

    // �撣���ĂĂ��炢�I�I�I�I�I�I
{
    public class WarpHole : MonoBehaviour
    {
        [SerializeField] private GameObject player; // �v���C���[�I�u�W�F�N�g���擾
        private ITestKey _ITestKey;
        [SerializeField] private GameObject warpPoint; // ���[�v��I�u�W�F�N�g���擾
        private bool stayFlg = false;                  // �؍݂��Ă��邩�t���O

        private GameObject camObj;
        private Camera cam;
        private bool cameraZoomFlg;

        void Start()
        {
            _ITestKey = GetComponent<ITestKey>();
            //cameraZoomFlg = false;
            //camObj = GameObject.Find("Main Camera");
            //cam = camObj.GetComponent<Camera>();
        }

        void Update()
        {
            if (StayInput())
            {
                Warp();
                //cameraZoomFlg = true;
            }
        }

        void FixedUpdate()
        {
            //if (cameraZoomFlg == true)
            //{
            //    cam.orthographicSize -= 5 * Time.deltaTime;
            //    if (cam.orthographicSize <= 0)
            //    {
            //        cam.orthographicSize += 5 * Time.deltaTime;
            //    }
            //    else if (cam.orthographicSize >= 7)
            //    {
            //        cam.orthographicSize = 7;
            //        cameraZoomFlg = false;
            //    }
            //}
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[�������ė�����
            if (collision.gameObject == player) stayFlg = true; // �؍݃t���O��true
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            // �v���C���[���o�čs������
            if (collision.gameObject == player) stayFlg = false; // �؍݃t���O��false
        }

        /// <summary>
        /// �v���C���[���G��Ă���A���AQ�L�[������
        /// </summary>
        /// <returns></returns>
        bool StayInput()
        {
            return stayFlg == true && _ITestKey.EventKey();
        }

        /// <summary>
        /// ���[�v��Ƀv���C���[���ړ�������
        /// </summary>
        void Warp()
        {
            player.transform.position = warpPoint.transform.position;
        }

        //IEnumerator Damage()
        //{
        //    // while����10�񃋁[�v
        //    int count = 10;
        //    while (count > 0)
        //    {
        //        // 0.05�b�҂�
        //        yield return new WaitForSeconds(0.05f);
                
        //        // 0.05�b�҂�
        //        yield return new WaitForSeconds(0.05f);
        //        count--;
        //    }
        //}
    }
}
