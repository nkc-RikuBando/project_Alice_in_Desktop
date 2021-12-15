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

        [SerializeField] private string sceneName;        // �V�[���ړ���̖��O
        [SerializeField] private float fadeTime;     // �t�F�[�h���鎞��

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
                    // �N���A�V�[���Ɉړ�
                    FadeManager.Instance.LoadScene(sceneName, fadeTime);
                }
            }
        }
    }
}
