---
---                 ColaFramework
--- Copyright © 2018-2049 ColaFramework 马三小伙儿
---                 通用接口工具类
---

local LuaCommon_Utils = Class("LuaCommon_Utils")

LuaCommon_Utils._instance = nil

-- override 初始化各种数据
function LuaCommon_Utils.initialize()
    
end

--字符串分隔方法
function LuaCommon_Utils.Split(str, sep)
    local sep, fields = sep or ":", {}
    local pattern = string.format("([^%s]+)", sep)
    str:gsub(pattern, function (c) fields[#fields + 1] = c end)
    return fields
end

function LuaCommon_Utils.Split1(s,p)
	local a={}
	string.gsub(s,'[^'..p..']+',function(w) table.insert(a,w) end)
	return a
end


return LuaCommon_Utils