using System;
using UnityEngine;

public class MoveAndContact : MonoBehaviour {
    //定义针发射速度
    private float speed = 20f;

    /*
     * 检测碰撞
     */
    private void OnTriggerEnter(Collider other) {

        if (other.name =="Sphere")
        {
            speed = 0;
        }
    }
    // Update is called once per frame
    private void Update() {
        if (Input.GetButton("Fire1")) {
            /*
             * 发射针，冻结X,Z轴速度
             */
            var movement = new Vector3(0.0f, speed, 0.0f);
            GetComponent<Rigidbody>().velocity = movement;
            Debug.Log("当前速度=" + GetComponent<Rigidbody>().velocity.y);
        }
    }
    
   

    
}
