-- object base
local Unit = Class("Unit")


function Unit:initialize(data, id)
	self.hp = 0
	self.mp = 0
	self.type = ECEnumType.UnitType.NULL
	self.id = id
	self.baseID = data.id
end

function Unit:SetHp(hp)
	self.hp = hp
end

function Unit:GetHp()
	return self.hp
end

function Unit:AddHp(hp)
	self.hp = self.hp + hp

	if self.hp < 0 then
		self.hp = 0
	end
end

function Unit:SetMp(mp)
	self.mp = mp
end

function Unit:GetMp()
	return self.mp
end


return Unit