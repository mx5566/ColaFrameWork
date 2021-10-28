using UnityEngine;
using System.Collections;

namespace Bullet 
{
    /// <summary>
    /// 子弹横向左右移动
    /// </summary>
    public class BulletHorizontal : MonoBehaviour
    {
        public bool isLeft;                 //是否向左移动
        public float moveLocation;          //移动的位置
        public float speed;                 //移动速度
        private Vector3 leftPosition;       //左边的目标点
        private Vector3 rightPosition;      //右边的目标点

        void Awake()
        {
            leftPosition = new Vector3(transform.localPosition.x - moveLocation, transform.localPosition.y, transform.localPosition.z);
            rightPosition = new Vector3(transform.localPosition.x + moveLocation, transform.localPosition.y, transform.localPosition.z);
        }

        void Update()
        {

            if (isLeft)
            {
                transform.localPosition += (Vector3.right * speed * Time.deltaTime);
                //transform.Translate(Vector3.right * speed * Time.deltaTime);

                if (transform.localPosition.x >= rightPosition.x)
                {
                    isLeft = !isLeft;
                }
            }
            else
            {
                transform.localPosition += (Vector3.left * speed * Time.deltaTime);
                //transform.Translate(Vector3.left * speed * Time.deltaTime);

                if (transform.localPosition.x <= leftPosition.x)
                {
                    isLeft = !isLeft;
                }
            }
        }
    }
}