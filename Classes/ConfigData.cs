using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;


namespace KompasTools.Classes
{
     internal class ConfigData
    {
        [JsonProperty("dir_update")]
        public string DirUpdate { get; set; } = @"\\auxserver\Обменник_ОГК\199 Компас\KompasTools";
        [JsonProperty("path_drawing_kompas")]
        public string PathDrawingKompas { get; set; } = "\\\\auxserver\\ОГК\\0. Чертежи компас";
        [JsonProperty("path_completed_drawing")]
        public string PathCompletedDrawing { get; set; } = "\\\\auxserver\\ОГК\\4. Завершённые_объекты_pdf";
        [JsonProperty("path_model3D")]
        public string PathModel3D { get; set; } = "\\\\auxserver\\ОГК\\7. 3D";

    }
}
