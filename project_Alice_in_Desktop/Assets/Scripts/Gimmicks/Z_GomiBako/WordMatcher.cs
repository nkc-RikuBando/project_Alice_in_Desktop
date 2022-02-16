using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMatcher
{
    // 入力対象の文字列
    private string target_string;

    // 現在の入力文字位置
    private int current_position;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public WordMatcher(string target_word)
    {
        this.target_string = target_word;
        current_position = 0;
    }

    /// <summary>
    /// 指定された文字と現在位置の文字を比較する
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
    /// 現在入力済みの文字列を返す
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
