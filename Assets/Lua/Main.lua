--主入口函数。从这里开始lua逻辑
local rawset = rawset

local common = require("Common.Common")

-- 全局函数
-- 用于声明全局变量
function define(name, value)
    rawset(_G, name, value)
end

local function initialize()
    LuaLogHelper.initialize()
    NetManager.Initialize()

    --UIManager初始化
    UIManager.initialize()
    --LevelMgr初始化
    LevelMgr.Initialize(LevelMgr)
    PlayerMgr:Initialize()

    -- 模块开始加载
    Modules.PriorityBoot()
end

-- 在此处定义注册一些全局变量
local function gloablDefine()
    require("Common.ECEnumType")
    require("Common.LuaAppConst")
    -- 必须首先注册全局Class,顺序敏感
    _G.Class = require("Core.middleclass")
    define("LuaLogHelper", require("Utilitys.LuaLogHelper"))
    _G.EventMgr = require("Mgrs.EventMgr")
    require("Game.Main.Modules")
    require("Game.Main.GUICollections")
    require("Game.Main.I18NCollections")
    -- 模块初始化
    Modules.Initialize()
    _G.UIManager = require("Mgrs.UIManager")
    _G.ConfigMgr = require("Mgrs.ConfigMgr")
    _G.Protocol = require("Protocols.Protocol")
    _G.NetManager = require("Core.Net.NetManager")
    -- 初始化关卡管理器
    _G.LevelMgr = require("Mgrs.LevelMgr")
    _G.PlayerMgr = require("Mgrs.PlayerMgr")
    _G.LID = {LID = 0}
    --控制全局变量的新建与访问
    require("Utilitys.LuaGlobalCheck")
    
end

-- 初始化一些参数
local function initParam()
    -- 初始化随机种子
    math.randomseed(tostring(os.time()):reverse():sub(1, 6))

    --垃圾收集器间歇率控制着收集器需要在开启新的循环前要等待多久。 增大这个值会减少收集器的积极性。
    --当这个值比 100 小的时候，收集器在开启新的循环前不会有等待。 设置这个值为 200 就会让收集器等到总内存使用量达到之前的两倍时才开始新的循环。
    collectgarbage("setpause", 99)
    --垃圾收集器步进倍率控制着收集器运作速度相对于内存分配速度的倍率。 增大这个值不仅会让收集器更加积极，还会增加每个增量步骤的长度。
    --不要把这个值设得小于 100 ， 那样的话收集器就工作的太慢了以至于永远都干不完一个循环。 默认值是 200 ，这表示收集器以内存分配的"两倍"速工作。
    collectgarbage("setstepmul", 2000)
    --重启垃圾收集器的自动运行
    collectgarbage("restart")
end

function Main()
    gloablDefine()
    initParam()
    initialize()

    if false then
        UIManager.Open(ECEnumType.UIEnum.Loading)
        CommonUtil.GetSceneMgr():LoadSceneAsync("xinshoucun", function(sceneName)
            print("------>LoadSceneAsync  xinshoucun finished")
            EventMgr.DispatchEvent(Modules.moduleId.Common, Modules.notifyId.Common.CREATE_PANEL, ECEnumType.UIEnum.Login)
            UIManager.Close(ECEnumType.UIEnum.Loading)
        end)
    else
        -- myspace
        -- 正常应该先切到登录场景
        -- 账号密码验证通过之后切入到游戏场景
        UIManager.Open(ECEnumType.UIEnum.Loading)
        CommonUtil.GetSceneMgr():LoadSceneAsync("Demo_Scene", function(sceneName)
            Modules.Boot()
            -- EventMgr.DispatchEvent(Modules.moduleId.Common, Modules.notifyId.Common.CREATE_PANEL, ECEnumType.UIEnum.Login)
            -- 创建玩家对象
            Time.timeScale = 0
            PlayerMgr:CreatePlayer({id=1, name= "plane"}, true, common.GenerateID())
            
            EventMgr.DispatchEvent(Modules.moduleId.Common, Modules.notifyId.Common.CREATE_PANEL, ECEnumType.UIEnum.GameStart)
            UIManager.Close(ECEnumType.UIEnum.Loading)
        end)
    end

    ColaHelper.Update = Update
end

function Update(delta)
    PlayerMgr:Update(delta)
end

--场景切换通知
function OnLevelWasLoaded(level)
    collectgarbage("collect")
    Time.timeSinceLevelLoad = 0
end

function OnApplicationQuit()
end
-- https://blog.csdn.net/weixin_34389926/article/details/91939395
-- 如何利用ndk编译c代码成动态库