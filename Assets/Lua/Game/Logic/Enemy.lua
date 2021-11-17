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
	local cfg = ConfigMgr.GetItem("Enemy", self.baseID)

	self.enemyObj = CommonUtil.InstantiatePrefab(cfg.path, nil)

	local e = self.enemyObj:GetComponent(typeof(Enemy))
	e.ID = id

	self.waveID = 0
	-- 从表里查数据根据waveID
	self.waveData = nil
	self.nextFire = int64.new(int64.tonum2(ngx.now() * 1000))	

	self.type = ECEnumType.UnitType.ENEMY

	-- 敌人的分类 boss 小型 中型  大型

	self.shotTimeMin = 1000.0
	self.shotTimeMax = 5000.0
	self.shotChance = 50.0
	-- self.tim = timer.New(self:ActivateShooting(), 1, 1, true)

	local enemyC = self.projectileObj:GetComponent("Projectile")
	-- bind collison 
	enemyC.OnTriggerEnter2DLua = Enemy.OnTriggerEnter2D

end

function Enemy:Update(delta)
	local t = Common.Random(self.shotTimeMin, self.shotTimeMax) 
	local tt = int64.new(int64.tonum2(ngx.now() * 1000))
	if int64.__lt(tt, self.nextFire) then
		self:ActivateShooting()
	end

	self.nextFire = int64.__add(t, tt)
end

function Enemy:GetObj()
	return self.enemyObj
end

function Enemy:ActivateShooting()
	if Common.Random(0,1) < self.shotChance / 100 then
		local pro = Projectile:new(true, 1, self.id)
		pro:SetPosition(self.enemyObj.transform.position)
	end
end

function Enemy:AddHp(hp)
	Unit:AddHp(hp)
	
	if hp < 0 then
		if self.hp == 0 then
			self:Effect("Arts/Plane/Prefabs/VFX/Lazer Ray Hit Effect.prefab", false)
			
			local enemy = EnemyMgr:GetPlayer(self.id)

			local cfg = ConfigMgr.GetItem("Enemy", enemy.baseID)
	
			local player = PlayerMgr:GetPlayer(self.ownerID)
			player.AddScore(cfg.score)


			self:Destroy()
		elseif self.hp > 0 then
			self:Effect("Arts/Plane/Prefabs/VFX/Lazer Ray Hit Effect.prefab", true)
		end
	end
end

function Enemy:Effect(path, isparent)
	local effect = nil
	if isparent then
		effect = CommonUtil.InstantiatePrefab(path, self.enemyObj.transform)
	else
		effect = CommonUtil.InstantiatePrefab(path, nil)
	end

	effect.transform.position = self.enemyObj.transform.position
	effect.transform.rotation = Quaternion.identity
	-- 何时销毁效果呢？
	local t = CommonUtil.ParticleSystemLength(effect.transform)
	
	local ff = function ()
		CommonUtil.ReleaseGameObject(path, effect)
	end
	-- 执行一次
	Timer.New(ff, t, 1, true)
end

function Enemy:OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") then
		local mainPlayer = PlayerMgr:GetMainPlayer()
		mainPlayer:AddHp(-1)
	end
end

function Enemy:Destroy()
	self.tim:Stop()
	if self.enemyObj ~= nil then
		CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Enemy_straight_projectile.prefab", self.enemyObj)
		self.enemyObj = nil
	end

	-- 从enemymgr移除玩家
	EnemyMgr.Delete(self.id)
end


return Enemy