using UnityEngine;

public class Spin : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 100;
    private GameController _gameController;
    
    private void Start()
    {
        var gameControllerObject = GameObject.FindWithTag("MainCamera");
        _gameController = gameControllerObject.GetComponent<GameController>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    private void Update()
    {
        //如果游戏状态为结束则不再自转
        if (! _gameController.overOnOff)
        {
            //球体自转
            transform.Rotate (0,
                0, 
                speed * Time.deltaTime);
        }    
    }
}