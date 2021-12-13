using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSystem
{
    public class ClearFlg : MonoBehaviour, IGetKey
    {// ����m���Ă���B

        [SerializeField] private List<GameObject> keyList = new List<GameObject>();

        //[System.NonSerialized] public bool clearFlg; 
        private GameObject player;
        [SerializeField] private string playerName; // �v���C���[�̖��O���擾

        private bool clearFlg;
        [SerializeField] private string sceneName;        // �V�[���ړ���̖��O
        [SerializeField] private float fadeTime;     // �t�F�[�h���鎞��

        void Start()
        {
            player = GameObject.Find(playerName); // �v���C���[�I�u�W�F�N�g���擾
            clearFlg = false;
            if (keyList.Count <= 0) Clear();
        }

        public void AddKey(GameObject set)
        {
            keyList.Add(set);
        }

        // �����������烊�X�g�������
        public void GetKey(GameObject get)
        {
            keyList.Remove(get); // ���X�g���献������
            if (keyList.Count <= 0)
            {
                Clear(); // �N���A���\�b�h���Ă�
            }
            Destroy(get); // ���X�g����������献���g������
        }

        public void Clear()
        {
            clearFlg = true; // �N���A�t���O��true�ɂ���
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // �v���C���[���G�ꂽ�A�N���A�t���O��true�̂Ƃ�
            if (collision.gameObject == player && clearFlg == true)
            {
                // �N���A�V�[���Ɉړ�
                FadeManager.Instance.LoadScene(sceneName, fadeTime);
            }
        }
    }
}
