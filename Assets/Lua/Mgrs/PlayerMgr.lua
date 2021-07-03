local PlayerMgr = {}
local Player = require("Game.Logic.Player")

-- 点号相当于静态方法
-- 冒号相当于成员方法
function PlayerMgr:Initialize()
	self.mapPlayers = {}
	self.mainPlayer = nil
end

function PlayerMgr:CreatePlayer(data, ismain, id)
	if data == nil then
		return nil
	end

	-- if data.id == nil then
	-- 	return nil
	-- end

	if self.mapPlayers[id] ~= nil then
		return self.mapPlayers[id]
	end

	-- create player
	local player = Player:new(data, ismain, id)
	self.mapPlayers[id] = player

	if ismain then
		self.mainPlayer = player
		
	end

	return player
end

function PlayerMgr:GetMainPlayer()
	-- body
	return self.mainPlayer
end

function PlayerMgr:GetPlayer(id)
	return self.mapPlayers[id]
end

return PlayerMgr