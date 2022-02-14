using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

namespace Gimmicks
{
    public class BoxRigid : MonoBehaviour, IWindowTouch, IWindowLeave
    {
        private Rigidbody2D rigid;

        void Start()
        {
            rigid = GetComponent<Rigidbody2D>();
        }

        public void WindowTouchAction()
        {
            rigid.bodyType = RigidbodyType2D.Kinematic;
        }

        public void WindowLeaveAction()
        {
            rigid.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
