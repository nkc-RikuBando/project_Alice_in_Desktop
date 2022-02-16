using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetWord : MonoBehaviour
{
    private WordMatcher matcher;
    private InputField input_field;

    // Start is called before the first frame update
    void Start()
    {
        input_field = this.GetComponentInChildren<InputField>();

        string target_word = "goma";
        matcher = new WordMatcher(target_word);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onInput(string text)
    {
        if (text.Length == 0)
        {
            return;
        }

        string input_char = text.Substring(text.Length - 1, 1);
        bool result = matcher.matching(input_char);

        // “ü—Í“à—e‚Æˆê’v‚µ‚È‚©‚Á‚½ê‡‚Í–ß‚·
        if (!result)
        {
            input_field.text = matcher.getInputtedString();
        }
        else Debug.Log("FGO");
    }
}
