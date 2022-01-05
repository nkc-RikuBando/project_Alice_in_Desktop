using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class WindowEffect : MonoBehaviour
{
    [SerializeField] private GameObject effectPre;
    private GameObject effectObj;

    private PostProcessVolume windowVolume;
    private PostProcessVolume cautionVolume;
    //private SpriteRenderer cautionRenderer;

    private bool flg;
    
    // Start is called before the first frame update
    void Start()
    {
        //cautionRenderer = GameObject.Find("Caution").GetComponent<SpriteRenderer>();
        windowVolume = GameObject.Find("Post_Window").GetComponent<PostProcessVolume>();
        cautionVolume = GameObject.Find("Post_Caution").GetComponent<PostProcessVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flg) Mathf.Clamp01(windowVolume.weight += 4 * Time.deltaTime);
        else Mathf.Clamp01(windowVolume.weight -= 6 * Time.deltaTime);
    }

    public void StartWindowEffect()
    {
        effectObj = Instantiate(effectPre);

        effectObj.transform.position = GetMousePosition();
        //processVolume.weight = 1;
        windowVolume.weight = 0;
        flg = true;
    }

    public void EndWindowEffect()
    {
        effectObj.GetComponent<Animator>().SetTrigger("Close");
        effectObj.transform.position = GetMousePosition();
        windowVolume.weight = 1;
        flg = false;
    }

    public void DeadCaution(bool isDead)
    {
        cautionVolume.weight = isDead ? 1 : 0;
        //processVolume.weight = isDead | !flg ? 0 : 1;
    }

    private Vector3 GetMousePosition()
    {
        // Vector3でマウス位置座標を取得する
        Vector3 position = Input.mousePosition;
        // Z軸修正
        position.z = 10f;
        // マウス位置座標をスクリーン座標からワールド座標に変換する
        Vector3 screenToWorldPointPosition = Camera.main.ScreenToWorldPoint(position);

        return screenToWorldPointPosition;
    }
}
