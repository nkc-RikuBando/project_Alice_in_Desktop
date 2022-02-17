using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.MySceneManager;

namespace StageSelect
{
    public class StageSelectButton : MonoBehaviour
    {
        [SerializeField] private GameObject cameraObj;
        private WindowFade windowFade;

        private StageUnlock stageUnlock;

        private float doubleClickTime = 0.5f;
        private bool doubleClickFlg = false;

        private int openedWorldNum = -1;

        private AudioSource audioSource;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();

            windowFade = cameraObj.GetComponent<WindowFade>();
            stageUnlock = gameObject.GetComponent<StageUnlock>();
        }


        // ステージ選択ボタンが押されたとき
        public void PressedStageSelectButton(string stageNumStr)
        {

            Debug.Log(int.Parse(stageNumStr));
            int stageNum = int.Parse(stageNumStr);
            int getStageNum = ((stageNum / 100) * 8) + stageNum % 100;
            GameObject pressedButton = stageUnlock.GetStageFolder(getStageNum);

            // 押されたボタンの状態が未開放(ジップ)だったら早期リターン
            if (pressedButton.transform.GetChild(2).gameObject.activeSelf == true) return;

            if (doubleClickFlg)
            {
                windowFade.WindowFadeOut("Stage"+stageNumStr);
                audioSource.Play();
            }

            StartCoroutine(DoubleClickTimeCount());
        }

        public void PressedWorldSelectButton(int worldNum)
        {
            // ワールド選択
            GameObject pressedButton = stageUnlock.GetWorldFolder(worldNum);
            if (pressedButton.transform.GetChild(2).gameObject.activeSelf == true) return;

            if (doubleClickFlg)
            {
                stageUnlock.StageFolderActiveSwitch(worldNum,true);
                openedWorldNum = worldNum;
                audioSource.Play();
            }

            StartCoroutine(DoubleClickTimeCount());
        }

        public void PressedPrevButton()
        {
            if (openedWorldNum == -1) return;
            stageUnlock.StageFolderActiveSwitch(openedWorldNum, false);
            audioSource.Play();
        }

        public void PressedNextButton()
        {
            if (openedWorldNum == -1) return;
            stageUnlock.StageFolderActiveSwitch(openedWorldNum, true);
            audioSource.Play();
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
