local BonusLua = class("Bonus")

function BonusLua.initialize()
	Bonus.OnTriggerEnter2DLua = BonusLua.OnTriggerEnter2D
end


function BonusLua.OnTriggerEnter2D(collison, object)
	if collison.CompareTag("Player") 
	

		UnityEngine.Object.Destroy(object)
	end
end

return BonusLua
