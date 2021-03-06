local EnemyMgr = {}
local Enemy = require("Game.Logic.Enemy")

-- 点号相当于静态方法
-- 冒号相当于成员方法
function EnemyMgr:Initialize()
	self.mapEnemys = {}
end

function EnemyMgr:Update(delta)
	for k, v in pairs(self.mapEnemys) do
		v.Update(delta)
	end
end

function EnemyMgr:CreateEnemy(data, id)
	if data == nil then
		return nil
	end

	if self.mapEnemys[id] ~= nil then
		return self.mapEnemys[id]
	end

	-- create enemy
	local enemy = Enemy:new(data, id)
	self.mapEnemys[id] = enemy

	return enemy
end

function EnemyMgr:GetPlayer(id)
	return self.mapEnemys[id]
end

function EnemyMgr:Delete(id)
	self.mapEnemys[id] = nil 
end

return EnemyMgr