using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

using Discord;

namespace ClapBot
{
  /// <summary>
  /// This script contains all systems for saving and loading information from files
  /// </summary>
  public static class SaveSystem
  {
    #region File Locations
    /// <summary>
    /// Director of current program
    /// </summary>
    private static string CurrentDirectory
    {
      get
      {
        return Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
      }
    }
    /// <summary>
    /// Path to Data folder
    /// </summary>
    private static string DataPath
    {
      get
      {
        string _path = Path.Combine(Directory.GetParent(CurrentDirectory).ToString(), "Data");
        if (!Directory.Exists(_path))
          Directory.CreateDirectory(_path);
        return _path;
      }
    }
    /// <summary>
    /// Path to logs save file
    /// </summary>
    private static string LogSaveDirectory
    {
      get
      {
        string _path = Path.Combine(DataPath, "ClapLog.txt");
        VerifyFile(_path);
        return _path;
      }
    }
    /// <summary>
    /// Path to mocked save file
    /// </summary>
    private static string MockedSaveDirectory
    {
      get
      {
        string _path = Path.Combine(DataPath, "Mocked.txt");
        VerifyFile(_path);
        return _path;
      }
    }
    /// <summary>
    /// Path to mocked user save file
    /// </summary>
    private static string ReactUserSaveDirectory
    {
      get
      {
        string _path = Path.Combine(DataPath, "ReactUser.txt");
        VerifyFile(_path);
        return _path;
      }
    }
    /// <summary>
    /// Path to mocked save channel
    /// </summary>
    private static string ReactChannelDirectory
    {
      get
      {
        string _path = Path.Combine(DataPath, "ReactChannel.txt");
        VerifyFile(_path);
        return _path;
      }
    }
    /// <summary>
    /// Path for admins id save location
    /// </summary>
    private static string AdminDirectory
    {
      get
      {
        string _path = Path.Combine(DataPath, "Admins.txt");
        VerifyFile(_path);
        return _path;
      }
    }
    /// <summary>
    /// Path of bots login token location
    /// </summary>
    private static string TokenDirectory
    {
      get
      {
        string _path = Path.Combine(DataPath, "Token.txt");
        VerifyFile(_path);
        return _path;
      }
    }
    #endregion

    #region ImportantFiles
    /// <summary>
    /// Gets the Token for logging in the bot
    /// </summary>
    /// <returns></returns>
    public static string Token
    {
      get
      {
        string[] tokenLines = File.ReadAllLines(TokenDirectory);
        return tokenLines[0];
      }
    }
    /// <summary>
    /// Gets all the current admins discord ids
    /// </summary>
    /// <returns>Ulong list of ids</returns>
    public static async Task<List<ulong>> GetAdminIds()
    {
      return await LoadUlongData(AdminDirectory);
    }
    /// <summary>
    /// Adds id to the list of admins
    /// </summary>
    /// <param name="id">Id of user to add</param>
    /// <returns></returns>
    public static async Task AddAdminId(ulong id)
    {
      List<ulong> admins = await GetAdminIds();
      if (!admins.Contains(id))
      {
        admins.Add(id);
        SaveUlongData(AdminDirectory, admins);
      }
    }
    /// <summary>
    /// Removes an user from the admin list
    /// </summary>
    /// <param name="id">Id for user to be removed</param>
    /// <returns></returns>
    public static async Task RemoveAdminId(ulong id)
    {
      List<ulong> admins = await GetAdminIds();
      if (admins.Contains(id))
      {
        admins.Remove(id);
        SaveUlongData(AdminDirectory, admins);
      }
    }
    #endregion

    #region SaveLog
    /// <summary>
    /// Adds string to the end of save log
    /// </summary>
    /// <param name="message">Text that gets written to file</param>
    /// <returns></returns>
    public static async Task AddToSaveLog(string message)
    {
      await File.AppendAllTextAsync(LogSaveDirectory, message + '\n');
    }

    /// <summary>
    /// Clears all saved log data
    /// </summary>
    /// <returns></returns>
    public static async Task ClearLog()
    {
      await File.WriteAllTextAsync(LogSaveDirectory, string.Empty);
    }

    /// <summary>
    /// Returns the log line by line
    /// </summary>
    /// <returns>Save log line by line</returns>
    public static async Task<string[]> GetLog()
    {
      return await File.ReadAllLinesAsync(LogSaveDirectory);
    }
    #endregion

