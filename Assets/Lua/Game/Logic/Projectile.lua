local Projectile = class("Projectile")
local common = require("Common.Common")

-- 能量球奖励类 玩家触碰到获取奖励，子弹变异
function Projectile:initialize(isenemy, id)

    -- 根据id可以得到各种数据
    self.id = id
    self.isEnemy = isenemy
    if isenemy then
		self.projectileObj = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Projectiles/Enemy_Straight_Projetile.prefab", nil)
    else
		self.projectileObj = CommonUtil.InstantiatePrefab("Arts/Plane/Prefabs/Projectiles/Player_Short_Lazer.prefab", nil)
    end

    -- self
	self.projectileObj.transform.position = 0

	local projectileC = self.projectileObj:GetComponent("Projectile")
	-- bind collison 
	projectileC.OnTriggerEnter2DLua = Projectile.OnTriggerEnter2D
end

--[[
    if (enemyBullet && collision.tag == "Player") 
    {
        Player.instance.GetDamage(damage); 
        if (destroyedByCollision)
            Destruction();
    }
    else if (!enemyBullet && collision.tag == "Enemy")
    {
        collision.GetComponent<Enemy>().GetDamage(damage);
        if (destroyedByCollision)
            Destruction();
    }
--]]

function Projectile:OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") and self.isEnemy then
		local mainPlayer = PlayerMgr:GetMainPlayer()
        mainPlayer:AddHp(-1)

        CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Projectiles/Player_Short_Lazer.prefab", self.projectileObj)
    elseif collison.CompareTag("Enemy") and (not self.isEnemy) then
        -- enemy 删除掉
        -- 怎么找到是哪个enemy呢？
        local id = collison:GetComponent(typeof(Enemy)).ID
        

        CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Projectiles/Enemy_Straight_Projetile.prefab", self.projectileObj)
	end
end

function Projectile:SetPosition(position)
    self.projectileObj.transform.position = position
end

return Projectile

