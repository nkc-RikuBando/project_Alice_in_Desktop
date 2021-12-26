using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Window
{
    public class HoldObj : MonoBehaviour
    {
        private GameObject catchObj;
        private WindowManager windowManager;
        [SerializeField] private LayerMask mouseTouchableLayer;

        private void Start()
        {
            windowManager = GetComponent<WindowManager>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                // クリックしたとき
                RaycastHit2D hit2d= Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition),new Vector3(0,0,1),10f, mouseTouchableLayer);

                if (hit2d.collider != null)
                {
                    catchObj = hit2d.collider.gameObject;
                    windowManager.SetMoveFlg(catchObj, true);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                // クリック離した時
                windowManager.SetMoveFlg(catchObj, false);
                catchObj = null;
            }

        }
        public GameObject GetCatchObj()
        {
            return catchObj;
        }
    }
}