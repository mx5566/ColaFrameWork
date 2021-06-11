local WaveLua = class("Wave")
local common = require("Common.Common")


function WaveLua:initialize()
	self.powerObj = CommonUtil.InstantiatePrefab("Arts/Plane/Animation/Bonuses/Power Up.prefab", nil)


end

function WaveLua:CreateEnemyWave()
    
end

return WaveLua

