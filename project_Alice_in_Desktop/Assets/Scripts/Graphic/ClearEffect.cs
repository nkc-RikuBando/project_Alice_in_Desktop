using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameSystem;


namespace Animation
{
    public class ClearEffect : MonoBehaviour
    {
        private GameObject player;
        // [SerializeField] private GameObject door;

        private Animator fadeAnimator;
        private RectTransform unMaskRect;

        const float SHIFT_Y = 0;

        private WindowFade windowFade;

        [SerializeField] private string nextSceneName;

        // Start is called before the first frame update
        void Start()
        {
            player = GetGameObject.playerObject;
            fadeAnimator = GameObject.Find("FadeMask").GetComponent<Animator>();
            unMaskRect = GameObject.Find("UnMask_Clear").GetComponent<RectTransform>();
            windowFade = GameObject.Find("Camera").GetComponent<WindowFade>();
        }

        // Update is called once per frame
        void Update()
        {
        }

        // �X�e�[�W�N���A���ɌĂяo��
        public void StartClearEffect()
        {
            StartCoroutine("PlayEffect");
        }


        IEnumerator PlayEffect()
        {
            //�����ɏ���������
            unMaskRect.position = new Vector2(player.transform.position.x, player.transform.position.y + SHIFT_Y);
            fadeAnimator.SetTrigger("StageClear");
            player.GetComponent<Animator>().SetTrigger("StageClear");

            //1�t���[����~
            yield return new WaitForSeconds(2);

            //�����ɍĊJ��̏���������
            windowFade.WindowFadeOut(nextSceneName);
        }
    }
}
