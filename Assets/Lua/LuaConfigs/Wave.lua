--[[Notice:This lua config file is auto generate by Xls2Lua Tools，don't modify it manually! --]]
local fieldIdx = {}
fieldIdx.id = 1
fieldIdx.name = 2
fieldIdx.mid = 3
local data = {
{1,[[一号]],3},
{2,[[二号]],5},
{3,[[三号]],7},
{4,[[四号]],1},
{5,[[五号]],2},
{6,[[六号]],2},
{7,[[七号]],3},
{8,[[八号]],4},
{9,[[九号]],3},
{10,[[十号]],2},
{11,[[十一号]],2},
{12,[[十二号]],2},}
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