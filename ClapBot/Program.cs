using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.Commands;
using Discord.Rest;
using Discord.WebSocket;

namespace ClapBot
{
  class Program
  {
    private DiscordSocketClient client;
    private CommandService commands;

    private List<string> activeChannels = new List<string>();
    private string[] AcClap
    {
      get
      {
        string path = Directory.GetCurrentDirectory().Split("bin")[0] + @"Data\";
        if (!Directory.Exists(path))
          Directory.CreateDirectory(path);

        path += "AciiClap.txt";
        Console.WriteLine(path);

        if (File.Exists(path))
        {
          string[] message = File.ReadAllLines(path);
          return message;
        }
        return null;
      }
    }

    static void Main(string[] args) =>
      new Program()
        .MainAsync()
        .GetAwaiter()
        .GetResult();

    private async Task MainAsync()
    {
      client = new DiscordSocketClient(new DiscordSocketConfig
      {
        LogLevel = LogSeverity.Debug
      });

      commands = new CommandService(new CommandServiceConfig
      {
        CaseSensitiveCommands = true,
        DefaultRunMode = RunMode.Async,
        LogLevel = LogSeverity.Debug
      });

      client.MessageReceived += ClientMessageRecived;
      await commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

      client.Ready += ClientReady;
      client.Log += ClientLog;

      string token = "NTcyODY5NjAyMDMyNDg0Mzgz.XMinkQ.HSqMXhxgDPNcReZs0NNBWxcfhE8";

      await client.LoginAsync(TokenType.Bot, token);

      await client.LoginAsync(TokenType.Bot, token);
      await client.StartAsync();

      await Task.Delay(-1);
    }

    private async Task ClientLog(LogMessage message) =>
      Console.WriteLine($"{DateTime.Now}: {message.Source}: {message.Message}");

    private async Task ClientReady()
    {
      await client.SetGameAsync($"with the {new Emoji("👏")} ", "", ActivityType.Playing);
    }

    private async Task ClientMessageRecived(SocketMessage rawMessage)
    {
      RestUserMessage rMessage = (RestUserMessage) await rawMessage.Channel.GetMessageAsync(rawMessage.Id);
      string message = rMessage.Content;
      var channel = client.GetChannel(rawMessage.Channel.Id) as IMessageChannel;

      if (message.StartsWith("!"))
      {
        string command = message.Split('!')[1];
        command = command.Trim();
        Console.WriteLine($"{DateTime.Now}: Recived command \"{command}\"");
        if (command != string.Empty)
        {
          switch (command.ToLower())
          {
            case "asciiclap":
              await channel.SendMessageAsync("trying to send ascii art");
              for (int i = 0; i < AcClap.Length; i++)
              {
                await channel.SendMessageAsync(AcClap[i]);
              }
              break;
            case "truefact":
              await channel.SendMessageAsync("Dan is retarded");
              break;
          }
        }
      }

      await rMessage.AddReactionAsync(new Emoji("👏"));
      Console.WriteLine($"{DateTime.Now}: Processing Message: \"{message}\" from {rawMessage.Author.Username}");

    }
  }
}
