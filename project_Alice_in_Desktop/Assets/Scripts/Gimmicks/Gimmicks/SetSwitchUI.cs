using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class SetSwitchUI : MonoBehaviour
    {
        public void Awake()
        {
            GetUIObject.SwitchUI = gameObject;
        }
    }
}
