using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ClapBot.Resources;

namespace ClapBot.Commands
{
  public class ClapThisMan : ModuleBase<SocketCommandContext>
  {
    public async Task _ClapThisMan()
    {
      IAsyncEnumerable<IReadOnlyCollection<Discord.IMessage>> messages = Context.Channel.GetMessagesAsync(1);
      foreach (var message in messages)
      {

      }

    }
  }
}
