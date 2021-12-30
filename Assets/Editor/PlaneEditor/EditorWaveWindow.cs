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
    class WaveFMgr : SerializedScriptableObject
    {
        public List<WaveF> Waves;
    }

    // Formation
    class WaveF
    {
        [LabelText("阵型ID")]
        public int ID;

        [LabelText("名字")]
        public string Name;

        [LabelText("阵型类型")]
        public WaveType Type;

        [LabelText("预制路径")]
        public string Path;

        [LabelText("敌人个数")]
        public int Num;

        [LabelText("半径")]
        public int Radius;

        [LabelText("速度(移动速度或角速度)")]
        public float Speed;

        [LabelText("距离屏幕左边缘的百分比")]
        public int LeftPercent;

        [LabelText("距离屏幕上边缘的百分比")]
        public int TopPercent;
        
        [LabelText("方向上|下|左|右")]
        public int Direcion;



        public WaveF()
        {
            Path = string.Empty;
            Name = string.Empty;
        }
    }

    class EditorWaveWindow : BaseTableEditorWindow
    {
        EditorWaveWindow()
        {
            winName = "阵型编辑";
            fileName = "wavef.json";
        }

        WaveFMgr wMgr;

        FieldInfo[] waveFieldInfoArray;

        Dictionary<int, bool> mapTogglePlanes = new Dictionary<int, bool>();
        #region
        
        // id的最大值
        int maxID = 0;
        List<int> idList = new List<int>();

        #endregion

        string file;

        [MenuItem("CustomEditor/Plane/Wave")]
        public static void GetWindow()
        {
            var window = GetWindow<EditorWaveWindow>(winName);
            window.Show();
        }

        protected override void OnLoad()
        {
            waveFieldInfoArray = typeof(Enemy).GetFields();

            string fName = AppConst.jsonFilePath + "/" + fileName;
            string jsonStr;

            bool isExistFile = FileHelper.IsFileExist(fName);
            if (!isExistFile)
            {
                wMgr = ScriptableObject.CreateInstance<WaveFMgr>();
                wMgr.Waves = new List<WaveF>();

                jsonStr = JsonMapper.ToJson(wMgr);

                FileHelper.WriteString(fName, jsonStr);
            }

            string strContent = FileHelper.ReadString(fName);

            wMgr = JsonMapper.ToObject<WaveFMgr>(strContent);

            for (int i = 0; i < wMgr.Waves.Count; i++)
            {
                int id = wMgr.Waves[i].ID;

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
            for (int i = 0; i < waveFieldInfoArray.Length; ++i)
            {
                GUIStyle style = new GUIStyle();
                style.alignment = TextAnchor.MiddleCenter;
                style.fontStyle = FontStyle.Normal;
                style.normal.textColor = Color.white;
                EditorGUILayout.LabelField(waveFieldInfoArray[i].Name, style, GUILayout.Width(100));
            }

            // 增加添加按钮
            if (GUILayout.Button("+", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
            {
                var wave = new WaveF();
                wave.ID = ++maxID;
                wMgr.Waves.Add(wave);
                mapToggleRows[wave.ID] = false;
                idList.Add(wave.ID);
            }

            SirenixEditorGUI.EndHorizontalToolbar();
            for (int i = 0; i < wMgr.Waves.Count; i++)
            {
                SirenixEditorGUI.BeginHorizontalToolbar();

                var id = wMgr.Waves[i].ID;
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


                for (int j = 0; j < waveFieldInfoArray.Length; j++)
                {
                    if (waveFieldInfoArray[j].Name == "Name")
                    {
                        if (string.IsNullOrEmpty(wMgr.Waves[i].Name))
                        {
                            wMgr.Waves[i].Name = string.Empty;
                        }
                        wMgr.Waves[i].Name = EditorGUILayout.TextField(wMgr.Waves[i].Name, GUILayout.Width(100));
                    }
                    else if (waveFieldInfoArray[j].Name == "ID")
                    {
                        wMgr.Waves[i].ID = EditorGUILayout.IntField(wMgr.Waves[i].ID, GUILayout.Width(100));
                    }
                    else if (waveFieldInfoArray[j].Name == "Type")
                    {
                        wMgr.Waves[i].Type = (WaveType)EditorGUILayout.EnumPopup(wMgr.Waves[i].Type, GUILayout.Width(100));
                    }
                    else if (waveFieldInfoArray[j].Name == "Num")
                    {
                        wMgr.Waves[i].Num = EditorGUILayout.IntField(wMgr.Waves[i].Num, GUILayout.Width(100));
                    }
                    else if (waveFieldInfoArray[j].Name == "Radius")
                    {
                        wMgr.Waves[i].Radius = EditorGUILayout.IntField(wMgr.Waves[i].Radius, GUILayout.Width(100));
                    }
                    else if (waveFieldInfoArray[j].Name == "Speed")
                    {
                        wMgr.Waves[i].Speed = EditorGUILayout.FloatField(wMgr.Waves[i].Speed, GUILayout.Width(100));
                    }
                    else if (waveFieldInfoArray[j].Name == "LeftPercent")
                    {
                        wMgr.Waves[i].LeftPercent = EditorGUILayout.IntField(wMgr.Waves[i].LeftPercent, GUILayout.Width(100));
                    }
                    else if (waveFieldInfoArray[j].Name == "TopPercent")
                    {
                        wMgr.Waves[i].TopPercent = EditorGUILayout.IntField(wMgr.Waves[i].TopPercent, GUILayout.Width(100));
                    }
                    else if (waveFieldInfoArray[j].Name == "Direcion")
                    {
                        wMgr.Waves[i].Direcion = EditorGUILayout.IntField(wMgr.Waves[i].Direcion, GUILayout.Width(100));
                    }
                    else if (waveFieldInfoArray[j].Name == "Path")
                    {
                        if (string.IsNullOrEmpty(wMgr.Waves[i].Path))
                        {
                            wMgr.Waves[i].Path = "";
                        }

                        bool isClick = false;
                        if (GUILayout.Button("select", GUILayout.Width(50)))
                        {
                            string[] strs = new string[] { "PREFAB", "prefab" };
                            file = EditorUtility.OpenFilePanelWithFilters("选择文件", "Assets/GameAssets", strs);

                            int index = file.IndexOf(Constants.GameAssetBasePath);
                            if (index != -1)
                            {
                                file = file.Substring(index + 18);
                            }
                            else
                            {
                                file = "";
                            }
                            isClick = true;
                        }

                        if (isClick)
                        {
                            if (!string.IsNullOrEmpty(file))
                            {
                                wMgr.Waves[i].Path = file;
                            }
                        }

                        EditorGUILayout.LabelField(new GUIContent(wMgr.Waves[i].Path, wMgr.Waves[i].Path), GUILayout.Width(50));
                    }
                }

                // 增加删除按钮
                if (GUILayout.Button("-", GUILayout.Width(20), GUILayout.Height(EditorGUIUtility.singleLineHeight)))
                {
                    wMgr.Waves.RemoveAt(i);
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
                idList.Remove(wMgr.Waves[allSelectRow[i]].ID);
                mapToggleRows.Remove(wMgr.Waves[allSelectRow[i]].ID);
                wMgr.Waves.RemoveAt(allSelectRow[i]);
            }
            allSelectRow.Clear();
        }

        private void SaveData()
        {
            string fName = AppConst.jsonFilePath + "/" + fileName;
            string jsonStr;

            jsonStr = ToJson<WaveFMgr>(wMgr);

            //jsonStr = JsonMapper.ToJson(eMgr);

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