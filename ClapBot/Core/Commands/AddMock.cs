using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Discord;
using Discord.Commands;

namespace ClapBot.Core.Commands
{
  public class AddMockPerson : ModuleBase<SocketCommandContext>
  {
    [Command("AddMock"), Summary("mocks selected user")]

    public async Task AddMock()
    {
      if (Context.User.Discriminator == Starter.MyId)
      {
        string usersBeingAdded = string.Empty;
        foreach(var user in Context.Message.MentionedUsers)
        {
          if (!MessageHandler.mocked.Contains(user))
          {
            usersBeingAdded += user.Username + ", ";
            MessageHandler.mocked.Add(user);
          }
        }

        ActionLog.ClientLog($"Adding {usersBeingAdded} to mock list");
      }

      await Context.Message.DeleteAsync();
    }
  }
}
