-- enemy base
local Unit = require("Unit")
local Enemy = Class("Enemy", Unit)


function Enemy:initialize()
	Unit.initialize(self)
end



return Enemy
