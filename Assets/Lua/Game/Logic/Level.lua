-- �ؿ�
local Level = Class("Level")

function Level:initialize(cfg)
	self.name = cfg.Name
	self.level = cfg.Level
	self.cfg = cfg
end

function Level:Start()
	-- body
	-- ��Ҫȥ�����ײ�
	-- ���ݴ����c#��
	local obj = UnityEngine.GameObject.Find("��ȡ����")
	local levelC = obj.GetComponent(typeof(LevelController))
	if levelC = nil then
		levelC = obj.AddSingleComponent(typeof(LevelController))
	end


	-- ��ʼ��LevelController ���������
end

function Level:GetLevel()
	return self.Level
end


return Level