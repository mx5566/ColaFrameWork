using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ColaFramework;

/// <summary>
/// This script defines the size of the ‘Boundary’ depending on Viewport. When objects go beyond the ‘Boundary’, they are destroyed or deactivated.
/// </summary>
public class Boundary1 : MonoBehaviour
{
    public Borders borders;

    Camera mainCamera;

    private void Start()
    {
        mainCamera = GUIHelper.GetMainCamera();
    }

    private void ResizeBorders()
    {
        borders.minX = mainCamera.ViewportToWorldPoint(Vector2.zero).x - borders.minXOffset;
        borders.minY = mainCamera.ViewportToWorldPoint(Vector2.zero).y - borders.minYOffset;
        borders.maxX = mainCamera.ViewportToWorldPoint(Vector2.right).x + borders.maxXOffset;
        borders.maxY = mainCamera.ViewportToWorldPoint(Vector2.up).y + borders.maxYOffset;
    }

    private void Update()
    {

    }

    void CornerPos()
    {

        /// <summary>
        /// 视窗坐标转世界坐标。视窗坐标是以视窗左下角（0,0）坐标为起始点，长宽为1.
        /// </summary>
        //Vector3 LeftUpWorldoCornerPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 1, -mainCamera.transform.position.z));//视窗左上角转为世界坐标
        //Vector3 LeftDownWorldCornerPos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, -mainCamera.transform.position.z));//视窗左下角转为世界坐标
        //Vector3 RightUpWorldCornerPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, -mainCamera.transform.position.z));//视窗右上角转为世界坐标
        //Vector3 RightDownWorldCornerPos = mainCamera.ViewportToWorldPoint(new Vector3(1, 0, -mainCamera.transform.position.z));//视窗右下角转为世界坐标


        /// <summary>
        /// 视窗坐标转世界屏幕，可以手动得到其屏幕长宽
        /// </summary>
        //Vector3 LeftUpScreenCornerPos = mainCamera.ViewportToScreenPoint(new Vector3(0, 1, -mainCamera.transform.position.z));//视窗左上角转为屏幕坐标
        //Vector3 LeftDownScreenCornerPos = mainCamera.ViewportToScreenPoint(new Vector3(0, 0, -mainCamera.transform.position.z));//视窗左下角转为屏幕坐标
        //Vector3 RightUpScreenCornerPos = mainCamera.ViewportToScreenPoint(new Vector3(1, 1, -mainCamera.transform.position.z));//视窗右上角转为屏幕坐标
        //Vector3 RightDownScreenCornerPos = mainCamera.ViewportToScreenPoint(new Vector3(1, 0, -mainCamera.transform.position.z));//视窗右下角转屏幕界坐标
        //float myWidth = Vector3.Magnitude(LeftUpScreenCornerPos - RightUpScreenCornerPos);
        //float myHeight = Vector3.Magnitude(LeftUpScreenCornerPos - LeftDownScreenCornerPos);

        /// <summary>
        /// 视窗坐标转世界坐标。得到四个视窗中心点的世界坐标，与物体判断是否到达边界.
        /// </summary>
        Vector3 upMiddlePos = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 1f, -mainCamera.transform.position.z)); //视窗上方中心转为世界坐标
        Vector3 downMiddlePos = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0, -mainCamera.transform.position.z));//视窗下方中心转为世界坐标
        Vector3 leftMiddlePos = mainCamera.ViewportToWorldPoint(new Vector3(0, 0.5f, -mainCamera.transform.position.z));//视窗左边中心转为世界坐标
        Vector3 rightMiddlePos = mainCamera.ViewportToWorldPoint(new Vector3(1, 0.5f, -mainCamera.transform.position.z));//视窗右边中心转为世界坐标

        bool upOut = upMiddlePos.y - transform.position.y <= 0 ? true : false; //与上方屏幕边缘碰到否
        bool downOut = downMiddlePos.y - transform.position.y >= 0 ? true : false;//与下方屏幕边缘碰到否
        bool leftOut = leftMiddlePos.x - transform.position.x >= 0 ? true : false;//与左边屏幕边缘碰到否
        bool rightOut = rightMiddlePos.x - transform.position.x <= 0 ? true : false;//与右边屏幕边缘碰到否

        Debug.Log("upOut : " + upOut);
        Debug.Log("downOut : " + downOut);
        Debug.Log("leftOut : " + leftOut);
        Debug.Log("rightOut : " + rightOut);
    }

}
