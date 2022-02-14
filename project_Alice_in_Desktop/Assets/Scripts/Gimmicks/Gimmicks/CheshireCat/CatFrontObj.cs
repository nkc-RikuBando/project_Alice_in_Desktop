using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gimmicks
{
    public class CatFrontObj : MonoBehaviour
    {
        private bool isFrontObj;
        private int enterObjNum;

        public bool IsFrontObj
        {
            get { return isFrontObj; } 
            set { isFrontObj = value; }
        }

        public int EnterObjNum
        {
            get { return enterObjNum; }
            set { enterObjNum = value; }
        }

        void Start()
        {
            enterObjNum = default;
        }
    }
}
