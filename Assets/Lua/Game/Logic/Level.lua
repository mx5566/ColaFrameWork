-- 关卡
local Level = Class("Level")
local Bonus = require("Game.Logic.Bonus")
local Planet = require("Game.Logic.Planet")
local common = require("Common.Common")


-- 临时的行星列表数据 以后会配置在表里面
local pathPlanets = {
	"Arts/Plane/Prefabs/Planets/Cream-Violet-Planet.prefab",
	"Arts/Plane/Prefabs/Planets/Purple-Planet-wih-Moon.prefab",
	"Arts/Plane/Prefabs/Planets/Red-Lines-PLanet.prefab",
}

function Level:initialize(cfg)
	self.name = cfg.name
	self.level = cfg.level
	self.cfg = cfg

	self.gameControler = nil
	-- bonus
	self.coroutineBonus = nil
	self.timerBonus = nil

	-- planets
	self.coroutinePlanets = nil
	self.timerPlanets = nil
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
	self.coroutineBonus = coroutine.start(self.CreateBonus, self)

	-- 启动一个协程产生行星预制对象
	self.coroutinePlanets = coroutine.start(self.CreatePlanets, self)

	-- 初始化LevelController 里面的数据
end

function Level:GetLevel()
	return self.level
end

function Level.CreateBonus(self)
	print('Coroutine bonus started')

	self.timerBonus = Timer.New(ff, 5, -1, true)

	local ff = function ()
		Bonus:new()
	end

	self.timerBonus:Start()

	-- while true do
	-- 	coroutine.wait(5)
	-- 	Bonus:new()
	-- end

    print('Coroutine bonus ended')
end


function Level.CreatePlanets(self)
	print('Coroutine planets started')
	coroutine.wait(10)

	self.timerPlanets = Timer.New(ff, 5, -1, true)

	local ff = function ()
		local path = pathPlanets[common.Random(1, table.getn())]
		Planet:new(path)
	end

	self.timerPlanets:Start()

	-- while true do
	-- 	coroutine.wait(5)
	-- 	Bonus:new()
	-- end

    print('Coroutine planets ended')
end


function Level:Destroy()
	CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Game_Controller.prefab", self.gameControler)

	if self.coroutineBonus ~= nil then
		self.timerBonus.Stop() -- 终止能量球定时器
		coroutine.stop(self.coroutineBonus)
	end

	if self.coroutinePlanets ~= nil then
		self.timerPlanets.Stop()
		coroutine.stop(self.coroutinePlanets)
	end
end


return Level

-- https://blog.csdn.net/weixin_30578677/article/details/99053830