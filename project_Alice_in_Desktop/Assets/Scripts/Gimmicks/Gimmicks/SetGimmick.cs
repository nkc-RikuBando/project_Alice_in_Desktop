using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class SetGimmick : MonoBehaviour
    {
        public void Awake()
        {
            GetGameObject.GimmickObj = gameObject;
        }
    }
}
