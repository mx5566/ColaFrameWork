local LevelMgr = {}
local Level = require("Game.Level")

function LevelMgr:Initialize()
	-- body
	self.level = 0
	self.CurrentLevel = nil
end

function LevelMgr:SetLevelClass(level)
	-- body
	self.CurrentLevel = level
end

function NextLevel()
	-- body
	local nextLevel = self.level + 1
	local isHave = false
	if isHave
		-- 初始化关卡数据
		local cfg = ConfigMgr.GetItem("level", nextLevel)
		Level:Initialize(cfg)
		self:SetLevelClass(Level)
		self:level = nextLevel
		Level:Start()
		-- CommonUtil.InstantiatePrefab()
	end
end

