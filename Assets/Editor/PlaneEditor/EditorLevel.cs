using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEditor;
using ColaFramework.Foundation;
using LitJson;
using Sirenix.Utilities.Editor;
using System.Reflection;
using Sirenix.OdinInspector.Editor;

class LevelMgr: SerializedScriptableObject
{
    public List<Level> Levels;
}


class Level
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

class EditorStageWindow: EditorWindow
{
    public List<Stage> stages;

    private void OnEnable()
    {
        
    }

    private void OnGUI()
    {
        SirenixEditorGUI.BeginHorizontalToolbar();



        SirenixEditorGUI.EndHorizontalToolbar();
    }
}


class EditorLevelWindow: EditorWindow
{
    LevelMgr lMgr;

    Vector2 ScrollViewContentOffset = Vector2.zero;//纪录ScrollView滚动的位置
    FieldInfo[] levelFieldInfoArray;

    List<bool> toggleValues = new List<bool>();//单选框信息
    int selectedIndex = -1;//当前选择的某行数据下标

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
        DrawLevelData();
    }

    private void DrawLevelData()
    {
        //EditorGUILayout.BeginHorizontal();
        SirenixEditorGUI.BeginHorizontalToolbar();
        ScrollViewContentOffset = EditorGUILayout.BeginScrollView(ScrollViewContentOffset, GUILayout.Width(750), GUILayout.Height(250));


        SirenixEditorGUI.BeginHorizontalToolbar();
        GUILayout.Space(100);
        for(int i = 0; i < levelFieldInfoArray.Length; ++i)
        {
            EditorGUILayout.LabelField(levelFieldInfoArray[i].Name, GUILayout.Width(100));
        }
        SirenixEditorGUI.EndHorizontalToolbar();

        

        for (int i = 0; i < lMgr.Levels.Count; i++)
        {
            SirenixEditorGUI.BeginHorizontalToolbar();

            toggleValues.Add(false);
            if (toggleValues[i] = EditorGUILayout.Toggle(toggleValues[i], GUILayout.Width(100)))
            {
                if (selectedIndex != i)
                {
                    ChangeSelect(i);
                }
            }

            for (int j = 0; j < levelFieldInfoArray.Length; j++)
            {
                bool isArray = levelFieldInfoArray[j].FieldType.IsArray;
                string name = levelFieldInfoArray[j].Name;
                if (name == "Stage")
                {
                    if (GUI.Button(new Rect(0, 0, 100, 20), "stage"))
                    {
                        EditorWindow wTemp = EditorWindow.GetWindowWithRect(typeof(EditorStageWindow), new Rect(0, 0, 600, 400), true, "编辑阶段");
                        EditorStageWindow esw = wTemp as EditorStageWindow;

                        esw.stages = lMgr.Levels[i].Stage;

                        wTemp.ShowAsDropDown(new Rect(0, 0, 100, 20), new Vector2(320, 160));
                    }
                }
                else
                {
                    EditorGUILayout.LabelField(levelFieldInfoArray[j].GetValue(lMgr.Levels[i])?.ToString(), GUILayout.Width(100));
                }
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }

        EditorGUILayout.EndScrollView();
        SirenixEditorGUI.EndHorizontalToolbar();
    }

    private void DrawContentRecursion()
    {

    }

    //选择某行数据
    void ChangeSelect(int index)
    {
        if (selectedIndex != index)
        {
            if (selectedIndex != -1)
            {
                toggleValues[selectedIndex] = false;
            }
            toggleValues[index] = true;
        }
        selectedIndex = index;
    }

    private void LoadLevelData()
    {
        levelFieldInfoArray = typeof(Level).GetFields();

        bool isExist = FileHelper.IsDirectoryExist(AppConst.jsonFilePath);
        if (!isExist)
        {
            FileHelper.Mkdir(AppConst.jsonFilePath);
        }

        string fileName = AppConst.jsonFilePath + "/level.json";
        string jsonStr = "";


        bool isExistFile = FileHelper.IsFileExist(fileName);
        if (!isExistFile)
        {
            lMgr = ScriptableObject.CreateInstance<LevelMgr>();

            jsonStr = JsonMapper.ToJson(lMgr);

            Debug.LogFormat("jsonstr111[{0}]", jsonStr);

            FileHelper.WriteString(fileName, jsonStr);
        }

        string strContent = FileHelper.ReadString(fileName);

        lMgr = JsonMapper.ToObject<LevelMgr>(strContent);

        Debug.LogFormat("jsonstr[{0}]", strContent);
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

