
---
---                 ColaFramework
--- Copyright ? 2018-2049 ColaFramework 马三小伙儿
---                   登录界面
---

local UIBase = require("Core.ui.UIBase")
local UIStartPanel = Class("UIStartPanel", UIBase)
local i18n = require("3rd.i18n.init")


local _instance = nil

function UIStartPanel.Instance()
    if nil == _instance then
        _instance = UIStartPanel:new()
    end
    return _instance
end

function UIStartPanel:InitParam()
    self.uiDepthLayer = ECEnumType.UIDepth.NORMAL
    self:ShowUIMask(false)
    print(i18n.translate("hello"))
end

-- override UI面板创建结束后调用，可以在这里获取gameObject和component等操作
function UIStartPanel:OnCreate()
    
end

-- 界面可见性变化的时候触发
function UIStartPanel:OnShow(isShow)
    CommonUtil.PlayMultipleSound("Audio/2d/welcome.mp3")
end

function UIStartPanel:onClick(name)
    if name == "game_start_btn" then
        Ctrl.Start.NextLevel()
    elseif name == "game_restart_btn" then
        
    end
end

-- 界面销毁的过程中触发
function UIStartPanel:OnDestroy()
    UIBase.OnDestroy(self)
end

return UIStartPanel