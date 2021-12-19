using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Animation
{
    public class DeleteObject : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void DestroyObject()
        {
            Destroy(gameObject);
        }
    }

}
