using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KompasTools.Classes.Sundry.Welding.WeldEnum;

namespace KompasTools.Classes.Sundry.Welding
{
    static public class WeldEnum
    {
        /// <summary>
        /// Тип соединения
        /// </summary>
        public enum ConnectionTypeEnum
        {
            НЕ_УКАЗАНО,
            Стыковое,
            Угловое,
            Тавровое
        }
        /// <summary>
        /// Форма подготовленных кромок
        /// </summary>
        public enum ShapePreparedEdgesEnum
        {
            НЕ_УКАЗАНО,
            Без_скоса_стыковое,
            Без_скоса_тавровое,
            Без_скоса_угловое,
            Без_притупления,
            Со_скосом_одной_кромки,
            С_двумя_симметричными_скосами,
            С_двумя_не_симметричными_скосами_h_со_стороны_угла,
            С_двумя_не_симметричными_скосами_h_с_противоположной_стороны_угла,
            Притупление_зависит_от_толщины,
            Со_скосом_одной_кромки_без_притупления,
            h_зависит_от_толщины

        }
        /// <summary>
        /// Размещение детали
        /// </summary>
        public enum LocationPart
        {
            Лево_Верх,
            Лево_Низ,
            Право_Верх,
            Право_Низ,
            Верх_Лево,
            Верх_Право,
            Низ_Лево,
            Низ_Право
        }
        /// <summary>
        /// Тип перехода
        /// </summary>
        public enum TransitionTypeEnum
        {
            Без_перехода,
            Обычный,
            Занижение
        }

        /// <summary>
        /// Способ сварки
        /// </summary>
        public enum WeldingMethodEnum
        {
            НЕ_УКАЗАНО,
            Р,
            АФ,
            АФф,
            АФм,
            АФо,
            АФп,
            АФш,
            АФк,
            МФ,
            МФо,
            МФш,
            МФк,
            А,
            Ас,
            Апш,
            П,
            Пс,
            Ппш,
            ИН,
            ИНп,
            ИП,
            УП
        }

        /// <summary>
        /// Тип шва
        /// </summary>
        public enum WeldTypeEnum
        {
            НЕ_УКАЗАНО,
            Катет,
            Параметрический
        }
        /// <summary>
        /// Характер выполненного шва
        /// </summary>
        public enum NatureSeamPerformedEnum
        {
            НЕ_УКАЗАНО,
            Односторонний,
            Односторонний_на_съемной_подкладке,
            Односторонний_на_остающейся_подкладке,
            Двухсторонний_симметричный,
            Двухсторонний_не_симметричный
        }
        /// <summary>
        /// Зависимость параметров шва от толщины
        /// </summary>
        public enum DependenceSeamThicknessEnum
        {
            НЕ_УКАЗАНО,
            Не_зависят,
            Зависит_только_e,
            Зависит_только_g,
            Зависят_оба
        }
    }
}
