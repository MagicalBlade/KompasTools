using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace KompasTools.Classes
{
     internal class ConfigData
    {
        /// <summary>
        /// Путь к папке обновления
        /// </summary>
        private string _dirUpdate = @"\\auxserver\Обменник_ОГК\199 Компас\KompasTools";
        [JsonProperty("dir_update")]
        public string DirUpdate { get => _dirUpdate; set => _dirUpdate = value; }
    }
}
