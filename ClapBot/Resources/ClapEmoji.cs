using Discord;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClapBot.Resources
{
  public class ClapEmoji
  {
    public static Emoji YellowClap
    {
      get { return new Emoji("👏"); }
    }

    public static Emoji LightClap
    {
      get { return new Emoji("👏🏻"); }
    }

    public static Emoji MediumLightClap
    {
      get { return new Emoji("👏🏼"); }
    }

    public static Emoji MediumClap
    {
      get { return new Emoji("👏🏽"); }
    }

    public static Emoji MediumDarkClap
    {
      get { return new Emoji("👏🏾"); }
    }

    public static Emoji DarkClap
    {
      get { return new Emoji("👏🏿"); }
    }
  }
}
