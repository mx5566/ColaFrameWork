--[[Notice:This lua config file is auto generate by Xls2Lua Tools，don't modify it manually! --]]
local fieldIdx = {}
fieldIdx.id = 1
fieldIdx.para1 = 2
fieldIdx.para2 = 3
local data = {
{10000,110,110},
{10001,120,120},}
local mt = {}
mt.__index = function(a,b)
	if fieldIdx[b] then
		return a[fieldIdx[b]]
	end
	return nil
end
mt.__newindex = function(t,k,v)
	error('do not edit config')
end
mt.__metatable = false
for _,v in ipairs(data) do
	setmetatable(v,mt)
end
return data