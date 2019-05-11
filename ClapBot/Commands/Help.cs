using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using DiscordBots.DataTypes;

namespace ClapBot.Commands
{
  public class HelpDM : ModuleBase<SocketCommandContext>
  {
    [Command("HelpClapBot"), Alias("HelpDM"), Summary("Sends a DM of commands to the user")]
    public async Task _HelpDM()
    {
      await Context.Message.DeleteAsync();

      IDMChannel dm = await Context.User.GetOrCreateDMChannelAsync();

      StringBuilder sb = new StringBuilder();
      foreach (var command in Starter.Commands.Commands)
      {
        sb.AppendLine($"!{command.Name} - {command.Summary}");
      }

      await dm.SendMessageAsync(sb.ToString());
      await ClientConsole.Log(new TargetedCommandMessage("HelpDM", Context, Context.User));
    }
  }

  public class HelpChannel : ModuleBase<SocketCommandContext>
  {
    [Command("HelpClapBotChannel"), Summary("Displays all commands in the channel")]
    public async Task _HelpChannel()
    {
      await Context.Message.DeleteAsync();

      StringBuilder sb = new StringBuilder();
      foreach (var command in Starter.Commands.Commands)
        sb.AppendLine($"{command.Name} - {command.Summary}");

      await Context.Channel.SendMessageAsync(sb.ToString());
      await ClientConsole.Log(new TargetedCommandMessage("HelpChannel", Context, Context.Channel));
    }
  }
}
