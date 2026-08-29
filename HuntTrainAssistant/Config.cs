using Dalamud.Configuration;
using ECommons.ChatMethods;
using ECommons.Configuration;
using HuntTrainAssistant.DataStructures;

namespace HuntTrainAssistant;

public class Config : IEzConfig
{
    public bool Enabled = true;
    public bool AutoTeleport = true;
    public float AutoTeleportAetheryteDistanceDiff = 3f;
    public bool SuppressChatOtherPlayers = true;
    public List<Sender> Conductors = [];
    public bool Debug = false;
    public bool AutoOpenMap = true;
    public bool DistanceCompensationHack = false;
    public bool SonarIntegration = false;
    public bool HuntAlertsIntegration = false;
    public bool AutoVisitTeleportEnabled = false;
    public bool AutoVisitCrossWorld = false;
    public bool AutoVisitCrossDC = false;
    public bool AutoVisitModifyChat = true;
    public Dictionary<Rank, List<Expansion>> AutoVisitExpansionsBlacklist = [];
    public List<uint> AetheryteBlacklist = [148];
    public bool EnableSonarInstanceSwitching = false;
    public bool AutoSwitchInstanceTwoRanks = false;
    public bool AutoSwitchInstanceToOne = false;
    public bool NoDuplicateFlags = true;
    public bool ContextMenu = true;
    public bool AudioAlert = false;
    public float AudioAlertVolume = 0.5f;
    public bool AudioAlertOnlyMinimized = false;
    public string AudioAlertPath = "";
    public int AudioThrottle = 500;
    public bool FlashTaskbar = false;
    public bool TrayNotification = true;
    public bool ExecuteMacroOnFlag = false;
    public int MacroIndex = 0;
    public List<uint> WorldBlacklist = [];
    public string PfinderString = "";
    public bool PfinderEnable = false;
    public bool TeleportDelayEnabled = false;
    public int TeleportDelayMin = 200;
    public int TeleportDelayMax = 700;
    public bool ShowKilledARanks = true;
    public bool UseMount = true;
    public bool UseDRWorldTravelCommand = true;
    public int Mount = 0;

    // vnavmesh related
    public bool UseSafeStopDistance = false; // 寻路终点安全距离开关
    public float SafeStopDistance = 45; // 寻路终点安全距离
    public bool RandomDestinationOffset = true; // 寻路终点随机偏移
    public float RandomDestinationOffsetRadius = 6; // 寻路终点随机偏移范围半径

    public List<uint> SRankBlacklist = new(); // S怪黑名单列表

    // misc
    public bool BluPlaceholder = false; // 青魔招募占位
    public bool ForceGroundPathfinding = false; //强制使用地面寻路

    // 车头链接自动寻路
    public bool AutoPathfindOnConductorLink = false; // 车头发送地图位置时自动寻路前往
    public bool AutoPathfindAfterTeleport = true; // 跨地图传送后接力寻路
    public bool AutoPathfindUseSafeDistance = true; // 车头链接寻路安全距离开关
    public float AutoPathfindSafeDistance = 20f; // 车头链接寻路安全距离
}
