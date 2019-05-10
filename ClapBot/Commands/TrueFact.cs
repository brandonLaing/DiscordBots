using Discord.Commands;
using System.Threading.Tasks;
using DiscordBots.DataTypes;

namespace ClapBot.Core.Commands
{
  public class TrueFact : ModuleBase<SocketCommandContext>
  {
    [Command("TrueFact"), Summary("Spits out a true Fact")]
    public async Task _TrueFact()
    {
      await ClientConsole.Log(new CommandMessage("TrueFact", Context));
      await Context.Channel.SendMessageAsync("Dan is retarded");
    }
  }
}
