using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.Inputer;

namespace Inputer
{
    public class TestKeyInputer : MonoBehaviour, ITestKey
    {
        public bool EventKey()
        {
            return Input.GetKeyDown(KeyCode.Q);
        }

        public bool EventNagaoshiKey()
        {
            return Input.GetKey(KeyCode.Q);
        }

        public bool EventKeyUp()
        {
            return Input.GetKeyUp(KeyCode.Q);
        }
    }
}

