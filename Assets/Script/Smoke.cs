using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public GameObject smokePrehab;

    public AudioClip smokeSE;
    AudioSource aud;

    private void OnCollisionEnter(Collision collision)
    {
        this.aud = GetComponent<AudioSource>();

        if (collision.gameObject.CompareTag("player2"))
        {
            GameObject Smoke = Instantiate(smokePrehab, collision.transform.position, Quaternion.identity);

            Smoke.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);

            this.aud.PlayOneShot(this.smokeSE);
        }
    }
}
