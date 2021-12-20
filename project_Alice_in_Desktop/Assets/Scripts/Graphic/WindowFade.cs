using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WindowFade : MonoBehaviour
{
    private Animator cameraAnimator;
    private string nextName = "";

    // Start is called before the first frame update
    void Start()
    {
        cameraAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift)) WindowFadeOut("BandoScene_NEO");
    }

    // �V�[���J�ڎ��ɌĂяo�� �����ɑJ�ڐ�̃V�[����
    public void WindowFadeOut(string nextSceneName)
    {
        nextName = nextSceneName;
        cameraAnimator.SetTrigger("Close");
    }

    // �A�j���[�V��������Ăяo��
    public void SceneChange()
    {
        SceneManager.LoadScene(nextName);
    }
}
