using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class SetKeyUi : MonoBehaviour
    {
        public void Awake()
        {
            GetUIObject.KeyUI = gameObject;
        }
    }
}