    #region Mocked Users
    /// <summary>
    /// Gets ids of users that should be mocked
    /// </summary>
    /// <returns>List of user ids</returns>
    public static async Task<List<ulong>> GetMocked()
    {
      return await LoadUlongData(MockedSaveDirectory);
    }
    /// <summary>
    /// Adds user to mocked list
    /// </summary>
    /// <param name="userId">Id to add</param>
    public static async Task AddMocked(ulong userId)
    {
      List<ulong> mocked = await GetMocked();
      if (!mocked.Contains(userId))
      {
        mocked.Add(userId);
        SaveUlongData(MockedSaveDirectory, mocked);
      }
    }
    /// <summary>
    /// Removes a user from the mocked list
    /// </summary>
    /// <param name="userId">Id to remove</param>
    public static async Task RemoveMocked(ulong userId)
    {
      List<ulong> mocked = await GetMocked();
      if (mocked.Contains(userId))
      {
        mocked.Remove(userId);
        SaveUlongData(MockedSaveDirectory, mocked);
      }
    }
    #endregion

    #region React user
    /// <summary>
    /// Gets ids of users that should be reacted to
    /// </summary>
    /// <returns>List of user ids</returns>
    public static async Task<List<ulong>> GetReactUser()
    {
      return await LoadUlongData(ReactUserSaveDirectory);
    }
    /// <summary>
    /// Adds user to react list
    /// </summary>
    /// <param name="userId">Id to add</param>
    /// <returns></returns>
    public static async Task AddReactUser(ulong userId)
    {
      List<ulong> reactUser = await GetReactUser();
      if (!reactUser.Contains(userId))
      {
        reactUser.Add(userId);
        SaveUlongData(ReactUserSaveDirectory, reactUser);
      }
    }
    /// <summary>
    /// Removes user from react list
    /// </summary>
    /// <param name="userId">Id to remove</param>
    /// <returns></returns>
    public static async Task RemoveReactUser(ulong userId)
    {
      List<ulong> reactUser = await GetReactUser();
      if (reactUser.Contains(userId))
      {
        reactUser.Remove(userId);
        SaveUlongData(ReactUserSaveDirectory, reactUser);
      }
    }
    #endregion

    #region React Channel
    /// <summary>
    /// Gets ids of channels that should be reacted to
    /// </summary>
    /// <returns>List of channel ids</returns>
    public static async Task<List<ulong>> GetReactChannel()
    {
      return await LoadUlongData(ReactChannelDirectory);
    }
    /// <summary>
    /// Adds channel to react list
    /// </summary>
    /// <param name="channelId">Channel id to add</param>
    public static async Task AddReactChannel(ulong channelId)
    {
      List<ulong> reactChannel = await GetReactChannel();
      if (!reactChannel.Contains(channelId))
      {
        reactChannel.Add(channelId);
        SaveUlongData(ReactChannelDirectory, reactChannel);
      }
    }
    /// <summary>
    /// Removes channel from react list
    /// </summary>
    /// <param name="channelId">Channel id to remove</param>
    public static async Task RemoveReactChannel(ulong channelId)
    {
      List<ulong> reactChannel = await GetReactChannel();
      if (reactChannel.Contains(channelId))
      {
        reactChannel.Remove(channelId);
        SaveUlongData(ReactChannelDirectory, reactChannel);
      }
    }
    #endregion

    #region File read/write
    /// <summary>
    /// Make file in desired location if none exists
    /// </summary>
    /// <param name="path"></param>
    private static void VerifyFile(string path)
    {
      if (!File.Exists(path))
        File.Create(path);
    }
    /// <summary>
    /// Loads ulong data from file
    /// </summary>
    /// <param name="path">Path of file to load from</param>
    /// <returns>List of ulongs</returns>
    private static async Task<List<ulong>> LoadUlongData(string path)
    {
      List<ulong> users = new List<ulong>();
      string[] lines = await File.ReadAllLinesAsync(path);
      foreach (string line in lines)
      {
        ulong.TryParse(line, out ulong id);
        users.Add(id);
      }

      return users;
    }
    /// <summary>
    /// Saves ulong data to file
    /// </summary>
    /// <param name="path">File to save into</param>
    /// <param name="data">Data to save into </param>
    private static void SaveUlongData(string path, List<ulong> data)
    {
      List<string> lines = new List<string>();
      foreach (ulong user in data)
        lines.Add(user.ToString());

      File.WriteAllLines(path, lines);
    }
    #endregion
  }
}