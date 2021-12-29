using UnityEngine;
using System.Collections;


// https://www.cnblogs.com/plateFace/p/4301720.html
namespace Bullet 
{
    /// <summary>
    /// 子弹旋转
    /// </summary>
    public class BulletRotate : MonoBehaviour
    {
        public RotationDirection rotationDirection;
        public float speed;
        public Transform rotateNode;

        public void Update()
        {
            if (rotateNode != null)
            {
                //围绕某一节点旋转
                switch (rotationDirection)
                {
                    case RotationDirection.Left:
                        this.transform.RotateAround(rotateNode.transform.position, Vector3.forward, speed);
                        break;
                    case RotationDirection.Right:
                        this.transform.RotateAround(rotateNode.transform.position, Vector3.forward, -(speed));
                        break;
                }
            }
            else
            {
                //自身旋转
                switch (rotationDirection)
                {
                    case RotationDirection.Left:
                        this.transform.Rotate(Vector3.forward, speed);
                        break;
                    case RotationDirection.Right:
                        this.transform.Rotate(Vector3.forward, -(speed));
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 旋转的方向
    /// </summary>
    public enum RotationDirection
    {
        Left,
        None,
        Right
    }
}