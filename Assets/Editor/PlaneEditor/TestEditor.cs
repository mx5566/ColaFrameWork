using UnityEngine;

using System.Collections;

using UnityEditor;


public class TestEditor : EditorWindow

{

    float secs = 10.0f;

    double startVal = 0;

    double progress = 0;

    bool isShow = false;


    [MenuItem("Examples/Cancelable Progress Bar Usage")]

    static void Init()

    {

        var window = GetWindow(typeof(TestEditor));

        window.Show();

    }


    void OnGUI()

    {

        secs = EditorGUILayout.FloatField("Time to wait:", secs);

        if (GUILayout.Button("Display bar"))

        {

            startVal = EditorApplication.timeSinceStartup; //开始编译到现在的时间

            isShow = !isShow;

        }


        if (GUILayout.Button("Clear bar"))

        {

            EditorUtility.ClearProgressBar(); //清空进度条的值， 基本没什么用

        }


        if (progress < secs && isShow == true)

        {

            //使用这句代码，在进度条后边会有一个 关闭按钮，但是用这句话会直接卡死，切记不要用

            // EditorUtility.DisplayCancelableProgressBar("Simple Progress Bar", "Show a progress bar for the given seconds", (float)(progress / secs));

            //使用这句创建一个进度条， 参数1 为标题，参数2为提示，参数3为 进度百分比 0~1 之间

            EditorUtility.DisplayProgressBar("Simple Progress Bar", "Show a progress bar for the given seconds", (float)(progress / secs));

        }

        else
        {

            startVal = EditorApplication.timeSinceStartup;

            progress = 0.0f;

            return;

        }

        progress = EditorApplication.timeSinceStartup - startVal;

    }


    void OnInspectorUpdate() //更新

    {

        Repaint(); //重新绘制

    }

}

