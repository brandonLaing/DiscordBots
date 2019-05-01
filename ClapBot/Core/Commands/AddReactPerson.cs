using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClapBot.Core.Commands
{
  class AddReactPerson : ModuleBase<SocketCommandContext>
  {
    [Command("AddReactPerson"), Summary("Adds user to be reacted to")]
    public async Task AddPerson()
    {
      string usersBeingAdded = string.Empty;
      foreach (var user in Context.Message.MentionedUsers)
      {
        if (!MessageHandler.usersToReactTo.Contains(user))
        {
          usersBeingAdded += user.Username + ", ";
          MessageHandler.mocked.Add(user);
        }
      }
      ActionLog.ClientLog($"Adding {usersBeingAdded} to react list");

      await Context.Message.DeleteAsync();
    }
  }
}
