using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Log
    {
        public static readonly LogLevel m_LogLevel = LogLevel.LevelInfo;

        public enum LogLevel
        {
            LevelError = 0,
            LevelWarning,
            LevelInfo
        };

        public static void DebugLog<T>(string message, LogLevel level) where T : class
        {
            if (level <= m_LogLevel)
            {
                Debug.Log(typeof(T) + " " + level + " " + message);
            }
        }

        public static void Info(string message)
        {
            if (m_LogLevel <= LogLevel.LevelInfo)
            {
                Debug.Log("[Info]: " + message);
            }
        }

        public static void Error(string message)
        {
            if (m_LogLevel <= LogLevel.LevelError)
            {
                Debug.Log("[ERROR]: " + message);
            }
        }

        public static void Warning(string message)
        {
            if (m_LogLevel <= LogLevel.LevelWarning)
            {
                Debug.Log("[WARNING]: " + message);
            }
        }
    }
}