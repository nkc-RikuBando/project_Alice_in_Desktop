using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.MySceneManager;

namespace StageSelect
{
    public class StageSelectButton : MonoBehaviour
    {
        [SerializeField] private ISceneChange iSceneChange;

        private float doubleClickTime = 0.5f;
        private bool doubleClickFlg = false;

        private void Start()
        {
            iSceneChange = GameObject.Find("SceneManager").GetComponent<ISceneChange>();
        }

        public void PressedStageSelectButton(string stageName)
        {
            if (doubleClickFlg)
            {
                Debug.Log("ダブルクリック");
                iSceneChange.SceneChange(stageName);
            }

            StartCoroutine(DoubleClickTimeCount());
        }

        IEnumerator DoubleClickTimeCount()
        {
            doubleClickFlg = true;

            yield return new WaitForSeconds(doubleClickTime);

            doubleClickFlg = false;
        }
    }
}
