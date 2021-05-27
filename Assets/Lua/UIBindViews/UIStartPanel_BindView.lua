--[[Notice:This lua uiview file is auto generate by UIViewExporter，don't modify it manually! --]]

local public = {}
local cachedViews = nil

public.viewPath = "Arts/UI/Prefabs/UIStartPanel.prefab"

function public.BindView(uiView, Panel)
	cachedViews = {}
	if nil ~= Panel then
		local collection = Panel:GetComponent("UIComponentCollection")
		if nil ~= collection then
			uiView.m_game_start_btn = collection:Get(0)
			uiView.m_game_restart_btn = collection:Get(1)
		else
			error("BindView Error! UIComponentCollection is nil!")
		end
	else
		error("BindView Error! Panel is nil!")
	end
end

function public.UnBindView(uiView)
	cachedViews = nil
	uiView.m_game_start_btn = nil
	uiView.m_game_restart_btn = nil
end

return public