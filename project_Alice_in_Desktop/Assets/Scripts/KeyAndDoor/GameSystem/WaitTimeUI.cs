using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Gimmicks;

namespace GameSystem
{
    public class WaitTimeUI : MonoBehaviour
    {
        private Image waitTime;

        void Start()
        {
            waitTime = GetComponent<Image>();
            waitTime.fillAmount = 0;
        }

        void Update()
        {
            if (Input.GetKey(KeyCode.Q))
            {
                waitTime.fillAmount += 0.01f;
            }
        }
    }
}
