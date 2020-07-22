using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickUp : MonoBehaviour
{
    public Transform theDest;
    Rigidbody rb;
    public float speed;

    //追加
    public AudioClip holdSE;
    public AudioClip throwSE;
    //public AudioClip destroySE;

    AudioSource aud;


    void Start()
    {

        rb = GetComponent<Rigidbody>();
        //dest = GameObject.Find("Destination");

        //new
        this.aud = GetComponent<AudioSource>();

    }
    void Update()
    {
        Vector3 p1 = transform.position;
        Vector3 p2 = this.theDest.position;
        Vector3 dir = p1 - p2;
        float d = dir.magnitude;

        if (Input.GetKeyDown("e") && d < 3)
        {
            this.rb.useGravity = false;
            this.transform.position = theDest.position;
            this.transform.parent = GameObject.Find("Destination").transform;
            this.transform.eulerAngles = theDest.transform.eulerAngles;

            //new
            this.aud.PlayOneShot(this.holdSE);

        }
        if (Input.GetKeyDown("r"))
        {
            this.rb.useGravity = true;
            this.transform.parent = null;
        }


        if (Input.GetMouseButtonDown(0) && this.transform.position.y >= 5.5)
        {
            this.transform.parent = null;
            this.rb.useGravity = true;
            this.rb.AddForce(transform.forward * speed);

            //new
            this.aud.PlayOneShot(this.throwSE);
        }

        if (this.transform.position.y < 0)
        {
            Destroy(gameObject);

        }
    }


}
