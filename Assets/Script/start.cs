using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class start : MonoBehaviour
{
    public AudioClip startSE;
    AudioSource aud;

    // Start is called before the first frame update
    void Start()
    {
        this.aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void gameStart()
    {
        this.aud.PlayOneShot(this.startSE);
        SceneManager.LoadScene("GameScene");
    }
}
