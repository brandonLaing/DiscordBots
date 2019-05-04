using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
        string _dataPath = CurrentDirectory + @"\Data";
        if (!Directory.Exists(_dataPath))
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
        string _LogSaveDirectory = DataPath + @"\SaveLog.txt";
        if (!File.Exists(_LogSaveDirectory))
          File.Create(_LogSaveDirectory);
        return _LogSaveDirectory;
      }
    }
    /// <summary>
    /// Path to mocked save file
    /// </summary>
    private static string MockedSaveDirectory
    {
      get
      {
        string _MockedSaveDirectory = DataPath + @"\Mocked.txt";
        if (!File.Exists(_MockedSaveDirectory))
          File.Create(_MockedSaveDirectory);
        return _MockedSaveDirectory;
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
    public static List<SocketUser> Mocked
    {
      get
      {
        return GetUsers(MockedSaveDirectory);
      }
      set
      {
        Console.WriteLine("In mocked property");
        SetUsers(MockedSaveDirectory, value);
      }
    }

    private static List<SocketUser> GetUsers(string path)
    {
      List<SocketUser> users = new List<SocketUser>();
      foreach(string line in File.ReadAllLines(path))
      {
        ulong.TryParse(line, out ulong id);
        Starter.Client.GetUser(id);
      }
      return users;
    }

    private static void SetUsers(string path, List<SocketUser> users)
    {
      List<string> lines = new List<string>();
      foreach (SocketUser user in users)
      {
        Console.WriteLine(user.Id.ToString());
        lines.Add(user.Id.ToString());
      }

      File.WriteAllLines(path, lines);
    }
    #endregion
  }
}