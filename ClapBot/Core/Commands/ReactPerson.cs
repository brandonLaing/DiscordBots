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
      await Context.Message.DeleteAsync();
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
        if (usersAdded != string.Empty)
          ClientConsole.Log($"Adding {usersAdded} to react list");
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
        foreach (var user in Context.Message.MentionedUsers)
        {
          if (MessageHandler.usersToReactTo.Contains(user))
          {
            usersRemoved += user.Username + ", ";
            MessageHandler.usersToReactTo.Remove(user);
          }
        }
        if (usersRemoved != string.Empty)
          ClientConsole.Log($"Removing {usersRemoved} from react list");
      }
    }
  }
}