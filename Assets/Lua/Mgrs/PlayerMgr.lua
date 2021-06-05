local PlayerMgr = {}
local Player = require("Game.Logic.Player")

-- 点号相当于静态方法
-- 冒号相当于成员方法
function PlayerMgr:Initialize()
	self.mapPlayers = {}
	self.mainPlayer = nil
end

function PlayerMgr:CreatePlayer(data, ismain)
	if data == nil then
		return nil
	end

	if data.id == nil then
		return nil
	end

	if self.mapPlayers[data.id] ~= nil then
		return self.mapPlayers[data.id]
	end

	-- create player
	local player = Player:new(data, ismain)
	self.mapPlayers[data.id] = player

	if ismain then
		self.mainPlayer = player
	end

	return player
end

function PlayerMgr:GetMainPlayer()
	-- body
	return self.mainPlayer
end


return PlayerMgr