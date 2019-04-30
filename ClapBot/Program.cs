using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

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
      //using (FileStream stream = new FileStream((Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)).Replace(@"bin\Debug\netcoreapp2.0", @"Data\Token.txt"), FileMode.Open, FileAccess.Read))
      //using (var readToken = new StreamReader(stream))
      //{
      //  token = readToken.ReadToEnd();
      //}
      await client.LoginAsync(TokenType.Bot, token);

      await client.LoginAsync(TokenType.Bot, token);
      await client.StartAsync();

      await Task.Delay(-1);
    }

    private async Task ClientLog(LogMessage message)
    {
      Console.WriteLine($"{DateTime.Now} at {message.Source}: {message.Message}");
    }

    private async Task ClientReady()
    {
      await client.SetGameAsync("Claping", @"https://www.google.com/", ActivityType.Playing);

    }

    private async Task ClientMessageRecived(SocketMessage message)
    {
      RestUserMessage rMessage = (RestUserMessage) await message.Channel.GetMessageAsync(message.Id);
      await rMessage.AddReactionAsync(new Emoji("👏"));

      Console.WriteLine($"Processing Message: {rMessage}");
      if (rMessage.ToString().)
    }
  }
}
