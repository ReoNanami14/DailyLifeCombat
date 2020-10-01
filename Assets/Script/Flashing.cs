using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flashing : MonoBehaviour
{
    public float speed = 1.0f;

    private Text text;
    private float time;

    private enum ObjType
    {
        TEXT
    }

    private ObjType thisObjType = ObjType.TEXT;

    // Start is called before the first frame update
    void Start()
    {
        //アタッチしているオブジェクトを判別
        if (this.gameObject.GetComponent<Text>())
        {
            thisObjType = ObjType.TEXT;
            text = this.gameObject.GetComponent<Text>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //オブジェクトのAlpha値を更新
        if (thisObjType == ObjType.TEXT)
        {
            text.color = GetAlphaColor(text.color);
        }
    }

    //Alpha値を更新してColorを返す
    Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 0.5f * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;

        return color;
    }
}
