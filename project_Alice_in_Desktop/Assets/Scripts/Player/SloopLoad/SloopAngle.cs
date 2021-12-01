using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.StageObject;

namespace StageObject
{
    public class SloopAngle : MonoBehaviour, ISloopAngleSetable
    {
        // ��̊p�x����

        [SerializeField, Tooltip("��̊p�x")] private float angle;

        public float AngleSentable()
        {
            return angle;
        }
    }

}
