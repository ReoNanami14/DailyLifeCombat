using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bgm : MonoBehaviour
{
    //public AudioClip BGM;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
        Invoke("BGMstart", 5.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void BGMstart()
    {
        aud.Play();
    }
}
