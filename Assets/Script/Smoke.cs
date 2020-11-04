using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public GameObject smokePrehab;
    public GameObject destPos;
    public GameObject destPos2;

    public AudioClip smokeSE;
    AudioSource aud;

    bool touch=false;

    private void Update()
    {
        if (this.transform.position == destPos.transform.position || this.transform.position == destPos2.transform.position)
        {
            touch = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (touch)
        {
            this.aud = GetComponent<AudioSource>();

           // if (collision.gameObject.CompareTag("player1") || collision.gameObject.CompareTag("player2"))
           // {
                GameObject Smoke = Instantiate(smokePrehab, collision.transform.position, Quaternion.identity);

                Smoke.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

                this.aud.PlayOneShot(this.smokeSE);

            this.transform.position = new Vector3(-0.033f, 1.02f, 0.681f);
            //}
        }
        else  touch = false; 
    }
}
