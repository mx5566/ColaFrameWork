using UnityEngine;
using System.Collections;
 
public class zhuan : MonoBehaviour
{
 
    public Transform sun;
 
    public float r; //半径
    public float w; //角度
    public float speed;
 
    public float x;
    public float y;
 
    void Awake()
    {
        transform.position = new Vector3(10 * Random.value, 10 * Random.value, 0); //重置做圆周的开始位置
 
        GameObject sun = GameObject.FindGameObjectWithTag("sun"); //取得圆点 我用一个sphere 表示
        r = Vector3.Distance(transform.position, sun.transform.position); //两个物品间的距离
        w = 0.3f; // ---角速度
        speed = 1 * Random.value; // 这个应该所角速度了
 
 
    }
 
    // Use this for initialization
    void Start()
    {
 
    }
 
    // Update is called once per frame
    void Update()
    {
        //下面的概念有点模糊了
        w += speed * Time.deltaTime; // 

        x = Mathf.Cos(w) * r;
        y = Mathf.Sin(w) * r;

        transform.position = new Vector3(x, y, transform.position.z);
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