local WaveLua = Class("Wave")
local common = require("Common.Common")
local Enemy = require("Game.Logic.Enemy")

local enemyWaves = {
	"Arts/Plane/Prefabs/EnemyWaves/Wave_1.prefab",
	"Arts/Plane/Prefabs/EnemyWaves/Wave_2.prefab",
	"Arts/Plane/Prefabs/EnemyWaves/Wave_3.prefab",
	"Arts/Plane/Prefabs/EnemyWaves/Wave_4.prefab",
	"Arts/Plane/Prefabs/EnemyWaves/Wave_5.prefab",
	"Arts/Plane/Prefabs/EnemyWaves/Wave_6.prefab",
}

local enemyPath = {
    "Arts/Plane/Prefabs/Enemies/Enemy_straight_projectile.prefab"
}

--[[
            GameObject newEnemy;
            newEnemy = Instantiate(enemy, enemy.transform.position, Quaternion.identity);
            FollowThePath followComponent = newEnemy.GetComponent<FollowThePath>(); 
            followComponent.path = pathPoints;         
            followComponent.speed = speed;        
            followComponent.rotationByPath = rotationByPath;
            followComponent.loop = Loop;
            followComponent.SetPath(); 
            Enemy enemyComponent = newEnemy.GetComponent<Enemy>();  
            enemyComponent.shotChance = shooting.shotChance; 
            enemyComponent.shotTimeMin = shooting.shotTimeMin; 
            enemyComponent.shotTimeMax = shooting.shotTimeMax;
            newEnemy.SetActive(true);      
            yield return new WaitForSeconds(timeBetween);
--]]

-- id是组的id 可以找到基本的配置 比如个数 对应的敌人的配置id
function WaveLua:initialize(id)
	-- self.powerObj = CommonUtil.InstantiatePrefab("Arts/Plane/Animation/Bonuses/Power Up.prefab", nil)
    -- 读取配置文件 目前没有
    local path = enemyWaves[id]
    self.id = id
    self.waveObj = CommonUtil.InstantiatePrefab(path, nil)
    -- 根据id从表里面读取wave的配置数据
    -- 敌人的资源路径
    -- 敌人的个数
    -- wave speed
    -- 每个wave中敌人产生的间隔
    -- 路径点
    -- 是否旋转
    -- wave是否重复
    -- 编辑器静态显示的路径颜色
    -- wave射击目标的概率
    -- 每个敌人射击目标的最短和最长时间
    
    self.CoroutineWaveEnemy = coroutine.create(self.CreateEnemyWave, self)
end

function WaveLua.CreateEnemyWave(self)
    print("Coroutine wave enemy started")

    local count = 5
    for i = 1, 5 do
        -- 不同敌人不同样式 固定写死 配表就是策划的事情了根据id得到基础数据
        -- TODO:
        local ene = Enemy:new({id=i, name= "enemy"..i}, common.GenerateID())
        local eneObj = ene:GetObj()
        local followComponent = eneObj:GetComponent(typeof(FollowThePath))
        followComponent.path = 0
        followComponent.speed = 1
        followComponent.rotationByPath = false
        followComponent.loop = false
        followComponent:SetPath()

        ene.waveID = self.id

        -- active
        eneObj:SetActive(true)
        
        coroutine.wait(1) 
    end

    self:Desroy()

    print("Coroutine wave enemy ended")
end

function WaveLua:Destroy()
    CommonUtil.ReleaseGameObject(enemyWaves[self.id], self.planeInstance)
    self.id = 0
    self.waveObj = nil
end

return WaveLua

