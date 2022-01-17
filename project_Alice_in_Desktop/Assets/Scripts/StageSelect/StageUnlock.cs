using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace StageSelect
{
    class StageUnlock:MonoBehaviour
    {
        private StageManagerSingleton stageManagerSingleton;
        [SerializeField] private GameObject worldFolderPanelParent;
        [SerializeField] private List<GameObject> worldFolderPanels;
        [SerializeField] private List<GameObject> stageFolderPanels;

        [SerializeField] private List<GameObject> folderPanel;
        //[SerializeField] private List<GameObject> zipFolder;
        //[SerializeField] private List<GameObject> stageFolder;

        [SerializeField] private int debugClearStageNum;

        private void Awake()
        {

            for (int i=1;i<=45;++i)
            {
                folderPanel.Add(GameObject.Find("FolderPanel" + i.ToString("D2")));
                //stageFolder.Add(folderPanel[i-1].transform.GetChild(1).gameObject);
                //zipFolder.Add(folderPanel[i-1].transform.GetChild(2).gameObject);
            }

        }

        private void Start()
        {
            stageManagerSingleton = GameObject.Find("StageManager").GetComponent<StageManagerSingleton>();
            stageManagerSingleton.SendClearStage(debugClearStageNum);

            for (int i=0;i<stageManagerSingleton.GetClearStage().Length;++i)
            {

                if(stageManagerSingleton.GetClearStage()[i])
                {
                    folderPanel[i].transform.GetChild(1).gameObject.SetActive(true);
                    folderPanel[i].transform.GetChild(2).gameObject.SetActive(false);

                    if (i == 0 || i == 14 || i == 29)
                    {
                        worldFolderPanels[(int)i / 14].transform.GetChild(1).gameObject.SetActive(true);
                        worldFolderPanels[(int)i / 14].transform.GetChild(2).gameObject.SetActive(false);
                    }

                }
                else
                {
                    folderPanel[i].transform.GetChild(1).gameObject.SetActive(true);
                    folderPanel[i].transform.GetChild(2).gameObject.SetActive(false);

                    // あとでなおす
                    worldFolderPanels[0].transform.GetChild(1).gameObject.SetActive(true);
                    worldFolderPanels[0].transform.GetChild(2).gameObject.SetActive(false);

                    break;
                }
            }

            for (int i = 1; i <= stageFolderPanels.Count; ++i)
            {
                StageFolderActiveSwitch(i, false);
            }

        }

        private void Update()
        {
            // デバッグ用
            // Rキーでシーンリロード
            if (Input.GetKeyDown(KeyCode.R))
            {
                stageManagerSingleton.SendClearStage(debugClearStageNum);
                SceneManager.LoadScene("SelectScene");
            }
        }


        public void StageFolderActiveSwitch(int worldNum,bool val)
        {
            stageFolderPanels[worldNum - 1].SetActive(val);

            if(val)
            {
                worldFolderPanelParent.SetActive(false);
            }
        }

        public GameObject GetStageFolder(int i)
        {
            // ステージiを渡す(配列ではi-1番目)
            return folderPanel[i-1];
        }

        public GameObject GetWorldFolder(int i)
        {
            // ワールドiを渡す(配列ではi-1番目)
            return worldFolderPanels[i - 1];
        }

    }
}
