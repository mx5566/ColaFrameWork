using UnityEngine;
using System.Collections;

public class ProjectileTest : MonoBehaviour
{
    public GameObject target;
    public float speed = 10;
    private float distanceToTarget;
    private bool move = true;

    void Start()
    {
        //获取两点之间的距离
        distanceToTarget = Vector3.Distance(this.transform.position, target.transform.position);
        StartCoroutine(Shoot());
    }

    IEnumerator Shoot()
    {

        while (move)
        {
            Vector3 targetPos = target.transform.position;

            this.transform.LookAt(targetPos);

            
            float angle = Mathf.Min(1, Vector3.Distance(this.transform.position, targetPos) / distanceToTarget) * 45;
            this.transform.rotation = this.transform.rotation * Quaternion.Euler(Mathf.Clamp(-angle, -42, 42), 0, 0);

            //向目标点移动
            float currentDist = Vector3.Distance(this.transform.position, target.transform.position);
            this.transform.Translate(Vector3.forward * Mathf.Min(speed * Time.deltaTime, currentDist));
            
            //如果距离目标0.5就停止移动
            if (currentDist < 0.5f) 
            {
                move = false;
            }
            

            yield return null;
        }
    }
}
//  https://www.cnblogs.com/plateFace/p/4394095.html