﻿//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class SceneMgrWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(SceneMgr), typeof(UnityEngine.MonoBehaviour));
		L.RegFunction("LoadSceneAdditiveAsync", LoadSceneAdditiveAsync);
		L.RegFunction("LoadScene", LoadScene);
		L.RegFunction("LoadSceneAsync", LoadSceneAsync);
		L.RegFunction("UnloadSceneAsync", UnloadSceneAsync);
		L.RegFunction("__eq", op_Equality);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("currentScene", get_currentScene, set_currentScene);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadSceneAdditiveAsync(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			SceneMgr obj = (SceneMgr)ToLua.CheckObject<SceneMgr>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			OnSceneNameChanged arg1 = (OnSceneNameChanged)ToLua.CheckDelegate<OnSceneNameChanged>(L, 3);
			obj.LoadSceneAdditiveAsync(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadScene(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			SceneMgr obj = (SceneMgr)ToLua.CheckObject<SceneMgr>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			obj.LoadScene(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadSceneAsync(IntPtr L)
	{
		try
		{
			int count = LuaDLL.lua_gettop(L);

			if (count == 3 && TypeChecker.CheckTypes<int, OnSceneIndexChanged>(L, 2))
			{
				SceneMgr obj = (SceneMgr)ToLua.CheckObject<SceneMgr>(L, 1);
				int arg0 = (int)LuaDLL.lua_tonumber(L, 2);
				OnSceneIndexChanged arg1 = (OnSceneIndexChanged)ToLua.ToObject(L, 3);
				obj.LoadSceneAsync(arg0, arg1);
				return 0;
			}
			else if (count == 3 && TypeChecker.CheckTypes<string, OnSceneNameChanged>(L, 2))
			{
				SceneMgr obj = (SceneMgr)ToLua.CheckObject<SceneMgr>(L, 1);
				string arg0 = ToLua.ToString(L, 2);
				OnSceneNameChanged arg1 = (OnSceneNameChanged)ToLua.ToObject(L, 3);
				obj.LoadSceneAsync(arg0, arg1);
				return 0;
			}
			else
			{
				return LuaDLL.luaL_throw(L, "invalid arguments to method: SceneMgr.LoadSceneAsync");
			}
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int UnloadSceneAsync(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			SceneMgr obj = (SceneMgr)ToLua.CheckObject<SceneMgr>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			OnSceneNameChanged arg1 = (OnSceneNameChanged)ToLua.CheckDelegate<OnSceneNameChanged>(L, 3);
			obj.UnloadSceneAsync(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int op_Equality(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			UnityEngine.Object arg0 = (UnityEngine.Object)ToLua.ToObject(L, 1);
			UnityEngine.Object arg1 = (UnityEngine.Object)ToLua.ToObject(L, 2);
			bool o = arg0 == arg1;
			LuaDLL.lua_pushboolean(L, o);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_currentScene(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SceneMgr obj = (SceneMgr)o;
			UnityEngine.SceneManagement.Scene ret = obj.currentScene;
			ToLua.PushValue(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index currentScene on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_currentScene(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			SceneMgr obj = (SceneMgr)o;
			UnityEngine.SceneManagement.Scene arg0 = StackTraits<UnityEngine.SceneManagement.Scene>.Check(L, 2);
			obj.currentScene = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index currentScene on a nil value");
		}
	}
}

