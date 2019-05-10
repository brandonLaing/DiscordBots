using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;

namespace ClapBot.DataTypes
{
  public class TargetedCommandMessage : CommandMessage
  {
    public TargetedCommandMessage(string commandName, SocketCommandContext context, string targetName, ulong targetId) 
      : base(commandName, context)
    {

    }
    #region Variables
    public override string ToString()
    {
      return string.Empty;
    }
  }
}
