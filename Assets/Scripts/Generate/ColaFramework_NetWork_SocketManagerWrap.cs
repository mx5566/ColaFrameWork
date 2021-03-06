//this source code was auto-generated by tolua#, do not modify it
using System;
using LuaInterface;

public class ColaFramework_NetWork_SocketManagerWrap
{
	public static void Register(LuaState L)
	{
		L.BeginClass(typeof(ColaFramework.NetWork.SocketManager), typeof(System.Object));
		L.RegFunction("SendMsg", SendMsg);
		L.RegFunction("Connect", Connect);
		L.RegFunction("Close", Close);
		L.RegFunction("SetTimeOut", SetTimeOut);
		L.RegFunction("Dispose", Dispose);
		L.RegFunction("__tostring", ToLua.op_ToString);
		L.RegVar("OnTimeOut", get_OnTimeOut, set_OnTimeOut);
		L.RegVar("OnFailed", get_OnFailed, set_OnFailed);
		L.RegVar("OnConnected", get_OnConnected, set_OnConnected);
		L.RegVar("OnReConnected", get_OnReConnected, set_OnReConnected);
		L.RegVar("OnClose", get_OnClose, set_OnClose);
		L.RegVar("OnErrorCode", get_OnErrorCode, set_OnErrorCode);
		L.RegVar("Instance", get_Instance, null);
		L.RegVar("IsConnceted", get_IsConnceted, null);
		L.EndClass();
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SendMsg(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)ToLua.CheckObject<ColaFramework.NetWork.SocketManager>(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			byte[] arg1 = ToLua.CheckByteBuffer(L, 3);
			obj.SendMsg(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Connect(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 3);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)ToLua.CheckObject<ColaFramework.NetWork.SocketManager>(L, 1);
			string arg0 = ToLua.CheckString(L, 2);
			int arg1 = (int)LuaDLL.luaL_checknumber(L, 3);
			obj.Connect(arg0, arg1);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Close(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)ToLua.CheckObject<ColaFramework.NetWork.SocketManager>(L, 1);
			obj.Close();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetTimeOut(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 2);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)ToLua.CheckObject<ColaFramework.NetWork.SocketManager>(L, 1);
			int arg0 = (int)LuaDLL.luaL_checknumber(L, 2);
			obj.SetTimeOut(arg0);
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Dispose(IntPtr L)
	{
		try
		{
			ToLua.CheckArgsCount(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)ToLua.CheckObject<ColaFramework.NetWork.SocketManager>(L, 1);
			obj.Dispose();
			return 0;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnTimeOut(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action ret = obj.OnTimeOut;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnTimeOut on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnFailed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action ret = obj.OnFailed;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnFailed on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnConnected(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action ret = obj.OnConnected;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnConnected on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnReConnected(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action ret = obj.OnReConnected;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnReConnected on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnClose(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action ret = obj.OnClose;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnClose on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_OnErrorCode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action<int> ret = obj.OnErrorCode;
			ToLua.Push(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnErrorCode on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Instance(IntPtr L)
	{
		try
		{
			ToLua.PushObject(L, ColaFramework.NetWork.SocketManager.Instance);
			return 1;
		}
		catch (Exception e)
		{
			return LuaDLL.toluaL_exception(L, e);
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_IsConnceted(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			bool ret = obj.IsConnceted;
			LuaDLL.lua_pushboolean(L, ret);
			return 1;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index IsConnceted on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnTimeOut(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
			obj.OnTimeOut = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnTimeOut on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnFailed(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
			obj.OnFailed = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnFailed on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnConnected(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
			obj.OnConnected = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnConnected on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnReConnected(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
			obj.OnReConnected = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnReConnected on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnClose(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action arg0 = (System.Action)ToLua.CheckDelegate<System.Action>(L, 2);
			obj.OnClose = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnClose on a nil value");
		}
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_OnErrorCode(IntPtr L)
	{
		object o = null;

		try
		{
			o = ToLua.ToObject(L, 1);
			ColaFramework.NetWork.SocketManager obj = (ColaFramework.NetWork.SocketManager)o;
			System.Action<int> arg0 = (System.Action<int>)ToLua.CheckDelegate<System.Action<int>>(L, 2);
			obj.OnErrorCode = arg0;
			return 0;
		}
		catch(Exception e)
		{
			return LuaDLL.toluaL_exception(L, e, o, "attempt to index OnErrorCode on a nil value");
		}
	}
}

