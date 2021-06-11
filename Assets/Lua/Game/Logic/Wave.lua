local WaveLua = class("Wave")
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
    self.waveObj = CommonUtil.InstantiatePrefab(path, nil)
    
    self:CreateEnemyWave()
end

function WaveLua:CreateEnemyWave()
    local count = 5
    for i = 1, 5 do
        -- 不同敌人不同样式 固定写死
        local ene = Enemy:new({{id=i, name= "enemy"..i}}, common.GenerateID()) 
          
    end
end

return WaveLua

