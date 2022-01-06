using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StageSelect
{
    public class StageManagerSingleton : MonoBehaviour,ISendClearStageNum
    {
        public static StageManagerSingleton instance;
        private static bool[] clearStage = new bool[45];

        public void SendClearStage(int stageNum)
        {
            for (int i = 0; i < stageNum; ++i)
            {
                clearStage[i] = true;
            }
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

        }


        public bool[] GetClearStage()
        {
            return clearStage;
        }
    }
}
