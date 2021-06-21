local Planet = Class("Planet")
local common = require("Common.Common")

-- 理论上行星应该有一张表来配置
-- 
function Planet:initialize(path)
	-- 创建行星对象
	self.planetObj = CommonUtil.InstantiatePrefab(path, nil)

	-- 后面把DirectMoving 这个组件lua化
	self.planetObj:GetComponent("DirectMoving").speed = 1.0;
end


return Planet