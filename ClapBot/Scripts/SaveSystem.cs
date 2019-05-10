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
        string _dataPath = Path.Combine(Directory.GetParent(CurrentDirectory).ToString(), "Data");
        if (!File.Exists(_dataPath))
          Directory.CreateDirectory(_dataPath);
        return _dataPath;
      }
    }

    /// <summary>
    /// Path to logs save file
    /// </summary>
    private static string LogSaveDirectory
    {
      get
      {
        return Path.Combine(DataPath, "ClapLog.txt");
      }
    }
    /// <summary>
    /// Path to mocked save file
    /// </summary>
    private static string MockedSaveDirectory
    {
      get
      {
        return Path.Combine(DataPath, "Mocked.txt");
      }
    }
    /// <summary>
    /// Path to mocked user save file
    /// </summary>
    private static string ReactUserSaveDirectory
    {
      get
      {
        return Path.Combine(DataPath, "ReactUser.txt");
      }
    }
    /// <summary>
    /// Path to mocked save channel
    /// </summary>
    private static string ReactChannelDirectory
    {
      get
      {
        return Path.Combine(DataPath, "ReactChannel.txt");
      }
    }
    #endregion

    #region ImportantFiles
    public static string GetKey()
    {
      return string.Empty;
    }

    public static List<ulong> GetPriorityIds()
    {
      return null;
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
      List<ulong> mockedIds = LoadUlongData(MockedSaveDirectory);
      string ids = string.Empty;
      foreach (ulong id in mockedIds)
        ids += id.ToString() + ", ";

      await ClientConsole.Log("Save System", $"Getting mocked users | {ids}");
      return mockedIds;
    }

    /// <summary>
    /// Adds user to mocked list
    /// </summary>
    /// <param name="userId">Id to add</param>
    public static async Task AddMocked(ulong userId)
    {
      var mocked = await GetMocked();
      if (!mocked.Contains(userId))
      {
        mocked.Add(userId);
        SaveUlongData(MockedSaveDirectory, mocked);
        await ClientConsole.Log("Save System", $"Adding {userId} to mocked list");
      }
    }

    /// <summary>
    /// Removes a user from the mocked list
    /// </summary>
    /// <param name="userId">Id to remove</param>
    public static async Task RemoveMocked(ulong userId)
    {
      var mocked = LoadUlongData(MockedSaveDirectory);
      if (mocked.Contains(userId))
      {
        mocked.Remove(userId);
        SaveUlongData(MockedSaveDirectory, mocked);
        await ClientConsole.Log("Save System", $"Removing {userId} from mocked list");
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
      List<ulong> reactUsers = LoadUlongData(ReactUserSaveDirectory);
      string ids = string.Empty;
      foreach (ulong id in reactUsers)
        ids += id.ToString() + ", ";

      await ClientConsole.Log("Save System", $"Getting react users | {ids}");
      return reactUsers;
    }

    /// <summary>
    /// Adds user to react list
    /// </summary>
    /// <param name="userId">Id to add</param>
    /// <returns></returns>
    public static async Task AddReactUser(ulong userId)
    {
      var reactUser = await GetReactUser();
      if (!reactUser.Contains(userId))
      {
        reactUser.Add(userId);
        SaveUlongData(ReactUserSaveDirectory, reactUser);
        await ClientConsole.Log("Save System", $"Adding {userId} to react users list");
      }
    }

    /// <summary>
    /// Removes user from react list
    /// </summary>
    /// <param name="userId">Id to remove</param>
    /// <returns></returns>
    public static async Task RemoveReactUser(ulong userId)
    {
      var reactUser = await GetReactUser();
      if (reactUser.Contains(userId))
      {
        reactUser.Remove(userId);
        SaveUlongData(ReactUserSaveDirectory, reactUser);
        await ClientConsole.Log("Save System", $"Removing {userId} from react user list");
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
      List<ulong> reactChannels = LoadUlongData(ReactChannelDirectory);
      string ids = string.Empty;
      foreach (ulong id in reactChannels)
        ids += id.ToString() + ", ";

      await ClientConsole.Log("Save System", $"Getting react channels | {ids}");
      return reactChannels;
    }

    /// <summary>
    /// Adds channel to react list
    /// </summary>
    /// <param name="channelId">Channel id to add</param>
    public static async Task AddReactChannel(ulong channelId)
    {
      var reactChannel = await GetReactChannel();
      if (!reactChannel.Contains(channelId))
      {
        reactChannel.Add(channelId);
        SaveUlongData(ReactChannelDirectory, reactChannel);
        await ClientConsole.Log("Save System", $"Adding {channelId} to react channel");
      }
    }

    /// <summary>
    /// Removes channel from react list
    /// </summary>
    /// <param name="channelId">Channel id to remove</param>
    public static async Task RemoveReactChannel(ulong channelId)
    {
      var reactChannel = await GetReactChannel();
      if (reactChannel.Contains(channelId))
      {
        reactChannel.Remove(channelId);
        SaveUlongData(ReactChannelDirectory, reactChannel);
        await ClientConsole.Log("Save System", $"Removing {channelId} from react channel");
      }
    }
    #endregion

    #region File read/write
    /// <summary>
    /// Loads ulong data from file
    /// </summary>
    /// <param name="path">Path of file to load from</param>
    /// <returns>List of ulongs</returns>
    private static List<ulong> LoadUlongData(string path)
    {
      List<ulong> users = new List<ulong>();
      foreach (string line in File.ReadAllLines(path))
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