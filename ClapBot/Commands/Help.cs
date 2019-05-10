using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBots.DataTypes;

namespace ClapBot.Core.Commands
{
  public class HelpDM : ModuleBase<SocketCommandContext>
  {
    [Command("HelpClapBot"), Alias("HelpDM"), Summary("Sends a DM of commands to the user")]
    public async Task _HelpDM()
    {
      IDMChannel dm = await Context.User.GetOrCreateDMChannelAsync();
      StringBuilder sb = new StringBuilder();
      foreach (var command in Starter.Commands.Commands)
      {
        sb.AppendLine($"!{command.Name} - {command.Summary}");
      }

      await ClientConsole.Log(new CommandMessage("HelpDM", Context));
      await dm.SendMessageAsync(sb.ToString());
      await Context.Message.DeleteAsync();
    }
  }

  public class HelpChannel : ModuleBase<SocketCommandContext>
  {
    [Command("HelpClapBotChannel"), Summary("Displays all commands in the channel")]
    public async Task _HelpChannel()
    {
      StringBuilder sb = new StringBuilder();
      foreach (var command in Starter.Commands.Commands)
        sb.AppendLine($"{command.Name} - {command.Summary}");

      await ClientConsole.Log(new CommandMessage("HelpChannel", Context));
      await Context.Channel.SendMessageAsync(sb.ToString());
    }
  }
}
