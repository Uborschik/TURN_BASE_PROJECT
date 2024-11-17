using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Utils
{
    public static class FileHandler
    {
        public static void SaveToJSON<T>(T toSave, string fileName)
        {
            var path = GetPath(fileName);
            var content = JsonUtility.ToJson(toSave);

            WriteFile(path, content);

            Debug.Log(path);
        }

        public static void SaveToJSON<T>(List<T> toSave, string fileName)
        {
            var path = GetPath(fileName);
            var content = JsonHelper.ToJson(toSave.ToArray());

            WriteFile(path, content);

            Debug.Log(path);
        }

        public static bool TryReadListFromJSON<T>(string fileName, out List<T> values)
        {
            var path = GetPath(fileName);
            var content = ReadFile(path);

            if (string.IsNullOrEmpty(content) || content == "{}")
            {
                values = new List<T>();
                return false;
            }

            values = JsonHelper.FromJson<T>(content).ToList();
            return true;
        }

        public static bool TryReadFromJSON<T>(string fileName, out T value)
        {
            var path = GetPath(fileName);
            var content = ReadFile(path);

            if (string.IsNullOrEmpty(content) || content == "{}")
            {
                value = default;
                return false;
            }

            value = JsonUtility.FromJson<T>(content);
            return true;
        }

        private static string GetPath(string fileName)
        {
            return Application.persistentDataPath + "/" + fileName;
        }

        private static void WriteFile(string path, string content)
        {
            var fileStream = new FileStream(path, FileMode.Create);

            using (var streamWriter = new StreamWriter(fileStream))
            {
                streamWriter.Write(content);
            }
        }

        private static string ReadFile(string path)
        {
            if (File.Exists(path))
            {
                using (var streamReader = new StreamReader(path))
                {
                    var content = streamReader.ReadToEnd();
                    return content;
                }
            }

            return "";
        }
    }
}