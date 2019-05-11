using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBots.DataTypes;

namespace ClapBot.Commands
{
  public class ReactPersonAdd : ModuleBase<SocketCommandContext>
  {
    [Command("AddReactUser"), Summary("Adds clap to someones message")]
    public async Task _ReactPersonAdd()
    {
      await Context.Message.DeleteAsync();

      List<ulong> reactUsers = await SaveSystem.GetReactUser();
      IReadOnlyCollection<SocketUser> mentioned = Context.Message.MentionedUsers;

      if (mentioned.Count == 0 && !reactUsers.Contains(Context.User.Id))
      {
        await SaveSystem.AddReactUser(Context.User.Id);
        await ClientConsole.Log(new TargetedCommandMessage("AddReactUser", Context, Context.Channel));
        await Context.Channel.SendMessageAsync($"Starting to add claps to messages from {Context.User.Mention}");
        return;
      }

      List<ulong> adminIds = await SaveSystem.GetAdminIds();
      if (adminIds.Contains(Context.User.Id) || adminIds.Count == 0)
      {
        foreach (SocketUser user in mentioned)
        {
          if (!reactUsers.Contains(user.Id))
          {
            await SaveSystem.AddReactUser(user.Id);
            await ClientConsole.Log(new TargetedCommandMessage("AddReactUser", Context, user));
            await Context.Channel.SendMessageAsync($"Starting to add claps to messages from {user.Mention}");
          }
        }
      }
    }
  }

  public class ReactPersonRemove : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveReactUser"), Summary("Removes user to be reacted to")]
    public async Task _ReactPersonRemove()
    {
      await Context.Message.DeleteAsync();

      List<ulong> reactUsers = await SaveSystem.GetReactUser();
      IReadOnlyCollection<SocketUser> mentioned = Context.Message.MentionedUsers;

      if (mentioned.Count == 0 && reactUsers.Contains(Context.User.Id))
      {
        await SaveSystem.RemoveReactUser(Context.User.Id);
        await ClientConsole.Log(new TargetedCommandMessage("RemoveReactUser", Context, Context.User));
        await Context.Channel.SendMessageAsync($"Stopped adding claps to messages from {Context.User.Mention}");
        return;
      }

      List<ulong> adminIds = await SaveSystem.GetAdminIds();
      if (adminIds.Contains(Context.User.Id) || adminIds.Count == 0)
      {
        foreach (SocketUser user in mentioned)
        {
          if (reactUsers.Contains(user.Id))
          {
            await SaveSystem.RemoveReactUser(user.Id);
            await ClientConsole.Log(new TargetedCommandMessage("RemoveReactUser", Context, user));
            await Context.Channel.SendMessageAsync($"Stopped adding claps to messages from {user.Mention}");
          }
        }
      }
    }
  }
}