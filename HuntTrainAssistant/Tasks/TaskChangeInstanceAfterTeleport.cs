using ECommons.Automation;
using ECommons.ExcelServices;
using ECommons.GameHelpers;
using ECommons.MathHelpers;
using ECommons.Throttlers;
using FFXIVClientStructs.FFXIV.Client.Game.UI;
using Lumina.Excel.Sheets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTrainAssistant.Tasks;
public static class TaskChangeInstanceAfterTeleport
{
    public static void Enqueue(Number num, Number territory)
    {
        var direct = false; // 已直接发起切线，跳过走近水晶的兜底流程
        P.TaskManager.Enqueue(() => ECommons.GameHelpers.Player.Territory.RowId == territory && Player.Interactable);
        P.TaskManager.Enqueue(() =>
        {
            if(!(S.LifestreamIPC.GetNumberOfInstances() == 0 || num == 0 || S.LifestreamIPC.GetCurrentInstance() == num))
            {
                P.TaskManager.InsertStack(() =>
                {
                    P.TaskManager.Enqueue(() => IsScreenReady() && Player.Interactable);
                    // 等待 Lifestream 自身任务(如传送)结束，否则 CanChangeInstance 恒为 false
                    P.TaskManager.Enqueue(() => !S.LifestreamIPC.IsBusy(), new(timeLimitMS: 5000));
                    // 稳定期，避免刚到达时的瞬态误判
                    P.TaskManager.Enqueue(() => EzThrottler.Throttle("HTAInstanceSwitchGrace", 500));
                    // 优先直接切线：可切线时不选中水晶
                    P.TaskManager.Enqueue(() =>
                    {
                        if(S.LifestreamIPC.GetCurrentInstance() == num) return true;
                        if(!S.LifestreamIPC.CanChangeInstance()) return false;
                        direct = true;
                        PluginLog.Information($"Changing instance directly to {num}");
                        S.LifestreamIPC.ChangeInstance(num);
                        return true;
                    }, new(timeLimitMS: 3000));
                    // 兜底：仅当未能直接切线时，走近水晶
                    P.TaskManager.Enqueue(() =>
                    {
                        if(direct || S.LifestreamIPC.GetCurrentInstance() == num) return true;
                        if(S.LifestreamIPC.CanChangeInstance())
                        {
                            direct = true;
                            S.LifestreamIPC.ChangeInstance(num);
                            return true;
                        }
                        var nearestAetheryte = Svc.Objects.Where(x => x.ObjectKind == Dalamud.Game.ClientState.Objects.Enums.ObjectKind.Aetheryte && x.IsTargetable).OrderBy(x => Vector3.Distance(Player.Position, x.Position)).FirstOrDefault();
                        if(nearestAetheryte != null)
                        {
                            if(nearestAetheryte.IsTarget() && EzThrottler.Throttle("Lockon"))
                            {
                                Chat.ExecuteCommand("/lockon");
                                P.TaskManager.Insert(() => Chat.ExecuteCommand("/automove on"));
                                return true;
                            }
                            else
                            {
                                if(EzThrottler.Throttle("SetTarget"))
                                {
                                    Svc.Targets.Target = nearestAetheryte;
                                }
                                return false;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    });
                    P.TaskManager.Enqueue(() =>
                    {
                        if(S.LifestreamIPC.GetCurrentInstance() == num) return true;
                        if(S.LifestreamIPC.CanChangeInstance())
                        {
                            Chat.ExecuteCommand("/automove off");
                            S.LifestreamIPC.ChangeInstance(num);
                            return true;
                        }
                        return false;
                    }, new(timeLimitMS: 15000));
                });
            }
        });

    }
}
