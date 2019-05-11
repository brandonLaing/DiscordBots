using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBots.DataTypes;
using ClapBot.Resources;

namespace ClapBot
{
  /// <summary>
  /// Controls all actions having to do with user messages
  /// </summary>
  static class MessageHandler
  {
    /// <summary>
    /// Action to be performed when a new message is received from discord
    /// </summary>
    /// <param name="rawMessage">Raw message data</param>
    /// <returns></returns>
    public static async Task ClientMessageRecived(SocketMessage rawMessage)
    {
      // make sure message has something in it and message isn't from a bot
      if (!(rawMessage is SocketUserMessage message) ||
        message.Content.Trim() == string.Empty ||
        message.Author.IsBot
        ) return;

      // display message
      await ClientConsole.Log("Message Handler", new ClientMessage(message));

      // check if its a command
      int prefixPos = 0;
      if (!message.HasCharPrefix('!', ref prefixPos) || message.HasMentionPrefix(Starter.Client.CurrentUser, ref prefixPos))
      {
        await Mock(message);
        await ReactWithClap(message);
        return;
      }

      // Get command context 
      var commandContext = new SocketCommandContext(Starter.Client, message);
      if (commandContext == null || commandContext.Message == null) return;

      // Execute command
      var result = await Starter.Commands.ExecuteAsync(commandContext, prefixPos, null);
      if (!result.IsSuccess)
        await ClientConsole.Log("Message Handler", $"Something went wrong with executing a command. Command: {commandContext.Message.Content} | {result.ErrorReason}");
      else if (result.IsSuccess)
        await ClientConsole.Log("Message Handler", $"Command was done successfully. Command: {commandContext.Message.Content}");
    }

    /// <summary>
    /// Deletes a users message and replies with a mocked version of that message
    /// </summary>
    /// <param name="message">Message that is being received</param>
    /// <returns></returns>
    private static async Task Mock(SocketUserMessage message)
    {
      // check if author should be mocked and that the message isn't empty
      var mockedList = await SaveSystem.GetMocked();
      if (!mockedList.Contains(message.Author.Id) || message.Content == string.Empty)
        return;

      // Save message
      char[] charArr = message.Content.Trim().ToCharArray();
      await message.DeleteAsync();

      // save emoji string
      string clap = ClapEmoji.LightClap.ToString();

      // start with a clap
      string responce = clap;
      for (int i = 0; i < charArr.Length; i++)
      {
        // change spaces for claps and randomly capitalize and lowercase letters
        if (charArr[i] == ' ')
          responce += clap;
        else if (new Random().Next(0, 2) == 0)
          responce += char.ToUpper(charArr[i]);
        else
          responce += char.ToLower(charArr[i]);
      }
      // end with clap
      responce += clap;

      // send info the logs
      await ClientConsole.Log("Command Message", $"Mocking {message.Author.Username}({message.Author.Id}) with message {responce} replacing {message.Content}");
      // send bot message with response
      await message.Channel.SendMessageAsync($"{responce} -From {message.Author.Username}");
    }

    /// <summary>
    /// Adds clap reaction to message
    /// </summary>
    /// <param name="message">Message received from the user</param>
    /// <returns></returns>
    private static async Task ReactWithClap(SocketUserMessage message)
    {

      List<ulong> reactUsers = await SaveSystem.GetReactUser();
      List<ulong> reactChannel = await SaveSystem.GetReactChannel();
      if ((reactChannel.Contains(message.Channel.Id) || reactUsers.Contains(message.Author.Id)) && !message.Author.IsBot)
      {
        await message.AddReactionAsync(ClapEmoji.LightClap);
        await ClientConsole.Log("Command Message", $"Adding clap reaction to {message.Author.Username}'s({message.Author.Id}) message {message.Content}");
      }
    }
  }
}
