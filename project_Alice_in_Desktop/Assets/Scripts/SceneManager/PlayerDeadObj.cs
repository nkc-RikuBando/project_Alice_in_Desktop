using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class PlayerDeadObj : MonoBehaviour
{
    // ��ʊO�ɍs����Player�����S���鏈��

    [SerializeField] private GameObject _fadeObj;  
    private FadeEffect _fadeEffect;

    private void Start()
    {
        _fadeEffect = _fadeObj.GetComponent<FadeEffect>();
    }

    // ����������
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Player���擾
        var playerCheck = collision.gameObject.GetComponent<PlayerStatus>();

        // ���������̂�Player�̏ꍇ
        if (playerCheck != null)
        {
            // �V�[���������[�h
            _fadeEffect.StartOutsideEffect();
        }
    }
}
