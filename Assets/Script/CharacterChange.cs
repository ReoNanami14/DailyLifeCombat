using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterChange : MonoBehaviour
{
    public GameObject brother;
    public GameObject sister;
    public GameObject cam;
    public GameObject cam_brother;
    public GameObject cam_sister;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("otouto"))
        {
            brother.transform.position = new Vector3(-2.78f, 5.47f, -3.21f);
            brother.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            cam.gameObject.SetActive(false);
            cam_brother.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }

        if (other.gameObject.CompareTag("onechan"))
        {
            sister.transform.position = new Vector3(-2.78f, 5.47f, -6.28f);
            sister.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
            cam.gameObject.SetActive(false);
            cam_sister.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
    }


}


