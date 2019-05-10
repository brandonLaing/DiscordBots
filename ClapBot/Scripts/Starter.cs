using System;
using System.Reflection;
using System.Threading.Tasks;

using Discord;
using Discord.Commands;
using Discord.WebSocket;
using DiscordBots.DataTypes;

namespace ClapBot
{
  /// <summary>
  /// Main beginning of the program. Holds client and commands.
  /// </summary>
  class Starter
  {
    #region Core Variables
    private static DiscordSocketClient _client;
    private static CommandService _commands;

    /// <summary>
    /// Bots client that it uses
    /// </summary>
    public static DiscordSocketClient Client
    {
      get
      {
        return _client;
      }
      private set
      {
        _client = value;
      }
    }
    /// <summary>
    /// Bots set of commands
    /// </summary>
    public static CommandService Commands
    {
      get
      {
        return _commands;
      }
      private set
      {
        _commands = value;
      }
    }

    /// <summary>
    /// Todo: need to change to user 
    /// </summary>
    public static string[] PriorityIds = { "2871", "6188", "5831"};

    /// <summary>
    /// Bot Token
    /// </summary>
    private static string Token
    {
      get
      {
        return "NTczNzY0Mjk5NTE2ODA1MTMw.XM3JYg.-mNtppAxfPf4nRaH_janlhbNb-c";
      }
    }
    #endregion

    #region Startup
    /// <summary>
    /// Starter for this program
    /// </summary>
    /// <param name="args"></param>
    private static void Main(string[] args)
    {
      new Starter()
        .Initilizer()
        .GetAwaiter()
        .GetResult();
    }

    /// <summary>
    ///  Main start sequence for the program 
    /// </summary>
    /// <returns></returns>
    private async Task Initilizer()
    {
      InitilizeVariables();

      // Sets up message handler
      Client.MessageReceived += MessageHandler.ClientMessageRecived;

      // Gets all commands
      await Commands.AddModulesAsync(Assembly.GetEntryAssembly(), null);

      // Setting up delegates
      Client.Ready += SetGame;
      Client.Log += ClientConsole.Log;
      Client.Connected += OnConnected;
      Client.Disconnected += OnDisconnected;
      Client.LoggedIn += OnLoggedIn;
      Client.LoggedOut += OnLoggedOut;
      Client.GuildAvailable += OnGuildAvailable;
      Client.GuildUnavailable += OnGuildUnavailable;
      Client.JoinedGuild += OnJoinedGuild;
      Client.LeftGuild += OnLeftGuild;

      // Connect client
      await Connect();

      await Task.Delay(-1);
    }

    /// <summary>
    /// Sets up the base variables for client and commands
    /// </summary>
    private void InitilizeVariables()
    {
      Client = new DiscordSocketClient(new DiscordSocketConfig
      {
        LogLevel = LogSeverity.Info
      });

      Commands = new CommandService(new CommandServiceConfig
      {
        CaseSensitiveCommands = false,
        DefaultRunMode = RunMode.Async,
        LogLevel = LogSeverity.Info,
        IgnoreExtraArgs = true
      });
    }

    /// <summary>
    /// Sets the game that the bot is playing
    /// </summary>
    /// <returns></returns>
    private async Task SetGame()
    {
      await Client.SetGameAsync($"with the {new Emoji("👏🏻")} ", "", ActivityType.Playing);
    }
    #endregion

    #region Client Events

    /// <summary>
    /// Attempts to connect client
    /// </summary>
    /// <returns></returns>
    private async Task Connect()
    {
      if (Token == string.Empty)
      {
        await ClientConsole.Log("Connector", "No token set. Bot will not connect");
        return;
      }
      await ClientConsole.Log("Connector", "Connecting to discord");

      await Client.LoginAsync(TokenType.Bot, Token);
      await Client.StartAsync();
    }

    /// <summary>
    /// Logic for when Client connect to discord
    /// </summary>
    /// <returns></returns>
    private async Task OnConnected()
    {
      await ClientConsole.Log("Connector", "Connected to server");
    }

    /// <summary>
    /// Logic for when client is disconnected from discord.
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    private async Task OnDisconnected(Exception exception)
    {
      await ClientConsole.Log("Connector", $"Disconnected from server. Exception {exception.Message}");
    }

    /// <summary>
    /// Logic for when client is logged in
    /// </summary>
    /// <returns></returns>
    private async Task OnLoggedIn()
    {
      await ClientConsole.Log("Connector", "Client has logged in");
    }

    /// <summary>
    /// Logic for when client is logged out
    /// </summary>
    /// <returns></returns>
    private async Task OnLoggedOut()
    {
      await ClientConsole.Log("Connector", "Client has logged out");
    }

    /// <summary>
    /// When server becomes available
    /// </summary>
    /// <param name="server">Serve that became available</param>
    /// <returns></returns>
    private async Task OnGuildAvailable(SocketGuild server)
    {
      await ClientConsole.Log("Connector", $"Client became connected to {server.Name}({server.Id})");
    }

    /// <summary>
    /// Logic for when a server becomes unavailable
    /// </summary>
    /// <param name="server"></param>
    /// <returns></returns>
    private async Task OnGuildUnavailable(SocketGuild server)
    {
      await ClientConsole.Log("Connector", $"Client became disconnected to {server.Name}({server.Id})");
    }

    /// <summary>
    /// Logic for when a server is joined
    /// </summary>
    /// <param name="server"></param>
    /// <returns></returns>
    private async Task OnJoinedGuild(SocketGuild server)
    {
      await ClientConsole.Log("Connector", $"Server {server.Name}({server.Id}) has been joined");
    }

    /// <summary>
    /// Logic for when a server is being left
    /// </summary>
    /// <param name="server"></param>
    /// <returns></returns>
    private async Task OnLeftGuild(SocketGuild server)
    {
      await ClientConsole.Log("Connector", $"Server {server.Name}({server.Id}) has been left");
    }
    #endregion
  }
}
