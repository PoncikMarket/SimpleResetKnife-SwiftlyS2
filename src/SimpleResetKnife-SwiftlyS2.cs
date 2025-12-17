using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using SwiftlyS2.Shared;
using SwiftlyS2.Shared.Plugins;
using SwiftlyS2.Shared.Commands;
using SwiftlyS2.Shared.Players;
using SwiftlyS2.Shared.SchemaDefinitions; 

namespace SimpleResetKnife_SwiftlyS2;

[PluginMetadata(Id = "SimpleResetKnife", Version = "1.0.0", Name = "SimpleResetKnife", Author = "PoncikMarket", Description = "Reset Knife Plugin")]
public class SimpleResetKnife : BasePlugin
{
    private PluginConfig _config = new PluginConfig();
    private Dictionary<int, long> _cooldowns = new();

    public SimpleResetKnife(ISwiftlyCore core) : base(core) { }

    public override void Load(bool hotReload)
    {
        Core.Configuration.InitializeJsonWithModel<PluginConfig>("config.json", "Main");

        var configSection = Core.Configuration.Manager.GetSection("Main");
        var loadedConfig = configSection.Get<PluginConfig>();
        
        if (loadedConfig != null)
        {
            _config = loadedConfig;
        }
    }

    public override void Unload() { }

    [Command("resetknife")] 
    [CommandAlias("rk")]
    public void OnResetKnifeCommand(ICommandContext context)
    {
        if (!context.IsSentByPlayer) return;

        IPlayer? player = context.Sender;
        if (player == null || !player.IsValid) return;

        var localizer = Core.Translation.GetPlayerLocalizer(player);
        
        string prefixRaw = ReplaceColors(_config.Prefix);

        var pawn = player.PlayerPawn;
        if (pawn == null) return;

        bool hasKnife = false;

        if (pawn.WeaponServices != null)
        {
            foreach (var weaponHandle in pawn.WeaponServices.MyWeapons)
            {
                var weapon = weaponHandle.Value;
                if (weapon == null) continue;

                if (weapon.DesignerName.Contains("knife") || weapon.DesignerName.Contains("bayonet"))
                {
                    hasKnife = true;
                    break; 
                }
            }
        }

        if (hasKnife)
        {
            string msg = localizer["error_has_knife"];
            msg = msg.Replace("{PREFIX}", prefixRaw);
            player.SendChat(ReplaceColors(msg));
            return;
        }

        long currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        int playerSlot = player.Slot;

        if (_cooldowns.TryGetValue(playerSlot, out long lastUsed))
        {
            int cooldownTime = _config.CooldownSeconds;

            if (currentTime - lastUsed < cooldownTime)
            {
                long remaining = cooldownTime - (currentTime - lastUsed);
                
                string msg = localizer["error_cooldown"]; 
                msg = msg.Replace("{PREFIX}", prefixRaw);
                msg = ReplaceColors(msg);
                player.SendChat(string.Format(msg, remaining));
                return;
            }
        }

        var controller = player.Controller;
        if (controller == null) return;

        int teamNum = controller.TeamNum; 
        string knifeName = "";

        if (teamNum == 2) knifeName = "weapon_knife_t";
        else if (teamNum == 3) knifeName = "weapon_knife";
        else
        {
            string msg = localizer["error_team"];
            msg = msg.Replace("{PREFIX}", prefixRaw);
            player.SendChat(ReplaceColors(msg));
            return;
        }

        try 
        {
            if (pawn.ItemServices != null)
            {
                pawn.ItemServices.GiveItem(knifeName);
                
                string msg = localizer["knife_given"];
                msg = msg.Replace("{PREFIX}", prefixRaw);
                player.SendChat(ReplaceColors(msg));

                _cooldowns[playerSlot] = currentTime;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ResetKnife Error] Hata: {ex.Message}");
        }
    }

    private string ReplaceColors(string message)
    {
        return message
            .Replace("{Default}", Helper.ChatColors.Default)
            .Replace("{White}", Helper.ChatColors.White)
            .Replace("{DarkRed}", Helper.ChatColors.DarkRed)
            .Replace("{Green}", Helper.ChatColors.Green)
            .Replace("{LightYellow}", Helper.ChatColors.LightYellow)
            .Replace("{LightBlue}", Helper.ChatColors.LightBlue)
            .Replace("{Olive}", Helper.ChatColors.Olive)
            .Replace("{Lime}", Helper.ChatColors.Lime)
            .Replace("{Red}", Helper.ChatColors.Red)
            .Replace("{Purple}", Helper.ChatColors.Purple)
            .Replace("{Grey}", Helper.ChatColors.Grey)
            .Replace("{Yellow}", Helper.ChatColors.Yellow)
            .Replace("{Gold}", Helper.ChatColors.Gold)
            .Replace("{Silver}", Helper.ChatColors.Silver)
            .Replace("{Blue}", Helper.ChatColors.Blue)
            .Replace("{DarkBlue}", Helper.ChatColors.DarkBlue)
            .Replace("{BlueGrey}", Helper.ChatColors.BlueGrey)
            .Replace("{Magenta}", Helper.ChatColors.Magenta)
            .Replace("{LightRed}", Helper.ChatColors.LightRed);
    }
}