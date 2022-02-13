using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

namespace Gimmicks
{
    public class BoxRigid : MonoBehaviour, IWindowTouch/*, IWindowLeave*/
    {
        private Rigidbody2D rigid;
        private BoxCollider2D boxCol;

        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
            boxCol = GetComponent<BoxCollider2D>();
        }

        public void WindowTouchAction()
        {
            rigid.gravityScale = 0;
            boxCol.enabled = false;
        }

        public void WindowLeaveAction()
        {
            rigid.gravityScale = 20;
            boxCol.enabled = true;
        }
    }
}
