---
---                 ColaFramework
--- Copyright © 2018-2049 ColaFramework 马三小伙儿
---              Start_Controller Controller业务逻辑
---

--- 公有字段和方法
local public = {}
--- 私有字段和方法
local private = {}

--- Controller模块的初始化，可以在这里做初始化和添加监听等操作
function public.OnInit()
	
end

--- Controller模块的销毁，可以在这里做清理工作和取消监听等操作
function public.OnDestroy()
	
end

function public:Start()
	LevelMgr:Start()
end

function public:Restart()

end


return public