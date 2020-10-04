using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CharacterChange : MonoBehaviour
{
    public GameObject brother;
    public GameObject sister;
    public GameObject cam;
    public GameObject cam_brother;
    public GameObject cam_sister;
    public Image healIcon;

    GameObject changeBrother;
    GameObject changeSister;

    private void Start()
    {
        changeBrother = GameObject.Find("ChangeToOtouto");
        changeSister = GameObject.Find("ChangeToOnechan");
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("otouto"))
        {
            brother.transform.position = new Vector3(changeBrother.transform.position.x, changeBrother.transform.position.y, changeBrother.transform.position.z);
            brother.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            cam.gameObject.SetActive(false);
            cam_brother.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("onechan"))
        {
            sister.transform.position = new Vector3(changeSister.transform.position.x, changeSister.transform.position.y, changeSister.transform.position.z);
            sister.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            cam.gameObject.SetActive(false);
            cam_sister.gameObject.SetActive(true);
            Destroy(other.gameObject);
            healIcon.gameObject.SetActive(true);
        }
    }


}


