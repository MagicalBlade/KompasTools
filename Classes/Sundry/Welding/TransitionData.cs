using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KompasTools.Classes.Sundry.Welding.WeldEnum;

namespace KompasTools.Classes.Sundry.Welding
{
    public class TransitionData
    {
        private double dimH = 0;
        private double dimL = 0;
        private TransitionTypeEnum transitionTypePart1 = TransitionTypeEnum.Без_перехода;
        private TransitionTypeEnum transitionTypePart2 = TransitionTypeEnum.Без_перехода;
        /// <summary>
        /// Высота перехода
        /// </summary>
        public double DimH { get => dimH; set => dimH = value; }
        /// <summary>
        /// Длина перехода
        /// </summary>
        public double DimL { get => dimL; set => dimL = value; }
        /// <summary>
        /// Тип перехода первой детали
        /// </summary>
        public TransitionTypeEnum TransitionTypePart1 { get => transitionTypePart1; set => transitionTypePart1 = value; }
        /// <summary>
        /// Тип перехода второй детали
        /// </summary>
        public TransitionTypeEnum TransitionTypePart2 { get => transitionTypePart2; set => transitionTypePart2 = value; }

        public TransitionData(TransitionTypeEnum transitionType, double thickness1, double thickness2, double dimL, bool indicateDimL)
        {
            if (thickness1 == thickness2 || transitionType == TransitionTypeEnum.Без_перехода)
            {
                return;
            }
            if (thickness1 > thickness2)
            {
                transitionTypePart1 = transitionType;
            }
            else
            {
                transitionTypePart2 = transitionType;
            }
            if (transitionType == TransitionTypeEnum.Симметричный)
            {
                dimH = Math.Abs((thickness1 - thickness2) / 2);
            }
            else
            {
                dimH = Math.Abs(thickness1 - thickness2);
            }
            //Если пользователь выбрать указать длину то просто приравниваем. Иначе это кофициент, а значит перемножаем
            if (indicateDimL)
            {
                DimL = dimL;
            }
            else
            {
                DimL = dimH * dimL;
            }
        }
    }
}