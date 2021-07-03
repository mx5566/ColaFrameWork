local Unit = require("game.Logic.Unit")
local Player = Class("Player", Unit)


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

	-- update bind
	ColaHelper.Update = self.Update
	-- 
    EventMgr.RegisterEvent(Modules.moduleId.Event, Modules.EventId.PlayerEventId.ADD_SCORE, Player.AddScore, self)
end

function Player:Update(delta)
	-- body
	print("player update1..."..dump(self))

	print("player update2..."..delta)
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

function Player:Delete()
	-- body
	if self.planeInstance ~= nil then
		CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Player.prefab", self.planeInstance)
		self.planeInstance = nil
	end
end

function Player:GetInstance()
	-- body
	return self.planeInstance
end


function Player:AddHp(hp)
	Unit:AddHp(hp)
	
	if hp > 0 then
		local hitEffect = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/VFX/Lazer Ray Hit Effect.prefab", self.planeInstance.transform)
		hitEffect.transform.rotation = Quaternion.identity
	else 
		local explosion = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/VFX/Player Explosion.prefab", nil)
		explosion.transform.position = self.planeInstance.transform.position
		explosion.transform.rotation = Quaternion.identity
		CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Player.prefab", self.planeInstance)
	end
end

return Player