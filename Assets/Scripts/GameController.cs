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

using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject fire;    //针
    public GameObject firePoint;    //发射位置
    public float fireRate = 0.3f; //    发射间隔
    public Text gameLv;//关卡级

    public Text gameTime;//游戏时间

    public Text gameOver;//游戏结果

    public Text gameTarget;//游戏目标
    public Text gameReset;
    public Text gameScore;  //游戏计分UI

    public int gaLv;    //游戏关卡
    public float gaTime;    //游戏时间
    public int terNumber;    //游戏目标
    public int score;   //游戏计分
    
    public bool overOnOff;    //判断游戏是否为结束状态
    private float _nextFire;    //下一次发射时间
    private double _time = 3;



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
        GetComponent<Camera>().backgroundColor = color;

    }

    private void GameLvUp()
    {
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
        gaTime += 60;
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
    }

    internal void GetNumber()
    {
        print("GetNumber执行");
        score++;
        terNumber --;
        gameTarget.text = terNumber.ToString();
        if (terNumber <= 0)
        {
            GameLvUp();
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (!overOnOff)
        {
            if (Input.GetButton("Fire1") && Time.time > _nextFire)
            {
                Fire();
            }

            gaTime -= Time.deltaTime;
            gameTime.text = "时间：" + gaTime.ToString("0.0") + "s";
            gameScore.text = "得分：" + score;
        }
        else
        {
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
