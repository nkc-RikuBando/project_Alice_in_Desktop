using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class SetHirakuUI : MonoBehaviour
    {
        public void Awake()
        {
            GetUIObject.HirakuUI = gameObject;
        }
    }
}
