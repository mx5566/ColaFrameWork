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
]]--

return Player