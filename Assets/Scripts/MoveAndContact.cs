/*
 *                   江城子 . 程序员之歌
 * 
 *               十年生死两茫茫，写程序，到天亮。
 *                   千行代码，Bug何处藏。
 *               纵使上线又怎样，朝令改，夕断肠。
 * 
 *               领导每天新想法，天天改，日日忙。
 *                   相顾无言，惟有泪千行。
 *               每晚灯火阑珊处，夜难寐，加班狂。
 * 
 */

using UnityEngine;

public class MoveAndContact : MonoBehaviour
{
    public Rigidbody2D rb;

    //定义针发射速度
    public float speed = 50;
    public bool moveOnOff = true;

    private GameController _gameController;
    
    private void Start()
    {        
        rb = GetComponent<Rigidbody2D>();
        var gameControllerObject = GameObject.FindWithTag("MainCamera");
        if (gameControllerObject != null)
        {
            _gameController = gameControllerObject.GetComponent<GameController>();
        }
        else
        {
            print("Can't find 'GameController' script.");
        }
        
    }

    private void Update()
         {
             if (moveOnOff)
             {
                 rb.MovePosition(rb.position + Vector2.up * (speed * Time.deltaTime));
             }
         }
    
    private void OnTriggerEnter2D(Collider2D other)
     {
     print("OnTriggerEnter调用");
         if (other.CompareTag("Target"))
     {
         Debug.Log("if语句调用");
         moveOnOff = false;
         transform.SetParent(other.gameObject.transform);
         _gameController.GetNumber(); //调用GetNumber()方法，减少目标数量
     }
     
     //结束游戏
     if (other.CompareTag("Player"))
     {
         print("OverGame执行");
         moveOnOff = false; 
         _gameController.OverGame();
     }
     }
}

