using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using DiscordBots.DataTypes;

namespace ClapBot.Commands
{
  public class ReactChannelAdd : ModuleBase<SocketCommandContext>
  {
    [Command("AddReactChannel"), Summary("Adds a clap to each message the channel")]
    public async Task _ReactChannelAdd()
    {
      List<ulong> adminIds = await SaveSystem.GetAdminIds();
      if (adminIds.Contains(Context.User.Id) || adminIds.Count == 0)
      {
        List<ulong> reactChannels = await SaveSystem.GetReactChannel();
        if (!reactChannels.Contains(Context.Channel.Id))
        {
          await SaveSystem.AddReactChannel(Context.Channel.Id);
          await ClientConsole.Log(new TargetedCommandMessage("AddReactChannel", Context, Context.Channel));
          await Context.Channel.SendMessageAsync("Starting reactions in this channel");
        }
      }
      await Context.Message.DeleteAsync();
    }
  }

  public class ReactChannelRemove : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveReactChannel"), Summary("Removes channel to be reacted to")]
    public async Task _ReactChannelRemove()
    {
      await Context.Message.DeleteAsync();

      List<ulong> adminIds = await SaveSystem.GetAdminIds();
      if (adminIds.Contains(Context.User.Id) || adminIds.Count == 0)
      {
        List<ulong> reactChannels = await SaveSystem.GetReactChannel();
        if (reactChannels.Contains(Context.Channel.Id) || adminIds.Count == 0)
        {
          await SaveSystem.RemoveReactChannel(Context.Channel.Id);
          await ClientConsole.Log(new TargetedCommandMessage("RemoveReactChannel", Context, Context.Channel));
          await Context.Channel.SendMessageAsync("Stopping reactions in this channel");
        }
      }
    }
  }
}
