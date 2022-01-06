using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject effectPre;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Generator()
    {
        GameObject effect = GameObject.Instantiate(effectPre);

        effect.transform.position = gameObject.transform.position;
        effect.transform.localScale = gameObject.transform.localScale;

    }
}
