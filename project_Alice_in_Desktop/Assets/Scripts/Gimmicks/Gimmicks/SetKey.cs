using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class SetKey : MonoBehaviour
    {
        public void Awake()
        {
            GetGameObject.KeyObj = gameObject;
        }
    }
}
