local BonusLua = class("Bonus")
local common = require("Common.Common")

-- 能量球奖励类 玩家触碰到获取奖励，子弹变异
function BonusLua:initialize()
	self.powerObj = CommonUtil.InstantiatePrefab("Arts/Plane/Animation/Bonuses/Power Up.prefab", nil)

	local mainPlayerPrefab = PlayerMgr:GetMainPlayer():GetInstance()
	local playerMove = mainPlayerPrefab:GetComponent("PlayerMoving")

	local mainCamera = CommonUtil.GetMainCamera()

	self.powerObj.transform.position = Vector3.New(common.Random(playerMove.instance.borders.minX, playerMove.instance.borders.maxX),0, 
		mainCamera:ViewportToWorldPoint(Vector3.up) + self.powerObj:GetComponent("Renderer").bounds.size.y // 2)

	local bonusC = self.powerObj:GetComponent("Bonus")
	-- bind collison 
	bonusC.OnTriggerEnter2DLua = BonusLua.OnTriggerEnter2D
end

function BonusLua:OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") then
		local mainPlayerPrefab = PlayerMgr:GetMainPlayer():GetInstance()
		local playerShoot = mainPlayerPrefab:GetComponent("PlayerShooting")
		if playerShoot.instance.weaponPower < playerShoot.instance.maxweaponPowerthen then
			playerShoot.instance.weaponPower = playerShoot.instance.weaponPower + 1
		end
		
		-- destroy bonus
		CommonUtil.ReleaseGameObject("Arts/Plane/Prefabs/Game_Controller.prefab", self.powerObj)

		-- UnityEngine.Object.Destroy(object)
	end
end

return BonusLua

