using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.StageObject;

namespace StageObject
{
    public class SloopAngle : MonoBehaviour, ISloopAngleSetable
    {
        // ç‚ÇÃäpìxèàóù

        [SerializeField, Tooltip("ç‚ÇÃäpìx")] private float angle;

        public float AngleSentable()
        {
            return angle;
        }
    }

}
