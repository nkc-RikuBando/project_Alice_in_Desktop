using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Connector.MySceneManager;

namespace Scene
{
    public class SceneTransition : MonoBehaviour
    {
        // ÉVÅ[ÉìëJà⁄

        [SerializeField] private GameObject _sceneManagerObj;

        private ISceneChange _sceneChange;

        private void Start()
        {
            _sceneChange = _sceneManagerObj.GetComponent<ISceneChange>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                Change();
            }
        }

        public void Reload()
        {
            _sceneChange.ReloadScene();
        }

        public void Change()
        {
            _sceneChange.SceneChange("SelectScene");
        }
    }
}

