using System.Collections;
using UnityEngine;

public class Changes : MonoBehaviour
{
    public Avatar[] avatars = new Avatar[2];
    public GameObject[] characters = new GameObject[2];
    private int index = 0;
    private Animator playerAnimator;


    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Change()
    {
       
            index++;

            if (index == 2) index = 0;

            foreach(GameObject gamObj in characters)
            {
                gamObj.SetActive(false);
            }

            characters[index].SetActive(true);
            playerAnimator.avatar = avatars[index];
            
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("SpItem"))
        {
            Change();
            Destroy(other.gameObject);
        }
    }
}
