local Player = Class("Player")


function Player:initialize()
	self.hp = 0
	self.mp = 0
	self.gun = {}
	self.score = 0

	EventMgr.RegisterEvent(1,1, Player:AddScore)
end

function Player:SetHp(hp)
	self.hp = hp
end

function Player:GetHp()
	return self.hp
end

function Player:SetMp(mp)
	self.mp = mp
end

function Player:GetMp()
	return self.mp
end

function Player:AddScore(score)
	self.score += score
end


--[[
-- ���⽱��
local BonusLua = {}

function BonusLua.Initialize()
	Bonus.OnTriggerEnter2DLua = BonusLua.OnTriggerEnter2D
end

-- ��ײ����
function BonusLua.OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") 
		-- �������++

		UnityEngine.Object.Destroy(object)
	end
end

return BonusLua
]]--

return Player