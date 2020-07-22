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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();

        rb.constraints = RigidbodyConstraints.FreezeRotation;

        caps = GetComponent<CapsuleCollider>();
        caps.center = new Vector3(0, 0.76f, 0);
        caps.radius = 0.23f;
        caps.height = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("LXaxis") * Time.deltaTime * speed;
        float z = Input.GetAxisRaw("LYaxis") * Time.deltaTime * speed;

        //前移動のときだけ方向転換させる
        if (z > 0)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, cam.eulerAngles.y, transform.rotation.z));
        }

        transform.position += transform.forward * z + transform.right * x;
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "item")
        {
            GameObject director = GameObject.Find("GameDirector");
            director.GetComponent<GameDirector>().DecreaseHp2();
            director.GetComponent<GameDirector>().YouWin2();
        }
    }
}
