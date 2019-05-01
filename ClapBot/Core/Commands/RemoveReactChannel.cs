using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClapBot.Core.Commands
{
  class RemoveReactChannel : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveReactChannel"), Summary("Removes channel for reactions")]
    public async Task RemoveChannel()
    {
      ActionLog.ClientLog($"Removing {Context.Channel.Name} to react channels");
      if (MessageHandler.channelsToReactIn.Contains(Context.Channel))
        MessageHandler.channelsToReactIn.Remove(Context.Channel);

      await Context.Message.DeleteAsync();
    }
  }
}
