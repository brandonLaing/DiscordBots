using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using Discord;
using Discord.Commands;

namespace ClapBot.Core.Commands
{
  public class RemoveMock : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveMock"), Summary("stops mocking selected user")]

    public async Task StopMock()
    {
      ActionLog.ClientLog("Doing command mock");
      if (Context.User.Discriminator == Starter.MyId)
      {
        string usersBeingRemoved = string.Empty;
        foreach (var user in Context.Message.MentionedUsers)
        {
          if (MessageHandler.mocked.Contains(user))
          {
            usersBeingRemoved += user.Username + ", ";
            MessageHandler.mocked.Remove(user);
          }
        }
        ActionLog.ClientLog($"Removing {usersBeingRemoved} to mock list");
      }

      await Context.Message.DeleteAsync();
    }
  }
}
