using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    [SerializeField] private AudioSource se;
    [SerializeField] private AudioClip[] seClips;

    public static AudioManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = GameObject.FindObjectOfType<AudioManager>();
            }
            return instance;
        }
    }

    // �Ώۂ�SE�z�񂩂�T��
    public void SeAction(string seName)
    {
        AudioClip seClip = null;
        for(int i = 0; i < seClips.Length; i++)
        {
            if(seClips[i].name == seName)
            {
                seClip = seClips[i];
                break;
            }
        }
        this.se.PlayOneShot(seClip);
    }
}
