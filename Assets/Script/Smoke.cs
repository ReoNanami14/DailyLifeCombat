using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smoke : MonoBehaviour
{
    public GameObject smokePrehab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("player2"))
        {
            GameObject Smoke = Instantiate(smokePrehab, collision.transform.position, Quaternion.identity);

            Smoke.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        }
    }
}
