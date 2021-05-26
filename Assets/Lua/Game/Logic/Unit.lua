-- object base
local Unit = Class("Unit")


function Unit:initialize(data)
	self.hp = 0
	self.mp = 0
	self.type = ECEnumType.UnitType.NULL
	self.id = data.id
end

function Unit:SetHp(hp)
	self.hp = hp
end

function Unit:GetHp()
	return self.hp
end

function Unit:SetMp(mp)
	self.mp = mp
end

function Unit:GetMp()
	return self.mp
end


return Unit