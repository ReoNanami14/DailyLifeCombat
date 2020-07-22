using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour
{
    GameObject hpGage;
    GameObject hpGage2;
    GameObject win;

    // Start is called before the first frame update
    void Start()
    {
        this.hpGage = GameObject.Find("hpgage");
        this.hpGage2 = GameObject.Find("hpgage2");
        this.win = GameObject.Find("win");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DecreaseHp()
    {
        this.hpGage.GetComponent<Image>().fillAmount -= 0.1f;
    }
    public void DecreaseHp2()
    {
        this.hpGage2.GetComponent<Image>().fillAmount -= 0.1f;
    }

    public void YouWin()
    {
        if (this.hpGage.GetComponent<Image>().fillAmount == 0.0f)
        {
            this.win.GetComponent<Text>().text = "You Win!!";
        }
    }
}
