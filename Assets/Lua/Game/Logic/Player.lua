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



-- 额外奖励
local BonusLua = {}

function BonusLua.Initialize()
	Bonus.OnTriggerEnter2DLua = BonusLua.OnTriggerEnter2D
end

-- 碰撞触发
function BonusLua.OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") 
		-- 玩家能量++

		UnityEngine.Object.Destroy(object)
	end
end

return BonusLua




return Player