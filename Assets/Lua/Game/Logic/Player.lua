local Unit = require("Unit")
local Player = Class("Player", Unit)


function Player:initialize()
	Unit.initialize(self)

	self.gun = {}
	self.score = 0
	self.type = ECEnumType.PLAYER

    EventMgr.RegisterEvent(Modules.moduleId.Event, Modules.EventId.PlayerEventId.ADD_SCORE, Player.AddScore)
end

function Player.AddScore(self, score)
	self.score = self.score + score
end

return Player