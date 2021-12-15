using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    public class ClearFig2 : MonoBehaviour, IHitSwitch
    {
        private bool switchFlg = false;
        [SerializeField] private GameObject player;
        //[SerializeField] private GameObject nanika;

        [SerializeField] private string sceneName;        // シーン移動先の名前
        [SerializeField] private float fadeTime;     // フェードする時間

        void Start()
        {
            //nanika.SetActive(false);
        }

        public void Switch(bool switchOn)
        {
            switchFlg = switchOn;
            {
                //if (switchFlg == true)
                //    nanika.SetActive(true);
                //else
                //    nanika.SetActive(false);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject == player)
            {
                if(switchFlg == true)
                {
                    // クリアシーンに移動
                    FadeManager.Instance.LoadScene(sceneName, fadeTime);
                }
            }
        }
    }
}
