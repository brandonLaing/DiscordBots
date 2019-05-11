using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBots.DataTypes;

namespace ClapBot.Commands
{
  public class MockAddPerson : ModuleBase<SocketCommandContext>
  {
    [Command("AddMockUser"), Summary("Takes user message edits it then sends it back to chat.")]

    public async Task _MockAddPerson()
    {
      await Context.Message.DeleteAsync();

      List<ulong> mocked = await SaveSystem.GetMocked();
      IReadOnlyCollection<SocketUser> mentioned = Context.Message.MentionedUsers;

      if (mentioned.Count == 0 && !mocked.Contains(Context.User.Id))
      {
        await SaveSystem.AddMocked(Context.User.Id);
        await ClientConsole.Log(new TargetedCommandMessage("AddMockUser", Context, Context.User));
        await Context.Channel.SendMessageAsync($"Starting to mock {Context.User.Mention}");
        return;
      }

      List<ulong> adminIds = await SaveSystem.GetAdminIds();
      if (adminIds.Contains(Context.User.Id) || adminIds.Count == 0)
        foreach (SocketUser user in mentioned)
        {
          if (!mocked.Contains(user.Id) && Context.User == user)
          {
            await SaveSystem.AddMocked(user.Id);
            await ClientConsole.Log(new TargetedCommandMessage("AddMockUser", Context, user));
            await Context.Channel.SendMessageAsync($"Starting to mock {user.Mention}");
          }
        }
    }
  }
  

  public class MockRemovePerson : ModuleBase<SocketCommandContext>
  {
    [Command("RemoveMockUser"), Summary("Stops mocking a user")]
    public async Task _MockRemovePerson()
    {
      await Context.Message.DeleteAsync();

      List<ulong> adminIds = await SaveSystem.GetAdminIds();
      if (adminIds.Contains(Context.User.Id) || adminIds.Count == 0)
      {
        List<ulong> mocked = await SaveSystem.GetMocked();
        IReadOnlyCollection<SocketUser> mentioned = Context.Message.MentionedUsers;

        if (mentioned.Count == 0 && mocked.Contains(Context.User.Id))
        {
          await SaveSystem.RemoveMocked(Context.User.Id);
          await ClientConsole.Log(new TargetedCommandMessage("RemoveReactUser", Context, Context.Channel));
          await Context.Channel.SendMessageAsync($"Stopped mocking messages from {Context.User.Mention}");
          return;
        }

        foreach (SocketUser user in mentioned)
        {
          if (mocked.Contains(user.Id))
          {
            await SaveSystem.RemoveMocked(user.Id);
            await ClientConsole.Log(new TargetedCommandMessage("RemoveMockUser", Context, user));
            await Context.Channel.SendMessageAsync($"Stopped mocking messages from {user.Mention}");
          }
        }
      }
    }
  }
}
