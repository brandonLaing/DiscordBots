using Discord.WebSocket;

namespace DiscordBots.DataTypes
{
  /// <summary>
  /// Type that is primarily used for taking user messages and displaying them to the console
  /// </summary>
  public class ClientMessage
  {
    #region Variables
    private string _authorUsername;
    private ulong _authorId;
    private string _channelName;
    private ulong _channelId;
    private string _messsage;
    #endregion

    #region Properties
    /// <summary>
    /// Message authors displayed name
    /// </summary>
    public string AuthorUsername
    {
      get { return _authorUsername; }
      private set { _authorUsername = value; }
    }
    /// <summary>
    /// Message authors discord id
    /// </summary>
    public ulong AuthorId
    {
      get { return _authorId; }
      private set { _authorId = value; }
    }
    /// <summary>
    /// Name of channel that message was sent in
    /// </summary>
    public string ChannelName
    {
      get { return _channelName; }
      private set { _channelName = value; }
    }
    /// <summary>
    /// Id of channel message was sent in
    /// </summary>
    public ulong ChannelId
    {
      get { return _channelId; }
      private set { _channelId = value; }
    }
    /// <summary>
    /// Message that was sent by the user
    /// </summary>
    public string Message
    {
      get { return _messsage; }
      private set { _messsage = value; }
    }
    #endregion

    #region Constructors
    /// <summary>
    /// Gabs important information and saves it into this data type
    /// </summary>
    /// <param name="_socketUserMessage">Message information being loaded in</param>
    public ClientMessage(SocketUserMessage _socketUserMessage)
    {
      AuthorUsername = _socketUserMessage.Author.Username;
      AuthorId = _socketUserMessage.Author.Id;
      ChannelName = _socketUserMessage.Channel.Name;
      ChannelId = _socketUserMessage.Channel.Id;
      Message = _socketUserMessage.Content;
    }
    #endregion

    /// <summary>
    /// Outputs message information in a easy to read string
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return $"Received message from {ChannelName}({ChannelId}) saying \"{Message}\" by {AuthorUsername}({AuthorId})";
    }
  }
}
