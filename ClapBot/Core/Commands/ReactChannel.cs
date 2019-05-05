﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ClapBot.Core.Commands
{
  public class ReactChannelAdd : ModuleBase<SocketCommandContext>
  {
    [Command("AddReactChannel"), Summary("Adds channel to be reacted to")]
    public async Task _ReactChannelAdd()
    {
      if (Starter.PriorityIds.Contains(Context.User.Discriminator))
      {
        var reactChannels = await SaveSystem.GetReactChannel();
        if (!reactChannels.Contains(Context.Channel.Id))
        {
          await SaveSystem.AddReactChannel(Context.Channel.Id);
          await ClientConsole.Log(new LogMessage(LogSeverity.Info, "Command-React Channel", $"Adding {Context.Channel.Name} to react channels by {Context.User.Username}"));
          await Context.Channel.SendMessageAsync("Starting reactions in this channel");
        }
      }
    }
  }

  public class ReactChannelRemove : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveReactChannel"), Summary("Removes channel to be reacted to")]
    public async Task _ReactChannelRemove()
    {
      if (Starter.PriorityIds.Contains(Context.User.Discriminator))
      {
        var reactChannels = await SaveSystem.GetReactChannel();
        if (reactChannels.Contains(Context.Channel.Id))
        {
          await SaveSystem.RemoveReactChannel(Context.Channel.Id);
          await ClientConsole.Log(new LogMessage(LogSeverity.Info, "Command-React Channel", $"Removing {Context.Channel.Name} from react channels by {Context.User.Username}"));
          await Context.Channel.SendMessageAsync("Stopping reactions in this channel");
        }
      }
    }
  }
}
