-- �ؿ�
local Level = Class("Level")

function Level:Initialize(cfg)
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
	if levelC = nil 
		levelC = obj.AddSingleComponent(typeof(LevelController))
	end




	-- ��ʼ��LevelController ���������
end


return Level