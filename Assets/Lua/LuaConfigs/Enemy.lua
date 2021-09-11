--[[Notice:This lua config file is auto generate by Xls2Lua Tools，don't modify it manually! --]]
local fieldIdx = {}
fieldIdx.id = 1
fieldIdx.name = 2
fieldIdx.type = 3
fieldIdx.path = 4
local data = {
{1,[[零]],1,[[Arts/Plane/Prefabs/Enemy_straight_projectile.prefab]]},
{2,[[一]],1,""},
{3,[[二]],1,""},
{4,[[三]],1,""},
{5,[[四]],1,""},
{6,[[五]],1,""},
{7,[[六]],1,""},
{8,[[七]],1,""},
{9,[[八]],1,""},
{10,[[九]],1,""},
{10000,[[测试文字1]],1,""},
{10001,[[测试文字2]],1,""},}
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