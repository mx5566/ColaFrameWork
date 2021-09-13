local Unit = require("game.Logic.Unit")
local Player = Class("Player", Unit)
local socket = require "socket"
local project = require("Game.Logic.Projectile")


function Player:initialize(data, ismain, id)
	Unit.initialize(self, data, id)

	self.gun = {}
	self.score = 0
	self.type = ECEnumType.UnitType.PLAYER
	-- 玩家的所处的关卡
	self.currentFloor = 1

	self.isMain = ismain
	self.planeInstance = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Player.prefab", nil)

	if self.isMain then
		CommonUtil.GetMainCamera().orthographic = true
		CommonUtil.GetMainCamera().nearClipPlane = -2
	end

	
	local p = self.planeInstance:GetComponent("Player")

	print(p)
	p.ID = id
	self.isActiveShoot = false
	self.nextFire = int64.New(socket.gettime() * 1000)
	self.weaponPower = 1

	-- 获取飞机身上的枪的对象
	self.guns = { 	
		["left"] = nil, ["center"] = nil, ["right"] = nil, 
		["leftVfx"] = nil, ["centerVfx"] = nil, ["rightVfx"] = nil 
	}



end

function Player:Update(delta)
	if self.isActiveShoot then
		local t = int64.New(socket.gettime()*1000)
		if t > self.nextFire then
			self:Shoot()
			self.nextFire =  int64.New(t + 100) -- delay 100ms
		end
	end

	print("player update1..."..dump(self))

	print("player update2..."..delta)
end

-- 不同的能量对应不同的子弹预制 对应不同的效果(例子特效)
function Player:Shoot()
	local switch = {  
		[1] = function()
			project:new(false, 1)
		end,  
		[2] = function()
			project:new(false, 2)
		end,
		[3] = function()
			project:new(false, 3)
		end,
		[4] = function()
			project:new(false, 4)
		end,  
	}
	
	local fSwitch = switch[self.weaponPower] 
	  
	if fSwitch then 
		fSwitch() 
	else 
		print("player weapon > maxweapon")
	end  
end

function Player.AddScore(self, score)
	self.score = self.score + score
end

function Player:GetFloor()
	return self.currentFloor
end

function Player:SetFloor(floor)
	self.currentFloor = floor
end

function Player:Delete()
	if self.planeInstance ~= nil then
		CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Player.prefab", self.planeInstance)
		self.planeInstance = nil
	end
end

function Player:GetInstance()
	return self.planeInstance
end


function Player:AddHp(hp)
	Unit:AddHp(hp)
	
	if self.hp > 0 then
		local hitEffect = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/VFX/Lazer Ray Hit Effect.prefab", self.planeInstance.transform)
		hitEffect.transform.rotation = Quaternion.identity

		-- 何时销毁效果呢？
		local t = CommonUtil.ParticleSystemLength(hitEffect.transform)
		if t > 0 then
			-- 执行一次
			Timer.New(ff, t, 1, true)
		end

		local ff = function ()
			CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/VFX/Lazer Ray Hit Effect.prefab", hitEffect)
		end

	else 
		local explosion = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/VFX/Player Explosion.prefab", nil)
		explosion.transform.position = self.planeInstance.transform.position
		explosion.transform.rotation = Quaternion.identity

		-- 何时销毁效果呢？
		local t = CommonUtil.ParticleSystemLength(explosion.transform)
		if t > 0 then
			-- 执行一次
			Timer.New(ff, t, 1, true)
		end

		local ff = function ()
			CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/VFX/Player Explosion.prefab", explosion)
		end

		-- 死亡触发结束事件给UI
		-- TODO

		self:Destroy()
	end
end

function Player:Destroy()
	self:Delete()
end

return Player