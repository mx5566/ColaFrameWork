-- enemy base
local Unit = require("Unit")
local Enemy = Class("Enemy", Unit)


function Enemy:initialize(data)
	Unit.initialize(self, data)
	
	self.enemyObj = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Player.prefab", nil)

	self.type = ECEnumType.UnitType.ENEMY
end


return Enemy