using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ClapBot
{
  static class MessageHandler
  {
    public static List<SocketUser> mocked = new List<SocketUser>();
    public static List<ISocketMessageChannel> channelsToReactIn = new List<ISocketMessageChannel>();
    public static List<SocketUser> usersToReactTo = new List<SocketUser>();

    public static async Task ClientMessageRecived(SocketMessage rawMessage)
    {
      // get info for message
      var message = rawMessage as SocketUserMessage;
      var context = new SocketCommandContext(Starter.Client, message);

      // make sure message has something in it
      if (
        context.Message == null || 
        context.Message.Content == string.Empty || 
        context.User.IsBot
        ) return;

      // display message
      ActionLog.ClientLog(message);



      // check if its a command
      int prefixPos = 0;
      if (!message.HasCharPrefix('!', ref prefixPos) || message.HasMentionPrefix(Starter.Client.CurrentUser, ref prefixPos))
      {
        await Mock(message);
        await ReactWithClap(message);

        return;
      }

      var result = await Starter.Commands.ExecuteAsync(context, prefixPos, null);
      if (!result.IsSuccess)
        ActionLog.ClientLog($"Something went wrong with executing a command. Command : {context.Message.Content} | {result.ErrorReason}");
    }

    private static async Task Mock(SocketUserMessage message)
    {
      if (!mocked.Contains(message.Author))
        return;

      char[] charArr = message.Content.ToCharArray();
      string responce = string.Empty;
      for (int i = 0; i < charArr.Length; i++)
      {
        if (charArr[i] == ' ')
          responce += ' ';
        else if (new Random().Next(0, 2) == 0)
          responce += Char.ToUpper(charArr[i]);
        else
          responce += Char.ToLower(charArr[i]);
      }

      await message.Channel.SendMessageAsync(responce);
    }

    public static async Task ReactWithClap(SocketUserMessage message)
    {
      if (channelsToReactIn.Contains(message.Channel) || usersToReactTo.Contains(message.Author))
        await message.AddReactionAsync(new Emoji("👏"));
    }

  }
}
