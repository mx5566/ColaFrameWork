-- object base
local Unit = Class("Unit")


function Unit:initialize()
	self.hp = 0
	self.mp = 0
	self.type = 1
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