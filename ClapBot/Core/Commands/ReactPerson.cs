using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace ClapBot.Core.Commands
{
  public class ReactPersonAdd : ModuleBase<SocketCommandContext>
  {
    [Command("AddReactUser"), Summary("Adds user to be reacted to")]
    public async Task _ReactPersonAdd()
    {
      if (Starter.PriorityIds.Contains(Context.User.Discriminator))
      {
        string usersAdded = string.Empty;
        foreach (var user in Context.Message.MentionedUsers)
        {
          if (!MessageHandler.usersToReactTo.Contains(user))
          {
            usersAdded += user.Username + ", ";
            MessageHandler.usersToReactTo.Add(user);
          }
        }
        ClientConsole.Log($"Adding {usersAdded} to react list");
      }
      await Context.Message.DeleteAsync();
    }
  }

  public class ReactPersonRemove : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveReactUser"), Summary("Removes user to be reacted to")]
    public async Task _ReactPersonRemove()
    {
      if (Starter.PriorityIds.Contains(Context.User.Discriminator))
      {
        string usersRemoved = string.Empty;
        foreach (var user in Context.Message.MentionedUsers)
        {
          if (MessageHandler.usersToReactTo.Contains(user))
          {
            usersRemoved += user.Username + ", ";
            MessageHandler.usersToReactTo.Remove(user);
          }
        }
        ClientConsole.Log($"Removing {usersRemoved} from react list");
      }
      await Context.Message.DeleteAsync();
    }
  }
}