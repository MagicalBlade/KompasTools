using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static KompasTools.Classes.Sundry.Welding.WeldEnum;

namespace KompasTools.Classes.Sundry.Welding
{
    internal class TransitionData
    {
        private double dimTHPart1 = 0;
        private double dimBHPart1 = 0;
        private double dimTHPart2 = 0;
        private double dimBHPart2 = 0;
        private TransitionTypeEnum transitionTypePart1 = TransitionTypeEnum.Без_перехода;
        private TransitionTypeEnum transitionTypePart2 = TransitionTypeEnum.Без_перехода;
    }
}
