using DocumentFormat.OpenXml.Wordprocessing;
using Kompas6API5;
using Kompas6Constants;
using KompasAPI7;
using KompasTools.Utils;
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
        private double paramN;
        private double minUserAngle;
        private double maxUserAngle;
        private string? rangeUserAngle;
        private WeldTypeEnum weldType;
        private NatureSeamPerformedEnum natureSeamPerformed;
        private double paramE;
        private double[] paramETolerance = new double[2];
        private double paramE1;
        private double[] paramE1Tolerance = new double[2];
        private double paramG;
        private double[] paramGTolerance = new double[2];
        private double paramG1;
        private double[] paramG1Tolerance = new double[2];
        private DependenceSeamThicknessEnum dependenceSeamThickness;

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
        /// n, уступ деталей для углового типа соединения
        /// </summary>
        public double ParamN { get => paramN; set => paramN = value; }
        /// <summary>
        /// Минимальный угол диапазона
        /// </summary>
        public double MinUserAngle { get => minUserAngle; set => minUserAngle = value; }
        /// <summary>
        /// Максимальный угол диапазона
        /// </summary>
        public double MaxUserAngle { get => maxUserAngle; set => maxUserAngle = value; }
        public string? RangeUserAngle { get => $"{MinUserAngle}-{MaxUserAngle}"; }
        /// <summary>
        /// Тип шва
        /// </summary>
        public WeldTypeEnum WeldType { get => weldType; set => weldType = value; }
        /// <summary>
        /// Характер выполненного шва
        /// </summary>
        public NatureSeamPerformedEnum NatureSeamPerformed { get => natureSeamPerformed; set => natureSeamPerformed = value; }
        /// <summary>
        /// e, ширина большего валика
        /// </summary>
        public double ParamE { get => paramE; set => paramE = value; }
        /// <summary>
        /// Допуск на e, ширина большего валика
        /// </summary>
        public double[] ParamETolerance { get => paramETolerance; set => paramETolerance = value; }
        /// <summary>
        /// e1, ширина меньшего валика
        /// </summary>
        public double ParamE1 { get => paramE1; set => paramE1 = value; }
        /// <summary>
        /// Допуск на e1, ширина меньшего валика
        /// </summary>
        public double[] ParamE1Tolerance { get => paramE1Tolerance; set => paramE1Tolerance = value; }
        /// <summary>
        /// g, высота большего валика
        /// </summary>
        public double ParamG { get => paramG; set => paramG = value; }
        /// <summary>
        /// допуск на g, высота большего валика
        /// </summary>
        public double[] ParamGTolerance { get => paramGTolerance; set => paramGTolerance = value; }
        /// <summary>
        /// g1, высота меньшего валика
        /// </summary>
        public double ParamG1 { get => paramG1; set => paramG1 = value; }
        /// <summary>
        /// допуск на g1, высота меньшего валика
        /// </summary>
        public double[] ParamG1Tolerance { get => paramG1Tolerance; set => paramG1Tolerance = value; }
        /// <summary>
        /// Зависимость параметров шва от толщины
        /// </summary>        
        public DependenceSeamThicknessEnum DependenceSeamThickness { get => dependenceSeamThickness; set => dependenceSeamThickness = value; }



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

        

        public void DrawingJoint()
        {

        }

        public void DrawingSeam()
        {

        }

        public void DrawingPart(KompasObject kompas, double thickness, LocationPart locationPart, bool numberPar, bool drawDimensions,
            TransitionTypeEnum transitionTypeUp, TransitionTypeEnum transitionTypeBottom, double extraLength = 20)
        {
            #region Проверка входящих данных
            if (kompas == null)
            {
                MessageBox.Show($"При создании детали не найден активный Компас");
                return;
            }
            if (thickness <= 0)
            {
                MessageBox.Show($"При создании детали толщина равна или меньше нуля");
                return;
            }
            #endregion
            IApplication application = (IApplication)kompas.ksGetApplication7();
            IKompasDocument? kompasDocument = application.ActiveDocument;
            IKompasDocument2D kompasDocument2D = (IKompasDocument2D)kompasDocument;
            IKompasDocument2D1 kompasDocument2D1 = (IKompasDocument2D1)kompasDocument;
            IViewsAndLayersManager viewsAndLayersManager = kompasDocument2D.ViewsAndLayersManager;
            IViews views = viewsAndLayersManager.Views;
            IView activeView = views.ActiveView;
            IDrawingContainer drawingContainer = (IDrawingContainer)activeView;
            ISymbols2DContainer symbols2DContainer = (ISymbols2DContainer)activeView;
            ILineSegments lineSegments = drawingContainer.LineSegments;
            //Если чертим вторую деталь то меняем направление на противомоложное
            if (!numberPar)
            {
                switch (locationPart)
                {
                    case LocationPart.Лево_Верх:
                        locationPart = LocationPart.Право_Верх;
                        break;
                    case LocationPart.Право_Верх:
                        locationPart = LocationPart.Лево_Верх;
                        break;
                    case LocationPart.Лево_Низ:
                        locationPart = LocationPart.Право_Низ;
                        break;
                    case LocationPart.Право_Низ:
                        locationPart = LocationPart.Лево_Низ;
                        break;
                    case LocationPart.Верх_Лево:
                        locationPart = LocationPart.Низ_Лево;
                        break;
                    case LocationPart.Низ_Лево:
                        locationPart = LocationPart.Верх_Лево;
                        break;
                    case LocationPart.Верх_Право:
                        locationPart = LocationPart.Низ_Право;
                        break;
                    case LocationPart.Низ_Право:
                        locationPart = LocationPart.Верх_Право;
                        break;
                    default:
                        break;
                }
            }
            ShapePreparedEdgesEnum shapePreparedEdges;
            if (numberPar)
            {
                shapePreparedEdges = shapePreparedEdgesPart1;
            }
            else
            {
                shapePreparedEdges = shapePreparedEdgesPart2;
            }
            switch (shapePreparedEdges)
            {
                case ShapePreparedEdgesEnum.НЕ_УКАЗАНО:
                    break;
                case ShapePreparedEdgesEnum.Без_скоса:
                    break;
                case ShapePreparedEdgesEnum.Без_притупления:
                    break;
                case ShapePreparedEdgesEnum.Со_скосом_одной_кромки:
                    switch (locationPart)
                    {
                        case LocationPart.Лево_Верх:
                            break;
                        case LocationPart.Лево_Низ:
                            break;
                        case LocationPart.Право_Верх:
                            //Без переходов
                            if (transitionTypeBottom == TransitionTypeEnum.Без_перехода && transitionTypeUp == TransitionTypeEnum.Без_перехода)
                            {
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                List<object> forboundaries = new();
                                IDrawingGroups drawingGroups = kompasDocument2D1.DrawingGroups;
                                IDrawingGroup drawingGroup = drawingGroups.Add(true, "Сварка");
                                drawingGroup.Open();
                                forboundaries.Add(DrawLineSegment(0, 0, 0, ParamC));
                                forboundaries.Add(DrawLineSegment(0, ParamC, xangle, thickness));
                                forboundaries.Add(DrawLineSegment(xangle, thickness, xangle + extraLength, thickness));
                                forboundaries.Add(DrawLineSegment(0, 0, xangle + extraLength, 0));
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = xangle + extraLength;
                                waveLine.Y1 = 0;
                                waveLine.X2 = xangle + extraLength;
                                waveLine.Y2 = thickness;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                forboundaries.Add(waveLine);
                                drawingGroup.Close();
                                //Штриховка
                                IHatches hatches = drawingContainer.Hatches;
                                IHatch hatch = hatches.Add();
                                IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                boundariesObject.AddBoundaries(forboundaries.ToArray(), false);
                                hatch.Update();

                                ksDocument2D document2DAPI5 = (ksDocument2D)kompas.ActiveDocument2D();
                                double xpaste = 0; double ypaste = 0;
                                ksPhantom phantom = (ksPhantom)kompas.GetParamStruct(6);
                                phantom.phantom = 1; //Указываем тип фантом "Фантом для сдвига группы"
                                ksType1 type1 = (ksType1)phantom.GetPhantomParam();
                                type1.gr = drawingGroup.Reference;
                                if (document2DAPI5.ksCursorEx(null, ref xpaste, ref ypaste, phantom, null) == 0) //Вызываем курсор для указания точки вставки. Если была нажата Esc, прерываем вставку.
                                {
                                    document2DAPI5.ksDeleteObj(type1.gr);
                                    return;
                                }
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, xpaste, ypaste);
                                drawingGroup?.Store();
                            }

                            //Обычный переход вверху

                            //Обычный переход внизу

                            //Обычный переход вверху и внизу

                            break;
                        case LocationPart.Право_Низ:
                            break;
                        case LocationPart.Верх_Лево:
                            break;
                        case LocationPart.Верх_Право:
                            break;
                        case LocationPart.Низ_Лево:
                            break;
                        case LocationPart.Низ_Право:
                            break;
                        default:
                            break;
                    }                    
                    break;
                case ShapePreparedEdgesEnum.С_двумя_симметричными_скосами:
                    break;
                case ShapePreparedEdgesEnum.С_двумя_не_симметричными_скосами_h_со_стороны_угла:
                    break;
                case ShapePreparedEdgesEnum.С_двумя_не_симметричными_скосами_h_с_противоположной_стороны_угла:
                    break;
                case ShapePreparedEdgesEnum.Притупление_зависит_от_толщины:
                    break;
                case ShapePreparedEdgesEnum.Со_скосом_одной_кромки_без_притупления:
                    break;
                case ShapePreparedEdgesEnum.h_зависит_от_толщины:
                    break;
                default:
                    break;
            }



            ILineSegment DrawLineSegment(double x1, double y1, double x2, double y2)
            {
                ILineSegment lineSegment = lineSegments.Add();
                lineSegment.X1 = x1;
                lineSegment.Y1 = y1;
                lineSegment.X2 = x2;
                lineSegment.Y2 = y2;
                lineSegment.Update();
                return lineSegment;
            }
        }

        public void DrawingPart3D()
        {

        }


    }
}
