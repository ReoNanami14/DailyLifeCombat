using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public float distance; //Rayの距離
    public Transform equipPosition;
    GameObject currentItem;

    bool canGrab;
    bool isJumping = false;

    public float holdTime; //アイテムを持つまでの時間
    public float count;
    public Text countText;
    bool isCountdownStart;

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

            if (Input.GetKey("w") || Input.GetKey("s"))
            {
                this.animator.SetBool(key_isRun, true);
            }
            if (Input.GetKeyUp("w") || Input.GetKeyUp("s"))
            {
                this.animator.SetBool(key_isRun, false);
            }
            if (Input.GetKey("d"))
            {
                this.animator.SetBool(key_isRunR, true);
            }
            if (Input.GetKeyUp("d"))
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



            if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
            {
                this.rb.AddForce(transform.up * jumpForce);
                this.animator.SetTrigger("isJump");

                this.aud.PlayOneShot(this.jumpSE);
                isJumping = true;
            }

            //前移動のときだけ方向転換させる
            if (z > 0)
            {
                transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, cam.eulerAngles.y, transform.rotation.z));
            }

            transform.position += transform.forward * z + transform.right * x;

            CheckGrab();

        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Invoke("PickUp", holdTime);
                this.aud.PlayOneShot(this.holdSE);
                isCountdownStart = true;
                countText.gameObject.SetActive(true);
            }

        }

        if (IsInvoking("PickUp"))
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.Space))
            {
                CancelInvoke();
                countText.gameObject.SetActive(false);
                count = Time.deltaTime;
            }

        }

        if (isCountdownStart)
        {
            count-= Time.deltaTime;
            countText.text = count.ToString("f2");
        }

        if (count < 0)
        {
            countText.gameObject.SetActive(false);
            isCountdownStart = false;
        }

    }

    void OnCollisionEnter(Collision other)
    {
       
        if (other.gameObject.CompareTag("item"))
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().YouWin();
        }

        if (other.gameObject.CompareTag("Stage"))
        {
            isJumping = false;
        }
    }

    private void CheckGrab()
    {
        Ray ray = new Ray(transform.position+new Vector3(0,0.15f,0) , transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, distance))
        {
            if (hit.transform.CompareTag("item"))
            {
               
                    currentItem = hit.transform.gameObject;
                    canGrab = true;
                    holdTime = hit.collider.gameObject.GetComponent<pickUp>().HTime;

                if (!isCountdownStart)
                {
                    count = hit.collider.gameObject.GetComponent<pickUp>().CountTime;
                }
            }

        }
        else
        {
            canGrab = false;
 
        }           

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

    private void Countdown()
    {
        holdTime -= Time.deltaTime;
        countText.text =holdTime.ToString("f2");
        if (holdTime <= 0)
        {
            countText.gameObject.SetActive(false);
            isCountdownStart = false;
        }
    }
 
}
