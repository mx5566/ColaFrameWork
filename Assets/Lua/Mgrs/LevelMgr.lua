local LevelMgr = {}
local Level = require("Game.Level")

function LevelMgr:Initialize()
	-- body
	self.currentLevel = nil

	EventMgr.RegisterEvent(1,1, LevelMgr:TestEvent)
end

function LevelMgr:TestEvent(name)
	UnityEngine.Debug("LevelMgr TestEvent"..name)	
end

function LevelMgr:SetLevelClass(level)
	self.currentLevel = level
end

function LevelMgr:Start()
	local currentFloor = 1
	if self.currentLevel != nil then
		currentFloor = self.currentLevel.GetLevel()
	end

	local nextLevel = self.level + 1
	local isHave = false
	if isHave then
		-- ��ʼ���ؿ�����
		local cfg = ConfigMgr.GetItem("level", nextLevel)
		local level = Level:new(cfg)
		self:SetLevelClass(level)
		level:Start()
	end
end

