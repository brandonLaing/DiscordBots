using Discord.WebSocket;

namespace ClapBot.DataTypes
{
  public class ClientMessage
  {
    #region Variables
    private string _author;
    private string _channelName;
    private string _messsage;
    #endregion

    #region Properties
    public string Author
    {
      get { return _author; }
      private set { _author = value; }
    }

    public string ChannelName
    {
      get { return _channelName; }
      private set { _channelName = value; }
    }

    public string Message
    {
      get { return _messsage; }
      private set { _messsage = value; }
    }
    #endregion

    #region Constructors
    public ClientMessage(string author, string channelName, string message)
    {
      Author = author;
      ChannelName = channelName;
      Message = message;
    }

    public ClientMessage(SocketUserMessage socketMessage)
    {
      Author = socketMessage.Author.Username;
      ChannelName = socketMessage.Channel.Name;
      Message = socketMessage.Content;
    }
    #endregion

    public override string ToString()
    {
      return $"Received message from {ChannelName}(channel) saying \"{Message}\"(message) by {Author}(author)";
    }
  }
}
