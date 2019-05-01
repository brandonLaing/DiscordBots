using System.Linq;
using System.Threading.Tasks;
using Discord.Commands;

namespace ClapBot.Core.Commands
{
  public class MockAddPerson : ModuleBase<SocketCommandContext>
  {
    [Command("AddMockUser"), Summary("Starts mocking a user")]

    public async Task _MockAddPerson()
    {
      if (Starter.PriorityIds.Contains(Context.User.Discriminator))
      {
        string usersAdded = string.Empty;
        foreach (var user in Context.Message.MentionedUsers)
        {
          if (!MessageHandler.mocked.Contains(user))
          {
            usersAdded += user.Username + ", ";
            MessageHandler.mocked.Add(user);
          }
        }
        ActionLog.ClientLog($"Adding {usersAdded} to mock list");
      }
      await Context.Message.DeleteAsync();
    }
  }

  public class MockRemovePerson : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveMockUser"), Summary("Stops mocking a user")]
    public async Task _MockRemovePerson()
    {
      if (Starter.PriorityIds.Contains(Context.User.Discriminator))
      {
        string usersRemoved = string.Empty;
        foreach (var user in Context.Message.MentionedUsers)
        {
          if (MessageHandler.mocked.Contains(user))
          {
            usersRemoved += user.Username + ", ";
            MessageHandler.mocked.Remove(user);
          }
        }
        ActionLog.ClientLog($"Removing {usersRemoved} from mock list");
      }
      await Context.Message.DeleteAsync();
    }
  }
}
