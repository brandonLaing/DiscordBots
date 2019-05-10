using Discord.Commands;
using DiscordBots.Enums;

namespace DiscordBots.DataTypes
{
  public class TargetedCommandMessage : CommandMessage
  {
    #region Variables
    private string _targetName;
    private ulong _targetId;
    private TargetType _targetType;
    #endregion

    #region Properties
    /// <summary>
    /// Name of whatever command is targeting
    /// </summary>
    public string TargetName
    {
      get { return _targetName; }
      private set { _targetName = value; }
    }
    /// <summary>
    /// Id of whatever command is targeting
    /// </summary>
    public ulong TargetId
    {
      get { return _targetId; }
      private set { _targetId = value; }
    }
    /// <summary>
    /// Type of object the command is targeting. Either a user, channel, or role.
    /// </summary>
    public TargetType TargetType
    {
      get { return _targetType; }
      private set { _targetType = value; }
    }
    #endregion

    #region Constructor
    public TargetedCommandMessage(string commandName, SocketCommandContext context, string targetName, ulong targetId, TargetType tragetType) 
      : base(commandName, context)
    {
      TargetName = targetName;
      TargetId = targetId;
      TargetType = tragetType;
    }
    #endregion

    /// <summary>
    /// Outputs command information for logging information
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return $"{Command}- From {AuthorUsername}({AuthorId}) in {ChannelName}({ChannelId}) targeting {TargetName}({TargetId}) that is a {TargetType.ToString()}";
    }
  }
}
