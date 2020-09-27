using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GameDirector : MonoBehaviour
{
    [SerializeField] float gameTime = 20.0f;
    public Text uiText;
    public Text startCount;
    public GameObject pauseUI;
    public PlayerController PC;
    public player2 P2;
    float currentTime;

    GameObject win;
    GameObject win2;
    GameObject lose;
    GameObject lose2;
    GameObject hpGage;
    GameObject hpGage2;
    GameObject finish;


    // Start is called before the first frame update
    void Start()
    {
        this.hpGage = GameObject.Find("hpgage");
        this.hpGage2 = GameObject.Find("hpgage2");
        this.win = GameObject.Find("win");
        this.win2 = GameObject.Find("win2");
        this.lose = GameObject.Find("lose");
        this.lose2 = GameObject.Find("lose2");
        this.finish = GameObject.Find("finish");

        currentTime = gameTime;

        //スタートと同時に5秒のカウントダウン開始
        StartCoroutine(CountdownCoroutine());

    }

    // Update is called once per frame
    void Update()
    {
       StartCoroutine(StartCoroutine());

        if (currentTime <= 0.0f)
        {
            currentTime = 0.0f;
            Time.timeScale = 0;
        }
        int minutes = Mathf.FloorToInt(currentTime / 60F);
        int seconds = Mathf.FloorToInt(currentTime - minutes * 60);
        uiText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseUI.SetActive(!pauseUI.activeInHierarchy);

            if (pauseUI.activeInHierarchy)
            {
                Time.timeScale = 0f;
            }
            else
                Time.timeScale = 1f;
        }
    }

    public void TimeOver()
    {
        if (currentTime <= 0.0f)
        {
            if (hpGage.GetComponent<Image>().fillAmount > hpGage2.GetComponent<Image>().fillAmount)
            {
                this.win.GetComponent<Text>().text = "You Win!!";
                this.lose.GetComponent<Text>().text = "You Lose!!";
            }
            if (hpGage.GetComponent<Image>().fillAmount < hpGage2.GetComponent<Image>().fillAmount)
            {
                this.win2.GetComponent<Text>().text = "You Win!!";
                this.lose2.GetComponent<Text>().text = "You Lose!!";
            }
            if(hpGage.GetComponent<Image>().fillAmount == hpGage2.GetComponent<Image>().fillAmount)
            {
                this.finish.GetComponent<Text>().text = "DRAW!!";
                uiText.gameObject.SetActive(false);
            }
        }
    }

    public void WinLose()
    {
        if (this.hpGage.GetComponent<Image>().fillAmount <= 0.0f)
        {
            this.win2.GetComponent<Text>().text = "You Win!!";
            this.lose2.GetComponent<Text>().text = "You Lose!!";
            this.finish.GetComponent<Text>().text = "Finish!!";
            uiText.gameObject.SetActive(false);
            Time.timeScale = 0;
        }        
    }

    public void WinLose2()
    {
        if (this.hpGage2.GetComponent<Image>().fillAmount <= 0)
        {
            this.win.GetComponent<Text>().text = "You Win!!";
            this.lose.GetComponent<Text>().text = "You Lose!!";
            this.finish.GetComponent<Text>().text = "FINISH!!";
            uiText.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }

    public void SisterSkill()
    {

    }

    IEnumerator CountdownCoroutine()
    {
        startCount.gameObject.SetActive(true);
        PC.enabled = false;
        P2.enabled = false;

        startCount.text = "5";
        yield return new WaitForSeconds(1.0f);

        startCount.text = "4";
        yield return new WaitForSeconds(1.0f);

        startCount.text = "3";
        yield return new WaitForSeconds(1.0f);

        startCount.text = "2";
        yield return new WaitForSeconds(1.0f);

        startCount.text = "1";
        yield return new WaitForSeconds(1.0f);

        startCount.text = "START!!";
        yield return new WaitForSeconds(1.0f);

        startCount.gameObject.SetActive(false);
        PC.enabled = true;
        P2.enabled = true;
    }

    IEnumerator StartCoroutine()
    {
        yield return new WaitForSeconds(6.0f);
        currentTime -= Time.deltaTime;

    }
}
