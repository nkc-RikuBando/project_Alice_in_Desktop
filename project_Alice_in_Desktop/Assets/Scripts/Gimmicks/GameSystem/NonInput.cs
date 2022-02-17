using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Window;

namespace GameSystem
{
    public class NonInput : MonoBehaviour, IWindowTouch, IWindowLeave
    {
        void Start()
        {

        }

        void Update()
        {

        }

        public void WindowLeaveAction()
        {
            throw new System.NotImplementedException();
        }

        public void WindowTouchAction()
        {
            throw new System.NotImplementedException();
        }
    }
}