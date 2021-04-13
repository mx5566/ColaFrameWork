
-- 额外奖励
local BonusLua = {}

function BonusLua.Initialize()
	Bonus.OnTriggerEnter2DLua = BonusLua.OnTriggerEnter2D
end

-- 碰撞触发
function BonusLua.OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") 
		-- 玩家能量++

		UnityEngine.Object.Destroy(object)
	end
end

return BonusLua
