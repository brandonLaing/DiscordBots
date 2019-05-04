using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ClapBot.Core.Commands
{
  public class MockAddPerson : ModuleBase<SocketCommandContext>
  {
    [Command("AddMockUser"), Summary("Starts mocking a user")]

    public async Task _MockAddPerson()
    {
      await Context.Message.DeleteAsync();
      string usersAdded = string.Empty;
      foreach (var user in Context.Message.MentionedUsers)
      {
        if (!SaveSystem.Mocked.Contains(user) && (Starter.PriorityIds.Contains(Context.User.Discriminator) || Context.User == user))
        {
          usersAdded += user.Username + ", ";
          Console.WriteLine($"Adding {user.Id} to moked");
          SaveSystem.Mocked.Add(user);
        }
      }
      if (usersAdded != string.Empty)
        await ClientConsole.Log(new LogMessage(LogSeverity.Info, "Command Mock User", $"Added {usersAdded} to mock list by {Context.User.Username}"));
    }
  }
  

  public class MockRemovePerson : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveMockUser"), Summary("Stops mocking a user")]
    public async Task _MockRemovePerson()
    {
      await Context.Message.DeleteAsync();

      if (Starter.PriorityIds.Contains(Context.User.Discriminator))
      {
        string usersRemoved = string.Empty;
        foreach (var user in Context.Message.MentionedUsers)
        {
          if (SaveSystem.Mocked.Contains(user))
          {
            usersRemoved += user.Username + ", ";
            SaveSystem.Mocked.Remove(user);
          }
        }
        if (usersRemoved != string.Empty)
          await ClientConsole.Log(new LogMessage(LogSeverity.Info, "Command Mock User", $"Removed {usersRemoved} from mock list by {Context.User.Username}"));
      }
    }
  }
}
