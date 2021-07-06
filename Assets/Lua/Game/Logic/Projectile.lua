local Projectile = Class("Projectile")
local common = require("Common.Common")


function Projectile:initialize(isenemy, baseid)
    -- 根据baseid得到基础的数据
    self.baseid = baseid
    -- 根据id可以得到各种数据
    self.id = common.GenerateID()
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


function Projectile:OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") and self.isEnemy then
        local id = collison:GetComponent(typeof(Player)).ID
        local player = PlayerMgr:GetPlayer(id)
        player:AddHp(-1)

        -- 销毁敌人子弹
        self:Destroy()

    elseif collison.CompareTag("Enemy") and (not self.isEnemy) then
        -- enemy 删除掉
        -- 怎么找到是哪个enemy呢？
        local id = collison:GetComponent(typeof(Enemy)).ID
        local enemy = PlayerMgr:GetPlayer(id)
        enemy:AddHp(-1)

        -- 销毁玩家子弹
        self:Destroy()

	end
end

function Projectile:SetPosition(position)
    self.projectileObj.transform.position = position
end

function Projectile:Destroy()
    if self.isEnemy then
        CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Projectiles/Enemy_Straight_Projetile.prefab", self.projectileObj)
    else
        CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Projectiles/Player_Short_Lazer.prefab", self.projectileObj)
    end
end

return Projectile

