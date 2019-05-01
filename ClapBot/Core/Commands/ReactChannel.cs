﻿using System;
using System.Linq;
using System.Threading.Tasks;
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
        if (!MessageHandler.channelsToReactIn.Contains(Context.Channel))
        {
          MessageHandler.channelsToReactIn.Add(Context.Channel);
          ClientConsole.Log($"Adding {Context.Channel.Name} to react channels");
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
        if (MessageHandler.channelsToReactIn.Contains(Context.Channel))
        {
          MessageHandler.channelsToReactIn.Remove(Context.Channel);
          ClientConsole.Log($"Removing {Context.Channel.Name} to react channels");
          await Context.Channel.SendMessageAsync("Stopping reactions in this channel");
        }
      }
    }
  }
}
