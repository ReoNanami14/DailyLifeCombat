using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
public class OnlySister2 : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    [SerializeField] float jumpForce = 300;

    private Animator animator;

    private const string key_isRun = "isRun";
    private const string key_isJump = "isJump";
    //private const string key_isRunR = "isRunR";
    //private const string key_isRunL = "isRunL";
    private const string key_isWait = "isWait";
    //private const string key_isDamaged = "isDamaged";

    [SerializeField] Transform cam;

    Rigidbody rb;

    GameObject theDest;

    AudioSource aud;
    public AudioClip jumpSE;
    public AudioClip holdSE;
    public AudioClip healSE;

    public float distance = 10f;
    public Transform equipPosition;
    GameObject currentItem;
    bool canGrab;
    bool isJumping = false;

    public float holdTime;
    public float count;
    public Text countText;
    bool isCountdownStart;

    //スキルによる回復
    GameObject hpGage;
    GameObject hpGage2;
    float coolTime = 0.0f;
    bool isHeal = false;
    public Image healIcon;
    public Image unHeal;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();
        theDest = GameObject.Find("Destination");

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        this.aud = GetComponent<AudioSource>();

        this.hpGage2 = GameObject.Find("hpgage2");

    }

    // Update is called once per frame
    void Update()
    {

        float x = Input.GetAxisRaw("Horizontal2") * Time.deltaTime * speed;
        float z = Input.GetAxisRaw("Vertical2") * Time.deltaTime * speed;

        if (Input.GetAxis("Horizontal2") == -1 || Input.GetAxis("Horizontal2") == 1 || Input.GetAxis("Vertical2") == -1 || Input.GetAxis("Vertical2") == 1)
        {
            this.animator.SetBool(key_isRun, true);
            this.animator.SetBool(key_isWait, false);
        }
        else
        {
            this.animator.SetBool(key_isWait, true);
        }


        //前移動のときだけ方向転換させる
        if (z > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, cam.eulerAngles.y, transform.rotation.z));
        }


        transform.position += transform.forward * z + transform.right * x;


        if (Input.GetKeyDown(KeyCode.Joystick1Button1) && isJumping == false)
        {
            this.rb.AddForce(transform.up * jumpForce);
            this.animator.SetBool(key_isJump, true);
            this.aud.PlayOneShot(this.jumpSE);

            isJumping = true;
        }
        else
        {
            this.animator.SetBool(key_isJump, false);
        }

        GameObject director = GameObject.Find("GameDirector");
        director.GetComponent<GameDirector>().TimeOver();

        CheckGrab();

        if (canGrab)
        {
            if (Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                Invoke("PickUp", holdTime);
                isCountdownStart = true;
                countText.gameObject.SetActive(true);
                this.aud.PlayOneShot(this.holdSE);
            }
        }

        if (IsInvoking("PickUp"))
        {
            if (Input.GetAxisRaw("Horizontal2") == -1 || Input.GetAxisRaw("Horizontal2") == 1 || Input.GetAxisRaw("Vertical2") == -1 || Input.GetAxisRaw("Vertical2") == 1 || Input.GetKeyDown(KeyCode.Joystick1Button1))
            {
                CancelInvoke();
                countText.gameObject.SetActive(false);
                count = Time.deltaTime;
            }

        }

        if (isCountdownStart)
        {
            count -= Time.deltaTime;
            countText.text = count.ToString("f2");
        }

        if (count < 0)
        {
            countText.gameObject.SetActive(false);
            isCountdownStart = false;
        }

        if (Input.GetKeyDown(KeyCode.Joystick2Button15) && !isHeal)
        {
            this.hpGage2.GetComponent<Image>().fillAmount += 0.3f;
            isHeal = true;
            healIcon.gameObject.SetActive(false);
            unHeal.gameObject.SetActive(true);
            this.aud.PlayOneShot(this.healSE);//回復音
        }

        if (isHeal)
        {
            coolTime += Time.deltaTime;

            if (coolTime >= 7.0)
            {
                isHeal = false;
                coolTime = 0.0f;

                unHeal.gameObject.SetActive(false);
                healIcon.gameObject.SetActive(true);
            }
        }

    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("item"))
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().WinLose2();
            //this.animator.SetBool(key_isDamaged, true);
        }
        else
        {
            //this.animator.SetBool(key_isDamaged, false);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Stage"))
        {
            isJumping = false;
        }
    }

    private void CheckGrab()
    {
        Ray ray = new Ray(transform.position + new Vector3(0, 0.13f, 0), transform.forward);
        Ray ray2 = new Ray(transform.position + new Vector3(0, 1.0f, 0), transform.forward);

        RaycastHit hit;
        RaycastHit hit2;

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
        else if (Physics.Raycast(ray2, out hit2, distance))
        {
            if (hit2.transform.CompareTag("item"))
            {
                currentItem = hit2.transform.gameObject;
                canGrab = true;
                holdTime = hit2.collider.gameObject.GetComponent<pickUp>().HTime;

                if (!isCountdownStart)
                {
                    count = hit2.collider.gameObject.GetComponent<pickUp>().CountTime;
                }
            }

        }
        else
            canGrab = false;

        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
        Debug.DrawRay(ray2.origin, ray2.direction * distance, Color.red);
    }

    private void PickUp()
    {
        currentItem.transform.position = equipPosition.position;
        currentItem.transform.parent = equipPosition;
        currentItem.transform.localEulerAngles = equipPosition.transform.localEulerAngles;
        currentItem.GetComponent<Rigidbody>().isKinematic = true;
    }


}