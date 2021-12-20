using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Animation
{
    public class ClearEffect : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        // [SerializeField] private GameObject door;

        private Animator fadeAnimator;
        private RectTransform unMaskRect;

        const float SHIFT_Y = 0;

        // Start is called before the first frame update
        void Start()
        {
            fadeAnimator = GameObject.Find("FadeMask").GetComponent<Animator>();
            unMaskRect = GameObject.Find("UnMask").GetComponent<RectTransform>();
        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.Return)) StartClearEffect();
        }

        // ステージクリア時に呼び出す
        public void StartClearEffect()
        {
            unMaskRect.position = new Vector2(player.transform.position.x, player.transform.position.y + SHIFT_Y);
            fadeAnimator.SetTrigger("StageClear");
            player.GetComponent<Animator>().SetTrigger("StageClear");
            // door.GetComponent<Animator>().SetTrigger("Action");
        }
    }
}
