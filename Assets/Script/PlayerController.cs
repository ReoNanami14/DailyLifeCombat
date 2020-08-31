using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] Transform cam;
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpForce = 300;

    private Animator animator;
    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";
    private const string key_isRunR = "isRunR";
    private const string key_isRunL = "isRunL";

    Rigidbody rb;
    CapsuleCollider caps;
    AudioSource aud;
    GameObject theDest;

    public Transform Dest;
    public AudioClip jumpSE;
    public AudioClip holdSE;

    public float distance = 10f;
    public Transform equipPosition;
    GameObject currentItem;

    bool canGrab;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();
        theDest = GameObject.Find("Destination");

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        caps = GetComponent<CapsuleCollider>();
        caps.center = new Vector3(0, 0.76f, 0);
        caps.radius = 0.23f;
        caps.height = 1.5f;

        this.aud = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        float z = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        if (Input.GetKey("w")||Input.GetKey("s"))
        {
            this.animator.SetBool(key_isRun, true);
        }
        if (Input.GetKeyUp("w") || Input.GetKeyUp("s"))
        {
            this.animator.SetBool(key_isRun, false);
        }
        if (Input.GetKey("d") )
        {
            this.animator.SetBool(key_isRunR, true);
        }
        if (Input.GetKeyUp("d") )
        {
            this.animator.SetBool(key_isRun, false);
        }
        if (Input.GetKey("a"))
        {
            this.animator.SetBool(key_isRunL, true);
        }
        if (Input.GetKeyUp("a"))
        {
            this.animator.SetBool(key_isRunL, false);
        }



        if (Input.GetKeyDown(KeyCode.Space)&&isJumping==false)
        {
            this.rb.AddForce(transform.up*jumpForce);
            this.animator.SetTrigger("isJump");

            this.aud.PlayOneShot(this.jumpSE);
            isJumping = true;
        }

        //前移動のときだけ方向転換させる
        if (z> 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, cam.eulerAngles.y, transform.rotation.z));
        }
       
        transform.position += transform.forward * z + transform.right * x;

        CheckGrab();      

        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Invoke("PickUp",3);
                this.aud.PlayOneShot(this.holdSE);
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.tag == "item")
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().YouWin();
        }

        if (other.gameObject.tag == "Stage")
        {
            isJumping = false;
        }
    }

    private void CheckGrab()
    {
        Ray ray = new Ray(transform.position+new Vector3(0,0.15f,0) , transform.forward);

        RaycastHit hit;

        if(Physics.Raycast(ray,out hit, distance))
        {
            if (hit.transform.tag == "item")
            {
                currentItem = hit.transform.gameObject;
                canGrab = true;
            }
           
        }
        else
            canGrab = false;
        //Raycastの可視化
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
    }

    //実際にアイテムを持つ
    private void PickUp()
    {
        currentItem.transform.position = equipPosition.position;
        currentItem.transform.parent = equipPosition;
        currentItem.transform.localEulerAngles = equipPosition.transform.localEulerAngles;
        currentItem.GetComponent<Rigidbody>().isKinematic = true;
    }

}
