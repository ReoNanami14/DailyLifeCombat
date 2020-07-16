using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("display connected:" + Display.displays.Length);
        if (Display.displays.Length > 0)
        {
            Display.displays[0].Activate();
        }
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
