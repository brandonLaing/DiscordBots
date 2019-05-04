using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace ClapBot
{
  static class MessageHandler
  {
    public static List<ISocketMessageChannel> channelsToReactIn = new List<ISocketMessageChannel>();
    public static List<SocketUser> usersToReactTo = new List<SocketUser>();

    /// <summary>
    /// Action to be performed when a new message is received from discord
    /// </summary>
    /// <param name="rawMessage">Raw message data</param>
    /// <returns></returns>
    public static async Task ClientMessageRecived(SocketMessage rawMessage)
    {
      // get info for message
      var message = rawMessage as SocketUserMessage;
      var context = new SocketCommandContext(Starter.Client, message);

      // make sure message has something in it
      if (
        context.Message == null ||
        context.Message.Content.Trim() == string.Empty ||
        context.User.IsBot
        ) return;

      // display message
      await ClientConsole.Log(message);

      // check if its a command
      int prefixPos = 0;
      if (!message.HasCharPrefix('!', ref prefixPos) || message.HasMentionPrefix(Starter.Client.CurrentUser, ref prefixPos))
      {
        Console.WriteLine("Not command");
        await Mock(message);
        await ReactWithClap(message);

        return;
      }

      var result = await Starter.Commands.ExecuteAsync(context, prefixPos, null);
      if (!result.IsSuccess)
        await ClientConsole.Log(new LogMessage(LogSeverity.Info, "Message Handler",$"Something went wrong with executing a command. Command : {context.Message.Content} | {result.ErrorReason}"));
    }

    /// <summary>
    /// Edits user message to randomly capitalize or lowercase letter and replace spaces with claps
    /// </summary>
    /// <param name="message">Message that is being received</param>
    /// <returns></returns>
    private static async Task Mock(SocketUserMessage message)
    {
      Console.WriteLine($"In Mocked {SaveSystem.GetMocked().Count}");

      foreach (var mocked in SaveSystem.GetMocked())
      {
        Console.WriteLine("Checking get mocked v author id \n" + mocked + "\n" + message.Author.Id);
        if (mocked == message.Author.Id)
          Console.WriteLine("user in get mocked == author of this message");
      }

      if (SaveSystem.GetMocked().Contains(message.Author.Id))
        Console.WriteLine("Save system contains the author of this message");

      // check if author should be mocked and that the message isn't empty
      if (!SaveSystem.GetMocked().Contains(message.Author.Id) || message.Content == string.Empty)
        return;

      Console.WriteLine("Should be mocked");
      // separate each character
      char[] charArr = message.Content.ToCharArray();
      string responce = new Emoji("👏").ToString();
      for (int i = 0; i < charArr.Length; i++)
      {
        // change spaces for claps and randomly capitalize and lowercase letters
        if (charArr[i] == ' ')
          responce += new Emoji("👏");
        else if (new Random().Next(0, 2) == 0)
          responce += Char.ToUpper(charArr[i]);
        else
          responce += Char.ToLower(charArr[i]);
      }
      // end with clap
      responce += new Emoji("👏");

      // send info the logs
      await ClientConsole.Log(new LogMessage(LogSeverity.Info, "Message Handler", $"Mocking {message.Author.Username} with message {responce} replacing {message.Content}"));
      // send bot message with response
      await message.Channel.SendMessageAsync($"{responce} -From {message.Author.Username}");
      await message.DeleteAsync();
    }

    /// <summary>
    /// Adds clap reaction to message
    /// </summary>
    /// <param name="message">Message received from the user</param>
    /// <returns></returns>
    private static async Task ReactWithClap(SocketUserMessage message)
    {
      if (channelsToReactIn.Contains(message.Channel) || usersToReactTo.Contains(message.Author) || message.Author.IsBot)
        await message.AddReactionAsync(new Emoji("👏"));
    }
  }
}
