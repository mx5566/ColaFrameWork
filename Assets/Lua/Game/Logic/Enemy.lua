-- enemy base
local Unit = require("Game.Logic.Unit")
local Enemy = Class("Enemy", Unit)
local Common = require("Common.Common")
local Projectile = require("Game.Logic.Projectile")

-- https://www.cnblogs.com/weiqiangwaideshijie/p/6762066.html
-- 判断rotation

-- https://blog.csdn.net/weixin_30578677/article/details/99053830
-- 定时器
function Enemy:initialize(data, id)
	Unit.initialize(self, data, id)

	self.baseID = data.id
	-- 敌机
	self.enemyObj = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Enemy_straight_projectile.prefab", nil)
	-- 子弹
	-- self.projectileObj = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Projectiles/Enemy_Straight_Projetile.prefab", nil)

	-- 飞机爆炸效果
	-- self.enemyExplosionObj = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/VFX/Enemy Explosion.prefab", nil)

	-- 子弹效果
	-- self.projectileHitObj = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/VFX/Lazer Ray Hit Effect.prefab", nil)
	local e = self.enemyObj:GetComponent(typeof(Enemy))
	e.ID = id

	self.type = ECEnumType.UnitType.ENEMY

	local tim = timer.New(self:ActivateShooting(), 1, 1, true)	
end

function Enemy:ActivateShooting()
	if Common.Random(0,1) < 50 / 100 then
		local pro = Projectile:new(true, 1)
		pro:SetPosition(self.enemyObj.transform.position)
	end
end

function Enemy:AddHp(hp)
	Unit:AddHp(hp)
	
	if hp < 0 then
		if self.hp == 0 then
			local explosion = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/VFX/Enemy Explosion.prefab", nil)
			explosion.transform.position = self.enemyObj.transform.position
			explosion.transform.rotation = Quaternion.identity
			CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Enemy_straight_projectile.prefab", self.enemyObj)
		elseif self.hp > 0 then
			local hitEffect = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/VFX/Lazer Ray Hit Effect.prefab", self.enemyObj.transform)
			hitEffect.transform.rotation = Quaternion.identity
		end
	end
end

function Enemy:OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") then
		local mainPlayer = PlayerMgr:GetMainPlayer()
		mainPlayer:AddHp(-1)
	end
end

function Enemy:Destroy()
	
end


return Enemy