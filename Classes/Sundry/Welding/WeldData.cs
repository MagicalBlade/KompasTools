﻿using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Kompas6API5;
using Kompas6Constants;
using KompasAPI7;
using KompasTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
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

        public void DrawingPart(IView view, double thickness, LocationPart locationPart, bool numberPar, bool drawDimensions,
            TransitionTypeEnum transitionTypeUp, TransitionTypeEnum transitionTypeBottom, IDrawingGroup drawingGroup, double gapDimToPart, double gapDimToDim,
            double gapDimToPartLeft, double extraLength)
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
            ILineDimensions lineDimensions = symbols2DContainer.LineDimensions;
            IAngleDimensions angleDimensions = symbols2DContainer.AngleDimensions;
            //Если чертим вторую деталь то меняем направление на противоположное
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
                                //Размер скоса
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                extraLength = xangle;
                                //Чертим графику
                                //Создаём основу разделки
                                ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, 0, 0, ParamC);
                                ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, 0, ParamC, xangle, thickness);
                                DrawLineSegment(lineSegments, xangle, thickness, xangle + extraLength, thickness);
                                DrawLineSegment(lineSegments, 0, 0, xangle + extraLength, 0);
                                IWaveLines waveLines = symbols2DContainer.WaveLines; //Волнистая линия
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = xangle + extraLength;
                                waveLine.Y1 = 0;
                                waveLine.X2 = xangle + extraLength;
                                waveLine.Y2 = thickness;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();

                                //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                IDrawingContour drawingContour = drawingContours.Add();
                                IContour contour = (IContour)drawingContour;
                                //Добавляем в контур элементы из группы созданные до этой строки
                                contour.CopySegments(drawingGroup.Objects[0], false);
                                drawingContour.Update();
                                //Штриховка
                                IHatches hatches = drawingContainer.Hatches;
                                IHatch hatch = hatches.Add();
                                IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                boundariesObject.AddBoundaries(drawingContour, true);
                                hatch.Update(); 

                                //Чертим размеры
                                if (drawDimensions)
                                {
                                    LineDimension(lineDimensions, xangle + extraLength, 0, xangle + extraLength, thickness, xangle + extraLength + gapDimToPartLeft, thickness / 2
                                        , ksLineDimensionOrientationEnum.ksLinDVertical);
                                    LineDimension(lineDimensions, 0, ParamC, xangle, thickness, xangle / 2, thickness + gapDimToPart,
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    IDimensionText dtParamC = (IDimensionText)LineDimension(lineDimensions, 0, 0, 0, ParamC, - gapDimToPart, - 1,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation(dtParamC, paramCTolerance);
                                    double r1 = (thickness - ParamC + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(thickness - ParamC + gapDimToPart + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        xangle / 2, thickness + gapDimToPart + gapDimToDim, angleDRadius);
                                    SetDeviation(dtParamA, ParamATolerance);
                                }
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



            
            

        }

        public void DrawingPart3D()
        {

        }


        //Статичные методы
        /// <summary>
        /// Создание отрезка
        /// </summary>
        /// <param name="lineSegments"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        private static ILineSegment DrawLineSegment(ILineSegments lineSegments, double x1, double y1, double x2, double y2)
        {
            ILineSegment lineSegment = lineSegments.Add();
            lineSegment.X1 = x1;
            lineSegment.Y1 = y1;
            lineSegment.X2 = x2;
            lineSegment.Y2 = y2;
            lineSegment.Update();
            return lineSegment;
        }
        /// <summary>
        /// Создание линейного размера
        /// </summary>
        /// <param name="lineDimensions"></param>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <param name="orientation"></param>
        /// <returns></returns>
        private static ILineDimension LineDimension(ILineDimensions lineDimensions, double x1, double y1, double x2, double y2, double x3, double y3,
            ksLineDimensionOrientationEnum orientation)
        {
            ILineDimension lineDimension = lineDimensions.Add();
            IDimensionParams dimensionParams = (IDimensionParams)lineDimension;
            dimensionParams.InitDefaultValues();
            dimensionParams.TextType = ksDimensionTextTypeEnum.ksDimTManual;
            lineDimension.X1 = x1;
            lineDimension.Y1 = y1;
            lineDimension.X2 = x2;
            lineDimension.Y2 = y2;
            lineDimension.X3 = x3;
            lineDimension.Y3 = y3;
            lineDimension.Orientation = orientation;
            lineDimension.Update();
            //Необходимо для размеров у которых текста размеры находится снаружи
            dimensionParams.TextType = ksDimensionTextTypeEnum.ksDimTAuto;
            lineDimension.Update();
            return lineDimension;
        }
        /// <summary>
        /// Создание углового размера
        /// </summary>
        /// <param name="angleDimensions"></param>
        /// <param name="baseobjAngle1"></param>
        /// <param name="baseobjAngle2"></param>
        /// <param name="x3"></param>
        /// <param name="y3"></param>
        /// <returns></returns>
        private static IAngleDimension AngleDimension(IAngleDimensions angleDimensions, ILineSegment baseobjAngle1, ILineSegment baseobjAngle2,
            double x3, double y3, double r)
        {
            IAngleDimension angleDimension = angleDimensions.Add(DrawingObjectTypeEnum.ksDrADimension);
            IDimensionParams dimensionParams = (IDimensionParams)angleDimension;
            dimensionParams.InitDefaultValues();
            angleDimension.BaseObject1 = baseobjAngle1;
            angleDimension.BaseObject2 = baseobjAngle2;
            angleDimension.X3 = x3;
            angleDimension.Y3 = y3;
            angleDimension.DimensionType = ksAngleDimTypeEnum.ksADMinAngle;
            angleDimension.Update();
            angleDimension.Radius = r;
            angleDimension.Update();
            return angleDimension;
        }

        /// <summary>
        /// Доабвление допуска к размеру
        /// </summary>
        /// <param name="dimensionText"></param>
        /// <param name="deviation"></param>
        private static void SetDeviation(IDimensionText dimensionText, double[] deviation)
        {
            dimensionText.HighDeviation.Str = deviation[0].ToString();
            dimensionText.LowDeviation.Str = deviation[1].ToString();
            dimensionText.DeviationOn = true;
            dimensionText.TextAlign = ksDimensionTextAlignEnum.ksDimACentre;
            ((DrawingObject)dimensionText).Update();
        }

    }
}
