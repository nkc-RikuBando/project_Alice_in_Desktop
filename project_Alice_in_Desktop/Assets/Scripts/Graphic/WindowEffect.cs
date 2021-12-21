using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WindowEffect : MonoBehaviour
{
    private PostProcessVolume processVolume;
    private SpriteRenderer cautionRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        cautionRenderer = GameObject.Find("Caution").GetComponent<SpriteRenderer>();
        processVolume = GameObject.Find("Post_Window").GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) StartWindowEffect();
        //if (Input.GetMouseButtonUp(0)) EndWindowEffect();
    }

    public void StartWindowEffect()
    {
        processVolume.weight = 1;
    }

    public void EndWindowEffect()
    {
        processVolume.weight = 0;
    }

    public void DeadCaution(bool isDead)
    {
        cautionRenderer.enabled = isDead;
        processVolume.weight = isDead ? 0 : 1;
    }
}
