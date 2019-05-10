using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClapBot.DataTypes
{
  /// <summary>
  /// Type that is used for converting comamnd messages into a string to be displayed to a log
  /// </summary>
  public class CommandMessage
  {
    #region Variables
    private string _command;
    private string _authorUsername;
    private ulong _authorId;
    private string _channelName;
    private ulong _channelId;
    #endregion

    #region Properties
    /// <summary>
    /// Command that message was sent from
    /// </summary>
    public string Command
    {
      get { return _command; }
      private set { _command = value; }
    }
    /// <summary>
    /// Name for user message was sent from
    /// </summary>
    public string AuthorUsername
    {
      get { return _authorUsername; }
      private set { _authorUsername = value; }
    }
    /// <summary>
    /// Id for user message was sent from
    /// </summary>
    public ulong AuthorId
    {
      get { return _authorId; }
      private set { _authorId = value; }
    }
    /// <summary>
    /// Name for channel message was sent from
    /// </summary>
    public string ChannelName
    {
      get { return _channelName; }
      private set { _channelName = value; }
    }
    /// <summary>
    /// Id for channel message was sent from
    /// </summary>
    public ulong ChannelId
    {
      get { return _channelId; }
      private set { _channelId = value; }
    }
    #endregion

    #region Constructor
    /// <summary>
    /// Grabs inportant information and saves it into this data type.
    /// </summary>
    /// <param name="commandName">Command name that message is comming from</param>
    /// <param name="context">Context given in each command</param>
    public CommandMessage(string commandName, SocketCommandContext context)
    {
      Command = commandName;
      AuthorUsername = context.User.Username;
      AuthorId = context.User.Id;
      ChannelName = context.Channel.Name;
      ChannelId = context.Channel.Id;
    }
    #endregion

    /// <summary>
    /// Outputs comamnd information in a easy to read string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return $"{Command}- From {AuthorUsername}({AuthorId}) in {ChannelName}({ChannelId})";
    }
  }
}
