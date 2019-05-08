namespace ClapBot.DataTypes
{
  public class ClientMessage
  {
    #region Variables
    private string _author;
    private string _serverName;
    private string _channelName;
    private string _messsage;
    #endregion

    #region Properties
    public string Author
    {
      get { return _author; }
      private set { _author = value; }
    }

    public string ServerName
    {
      get { return _serverName; }
      private set { _serverName = value; }
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
    public ClientMessage(string author, string serverName, string channelName, string message)
    {
      Author = author;
      ServerName = serverName;
      ChannelName = channelName;
      Message = message;
    }
    #endregion

    public string ToString()
    {
      return $"Recived message from {ServerName}(server) in {ChannelName}(channel) saying \"{Message}\"(message) by {Author}(author)";
    }

  }
}
