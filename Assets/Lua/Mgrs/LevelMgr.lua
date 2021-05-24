local LevelMgr = {}
local Level = require("Game.Level")

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
	local currentFloor = 1
	if self.currentLevel ~= nil then
		currentFloor = self.currentLevel.GetLevel()
	end

	local nextLevel = self.level + 1
	local isHave = false
	if isHave then
		-- 初始化关卡数据
		local cfg = ConfigMgr.GetItem("level", nextLevel)
		local level = Level:new(cfg)
		self:SetLevelClass(level)
		level:Start()
	end
end

