using UnityEngine;
using System.Collections;
using BearGame;

namespace Bullet
{
    /// <summary>
    /// 子弹前后移动
    /// </summary>
    public class BulletVertical : MonoBehaviour
    {
        public float speed = 0;
        public BulletDirectionEnum direction;
        private Vector3 dir;

        public delegate void BulletOutScreen(GameObject gameObject);
        /// <summary>
        /// 子弹越屏之后,触发的事件,用于销毁子弹
        /// </summary>
        /// <param name="gameObject"></param>
        public static event BulletOutScreen OnBulletDestroy;


        void Start()
        {
            switch (direction)
            {
                case BulletDirectionEnum.Up:
                    dir = Vector3.up;
                    break;
                case BulletDirectionEnum.Bottom:
                    dir = -Vector3.up;
                    break;
                case BulletDirectionEnum.Left:
                    dir = Vector3.left;
                    break;
                case BulletDirectionEnum.Right:
                    dir = Vector3.right;
                    break;
            }
        }

        void Update()
        {                    transform.Translate(dir * speed * Time.deltaTime);
                if (transform.localPosition.y > Screen.height)
                {
                    transform.gameObject.SetActive(false);
                    //调用子弹出屏幕事件                   }             }
    }

    /// <summary>
    /// 子弹移动的方向
    /// </summary>
    public enum BulletDirectionEnum
    {
        Up,
        Bottom,
        Left,
        Right,
        None
    }
}