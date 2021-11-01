using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


// https://my.oschina.net/kkkkkkkkkkkkk/blog/1511638
// https://blog.csdn.net/yu1368072332/article/details/82531891?utm_source=blogxgwz9
// https://blog.csdn.net/czhenya/article/details/77412300
/// <summary>
/// This script moves the ‘Enemy’ along the defined path.
/// </summary>
public class FollowThePath : MonoBehaviour {
        
    [HideInInspector] public Transform [] path; //path points which passes the 'Enemy' 
    [HideInInspector] public float speed; 
    [HideInInspector] public bool rotationByPath;   //whether 'Enemy' rotates in path direction or not
    [HideInInspector] public bool loop;         //if loop is true, 'Enemy' returns to the path starting point after completing the path
    float currentPathPercent;               //current percentage of completing the path
    Vector3[] pathPositions;                //path points in vector3
    [HideInInspector] public bool movingIsActive;   //whether 'Enemy' moves or not

    //setting path parameters for the 'Enemy' and sending the 'Enemy' to the path starting point
    public void SetPath() 
    {
        currentPathPercent = 0;
        pathPositions = new Vector3[path.Length];       //transform path points to vector3
        for (int i = 0; i < pathPositions.Length; i++)
        {
            pathPositions[i] = path[i].position;
        }
        transform.position = NewPositionByPath(pathPositions, 0); //sending the enemy to the path starting point
        if (!rotationByPath)
            transform.rotation = Quaternion.identity;
        movingIsActive = true;
    }

    private void Update()
    {
        if (movingIsActive)
        {
            currentPathPercent += speed / 100 * Time.deltaTime;     //every update calculating current path percentage according to the defined speed

            transform.position = NewPositionByPath(pathPositions, currentPathPercent); //moving the 'Enemy' to the path position, calculated in method NewPositionByPath
            if (rotationByPath)                            //rotating the 'Enemy' in path direction, if set 'rotationByPath'
            {
                transform.right = Interpolate(CreatePoints(pathPositions), currentPathPercent + 0.01f) - transform.position;
                transform.Rotate(Vector3.forward * 90);
            }
            if (currentPathPercent > 1)                    //when the path is complete
            {
                if (loop)                                   //when loop is set, moving to the path starting point; if not, destroying or deactivating the 'Enemy'
                    currentPathPercent = 0;
                else
                {
                    Destroy(gameObject);           
                }
            }
        }
    }

    Vector3 NewPositionByPath(Vector3 [] pathPos, float percent) 
    {
        return Interpolate(CreatePoints(pathPos), currentPathPercent);
    }

    Vector3 Interpolate(Vector3[] path, float t) 
    {
        int numSections = path.Length - Math.Min(path.Length, 3);
        int currPt = Mathf.Min(Mathf.FloorToInt(t * numSections), numSections - 1);
        float u = t * numSections - currPt;
        Vector3 a = path[currPt];
        Vector3 b = path[currPt + 1];
        Vector3 c = path[currPt + 2];
        Vector3 d = path[currPt + 3];
        return 0.5f * ((-a + 3f * b - 3f * c + d) * (u * u * u) + (2f * a - 5f * b + 4f * c - d) * (u * u) + (-a + c) * u + 2f * b);
    }

    Vector3[] CreatePoints(Vector3[] path) 
    {
        Vector3[] pathPositions;
        Vector3[] newPathPos;
        int dist = 2;
        pathPositions = path;
        newPathPos = new Vector3[pathPositions.Length + dist];
        Array.Copy(pathPositions, 0, newPathPos, 1, pathPositions.Length);
        newPathPos[0] = newPathPos[1] + (newPathPos[1] - newPathPos[2]);
        newPathPos[newPathPos.Length - 1] = newPathPos[newPathPos.Length - 2] + (newPathPos[newPathPos.Length - 2] - newPathPos[newPathPos.Length - 3]);
        if (newPathPos[1] == newPathPos[newPathPos.Length - 2])
        {
            Vector3[] LoopSpline = new Vector3[newPathPos.Length];
            Array.Copy(newPathPos, LoopSpline, newPathPos.Length);
            LoopSpline[0] = LoopSpline[LoopSpline.Length - 3];
            LoopSpline[LoopSpline.Length - 1] = LoopSpline[2];
            newPathPos = new Vector3[LoopSpline.Length];
            Array.Copy(LoopSpline, newPathPos, LoopSpline.Length);
        }
        return (newPathPos);
    }
	
	
	Vector3 NewPositionCircle()
	{
        int radius = 200;
		for (int i = 0; i < 361; i += 10)
        {
            float a = (float)(i * Mathf.PI / 180.0);
            float pos_y = radius * Mathf.Sin(a);
            float pos_x = radius * Mathf.Cos(a);
        }

        return Vector3.zero;
	}
	
	Vector3 NewPositionSin()
	{
		for (int i = 0; i < 100 /*SCENEWIDTH*/; i++)
		{
            float pos_x = i;
            float angle = (float)(i * Math.PI / 180);
            float pos_y = 200 * Mathf.Sin(angle) + 300;
		}

        return Vector3.zero;
	}
	
	
	/*
		例如:圆圈

		radius = 200
		for angle in range(0,361,10):
			a = angle*math.pi/180
			pos_x = radius *math.sin(a)
			pos_y = radius *math.cos(a)
		稍微复杂点 sin函数，控制下中心点y坐标（300）和振幅（200），也能实现很多

		for x in range(0,SCENEWIDTH):
			pos_x = x
			angle = x *math.pi/180
			pos_y = 200*math.sin(angle)+300
		再例如抛物线函数，螺旋曲线函数，都能实现飞机的各种花样轨迹。
		
	*/
	
	// Vectrosity 插件
	// dotween
	
	
}
