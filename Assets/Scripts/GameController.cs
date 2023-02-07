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
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour 
{
    public GameObject fire; //发射出去的对象
    public GameObject sphere; //目标对象
    public GameObject firePoint; //发射位置
    public float fireRate = 0.4f; //    发射间隔
    // 以下变量为游戏UI文本对象
    public Text gameLv; //关卡级

    public Text gameTime; //游戏时间

    public Text gameOver; //游戏结果

    public Text gameTarget; //游戏目标
    public Text gameReset;
    public Text gameScore; //游戏计分UI
    public Text overGameScore; //游戏结束输出分数

    public int gaLv; //游戏关卡
    public float gaTime; //游戏剩余时间
    public int terNumber; //游戏目标
    public int score; //游戏计分

    public bool overOnOff; //判断游戏是否为结束状态，true为结束，false为未结束
    private float _nextFire; //下一次发射时间
    private double _time = 3; //游戏结束后倒计时三秒
    private int _childCount;    //  子对象数量

    
    // Use this for initialization 此函数在游戏场景初始化时调用
    private void Start()
    {
        ResetGame();
    }

    //重置游戏场景
    private void ResetGame()
    {
        print("重置游戏");
        var color = new Color(49, 7, 21, 0);    //  设置背景颜色

        gaLv = 1;
        gaTime = 60;
        terNumber = 5;
        score = 0;
        GameObject[] games = GameObject.FindGameObjectsWithTag("Player");
        
        //遍历场景中标签为Player的对象，并销毁
        foreach (var item in games)
        {
            Debug.Log("删除Player");
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

    /*
     * 游戏关卡升级
     * 1.关卡级别+1
     * 2.游戏时间+20s
     * 3.游戏目标随机生成，范围为5-9，该算法待优化
     */
    private IEnumerator GameLvUp()
    {
        yield return new WaitForSeconds(1); //协同程序，等待1S执行，避免直接升级关卡，玩家无时间反应
        print("GameLvUp");
        GameObject[] games = GameObject.FindGameObjectsWithTag("Player");
        _childCount = sphere.transform.childCount; //  获取目标对象的子物体数量
        
        /*
         * TODO：此处算法待优化，如果子物体对象数量不少于6，则不删除现有对象
         * 修改为随机且子对象数量不少于6，删除子对象
         */
        
        /*if (_childCount >= 6)
        {
            // print("删除游戏对象");
            foreach (var item in games)
            {
                Destroy(item);
            }
        }*/

        var delObj = Random.Range(0,2); // 随机生成0-1的数字，用于判断是否删除现有对象
        
        if (delObj == 1)    // 如果随机数为1，且子对象数量为6
        {
            if (_childCount >= 6)
            {
                foreach (var item in games)
                {
                    Destroy(item);
                }
            }
        }
        
        gaLv += 1;
        gaTime += 20;
        
        //TODO: 随机生成游戏目标，算法待优化
        if (_childCount >= 6)
        {
            terNumber += Random.Range(3, 6);
        }
        else
        {
            terNumber += Random.Range(5, 9);
        }
        
        //更新游戏数据面板
        gameLv.text = "第" + gaLv + "关";
        gameTime.text = "时间：" + gaTime + "s";
        gameTarget.text = terNumber.ToString();
        
        //增加旋转速度
        Spin.speed += 10;
        //增加针移动速度
        MoveAndContact.speed += 5;
    }

    /*
     * 游戏结束
     */
    public void OverGame()
    {
        print("游戏结束");
        overOnOff = true;
        GetComponent<Camera>().backgroundColor = Color.red;
        gameOver.gameObject.SetActive(true);
        overGameScore.gameObject.SetActive(true);
        overGameScore.text = "你的得分为：" + score + "分";
    }

    /*
     * 游戏核心逻辑
     */
    internal void GetNumber()
    {
        print("GetNumber执行");
        score++;
        terNumber--;
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
                /*
                避免terNumber由于协同程序出现负数的情况, 艹，还是会出现负数
                在最后一根针还没有碰撞到球的时候，terNumber仍然为1，所以TMD仍然会触发这个if语句，
                并且，TMD由于terNumber为负数，甚至造成下一关卡游戏目标出现异常？？？F**K
                */

                // TODO:增加针发射间隔时间临时解决了这个bug
                    
                if (terNumber > 0) 
                {
                    Fire();
                }
            }

            gaTime -= Time.deltaTime; //  游戏时间减少
            if (gaTime <= 0) //如果倒计时归零，结束游戏
            {
                OverGame();
            }

            //实时更新UI显示
            gameTime.text = "时间：" + gaTime.ToString("0.0") + "s";
            gameScore.text = "得分：" + score;
        }
        else
        {
            //结束游戏后倒计时3S再显示游戏结束UI，避免直接重新加载游戏导致游戏结束UI来不及显示的问题
            _time -= Time.deltaTime;
            if (!(_time <= 1)) return;
            gameReset.gameObject.SetActive(true);
            if (Input.GetButton("Fire1"))
            {
                _time = 3;
                gameReset.gameObject.SetActive(false);
                ResetGame();
            }
        }
    }

    /*
     * 发射针
     */
    private void Fire()
    {
        print("Fire执行");
        _nextFire = Time.time + fireRate;
        Instantiate(fire,
            firePoint.transform.position,
            Quaternion.identity);
    }
}