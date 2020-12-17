/*
 * 
 *    ┏┓　　　┏┓
 *  ┏┛┻━━━┛┻┓
 *  ┃　　　　　　　┃
 *  ┃　　　━　　　┃
 *  ┃　＞　　　＜　┃
 *  ┃　　　　　　　┃
 *  ┃...　⌒　...　┃
 *  ┃　　　　　　　┃
 *  ┗━┓　　　┏━┛
 *      ┃　　　┃　
 *      ┃　　　┃
 *      ┃　　　┃
 *      ┃　　　┃  神兽保佑
 *      ┃　　　┃  代码无bug　　
 *      ┃　　　┃
 *      ┃　　　┗━━━┓
 *      ┃　　　　　　　┣┓
 *      ┃　　　　　　　┏┛
 *      ┗┓┓┏━┳┓┏┛
 *        ┃┫┫　┃┫┫
 *        ┗┻┛　┗┻┛
 */

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject fire;    //针
    public GameObject firePoint;    //发射位置
    public float fireRate = 0.4f; //    发射间隔
    public Text gameLv;//关卡级

    public Text gameTime;//游戏时间

    public Text gameOver;//游戏结果

    public Text gameTarget;//游戏目标
    public Text gameReset;
    public Text gameScore;  //游戏计分UI
    public Text overGameScore;  //游戏结束输出分数

    public int gaLv;    //游戏关卡
    public float gaTime;    //游戏时间
    public int terNumber;    //游戏目标
    public int score;   //游戏计分
    
    public bool overOnOff;    //判断游戏是否为结束状态
    private float _nextFire;    //下一次发射时间
    private double _time = 3;   //游戏结束后倒计时三秒
    
    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        var color = new Color(49, 7, 21, 0);
       
            gaLv = 1;
            gaTime = 60;
            terNumber = 5;
            score = 0;
            GameObject[] games = GameObject.FindGameObjectsWithTag("Player");
            foreach (var item in games)
            {
                Destroy(item);
            }
        
        overOnOff = false;
        gameLv.text = "第" + gaLv + "关";
        gameTime.text = "时间：" + gaTime + "s";
        gameTarget.text = terNumber.ToString();
        gameOver.gameObject.SetActive(false);
        overGameScore.gameObject.SetActive(false);
        GetComponent<Camera>().backgroundColor = color;

    }

    private IEnumerator GameLvUp()
    {
        yield return new WaitForSeconds(1); //协同程序，等待1S执行，避免直接升级关卡，玩家无时间反应
        print("GameLvUp被执行");
        GameObject[] games = GameObject.FindGameObjectsWithTag("Player");
        
            if (Random.Range(1,3) % 2 == 0)
            {
                print("删除游戏对象");
                foreach (var item in games)
                {
                    Destroy(item);
                }
            }

        gaLv += 1;
        gaTime += 20;
        terNumber += Random.Range(5, 9);
        gameLv.text = "第" + gaLv + "关";
        gameTime.text = "时间：" + gaTime + "s";
        gameTarget.text = terNumber.ToString();
    }

    public void OverGame()
    {
        overOnOff = true;
        GetComponent<Camera>().backgroundColor = Color.red;
        gameOver.gameObject.SetActive(true);
        overGameScore.gameObject.SetActive(true);
        overGameScore.text = "你的得分为：" + score + "分";
    }

    internal void GetNumber()
    {
        print("GetNumber执行");
        score++;
        terNumber --;
        gameTarget.text = terNumber.ToString();
        if (terNumber <= 0)
        {
            StartCoroutine(GameLvUp());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!overOnOff)
        {
            if (Input.GetButton("Fire1") && Time.time > _nextFire)
            {
                if (terNumber > 0)  /*避免terNumber由于协同程序出现负数的情况, 艹，还是会出现负数
                在最后一根针还没有碰撞到球的时候，terNumber仍然为1，所以TMD仍然会触发这个if语句，
                并且，TMD由于terNumber为负数，甚至造成下一关卡游戏目标出现异常？？？F**K
                增加针发射间隔时间解决了这个bug，为了避免bug，只能牺牲一点游戏体验了，舍不得孩子套不住狼，舍不得老婆逮不住流氓*/
                {
                    Fire();
                }
            }

            gaTime -= Time.deltaTime;   //  游戏时间减少
            if (gaTime <= 0)    //如果倒计时归零，结束游戏
            {
                OverGame();
            }
            gameTime.text = "时间：" + gaTime.ToString("0.0") + "s";
            gameScore.text = "得分：" + score;
        }
        else
        {
            //结束游戏后倒计时3S再显示游戏结束UI，避免直接重新加载游戏导致游戏结束UI来不及显示的问题
            _time = _time - Time.deltaTime;
            if (_time <= 1)
            {
                gameReset.gameObject.SetActive(true);
                if (Input.GetButton("Fire1"))
                {
                    _time = 3;
                    
                    gameReset.gameObject.SetActive(false);
                    ResetGame();
                }
            }
            
        }
    }

    private void Fire()
    {
        _nextFire = Time.time + fireRate;
        Instantiate(fire, 
                    firePoint.transform.position, 
                    Quaternion.identity);
    }
}