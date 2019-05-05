using Discord.Commands;
using System.Threading.Tasks;

namespace ClapBot.Core.Commands
{
  public class TrueFact : ModuleBase<SocketCommandContext>
  {
    [Command("TrueFact"), Summary("Spits out a true Fact")]
    public async Task _TrueFact()
    {
      await ClientConsole.Log(new Discord.LogMessage(Discord.LogSeverity.Info, "Command-TrueFact", "Sending out true fact"));
      await Context.Channel.SendMessageAsync("Dan is retarded");
    }
  }
}
