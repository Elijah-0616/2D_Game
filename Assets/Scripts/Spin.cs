using UnityEngine;

public class Spin : MonoBehaviour
{
   

    // Update is called once per frame
    private void Update()
    {
        //物体自转代码
        transform.Rotate (Vector3.forward,45*Time.deltaTime,Space.Self);    
    }
}
