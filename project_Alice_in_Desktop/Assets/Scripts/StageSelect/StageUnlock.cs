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

        private static int  openedWorldCount = -1;
        [SerializeField] private int openedStageCount = -1;

        [SerializeField] private Texture2D cursorTex;
        [SerializeField] private Vector2 hotSpot = new Vector2(50, 50);

        [SerializeField] private bool debugFlg = false;
        private int f12Count = 0;

        [SerializeField] private List<GameObject> trialStageFolderList;

        private void Awake()
        {

            for (int i=1;i<=32;++i)
            {
                folderPanel.Add(GameObject.Find("FolderPanel" + i.ToString("D2")));
                //stageFolder.Add(folderPanel[i-1].transform.GetChild(1).gameObject);
                //zipFolder.Add(folderPanel[i-1].transform.GetChild(2).gameObject);
            }
        }

        private void Start()
        {
            Cursor.SetCursor(cursorTex, hotSpot, CursorMode.Auto);

            stageManagerSingleton = GameObject.Find("StageManager").GetComponent<StageManagerSingleton>();
            if(debugClearStageNum > 0) stageManagerSingleton.SendClearStage(debugClearStageNum);

            // あとでなおす なおした
            WorldFolderOpenAnim();

            for (int i = 0; i < stageFolderPanels.Count; ++i)
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

            // デバッグ用 F12を5回押すと全ステージ解放
            if(Input.GetKeyDown(KeyCode.F12))
            {
                f12Count++;

                if(f12Count>=5 && debugFlg)
                {
                    stageManagerSingleton.SendClearStage(31);
                }
            }
        }


        public void StageFolderActiveSwitch(int worldNum,bool val)
        {

            stageFolderPanels[worldNum].SetActive(val);

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
            // ワールドi
            return worldFolderPanels[i];
        }

        private void WorldFolderOpenAnim()
        {
            // ワールド
            worldFolderPanels[0].GetComponent<Animator>().SetBool("Opened", true);

            // 体験版用
            worldFolderPanels[4].GetComponent<Animator>().SetBool("Opened", true);

            if (stageManagerSingleton.GetClearStage() >= 0)
            {
                if (openedWorldCount < 1)
                {
                    openedWorldCount = 1;
                    worldFolderPanels[0].GetComponent<Animator>().SetTrigger("UnLock");
                    // 体験版用
                    worldFolderPanels[4].GetComponent<Animator>().SetTrigger("UnLock");

                }
                else
                {
                    worldFolderPanels[0].GetComponent<Animator>().SetBool("Opened", true);
                    worldFolderPanels[4].GetComponent<Animator>().SetBool("Opened", true);
                }
            }


            if (stageManagerSingleton.GetClearStage() >= 8)
            {
                if (openedWorldCount < 2)
                {
                    openedWorldCount = 2;
                    worldFolderPanels[1].GetComponent<Animator>().SetTrigger("UnLock");
                }
                else
                {
                    worldFolderPanels[1].GetComponent<Animator>().SetBool("Opened", true);
                }
            }

            if(stageManagerSingleton.GetClearStage() >= 16)
            {
                if (openedWorldCount < 3)
                {
                    openedWorldCount = 3;
                    worldFolderPanels[2].GetComponent<Animator>().SetTrigger("UnLock");
                }
                else
                {
                    worldFolderPanels[2].GetComponent<Animator>().SetBool("Opened", true);
                }
            }

            if(stageManagerSingleton.GetClearStage() >= 24)
            {
                if (openedWorldCount < 4)
                {
                    openedWorldCount = 4;
                    worldFolderPanels[3].GetComponent<Animator>().SetTrigger("UnLock");
                }
                else
                {
                    worldFolderPanels[3].GetComponent<Animator>().SetBool("Opened", true);
                }
            }
        }

        private void StageFolderOpenAnim()
        {
            for (int i = 0; i < stageManagerSingleton.GetClearStage(); ++i)
            {
                folderPanel[i].GetComponent<Animator>().SetBool("Opened", true);
            }

            for (int i = 0; i < trialStageFolderList.Count; i++)
            {
                trialStageFolderList[i].GetComponent<Animator>().SetBool("Opened", true);
            }

            // 新しく解放されたステージ     
            if (openedStageCount != stageManagerSingleton.GetClearStage() && stageManagerSingleton.GetClearStage()!=32)
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
