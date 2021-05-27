local BonusLua = class("Bonus")

-- 能量球奖励类 玩家触碰到获取奖励，子弹变异
function BonusLua:initialize()
	Bonus.OnTriggerEnter2DLua = BonusLua.OnTriggerEnter2D
end

function BonusLua:OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") then
		local mainPlayerPrefab = PlayerMgr:GetMainPlayer():GetInstance()
		local playerShoot = mainPlayerPrefab:GetComponent("PlayerShooting")
		if playerShoot.instance.weaponPower < playerShoot.instance.maxweaponPowerthen then
			playerShoot.instance.weaponPower = playerShoot.instance.weaponPower + 1
		end

		UnityEngine.Object.Destroy(object)
	end
end

return BonusLua

