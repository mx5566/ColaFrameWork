using UnityEngine;

public class RorateWave : MonoBehaviour
{

    public Transform sun;

    public float r; //半径
    public float w; //角度
    public float speed;

    public float X;
    public float Y;

    int num = 5;
    void Awake()
    {
        transform.position = new Vector3(10 * Random.value, 10 * Random.value, 0); //重置做圆周的开始位置

        GameObject sun = GameObject.FindGameObjectWithTag("sun"); //取得圆点 我用一个sphere 表示
        r = Vector3.Distance(transform.position, sun.transform.position); //两个物品间的距离     
    }

    // Use this for initialization
    void Start()
    {
        float delta = 360.0f / num;
        for (int i = 0; i < num; i++)
        {
            Renderer render;
            GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);

            float t = w + Mathf.Deg2Rad * (i * delta + 90);
            float x = Mathf.Cos(t) * r;
            float y = Mathf.Sin(t) * r;

            // 需要记录他的起始位置的弧度大小
            // t 起始位置的弧度
            Rorate rorateComponent = go.GetComponent<Rorate>();
            rorateComponent.w = t;
            rorateComponent.r = r;

            // 设置位置
            go.transform.position = new Vector3(x, transform.position.y, y);

            render = go.GetComponent<Renderer>();
            render.material.color = Color.blue;
        }
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