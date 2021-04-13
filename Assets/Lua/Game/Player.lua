local Player = Class("Player")


function Player:Initialize()
	-- body
	self.Hp = 0
	self.Mp = 0
	self.Gun = {}
end

function Player:SetHp(hp)
	-- body
	self.Hp = hp
end

function Player:GetHp()
	-- body
	return self.Hp
end

function Player:SetMp(mp)
	-- body
	self.Mp = mp
end

function Player:GetMp()
	-- body
	return self.Mp
end

return Player