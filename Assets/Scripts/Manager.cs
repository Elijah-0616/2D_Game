using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Manager : MonoBehaviour
{

    public GameObject qidian;

    public GameObject feizhen;

    public Text gameLv;//关卡级

    public Text gameTime;//游戏时间

    public Text gameOver;//游戏结果

    public Text gameTarget;//游戏目标

    public int ga_Lv;
    public float ga_time;
    public int ter_Number;


    public bool over_OnOff=false;

    // Start is called before the first frame update
    void Start()
    {
        RestGame();
    }

    private void RestGame()
    {
        ga_Lv = 1;
        ga_time = 60;
        ter_Number = 5;
        gameLv.text = "第" + ga_Lv + "关";
        gameTime.text= "时间：" + ga_time + "s";
        gameTarget.text = ter_Number.ToString();
        gameOver.gameObject.SetActive(false);
        GameObject[] games = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in games)
        {
            Destroy(item);
        }
    }

    public void GameLvUp()
    {
        GameObject[] games = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in games)
        {
            Destroy(item);
        }
        ga_Lv += 1;
        ga_time += 60;
        ter_Number += Random.Range(0,9);
        gameLv.text = "第" + ga_Lv + "关";
        gameTime.text = "时间：" + ga_time + "s";
        gameTarget.text = ter_Number.ToString();
    }

    public void OverGame()
    {
        over_OnOff = true;
        GetComponent<Camera>().backgroundColor = Color.red;
        gameOver.gameObject.SetActive(true);
    }

    internal void GetNumber()
    {
        ter_Number--;
        gameTarget.text = ter_Number.ToString();
        if (ter_Number==0)
        {
            GameLvUp();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            Instantiate(feizhen,qidian.transform.position,Quaternion.identity);
        }

        ga_time -= Time.deltaTime;
    
        gameTime.text = "时间：" + ga_time.ToString("0.0") + "s";

    }
}
