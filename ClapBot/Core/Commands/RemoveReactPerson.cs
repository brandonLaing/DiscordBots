using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClapBot.Core.Commands
{
  class RemoveReactPerson : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveReactPerson"), Summary("Removes user to be reacted to")]
    public async Task RemovePerson()
    {
      string usersBeingRemoved = string.Empty;
      foreach (var user in Context.Message.MentionedUsers)
      {
        if (MessageHandler.usersToReactTo.Contains(user))
        {
          usersBeingRemoved += user.Username + ", ";
          MessageHandler.mocked.Remove(user);
        }
      }
      ActionLog.ClientLog($"Removing {usersBeingRemoved} to react list");

      await Context.Message.DeleteAsync();
    }
  }
}
