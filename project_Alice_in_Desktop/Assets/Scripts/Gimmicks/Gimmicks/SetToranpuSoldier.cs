using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSystem;

namespace Gimmicks
{
    public class SetToranpuSoldier : MonoBehaviour
    {
        public void Awake()
        {
            GetGameObject.GimmickObj = gameObject;
        }
    }
}
