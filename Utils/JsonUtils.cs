using System;
using System.IO;
using System.Security.AccessControl;
using System.Windows;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace KompasTools.Utils
{
    internal static class JsonUtils
    {
        /// <summary>
        /// Десериализация json. Обернуть в try catch
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <returns></returns>
        public static T Deserialize<T>(string path)
        {
            T? model = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return model;
        }
        public static void Serialize<T>(string path, T? obj)
        {
            JsonSerializer serializer = new();
            serializer.Formatting = Formatting.Indented;
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            using StreamWriter sw = new(path, false);
            using JsonWriter jw = new JsonTextWriter(sw);
            serializer.Serialize(jw, obj);
        }
    }
}
