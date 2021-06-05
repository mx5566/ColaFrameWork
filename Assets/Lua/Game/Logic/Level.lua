-- 关卡
local Level = Class("Level")
local Bonus = require("Game.Logic.Bonus")

function Level:initialize(cfg)
	self.name = cfg.name
	self.level = cfg.level
	self.cfg = cfg

	self.gameControler = nil
	self.coroutineBonus = nil
end

function Level:Start()
	-- body
	-- 需要去驱动底层
	-- 数据传输给c#层
	
	-- 获取场景里面动态对象的产生预制
	self.gameControler = UnityEngine.GameObject.Find("Game_Controller")
	if self.gameControler == nil then
		self.gameControler = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Game_Controller.prefab", nil)
	end

	local levelC = self.gameControler.GetComponent(typeof(LevelController))
	if levelC ~= nil then
		if levelC.isStart == true then
		-- already start
			return
		end
	else
		levelC = self.gameControler.AddSingleComponent(typeof(LevelController))
	end

	local ret = levelC.StratGame()

	-- 启动一个协程产生bonus预制对象
	-- ....
	self.coroutineBonus = coroutine.start(self.CreateBonus)

	-- 初始化LevelController 里面的数据
end

function Level:GetLevel()
	return self.level
end

function Level:CreateBonus()
	print('Coroutine started')

	while true do
		coroutine.wait(5)
		Bonus:new()
	end

    print('Coroutine ended')

end

function Level:Destroy()
	CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Game_Controller.prefab", self.gameControler)

	if self.coroutineBonus ~= nil then
		coroutine.stop(self.coroutineBonus)
	end
end

return Level

-- https://blog.csdn.net/weixin_30578677/article/details/99053830