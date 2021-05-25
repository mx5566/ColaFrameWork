local Unit = require("Unit")
local Player = Class("Player", Unit)


function Player:initialize(data)
	Unit.initialize(self, data)

	self.gun = {}
	self.score = 0
	self.type = ECEnumType.UnitType.PLAYER
	-- 玩家的所处的关卡
	self.currentFloor = 1

	self.planeInstance = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Player.prefab", nil)


    EventMgr.RegisterEvent(Modules.moduleId.Event, Modules.EventId.PlayerEventId.ADD_SCORE, Player.AddScore)
end

function Player.AddScore(self, score)
	self.score = self.score + score
end

function Player:GetFloor()
	-- body
	return self.currentFloor
end

function Player:SetFloor(floor)
	-- body
	self.currentFloor = floor
end

return Player