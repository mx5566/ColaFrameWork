local Unit = require("Unit")
local Player = Class("Player", Unit)


function Player:initialize()
	Unit.initialize(self)

	self.gun = {}
	self.score = 0
	self.type = ECEnumType.PLAYER

    EventMgr.RegisterEvent(Modules.moduleId.Event, Modules.NotifyId.EventId.PlayerEventId.ADD_SCORE, Player:AddScore)
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