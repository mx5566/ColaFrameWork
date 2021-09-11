--[[Notice:This lua config file is auto generate by Xls2Lua Tools，don't modify it manually! --]]
local fieldIdx = {}
fieldIdx.id = 1
fieldIdx.name = 2
fieldIdx.num = 3
fieldIdx.enemys = 4
local data = {
{1,[[一号]],4,{1,2,3,4}},
{2,[[二号]],4,{1,2,3,4}},
{3,[[三号]],4,{1,2,3,4}},
{4,[[四号]],4,{1,2,3,4}},
{5,[[五号]],4,{1,2,3,4}},
{6,[[六号]],4,{1,2,3,4}},
{7,[[七号]],4,{1,2,3,4}},
{8,[[八号]],4,{1,2,3,4}},
{9,[[九号]],4,{1,2,3,4}},
{10,[[十号]],4,{1,2,3,4}},
{11,[[十一号]],4,{1,2,3,4}},
{12,[[十二号]],4,{1,2,3,4}},}
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