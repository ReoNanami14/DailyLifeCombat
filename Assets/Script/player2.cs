using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class player2 : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpForce = 300;

    private Animator animator;

    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";
    private const string key_isRunR = "isRunR";
    private const string key_isRunL = "isRunL";

    [SerializeField] Transform cam;

    Rigidbody rb;

    CapsuleCollider caps;

    GameObject theDest;

    AudioSource aud;
    public AudioClip jumpSE;
    public AudioClip holdSE;

    public float distance = 10f;
    public Transform equipPosition;
    GameObject currentItem;
    bool canGrab;
    bool isJumping = false;

    public float holdTime;

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
        
            float x = Input.GetAxisRaw("Horizontal2") * Time.deltaTime * speed;
            float z = Input.GetAxisRaw("Vertical2") * Time.deltaTime * speed;


            //前移動のときだけ方向転換させる
            if (z > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, cam.eulerAngles.y, transform.rotation.z));
            }

        
            transform.position += transform.forward * z + transform.right * x;
       

        if (Input.GetKeyDown(KeyCode.Joystick2Button1)&&isJumping==false)
        {
            this.rb.AddForce(transform.up * jumpForce);
            this.animator.SetTrigger("isJump");
            this.aud.PlayOneShot(this.jumpSE);

            isJumping = true;
        }

        CheckGrab();

        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.Joystick2Button0))
            {
                Invoke("PickUp", holdTime);
                IsInvoking("PickUp");
                this.aud.PlayOneShot(this.holdSE);
            }
        }

        if (IsInvoking("PickUp"))
        {
            if (Input.GetAxisRaw("Horizontal2")==-1||Input.GetAxisRaw("Horizontal2")==1|| Input.GetAxisRaw("Vertical2") == -1 || Input.GetAxisRaw("Vertical2") == 1)
            {
                CancelInvoke();
            }

        }

    }

    void OnCollisionEnter(Collision other)
    {
            if (other.gameObject.tag == "item" )
            {
                GameObject director = GameObject.Find("GameDirector");
                director.GetComponent<GameDirector>().YouWin2();
            }

        if (other.gameObject.tag == "Stage")
        {
            isJumping = false;
        }
        
    }


    private void CheckGrab()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.15f, 0), transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.tag == "item")
            {
                holdTime = hit.collider.gameObject.GetComponent<pickUp>().HTime;
                currentItem = hit.transform.gameObject;
                canGrab = true;
            }

        }
        else
            canGrab = false;
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
    }

    private void PickUp()
    {
        currentItem.transform.position = equipPosition.position;
        currentItem.transform.parent = equipPosition;
        currentItem.transform.localEulerAngles = equipPosition.transform.localEulerAngles;
        currentItem.GetComponent<Rigidbody>().isKinematic = true;
    }


}
