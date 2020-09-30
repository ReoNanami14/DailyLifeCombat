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

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("otouto"))
        {
            brother.transform.position = new Vector3(-4.21f, 5.001f, -0.98f);
            brother.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            cam.gameObject.SetActive(false);
            cam_brother.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("onechan"))
        {
            sister.transform.position = new Vector3(4.22f, 5.07f, -1.06f);
            sister.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            cam.gameObject.SetActive(false);
            cam_sister.gameObject.SetActive(true);
            Destroy(other.gameObject);
            healIcon.gameObject.SetActive(true);
        }
    }


}


