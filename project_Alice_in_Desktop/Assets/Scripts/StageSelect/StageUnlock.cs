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

        [SerializeField] private Button nextButton;
        [SerializeField] private Button prevButton;

        private int openedWorldCount = 0;
        [SerializeField] private int openedStageCount = -1;

        private void Awake()
        {

            for (int i=1;i<=35;++i)
            {
                folderPanel.Add(GameObject.Find("FolderPanel" + i.ToString("D2")));
                //stageFolder.Add(folderPanel[i-1].transform.GetChild(1).gameObject);
                //zipFolder.Add(folderPanel[i-1].transform.GetChild(2).gameObject);
            }
        }

        private void Start()
        {
            stageManagerSingleton = GameObject.Find("StageManager").GetComponent<StageManagerSingleton>();
            if(debugClearStageNum > 0) stageManagerSingleton.SendClearStage(debugClearStageNum);

            // あとでなおす なおした
            WorldFolderOpenAnim();

            for (int i = 1; i <= stageFolderPanels.Count; ++i)
            {
                StageFolderActiveSwitch(i, false);
            }

            nextButton.interactable = false;
        }

        private void Update()
        {
            // デバッグ用
            // Rキーでシーンリロード
            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    stageManagerSingleton.SendClearStage(debugClearStageNum);
            //    SceneManager.LoadScene("SelectScene");
            //}
        }


        public void StageFolderActiveSwitch(int worldNum,bool val)
        {

            stageFolderPanels[worldNum - 1].SetActive(val);

            worldFolderPanelParent.SetActive(!val);

            if(val)
            {
                // ワールドセレクト→ステージセレクト
                nextButton.interactable = false;
                prevButton.interactable = true;
                StageFolderOpenAnim();
            }
            else
            {
                // ステージセレクト→ワールドセレクト
                nextButton.interactable = true;
                prevButton.interactable = false;
                WorldFolderOpenAnim();
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

        private void WorldFolderOpenAnim()
        {
            // ワールド
            worldFolderPanels[0].GetComponent<Animator>().SetBool("Opened", true);

            if (stageManagerSingleton.GetClearStage() >= 0)
            {
                if (openedWorldCount < 1)
                {
                    openedWorldCount = 1;
                    worldFolderPanels[0].GetComponent<Animator>().SetTrigger("UnLock");
                }
                worldFolderPanels[0].GetComponent<Animator>().SetBool("Opened", true);
            }


            if (stageManagerSingleton.GetClearStage() >= 14)
            {
                if(openedWorldCount<2)
                {
                    openedWorldCount = 2;
                    worldFolderPanels[1].GetComponent<Animator>().SetTrigger("UnLock");
                }
                worldFolderPanels[1].GetComponent<Animator>().SetBool("Opened", true);
            }

            if(stageManagerSingleton.GetClearStage() >= 24)
            {
                if (openedWorldCount < 3)
                {
                    openedWorldCount = 3;
                    worldFolderPanels[2].GetComponent<Animator>().SetTrigger("UnLock");
                }
                worldFolderPanels[2].GetComponent<Animator>().SetBool("Opened", true);
            }
        }

        private void StageFolderOpenAnim()
        {
            for (int i = 0; i < stageManagerSingleton.GetClearStage(); ++i)
            {
                folderPanel[i].GetComponent<Animator>().SetBool("Opened", true);
            }

            // 新しく解放されたステージ     
            if (openedStageCount != stageManagerSingleton.GetClearStage())
            {
                // 未開放なら開放済みに登録
                openedStageCount = stageManagerSingleton.GetClearStage();
                folderPanel[openedStageCount].GetComponent<Animator>().SetTrigger("UnLock");
            }
            else
            {
                folderPanel[openedStageCount].GetComponent<Animator>().SetBool("Opened",true);
            }
        }

    }
}
