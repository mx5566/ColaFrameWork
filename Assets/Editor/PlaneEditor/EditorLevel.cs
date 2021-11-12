using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;

class Level: SerializedScriptableObject
{
    [LabelText("关卡ID")]
    public int ID;

    [LabelText("关卡阶段")]
    public List<Stage> Stage;
}

class Stage
{
    [LabelText("时间阶段")]
    public int Time;

    [LabelText("触发的敌人")]
    public List<EnemyPlane> Planes;


}

class EnemyPlane
{
    public int ID;

    public int Type;

    public int Num;
}


class EditorLevelWindow: EditorWindow
{
    [MenuItem("CustomEditor/Plane/Level")]
    public static void GetWindow()
    {
        Rect rect = new Rect(0, 0, 800, 500);
        var window = EditorWindow.GetWindowWithRect(typeof(EditorLevelWindow), rect, true, "关卡编辑");
        window.Show();
    }

    int selectIndex = -1;

    private void OnEnable()
    {
        LoadLevelData();
    }

    private void OnGUI()
    {
        
    }

    private void LoadLevelData()
    {

    }

    private void Add()
    {

    }

    private void Delete()
    {

    }

    private void EditLevelData()
    {

    }

}

