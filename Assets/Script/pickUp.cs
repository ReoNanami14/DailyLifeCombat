using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pickUp : MonoBehaviour
{
    //プレイヤー１に付くDestination
    public Transform theDest;
    public Transform theDest_brother;
    public Transform theDest_sister;

    //プレイヤー２に付くDestination
    public Transform theDest2;
    public Transform theDest2_brother;
    public Transform theDest2_sister;

    Rigidbody rb;
    public float speed;
    GameObject hpGage;
    GameObject hpGage2;
    public float damage;

    //SE
   // public AudioClip holdSE;
    public AudioClip throwSE;
    public AudioClip damageSE;
    public AudioClip damagevoice;
    public AudioClip throwSE2;
    public AudioClip damageSE2;
    public AudioClip damagevoice2;
    AudioSource aud;

    bool touch=false;
    bool touch2 = false;

    public float HTime; //アイテムを持つまでの時間
    public float CountTime;

    void Start()
    {
        this.hpGage = GameObject.Find("hpgage");
        this.hpGage2 = GameObject.Find("hpgage2");

        rb = GetComponent<Rigidbody>();

        //new
        this.aud = GetComponent<AudioSource>();

    }
    void Update()
    {

        if (Input.GetKeyDown("r")||Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            this.rb.useGravity = true;
            this.rb.isKinematic=false;
            this.transform.parent = null;
        }


        if (this.transform.position == theDest.position|| this.transform.position == theDest_brother.position|| this.transform.position == theDest_sister.position)
        {
            touch = true;
            if (Input.GetMouseButtonDown(0))
            {
                this.transform.parent = null;
                this.rb.useGravity = true;
                this.rb.isKinematic = false;
                this.rb.AddForce(transform.forward * speed);

                //投げるSE
                //this.aud.PlayOneShot(this.throwSE);
            }
        }

        if (this.transform.position == theDest2.position || this.transform.position == theDest2_brother.position || this.transform.position == theDest2_sister.position)
        {
            touch2 = true;
            if (Input.GetKeyDown(KeyCode.Joystick2Button15))
            {
                this.transform.parent = null;
                this.rb.useGravity = true;
                this.rb.isKinematic = false;
                this.rb.AddForce(transform.forward * speed);

                //投げるSE
               // this.aud.PlayOneShot(this.throwSE2);
            }
        }


        if (this.transform.position.y < 0)
        {
            Destroy(gameObject);
        }

       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (touch)
        {
            if (collision.gameObject.CompareTag("player2"))
            {
                this.hpGage2.GetComponent<Image>().fillAmount -= damage;
                touch = false;

                //this.aud.PlayOneShot(this.damageSE2);//ぶつかり音
               // this.aud.PlayOneShot(this.damagevoice2);
            }
        }

        if (touch2)
        {
            if (collision.gameObject.CompareTag("player1"))
            {
                this.hpGage.GetComponent<Image>().fillAmount -= damage;
                touch2 = false;

                //this.aud.PlayOneShot(this.damageSE);//ぶつかり音
                //this.aud.PlayOneShot(this.damagevoice);
            }
        }
    }


}
