using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBots.DataTypes;

namespace ClapBot.Commands
{
  public class ReactPersonAdd : ModuleBase<SocketCommandContext>
  {
    [Command("AddReactUser"), Summary("Adds user to be reacted to")]
    public async Task _ReactPersonAdd()
    {
      await Context.Message.DeleteAsync();
      if (Starter.PriorityIds.Contains(Context.User.Discriminator))
      {
        string usersAdded = string.Empty;
        foreach (SocketUser user in Context.Message.MentionedUsers)
        {
          var reactUsers = await SaveSystem.GetReactUser();
          if (!reactUsers.Contains(user.Id))
          {
            usersAdded += user.Username + ", ";
            await SaveSystem.AddReactUser(user.Id);
            await ClientConsole.Log(new TargetedCommandMessage("AddReactUser", Context, user));
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
      if (Starter.PriorityIds.Contains(Context.User.Discriminator))
      {
        string usersRemoved = string.Empty;
        foreach (SocketUser user in Context.Message.MentionedUsers)
        {
          var reactUsers = await SaveSystem.GetReactUser();
          if (reactUsers.Contains(user.Id))
          {
            usersRemoved += user.Username + ", ";
            await SaveSystem.RemoveReactUser(user.Id);
            await ClientConsole.Log(new TargetedCommandMessage("RemoveReactUser", Context, user));
          }
        }
      }
    }
  }
}