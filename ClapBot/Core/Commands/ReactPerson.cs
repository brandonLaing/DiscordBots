using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace ClapBot.Core.Commands
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
          }
        }
        if (usersAdded != string.Empty)
          await ClientConsole.Log(new LogMessage(LogSeverity.Info, "Command-React User", $"Adding {usersAdded} to react list by {Context.User.Username}"));
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
          }
        }
        if (usersRemoved != string.Empty)
          await ClientConsole.Log(new LogMessage(LogSeverity.Info, "Command-React User", $"Removing {usersRemoved} from react list by {Context.User.Username}"));
      }
    }
  }
}