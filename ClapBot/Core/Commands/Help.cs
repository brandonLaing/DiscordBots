using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ClapBot.Core.Commands
{
  public class Help : ModuleBase<SocketCommandContext>
  {
    [Command("Help"), Summary("Sends a dm of commands to the user")]
    public async Task _Help()
    {
      IDMChannel dm = await Context.User.GetOrCreateDMChannelAsync();
      StringBuilder sb = new StringBuilder();
      foreach (var command in Starter.Commands.Commands)
      {
        sb.AppendLine($"!{command.Name} - {command.Summary}");
      }

      ActionLog.ClientLog($"Sending help to {Context.User}");
      await dm.SendMessageAsync(sb.ToString());
      await Context.Message.DeleteAsync();
    }
  }
}
