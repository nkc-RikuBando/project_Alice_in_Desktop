using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTest : MonoBehaviour
{
    // �{�^���������ꂽ���ɃV�[����ύX���鏈��

    private int _clickCount = 0;
    private float _timeCount = 0;

    private void Update()
    {
        TimeCount();
        ClickCountAction();
    }


    // ���̓J�E���g���\�b�h
    public void OnClickCount()
    {
        if (_clickCount < 2) _clickCount++;
    }


    // ���Ԍv�����\�b�h
    private void TimeCount()
    {
        // �v���J�n
        if (_clickCount == 1)
        {
            _timeCount += Time.deltaTime;
        }
    }

    // ���̓J�E���g�A�N�V�������\�b�h
    private void ClickCountAction()
    {
        // 2�b�ȓ���2��ڂ̓��͂�����΃A�N�V��������
        if (_timeCount < 2f)
        {
            SceneChange();
        }
        else
        {
            _clickCount = 0;
            _timeCount = 0;
        }
    }

    // �V�[���ύX���\�b�h
    private void SceneChange()
    {
        if (_clickCount == 2)
        {
            _clickCount = 0;
            _timeCount  = 0;
            Debug.Log("�V�[����ύX");
        }
    }

}
