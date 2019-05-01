using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClapBot.Core.Commands
{
  public class AddReactChannel : ModuleBase<SocketCommandContext>
  {
    [Command("AddReactChannel"), Summary("Adds channel for reactions")]
    public async Task AddChannel()
    {
      ActionLog.ClientLog($"Adding {Context.Channel.Name} to react channels");
      if (!MessageHandler.channelsToReactIn.Contains(Context.Channel))
        MessageHandler.channelsToReactIn.Add(Context.Channel);

      await Context.Message.DeleteAsync();
    }
  }
}
