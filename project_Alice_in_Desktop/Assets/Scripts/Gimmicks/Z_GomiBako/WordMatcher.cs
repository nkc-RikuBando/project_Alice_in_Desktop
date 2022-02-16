using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMatcher
{
    // ���͑Ώۂ̕�����
    private string target_string;

    // ���݂̓��͕����ʒu
    private int current_position;

    /// <summary>
    /// �R���X�g���N�^
    /// </summary>
    public WordMatcher(string target_word)
    {
        this.target_string = target_word;
        current_position = 0;
    }

    /// <summary>
    /// �w�肳�ꂽ�����ƌ��݈ʒu�̕������r����
    /// </summary>
    public bool matching(string input_char)
    {
        if (getCurrentChar() == input_char)
        {
            current_position += 1;
            return true;
        }
        return false;
    }

    /// <summary>
    /// ���ݓ��͍ς݂̕������Ԃ�
    /// </summary>
    public string getInputtedString()
    {
        return (target_string.Substring(0, current_position));
    }

    private string getCurrentChar()
    {
        return (target_string.Substring(current_position, 1));
    }

}
