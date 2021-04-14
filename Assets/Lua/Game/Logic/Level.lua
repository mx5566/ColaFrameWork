-- 关卡
local Level = Class("Level")

function Level:initialize(cfg)
	self.name = cfg.Name
	self.level = cfg.Level
	self.cfg = cfg
end

function Level:Start()
	-- body
	-- 需要去驱动底层
	-- 数据传输给c#层
	local obj = UnityEngine.GameObject.Find("获取对象")
	local levelC = obj.GetComponent(typeof(LevelController))
	if levelC = nil then
		levelC = obj.AddSingleComponent(typeof(LevelController))
	end


	-- 初始化LevelController 里面的数据
end

function Level:GetLevel()
	return self.Level
end


return Level