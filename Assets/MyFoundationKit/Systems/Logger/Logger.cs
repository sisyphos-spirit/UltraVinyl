using System;
using UnityEngine;
using System.Collections.Generic;

public enum LogCategory
{
    General,
    Grid,
    Time,
    Save,
    Event,
    StateMachine,
    Pathfinding,
    Combat,
    AI,
    Bootstrap
}

public static class LoggerSettings
{
    private static readonly HashSet<LogCategory> DisabledCategories = new();

    public static void Disable(LogCategory category)
    {
        DisabledCategories.Add(category);
    }

    public static void Enable(LogCategory category)
    {
        DisabledCategories.Remove(category);
    }

    public static bool IsEnabled(LogCategory category)
    {
        return !DisabledCategories.Contains(category);
    }
}

public static class Logger
{
    public static void Grid(string message, object sender = null)
        => Log(LogCategory.Grid, message, sender);

    public static void Time(string message, object sender = null)
        => Log(LogCategory.Time, message, sender);

    public static void Event(string message, object sender = null)
        => Log(LogCategory.Event, message, sender);

    public static void Save(string message, object sender = null)
        => Log(LogCategory.Save, message, sender);

    public static void Bootstrap(string message, object sender = null)
        => Log(LogCategory.Bootstrap, message, sender);

    public static void StateMachine(string message, object sender = null)
        => Log(LogCategory.StateMachine, message, sender);

    public static void Pathfinding(string message, object sender = null)
        => Log(LogCategory.Pathfinding, message, sender);

    public static void Combat(string message, object sender = null)
        => Log(LogCategory.Combat, message, sender);

    public static void AI(string message, object sender = null)
        => Log(LogCategory.AI, message, sender);

    public static void TimeWarning(string message, object sender = null)
        => Warning(LogCategory.Time, message, sender);

    public static void SaveWarning(string message, object sender = null)
        => Warning(LogCategory.Save, message, sender);

    public static void BootstrapWarning(string message, object sender = null)
        => Warning(LogCategory.Bootstrap, message, sender);

    public static void BootstrapError(string message, object sender = null)
        => Error(LogCategory.Bootstrap, message, sender);

    

    private static void Log(
        LogCategory category,
        string message,
        object sender)
    {
        if (!LoggerSettings.IsEnabled(category))
            return;

        Debug.Log(
            Format(category, message, sender, GetColor(category))
        );
    }

    private static void Warning(
        LogCategory category,
        string message,
        object sender)
    {
        if (!LoggerSettings.IsEnabled(category))
            return;

        Debug.LogWarning(
            Format(category, message, sender, "#FFD54F")
        );
    }

    private static void Error(
        LogCategory category,
        string message,
        object sender)
    {
        if (!LoggerSettings.IsEnabled(category))
            return;

        Debug.LogError(
            Format(category, message, sender, "#FF5252")
        );
    }

    private static string Format(
        LogCategory category,
        string message,
        object sender,
        string color)
    {
        string time =
            DateTime.Now.ToString("HH:mm:ss");

        string senderName =
            sender == null
                ? "Unknown"
                : sender.GetType().Name;

        return
            //$"[{time}] " +
            $"<b><color={color}>[{category}]</color></b> " +
            $"<b>[{senderName}]</b> " +
            $"{message}";
    }

    private static string GetColor(
        LogCategory category)
    {
        return category switch
        {
            LogCategory.Grid => "#00BFFF",
            LogCategory.Time => "#C77DFF",
            LogCategory.Save => "#66BB6A",
            LogCategory.Event => "#FF9800",
            LogCategory.StateMachine => "#F06292",
            LogCategory.Pathfinding => "#26C6DA",
            LogCategory.Combat => "#EF5350",
            LogCategory.AI => "#AB47BC",
            LogCategory.Bootstrap => "#BDBDBD",
            _ => "#FFFFFF"
        };
    }
}
