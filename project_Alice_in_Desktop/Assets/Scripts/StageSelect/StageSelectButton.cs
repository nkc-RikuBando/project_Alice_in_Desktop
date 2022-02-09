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

        private int openedWorldNum = 0;

        private void Start()
        {
            windowFade = cameraObj.GetComponent<WindowFade>();
            stageUnlock = gameObject.GetComponent<StageUnlock>();
        }


        // �X�e�[�W�I���{�^���������ꂽ�Ƃ�
        public void PressedStageSelectButton(string stageNumStr)
        {

            Debug.Log(int.Parse(stageNumStr));
            int stageNum = int.Parse(stageNumStr);
            int getStageNum = (((stageNum / 100) - 1) * 15) + stageNum % 100;
            GameObject pressedButton = stageUnlock.GetStageFolder(getStageNum);

            // �����ꂽ�{�^���̏�Ԃ����J��(�W�b�v)�������瑁�����^�[��
            if (pressedButton.transform.GetChild(2).gameObject.activeSelf == true) return;

            if (doubleClickFlg)
            {
                windowFade.WindowFadeOut("Stage"+stageNumStr);
            }

            StartCoroutine(DoubleClickTimeCount());
        }

        public void PressedWorldSelectButton(int worldNum)
        {
            // ���[���h�I��
            GameObject pressedButton = stageUnlock.GetWorldFolder(worldNum);
            if (pressedButton.transform.GetChild(2).gameObject.activeSelf == true) return;

            if (doubleClickFlg)
            {
                stageUnlock.StageFolderActiveSwitch(worldNum,true);
                openedWorldNum = worldNum;
            }

            StartCoroutine(DoubleClickTimeCount());
        }

        public void PressedPrevButton()
        {
            if (openedWorldNum == 0) return;
            stageUnlock.StageFolderActiveSwitch(openedWorldNum, false);
        }

        public void PressedNextButton()
        {
            if (openedWorldNum == 0) return;
            stageUnlock.StageFolderActiveSwitch(openedWorldNum, true);
        }

        // �_�u���N���b�N�̓��͗P�\���Ԃ̌v�� & �t���O�Ǘ�
        IEnumerator DoubleClickTimeCount()
        {
            doubleClickFlg = true;

            yield return new WaitForSeconds(doubleClickTime);

            doubleClickFlg = false;
        }
    }
}
