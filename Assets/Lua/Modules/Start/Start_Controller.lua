
-- ���⽱��
local BonusLua = {}

function BonusLua.Initialize()
	Bonus.OnTriggerEnter2DLua = BonusLua.OnTriggerEnter2D
end

-- ��ײ����
function BonusLua.OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") 
		-- �������++

		UnityEngine.Object.Destroy(object)
	end
end

return BonusLua
