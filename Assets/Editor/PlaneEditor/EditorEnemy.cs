using ColaFramework.Foundation;
using LitJson;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;


namespace ColaFramework.ToolKit
{
    class EnemyMgr : SerializedScriptableObject
    {
        public List<Enemy> Enemys;
    }

    class Enemy
    {
        [LabelText("敌机ID")]
        public int ID;

        [LabelText("名字")]
        public string Name;

        [LabelText("预制路径")]
        public string Path;
    }

    class EditorEnemyWindow : BaseTableEditorWindow
    {
        EditorEnemyWindow()
        {
            winName = "敌人编辑";
            fileName = "enemy.json";
        }

        EnemyMgr eMgr;

        FieldInfo[] enemyFieldInfoArray;

        Dictionary<int, bool> mapTogglePlanes = new Dictionary<int, bool>();

        // id的最大值
        int maxID = 0;
        List<int> idList = new List<int>();

        [MenuItem("CustomEditor/Plane/Enemy")]
        public static void GetWindow()
        {
            var window = GetWindow<EditorEnemyWindow>(winName);
            window.Show();
        }

        protected override void OnLoad()
        {
            //SetFileName("enemy.json");
            //winName = "敌人编辑";

            enemyFieldInfoArray = typeof(Enemy).GetFields();

            string fName = AppConst.jsonFilePath + "/" + fileName;
            string jsonStr;

            bool isExistFile = FileHelper.IsFileExist(fName);
            if (!isExistFile)
            {
                eMgr = ScriptableObject.CreateInstance<EnemyMgr>();
                eMgr.Enemys = new List<Enemy>();

                jsonStr = JsonMapper.ToJson(eMgr);

                //Debug.LogFormat("jsonstr111[{0}]", jsonStr);

                FileHelper.WriteString(fName, jsonStr);
            }

            string strContent = FileHelper.ReadString(fName);

            //Debug.LogFormat("jsonstrRead [{0}]", strContent);
            eMgr = JsonMapper.ToObject<EnemyMgr>(strContent);

            for (int i = 0; i < eMgr.Enemys.Count; i++)
            {
                int id = eMgr.Enemys[i].ID;

                if (maxID < id)
                {
                    maxID = id;
                }

                mapToggleRows[id] = false;
                idList.Add(id);
            }
        }

        private void OnGUI()
        {
            DrawData();
        }

        private void DrawData()
        {
            DrawHead();

            //EditorGUILayout.BeginToggleGroup();

            GUILayout.Space(10);
            SirenixEditorGUI.BeginHorizontalToolbar();
            ScrollViewContentOffset = EditorGUILayout.BeginScrollView(ScrollViewContentOffset, GUILayout.Width(750), GUILayout.Height(250));

            SirenixEditorGUI.BeginHorizontalToolbar();
            GUILayout.Space(100);
            for (int i = 0; i < enemyFieldInfoArray.Length; ++i)
            {
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.fontStyle = FontStyle.Normal;
                style.normal.textColor = Color.white;
                EditorGUILayout.LabelField(enemyFieldInfoArray[i].Name, style, GUILayout.Width(100));
            }

            // 增加添加按钮
            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                var enemy = new Enemy();
                enemy.ID = ++maxID;
                eMgr.Enemys.Add(enemy);
                mapToggleRows[enemy.ID] = false;
                idList.Add(enemy.ID);
            }

            SirenixEditorGUI.EndHorizontalToolbar();
            for (int i = 0; i < eMgr.Enemys.Count; i++)
            {
                SirenixEditorGUI.BeginHorizontalToolbar();

                var id = eMgr.Enemys[i].ID;
                mapToggleRows[id] = EditorGUILayout.Toggle(mapToggleRows[id], GUILayout.Width(100));
                if (mapToggleRows[id])
                {
                    // 选中的行记录下来 可以批量删除
                    if (!allSelectRow.Contains(i))
                    {
                        allSelectRow.Add(i);
                    }
                }
                else
                {
                    allSelectRow.Remove(i);
                }


                for (int j = 0; j < enemyFieldInfoArray.Length; j++)
                {
                    if (enemyFieldInfoArray[j].Name == "Name")
                    {
                        eMgr.Enemys[i].Name = EditorGUILayout.TextField(eMgr.Enemys[i].Name, GUILayout.Width(100));
                    }
                    else if (enemyFieldInfoArray[j].Name == "ID")
                    {
                        eMgr.Enemys[i].ID = EditorGUILayout.IntField(eMgr.Enemys[i].ID, GUILayout.Width(100));
                    }
                    else if (enemyFieldInfoArray[j].Name == "Path")
                    {
                        //string path = EditorUtility.OpenFilePanel("Overwrite with prefab", "", "prefab");

                        //Object go;
                        //go = EditorGUILayout.ObjectField(go, typeof(GameObject), false, GUILayout.Width(100));
                        eMgr.Enemys[i].Path = /*path*/EditorGUILayout.TextField(eMgr.Enemys[i].Path, GUILayout.Width(100));
                    }
                }

                // 增加删除按钮
                if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                {
                    eMgr.Enemys.RemoveAt(i);
                    mapToggleRows.Remove(id);
                    allSelectRow.Remove(i);
                    idList.Remove(id);

                    return;
                }

                SirenixEditorGUI.EndHorizontalToolbar();
            }

            EditorGUILayout.EndScrollView();
            SirenixEditorGUI.EndHorizontalToolbar();
        }

        private void OnDisable()
        {
            SaveData();
        }

        protected override void OnDelete()
        {
            for (int i = 0; i < allSelectRow.Count; i++)
            {
                idList.Remove(eMgr.Enemys[allSelectRow[i]].ID);
                mapToggleRows.Remove(eMgr.Enemys[allSelectRow[i]].ID);
                eMgr.Enemys.RemoveAt(allSelectRow[i]);
            }
            allSelectRow.Clear();
        }

        private void SaveData()
        {
            string fName = AppConst.jsonFilePath + "/" + fileName;
            string jsonStr;

            jsonStr = JsonMapper.ToJson(eMgr);

            FileHelper.WriteString(fName, jsonStr);
        }

        protected override void OnSave()
        {
            SaveData();
        }

        protected override void OnReload()
        {
            // 先清理
            mapTogglePlanes.Clear();

            OnLoad();
        }

        protected override void OnSelectAll()
        {
            for (int i = 0; i < idList.Count; i++)
            {
                mapToggleRows[idList[i]] = isAllSelect;
            }
        }
    }

}