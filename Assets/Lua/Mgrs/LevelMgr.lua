local LevelMgr = {}
local Level = require("Game.Logic.Level")

-- 点号相当于静态方法
-- 冒号相当于成员方法
function LevelMgr.Initialize(self)
	-- body
	self.currentLevel = nil

	EventMgr.RegisterEvent(1,1, LevelMgr.TestEvent)
end

function LevelMgr.TestEvent(self, name)
	UnityEngine.Debug("LevelMgr TestEvent"..name)	
end

function LevelMgr:SetLevelClass(level)
	self.currentLevel = level
end

function LevelMgr:Start()
	if self.currentLevel ~= nil then
		return
		
	end 

	local currentFloor = 1
	local m = PlayerMgr:GetMainPlayer()

	local nextLevel = m:GetFloor() + 1
	local isHave = false
	if isHave then
		-- 初始化关卡数据
		local cfg = ConfigMgr.GetItem("Level", nextLevel)
		local level = Level:new(cfg)
		self:SetLevelClass(level)
		level:Start()
		-- m:SetFloor(nextLevel)
	end
end

function LevelMgr:End()
	if self.currentLevel == nil then
		return
	end

	self.currentLevel:Destroy()
end

function LevelMgr:Update(delta)
	if self.currentLevel ~= nil then
		self.currentLevel:Update(delta)
	end
end

return LevelMgr