-- 关卡
local Level = Class("Level")

function Level:initialize(cfg)
	self.name = cfg.name
	self.level = cfg.level
	self.cfg = cfg

end

function Level:Start()
	-- body
	-- 需要去驱动底层
	-- 数据传输给c#层
	
	-- 获取场景里面动态对象的产生预制
	local obj = UnityEngine.GameObject.Find("Game_Controller")
	if obj == nil then
		-- local inis = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Player.prefab", nil)
		obj = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Game_Controller.prefab", nil)
	end

	local levelC = obj.GetComponent(typeof(LevelController))
	if levelC ~= nil then
		if levelC.isStart == true then
		-- already start
			return
		end
	else
		levelC = obj.AddSingleComponent(typeof(LevelController))
	end

	local ret = levelC.StratGame()

	-- 初始化LevelController 里面的数据
end

function Level:GetLevel()
	return self.level
end


return Level