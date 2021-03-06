//----------------------------------------------
//            ColaFramework
// Copyright © 2018-2049 ColaFramework 马三小伙儿
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Plugins.XAsset.Editor;

namespace ColaFramework.ToolKit
{
    [InfoBox("仅用于Editor编辑器模式下的临时性修改", InfoMessageType.Info)]

    public class AppSetting : SerializedScriptableObject
    {
        [LabelText("是否开启模拟模式")]
        [OnValueChanged("OnValueChanged")]
        public bool SimulateMode;

        [LabelText("游戏帧率")]
        [OnValueChanged("OnValueChanged")]
        public int GameFrameRate;

        [LabelText("是否开启Lua Bundle模式")]
        [OnValueChanged("OnValueChanged")]
        public bool LuaBundleMode;

        [LabelText("是否需要检测热更新")]
        [OnValueChanged("OnValueChanged")]
        public bool CheckUpdate;

        private void OnValueChanged()
        {
            OnInitialize();
        }

        [InitializeOnLoadMethod]
        private static void OnInitialize()
        {
            var setting = GetSetting();
            AppConst.SimulateMode = setting.SimulateMode;
            AppConst.GameFrameRate = setting.GameFrameRate;
            AppConst.CheckUpdate = setting.CheckUpdate;
            AppConst.LuaBundleModeEditMode = setting.LuaBundleMode;

            AssetsMenuItem.OnInitialize();
        }

        private static AppSetting GetSetting()
        {
            return ColaEditHelper.GetScriptableObjectAsset<AppSetting>("Assets/Editor/Settings/AppSetting.asset");
        }
    }
}