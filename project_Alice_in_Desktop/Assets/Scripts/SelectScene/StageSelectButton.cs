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

        // ステージ選択ボタンが押されたとき
        public void PressedStageSelectButton(string stageName)
        {
            if (doubleClickFlg)
            {
                Debug.Log("ダブルクリック");
                iSceneChange.SceneChange(stageName);
            }

            StartCoroutine(DoubleClickTimeCount());
        }

        // ダブルクリックの入力猶予時間の計測 & フラグ管理
        IEnumerator DoubleClickTimeCount()
        {
            doubleClickFlg = true;

            yield return new WaitForSeconds(doubleClickTime);

            doubleClickFlg = false;
        }
    }
}
