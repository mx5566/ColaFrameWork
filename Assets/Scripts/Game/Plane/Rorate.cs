using UnityEngine;
using System.Collections;

public class Rorate : MonoBehaviour
{
    public Transform sun;

    public float r; //半径
    public float w; // 初始角度
    public float speed;

    public float X;
    public float Y;

    void Awake()
    {
        // transform.position = new Vector3(10 * Random.value, 10 * Random.value, 0); //重置做圆周的开始位置

        // GameObject sun = GameObject.FindGameObjectWithTag("sun"); //取得圆点 我用一个sphere 表示
        // r = Vector3.Distance(transform.position, sun.transform.position); //两个物品间的距离
        // w = 0.3f; // ---角速度
        // speed = 1 * Random.value; // 这个应该所角速度了

        // X = sun.transform.position.x;
        // Y = sun.transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
        //下面的概念有点模糊了
        w += speed * Time.deltaTime; // 

        float x = Mathf.Cos(w) * r;
        float y = Mathf.Sin(w) * r;

        transform.position = new Vector3(X + x, transform.position.y, Y + y);
    }
}

/*
 var a : int;
 var b : int;
 var x: int;

 var y : int;
 var alpha : int;
 var X : int;
 var Y : int;

 function Update () {
     alpha += 10;
     X = x + (a * Mathf.Cos(alpha*.005));
     Y= y + (b * Mathf.Sin(alpha*.005));
     this.gameObject.transform.position = Vector3(X,0,Y);
 }
 
 */