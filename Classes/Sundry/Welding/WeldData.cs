using KompasAPI7;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static KompasTools.Classes.Sundry.Welding.WeldEnum;

namespace KompasTools.Classes.Sundry.Welding
{
    public class WeldData
    {
        private string nameGost = "";
        private string nameWeldJoint = "";
        private ConnectionTypeEnum connectionType;
        private ShapePreparedEdgesEnum shapePreparedEdgesPart1;
        private ShapePreparedEdgesEnum shapePreparedEdgesPart2;
        private WeldingMethodEnum weldingMethod;
        private double thicknessMin;
        private double thicknessMax;
        private string? rangeThickness;
        private double paramC;
        private double[] paramCTolerance = new double[2];
        private double paramA;
        private double[] paramATolerance = new double[2];
        private double paramH;
        private double paramB;
        private double[] paramBTolerance = new double[2];

        /// <summary>
        /// Наименование ГОСТа
        /// </summary>
        public string NameGost { get => nameGost; set => nameGost = value; }
        /// <summary>
        /// Услоавное обозначение сварного соединения
        /// </summary>
        public string NameWeldJoint { get => nameWeldJoint; set => nameWeldJoint = value; }
        /// <summary>
        /// Тип соединения
        /// </summary>
        public ConnectionTypeEnum ConnectionType { get => connectionType; set => connectionType = value; }
        /// <summary>
        /// Форма подготовленных кромок первой детали
        /// </summary>
        public ShapePreparedEdgesEnum ShapePreparedEdgesPart1 { get => shapePreparedEdgesPart1; set => shapePreparedEdgesPart1 = value; }
        /// <summary>
        /// Форма подготовленных кромок второй детали
        /// </summary>
        public ShapePreparedEdgesEnum ShapePreparedEdgesPart2 { get => shapePreparedEdgesPart2; set => shapePreparedEdgesPart2 = value; }
        /// <summary>
        /// Способ сварки
        /// </summary>
        public WeldingMethodEnum WeldingMethod { get => weldingMethod; set => weldingMethod = value; }
        /// <summary>
        /// Минимальная толщина диапазона. Включается в диапазон
        /// </summary>
        public double ThicknessMin { get => thicknessMin; set => thicknessMin = value; }
        /// <summary>
        /// Максимальная толщина диапазона. Не включается в диапазон
        /// </summary>
        public double ThicknessMax { get => thicknessMax; set => thicknessMax = value; }
        /// <summary>
        /// Диапазон толщин
        /// </summary>
        public string? RangeThickness { get => $"{ThicknessMin}-{ThicknessMax}"; }
        /// <summary>
        /// с, притупление
        /// </summary>
        public double ParamC { get => paramC; set => paramC = value; }
        /// <summary>
        /// Допуск на с, притупление
        /// </summary>
        public double[] ParamCTolerance { get => paramCTolerance; set => paramCTolerance = value; }
        /// <summary>
        /// Угол скоса детали
        /// </summary>
        public double ParamA { get => paramA; set => paramA = value; }
        /// <summary>
        /// Допуск на угол скоса детали
        /// </summary>
        public double[] ParamATolerance { get => paramATolerance; set => paramATolerance = value; }
        /// <summary>
        /// h, смещение притупления
        /// </summary>
        public double ParamH { get => paramH; set => paramH = value; }
        /// <summary>
        /// b, зазор в стыке
        /// </summary>
        public double ParamB { get => paramB; set => paramB = value; }
        /// <summary>
        /// Допуск на b, зазор в стыке
        /// </summary>
        public double[] ParamBTolerance { get => paramBTolerance; set => paramBTolerance = value; }



        /// <summary>
        /// Проверка вхождения толщины детали в диапазон толщин
        /// </summary>
        /// <param name="thickness"></param>
        /// <returns></returns>
        public bool CheckThickness(double thickness)
        {
            if (thickness >= thicknessMin && thickness < thicknessMax)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        

        public void DrawingJoint(IView view, double thickness, LocationPart locationPart, bool drawDimensions, double extraLength = 20)
        {

        }

        public void DrawingSeam(IView view, double thickness, LocationPart locationPart, bool drawDimensions, double extraLength = 20)
        {

        }

        public void DrawingPart(IView view, double thickness, LocationPart locationPart, bool drawDimensions, double extraLength = 20)
        {
            #region Проверка входящих данных
            if (view == null)
            {
                MessageBox.Show($"При создании детали view равен null");
                return;
            }
            if (thickness <= 0)
            {
                MessageBox.Show($"При создании детали толщина равна или меньше нуля");
                return;
            }
            #endregion

            IDrawingContainer drawingContainer = (IDrawingContainer)view;
            ISymbols2DContainer symbols2DContainer = (ISymbols2DContainer)view;
            ILineSegments lineSegments = drawingContainer.LineSegments;
            //switch (ShapePrepared)
            //{
            //    case ShapePreparedEdges.Без_скоса:
            //        break;
            //    case ShapePreparedEdges.Скос_одной_кромки:
            //        {
            //            //double xangle = (thickness - Prit) * Math.Tan(angle * Math.PI / 180);
            //            //DrawLineSegment(0, 0, );
            //        }
            //        break;
            //    case ShapePreparedEdges.Два_симметричные_скоса:
            //        break;
            //    case ShapePreparedEdges.Два_не_симметричные_скоса:
            //        break;
            //}

            void DrawLineSegment(double x1, double y1, double x2, double y2)
            {
                ILineSegment lineSegment = lineSegments.Add();
                lineSegment.X1 = x1;
                lineSegment.Y1 = y1;
                lineSegment.X2 = x2;
                lineSegment.Y2 = y2;
                lineSegment.Update();

            }
        }

        public void DrawingPart3D(IView view, double thickness, LocationPart locationPart)
        {

        }


    }
}
