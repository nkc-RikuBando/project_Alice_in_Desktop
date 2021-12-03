using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gimmicks;

namespace GameSystem
{
    public class WaitTimeUIGene : MonoBehaviour
    {
        [SerializeField] private GameObject waitTimeUI;
        private GameObject canvas;
        private Box boxScr;

        GameObject obj;
        [SerializeField] GameObject player;
        bool test;
        bool test2;

        void Start()
        {
            canvas = GameObject.Find("Canvas");
            boxScr = GetComponent<Box>();
        }

        void Update()
        {
            test = boxScr.PlHitFlg && Input.GetKey(KeyCode.Q) ? true : false;
            if (test == true && !test2)
            {
                obj = Instantiate(waitTimeUI, canvas.transform);
                obj.transform.position = player.transform.position + new Vector3(0, 2);
                test2 = true;
                //Vector3.zero, Quaternion.identity);
                //obj.transform.parent = canvas.transform;
            }
            else if(!test)
            {
                if (obj == null) return;
                Destroy(obj.gameObject);
                test2 = false;
            }
        }
    }
}
