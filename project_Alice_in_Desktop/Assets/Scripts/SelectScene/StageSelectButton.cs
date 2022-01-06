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

        // �X�e�[�W�I���{�^���������ꂽ�Ƃ�
        public void PressedStageSelectButton(string stageName)
        {
            if (doubleClickFlg)
            {
                Debug.Log("�_�u���N���b�N");
                iSceneChange.SceneChange(stageName);
            }

            StartCoroutine(DoubleClickTimeCount());
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
