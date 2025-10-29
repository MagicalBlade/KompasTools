using DocumentFormat.OpenXml.EMMA;
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
using System.Transactions;
using System.Windows.Forms;
using System.Windows.Media;
using static KompasTools.Classes.Sundry.Welding.WeldEnum;
using static System.TimeZoneInfo;

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
        /// Условное обозначение сварного соединения
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
        /// Удлинение для деталей без разделки
        /// </summary>
        private const double extraLengthNotChamfer = 10;

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
        

        public void DrawingJoint(KompasObject kompas, IView view, double thickness, LocationPart locationPart, bool drawDimensions,            
            IDrawingGroup drawingGroup, double gapDimToPart, double gapDimToDim, double gapDimToPartLeft, double extraLength, bool isCrossSection,
            bool isHatches, TransitionData transitionData)
        {
            if (kompas.ActiveDocument2D() is not ksDocument2D document2DAPI5) return;
            IDrawingContainer drawingContainer = (IDrawingContainer)view;
            ISymbols2DContainer symbols2DContainer = (ISymbols2DContainer)view;
            ILineSegments lineSegments = drawingContainer.LineSegments;
            ILineDimensions lineDimensions = symbols2DContainer.LineDimensions;
            IAngleDimensions angleDimensions = symbols2DContainer.AngleDimensions;
            switch (NameGost, ConnectionType, ShapePreparedEdgesPart1, ShapePreparedEdgesPart2)
            {
                case ("5264-80" or "8713-79" or "14771-76", ConnectionTypeEnum.Стыковое, ShapePreparedEdgesEnum.Без_скоса_стыковое, ShapePreparedEdgesEnum.Без_скоса_стыковое):
                    switch (transitionData.TransitionTypePart1, transitionData.TransitionTypePart2)
                    {                       
                        case (TransitionTypeEnum.Симметричный, TransitionTypeEnum.Без_перехода) or (TransitionTypeEnum.Без_перехода, TransitionTypeEnum.Симметричный):
                            #region Чтобы не плодить код, меняю положение детали на противоположное.
                            if (transitionData.TransitionTypePart1 == TransitionTypeEnum.Без_перехода && transitionData.TransitionTypePart2 == TransitionTypeEnum.Симметричный)
                            {
                                (transitionData.TransitionTypePart1, transitionData.TransitionTypePart2) = (transitionData.TransitionTypePart2, transitionData.TransitionTypePart1);
                                switch (locationPart)
                                {
                                    case LocationPart.Лево_Верх:
                                        locationPart = LocationPart.Право_Верх;
                                        break;
                                    case LocationPart.Лево_Низ:
                                        locationPart = LocationPart.Право_Низ;
                                        break;
                                    case LocationPart.Право_Верх:
                                        locationPart = LocationPart.Лево_Верх;
                                        break;
                                    case LocationPart.Право_Низ:
                                        locationPart = LocationPart.Лево_Низ;
                                        break;
                                    case LocationPart.Верх_Лево:
                                        locationPart = LocationPart.Низ_Лево;
                                        break;
                                    case LocationPart.Верх_Право:
                                        locationPart = LocationPart.Низ_Право;
                                        break;
                                    case LocationPart.Низ_Лево:
                                        locationPart = LocationPart.Верх_Лево;
                                        break;
                                    case LocationPart.Низ_Право:
                                        locationPart = LocationPart.Верх_Право;
                                        break;
                                }
                            } 
                            #endregion
                            switch (locationPart)
                            {
                                case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный горизонтальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, thickness / 2, paramBManual / 2, thickness / 2,
                                                paramBManual / 2 + 1, thickness / 2 + transitionData.DimH + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);                                            
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, -paramBManual / 2 - transitionData.DimL, -thickness / 2 - transitionData.DimH, -paramBManual / 2, thickness / 2,
                                                -paramBManual / 2 - transitionData.DimL / 2, ldParamB.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + extraLength, -thickness / 2, paramBManual / 2 + extraLength, thickness / 2,
                                                paramBManual / 2 + extraLength +  gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, -ldTransitionL.Y1, ldThicknessR.X1, ldTransitionL.Y2,
                                                ldThicknessR.X3, -ldTransitionL.Y1 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, -ldTransitionL.Y2,
                                                ldThicknessR.X3, ldTransitionL.Y1 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1 - extraLength, -ldTransitionL.Y1, ldTransitionL.X1 - extraLength, ldTransitionL.Y1,
                                                ldTransitionL.X1 - extraLength - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Верх or LocationPart.Право_Низ:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный горизонтальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, thickness / 2, paramBManual / 2, thickness / 2,
                                                -paramBManual / 2 - 1, thickness / 2 + transitionData.DimH + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, paramBManual / 2 + transitionData.DimL, -thickness / 2 - transitionData.DimH, paramBManual / 2, thickness / 2,
                                                paramBManual / 2 + transitionData.DimL / 2, ldParamB.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, -paramBManual / 2 - extraLength, -thickness / 2, -paramBManual / 2 - extraLength, thickness / 2,
                                                -paramBManual / 2 - extraLength - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, -ldTransitionL.Y1, ldThicknessR.X1, ldTransitionL.Y2,
                                                ldThicknessR.X3, -ldTransitionL.Y1 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, -ldTransitionL.Y2,
                                                ldThicknessR.X3, ldTransitionL.Y1 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1 + extraLength, -ldTransitionL.Y1, ldTransitionL.X1 + extraLength, ldTransitionL.Y1,
                                                ldTransitionL.X1 + extraLength + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Верх_Лево or LocationPart.Верх_Право:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный вертикальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2, -thickness / 2, paramBManual / 2,
                                                -thickness / 2 - transitionData.DimH - gapDimToPart, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный вертикальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, thickness / 2 + transitionData.DimH, paramBManual / 2 + transitionData.DimL , ldParamB.X2, ldParamB.Y2,
                                                ldParamB.X3, paramBManual / 2 + transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - extraLength , thickness / 2, -paramBManual / 2 - extraLength,
                                                0, -paramBManual / 2 - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, -ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, ldThicknessR.Y1,
                                                -ldTransitionL.X1 - 1, ldThicknessR.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X2, ldThicknessR.Y2,
                                                ldTransitionL.X1 + 1, ldThicknessR.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1 + extraLength, -ldTransitionL.X1, ldTransitionL.Y1 + extraLength,
                                                0, ldTransitionL.Y1 + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Лево or LocationPart.Низ_Право:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный вертикальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2, -thickness / 2, paramBManual / 2,
                                                -thickness / 2 - transitionData.DimH - gapDimToPart, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный вертикальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, thickness / 2 + transitionData.DimH, -paramBManual / 2 - transitionData.DimL, ldParamB.X1, ldParamB.Y1,
                                                ldParamB.X3, -paramBManual / 2 - transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + extraLength, thickness / 2, paramBManual / 2 + extraLength,
                                                0, paramBManual / 2 + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, -ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, ldThicknessR.Y1,
                                                -ldTransitionL.X1 - 1, ldThicknessR.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X2, ldThicknessR.Y2,
                                                ldTransitionL.X1 + 1, ldThicknessR.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1 - extraLength, -ldTransitionL.X1, ldTransitionL.Y1 - extraLength,
                                                0, ldTransitionL.Y1 - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                            }
                        break;
                        case (TransitionTypeEnum.Вверх, TransitionTypeEnum.Без_перехода) or (TransitionTypeEnum.Без_перехода, TransitionTypeEnum.Вверх)
                            or (TransitionTypeEnum.Вниз, TransitionTypeEnum.Без_перехода) or (TransitionTypeEnum.Без_перехода, TransitionTypeEnum.Вниз):
                            #region Чтобы не плодить код, меняю положение детали и расположение перехода на противоположное.
                            if (transitionData.TransitionTypePart1 == TransitionTypeEnum.Без_перехода && transitionData.TransitionTypePart2 == TransitionTypeEnum.Вверх)
                            {
                                (transitionData.TransitionTypePart1, transitionData.TransitionTypePart2) = (transitionData.TransitionTypePart2, transitionData.TransitionTypePart1);
                                switch (locationPart)
                                {
                                    case LocationPart.Лево_Верх:
                                        locationPart = LocationPart.Право_Верх;
                                        break;
                                    case LocationPart.Лево_Низ:
                                        locationPart = LocationPart.Право_Низ;
                                        break;
                                    case LocationPart.Право_Верх:
                                        locationPart = LocationPart.Лево_Верх;
                                        break;
                                    case LocationPart.Право_Низ:
                                        locationPart = LocationPart.Лево_Низ;
                                        break;
                                    case LocationPart.Верх_Лево:
                                        locationPart = LocationPart.Низ_Лево;
                                        break;
                                    case LocationPart.Верх_Право:
                                        locationPart = LocationPart.Низ_Право;
                                        break;
                                    case LocationPart.Низ_Лево:
                                        locationPart = LocationPart.Верх_Лево;
                                        break;
                                    case LocationPart.Низ_Право:
                                        locationPart = LocationPart.Верх_Право;
                                        break;
                                }
                            }
                            if (transitionData.TransitionTypePart1 == TransitionTypeEnum.Вниз && transitionData.TransitionTypePart2 == TransitionTypeEnum.Без_перехода)
                            {
                                transitionData.TransitionTypePart1 = TransitionTypeEnum.Вверх;
                                switch (locationPart)
                                {
                                    case LocationPart.Лево_Верх:
                                        locationPart = LocationPart.Лево_Низ;
                                        break;
                                    case LocationPart.Лево_Низ:
                                        locationPart = LocationPart.Лево_Верх;
                                        break;
                                    case LocationPart.Право_Верх:
                                        locationPart = LocationPart.Право_Низ;
                                        break;
                                    case LocationPart.Право_Низ:
                                        locationPart = LocationPart.Право_Верх;
                                        break;
                                    case LocationPart.Верх_Лево:
                                        locationPart = LocationPart.Верх_Право;
                                        break;
                                    case LocationPart.Верх_Право:
                                        locationPart = LocationPart.Верх_Лево;
                                        break;
                                    case LocationPart.Низ_Лево:
                                        locationPart = LocationPart.Низ_Право;
                                        break;
                                    case LocationPart.Низ_Право:
                                        locationPart = LocationPart.Низ_Лево;
                                        break;
                                }
                            }
                            if (transitionData.TransitionTypePart1 == TransitionTypeEnum.Без_перехода && transitionData.TransitionTypePart2 == TransitionTypeEnum.Вниз)
                            {
                                transitionData.TransitionTypePart2 = TransitionTypeEnum.Вверх;
                                (transitionData.TransitionTypePart1, transitionData.TransitionTypePart2) = (transitionData.TransitionTypePart2, transitionData.TransitionTypePart1);
                                switch (locationPart)
                                {
                                    case LocationPart.Лево_Верх:
                                        locationPart = LocationPart.Право_Низ;
                                        break;
                                    case LocationPart.Лево_Низ:
                                        locationPart = LocationPart.Право_Верх;
                                        break;
                                    case LocationPart.Право_Верх:
                                        locationPart = LocationPart.Лево_Низ;
                                        break;
                                    case LocationPart.Право_Низ:
                                        locationPart = LocationPart.Лево_Верх;
                                        break;
                                    case LocationPart.Верх_Лево:
                                        locationPart = LocationPart.Низ_Право;
                                        break;
                                    case LocationPart.Верх_Право:
                                        locationPart = LocationPart.Низ_Лево;
                                        break;
                                    case LocationPart.Низ_Лево:
                                        locationPart = LocationPart.Верх_Право;
                                        break;
                                    case LocationPart.Низ_Право:
                                        locationPart = LocationPart.Верх_Лево;
                                        break;
                                }
                            } 
                            #endregion
                            switch (locationPart)
                            {                                
                                case LocationPart.Лево_Верх:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный горизонтальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, thickness / 2, paramBManual / 2, thickness / 2,
                                                paramBManual / 2 + 1, thickness / 2 + transitionData.DimH + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, ldParamB.X1 - transitionData.DimL, ldParamB.Y1 + transitionData.DimH, ldParamB.X1, ldParamB.Y2,
                                                -paramBManual / 2 - transitionData.DimL / 2, ldParamB.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + extraLength, -thickness / 2, paramBManual / 2 + extraLength, thickness / 2,
                                                paramBManual / 2 + extraLength + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X2, ldThicknessR.Y2,
                                                ldThicknessR.X3, ldTransitionL.Y1 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1 - extraLength, -ldParamB.Y1, ldTransitionL.X1 - extraLength, ldTransitionL.Y1,
                                                ldTransitionL.X1 - extraLength - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Лево_Низ:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный горизонтальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -thickness / 2, paramBManual / 2, -thickness / 2,
                                                paramBManual / 2 + 1, -thickness / 2 - transitionData.DimH - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                            {
                                                ldParamB.Y3 = -thickness / 2 - transitionData.DimH - gapDimToPart * 3;
                                                ldParamB.Update();
                                            }
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, ldParamB.X1 - transitionData.DimL, ldParamB.Y1 - transitionData.DimH, ldParamB.X1, ldParamB.Y2,
                                                -paramBManual / 2 - transitionData.DimL / 2, ldParamB.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + extraLength, thickness / 2, paramBManual / 2 + extraLength, -thickness / 2,
                                                paramBManual / 2 + extraLength + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X2, ldThicknessR.Y2,
                                                ldThicknessR.X3, ldTransitionL.Y1 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1 - extraLength, -ldParamB.Y1, ldTransitionL.X1 - extraLength, ldTransitionL.Y1,
                                                ldTransitionL.X1 - extraLength - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Верх:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный горизонтальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, thickness / 2, paramBManual / 2, thickness / 2,
                                                -paramBManual / 2 - 1, thickness / 2 + transitionData.DimH + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, ldParamB.X2 + transitionData.DimL, ldParamB.Y2 + transitionData.DimH, ldParamB.X2, ldParamB.Y2,
                                                paramBManual / 2 + transitionData.DimL / 2, ldParamB.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, -paramBManual / 2 - extraLength, -thickness / 2, -paramBManual / 2 - extraLength, thickness / 2,
                                                -paramBManual / 2 - extraLength - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, ldThicknessR.Y2,
                                                ldThicknessR.X3, ldTransitionL.Y1 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1 + extraLength, ldThicknessR.Y1, ldTransitionL.X1 + extraLength, ldTransitionL.Y1,
                                                ldTransitionL.X1 + extraLength + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Низ:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный горизонтальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -thickness / 2, paramBManual / 2, -thickness / 2,
                                                -paramBManual / 2 - 1, -thickness / 2 - transitionData.DimH - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                            {
                                                ldParamB.Y3 = -thickness / 2 - transitionData.DimH - gapDimToPart * 3;
                                                ldParamB.Update();
                                            }
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, ldParamB.X2 + transitionData.DimL, ldParamB.Y2 - transitionData.DimH, ldParamB.X2, ldParamB.Y2,
                                                paramBManual / 2 + transitionData.DimL / 2, ldParamB.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, -paramBManual / 2 - extraLength, thickness / 2, -paramBManual / 2 - extraLength, -thickness / 2,
                                                -paramBManual / 2 - extraLength - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, ldThicknessR.Y2,
                                                ldThicknessR.X3, ldTransitionL.Y1 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1 + extraLength, ldThicknessR.Y1, ldTransitionL.X1 + extraLength, ldTransitionL.Y1,
                                                ldTransitionL.X1 + extraLength + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Верх_Лево:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный вертикальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2, -thickness / 2, paramBManual / 2,
                                                -thickness / 2 - transitionData.DimH - gapDimToPart, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный вертикальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, -thickness / 2 - transitionData.DimH, paramBManual / 2 + transitionData.DimL, ldParamB.X2, ldParamB.Y2,
                                                ldParamB.X3, paramBManual / 2 + transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - extraLength, thickness / 2, -paramBManual / 2 - extraLength,
                                                0, -paramBManual / 2 - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, ldThicknessR.Y1,
                                                ldTransitionL.X1 - 1, ldThicknessR.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ldThicknessR.X2, ldTransitionL.Y1 + extraLength, ldTransitionL.X1, ldTransitionL.Y1 + extraLength,
                                                0, ldTransitionL.Y1 + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Верх_Право:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный вертикальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, thickness / 2, -paramBManual / 2, thickness / 2, paramBManual / 2,
                                                thickness / 2 + transitionData.DimH + gapDimToPart * 2, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                            {
                                                ldParamB.Y3 = -thickness / 2 - transitionData.DimH - gapDimToPart * 3;
                                                ldParamB.Update();
                                            }
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный вертикальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, thickness / 2 + transitionData.DimH, paramBManual / 2 + transitionData.DimL, ldParamB.X2, ldParamB.Y2,
                                                ldParamB.X3, paramBManual / 2 + transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, thickness / 2, -paramBManual / 2 - extraLength, -thickness / 2, -paramBManual / 2 - extraLength,
                                                0, -paramBManual / 2 - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, ldThicknessR.Y1,
                                                ldTransitionL.X1 + 1, ldThicknessR.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ldThicknessR.X2, ldTransitionL.Y1 + extraLength, ldTransitionL.X1, ldTransitionL.Y1 + extraLength,
                                                0, ldTransitionL.Y1 + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Лево:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный вертикальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2, -thickness / 2, paramBManual / 2,
                                                -thickness / 2 - transitionData.DimH - gapDimToPart, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный вертикальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, -thickness / 2 - transitionData.DimH, -paramBManual / 2 - transitionData.DimL, ldParamB.X1, ldParamB.Y1,
                                                ldParamB.X3, -paramBManual / 2 - transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + extraLength, thickness / 2, paramBManual / 2 + extraLength,
                                                0, paramBManual / 2 + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, ldThicknessR.Y1,
                                                ldTransitionL.X1 - 1, ldThicknessR.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1 - extraLength, ldThicknessR.X2, ldTransitionL.Y1 - extraLength,
                                                0, ldTransitionL.Y1 - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Право:
                                    {
                                        //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                        //При это в размер забиваем вручную ноль
                                        double paramBManual = 2;
                                        if (ParamB != 0)
                                        {
                                            paramBManual = ParamB;
                                        }
                                        DrawingPart(view, thickness, locationPart, true, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                        DrawingPart(view, thickness, locationPart, false, false,
                                                    gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                        //Что бы не плодить код, делаю поправку смещения группы тут.
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                        if (drawDimensions)
                                        {
                                            extraLength += extraLengthNotChamfer / view.Scale;
                                            //Линейный вертикальный зазора в стыке
                                            ILineDimension ldParamB = LineDimension(lineDimensions, thickness / 2, -paramBManual / 2, thickness / 2, paramBManual / 2,
                                                thickness / 2 + transitionData.DimH + gapDimToPart * 2, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                            if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                            {
                                                ldParamB.Y3 = -thickness / 2 - transitionData.DimH - gapDimToPart * 3;
                                                ldParamB.Update();
                                            }
                                            //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                            //При это в размер забиваем вручную ноль
                                            if (ParamB == 0)
                                            {
                                                IDimensionText dtparamB = (IDimensionText)ldParamB;
                                                dtparamB.NominalValue = 0;
                                                ldParamB.Update();
                                            }
                                            //Линейный вертикальный перехода
                                            ILineDimension ldTransitionL = LineDimension(lineDimensions, thickness / 2 + transitionData.DimH, -paramBManual / 2 - transitionData.DimL, ldParamB.X1, ldParamB.Y1,
                                                ldParamB.X3, -paramBManual / 2 - transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный толщины в стыке
                                            ILineDimension ldThicknessR = LineDimension(lineDimensions, thickness / 2, paramBManual / 2 + extraLength, -thickness / 2, paramBManual / 2 + extraLength,
                                                0, paramBManual / 2 + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1, ldThicknessR.X1, ldThicknessR.Y1,
                                                ldTransitionL.X1 + 1, ldThicknessR.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ldTransitionL.X1, ldTransitionL.Y1 - extraLength, ldThicknessR.X2, ldTransitionL.Y1 - extraLength,
                                                0, ldTransitionL.Y1 - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                            }
                        break;
                    }
                    break;


                case ("5264-80" or "8713-79" or "14771-76", ConnectionTypeEnum.Стыковое, ShapePreparedEdgesEnum.С_двумя_симметричными_скосами, ShapePreparedEdgesEnum.С_двумя_симметричными_скосами):
                    switch (locationPart)
                    {
                        case LocationPart.Право_Верх or LocationPart.Право_Низ or LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                        break;
                                    case LocationPart.Право_Верх or LocationPart.Право_Низ:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                        break;
                                    default:
                                        break;
                                }
                                DrawingPart(view, thickness, locationPart, false, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                        break;
                                    case LocationPart.Право_Верх or LocationPart.Право_Низ:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                        break;
                                    default:
                                        break;
                                }
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamAL = LineDimension(lineDimensions, -xangle - paramBManual / 2, -thickness / 2, -paramBManual / 2, -ParamC / 2,
                                        -xangle / 2, -thickness / 2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    LineDimension(lineDimensions, xangle + paramBManual / 2, -thickness / 2, paramBManual / 2, -ParamC / 2,
                                        xangle / 2, ldParamAL.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        paramBManual / 2 + 1, ldParamAL.Y3 - gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если верхний и нижний допуск зазора одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = ldParamAL.Y3 - gapDimToDim * 1.5;
                                        ldParamB.Update();
                                    }
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCL = LineDimension(lineDimensions, -paramBManual / 2, ParamC / 2, -paramBManual / 2, -ParamC / 2,
                                        -xangle - extraLength - gapDimToPart - paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCL, paramCTolerance);
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        xangle + extraLength + gapDimToPart * 2 + paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCR.X3 = xangle + extraLength + gapDimToPart * 3 + paramBManual / 2;
                                        ldParamCR.Update();
                                    }
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamAHL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2, ParamC / 2,
                                        ldParamCL.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHL).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHL.Update();
                                    ILineDimension ldParamAHR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2, ParamC / 2,
                                        ldParamCR.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHR).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHR.Update();
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength, -thickness / 2,
                                        ldParamCL.X3 - gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldThicknessL.X3 = ldParamCL.X3 - gapDimToDim * 1.5;
                                        ldThicknessL.Update();
                                    }
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength, -thickness / 2,
                                        ldParamCR.X3 + gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, ParamC / 2, -paramBManual / 2 - xangle, thickness / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, ParamC / 2, paramBManual / 2 + xangle, thickness / 2);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        0, (thickness - ParamC) / 2 + gapDimToPart * 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Лево or LocationPart.Верх_Право or LocationPart.Низ_Лево or LocationPart.Низ_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Верх_Лево or LocationPart.Верх_Право:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                        break;
                                    case LocationPart.Низ_Лево or LocationPart.Низ_Право:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                        break;
                                    default:
                                        break;
                                }
                                DrawingPart(view, thickness, locationPart, false, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Верх_Лево or LocationPart.Верх_Право:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                        break;
                                    case LocationPart.Низ_Лево or LocationPart.Низ_Право:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                        break;
                                    default:
                                        break;
                                }
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -thickness / 2, -xangle - paramBManual / 2, -ParamC / 2, -paramBManual / 2,
                                        -thickness / 2 - gapDimToPart, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    LineDimension(lineDimensions, -thickness / 2, xangle + paramBManual / 2, -ParamC / 2, paramBManual / 2,
                                        ldParamA.X3, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -ParamC / 2, -paramBManual / 2, -ParamC / 2, paramBManual / 2,
                                        ldParamA.X3 - gapDimToDim, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCH = LineDimension(lineDimensions, -ParamC / 2, paramBManual / 2, ParamC / 2, paramBManual / 2,
                                        ParamC / 2 + 1, xangle + extraLength + gapDimToPart + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCH, paramCTolerance);
                                    ILineDimension ldParamCD = LineDimension(lineDimensions, -ParamC / 2, -paramBManual / 2, ParamC / 2, -paramBManual / 2,
                                        ParamC / 2 + 1, -xangle - extraLength - gapDimToPart * 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCD, paramCTolerance);
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCD.Y3 = -xangle - extraLength - gapDimToPart * 3 - paramBManual / 2;
                                        ldParamCD.Update();
                                    }
                                    //Линейный горизонтальный угла
                                    ILineDimension ldParamAHH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, -ParamC / 2, paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHH).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHH.Update();
                                    ILineDimension ldParamAHD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, -ParamC / 2, -paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCD.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHD).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHD.Update();
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength,
                                        0, ldParamCH.Y3 + gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {                                        
                                        ldThicknessH.Y3 = ldParamCH.Y3 + gapDimToDim * 1.5;
                                        ldThicknessH.Update();
                                    }
                                    ILineDimension ldThicknessD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength,
                                        0, ldParamCD.Y3 - gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, ParamC / 2, -paramBManual / 2, thickness / 2, -paramBManual / 2 - xangle);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, paramBManual / 2, thickness / 2, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        (thickness - ParamC) / 2 + gapDimToPart * 2.5, 0, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case ("5264-80" or "8713-79" or "14771-76", ConnectionTypeEnum.Стыковое, ShapePreparedEdgesEnum.С_двумя_симметричными_скосами, ShapePreparedEdgesEnum.Без_скоса_стыковое):
                    switch (locationPart)
                    {
                        case LocationPart.Право_Верх or LocationPart.Право_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -thickness / 2, paramBManual / 2, -ParamC / 2,
                                        -paramBManual / 2 - 1, -thickness / 2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если верхний и нижний допуск зазора одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -thickness / 2 - gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Горизонтальный угла                                
                                    LineDimension(lineDimensions, xangle + paramBManual / 2, -thickness / 2, paramBManual / 2, -ParamC / 2,
                                        xangle / 2, ldParamB.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления                                
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        xangle + extraLength + gapDimToPart * 2 + paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCR.X3 = xangle + extraLength + gapDimToPart * 3 + paramBManual / 2;
                                        ldParamCR.Update();
                                    }
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamAHR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2, ParamC / 2,
                                        ldParamCR.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHR).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHR.Update();
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength, -thickness / 2,
                                        ldParamCR.X3 + gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, -ParamC / 2, paramBManual / 2, ParamC / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, ParamC / 2, paramBManual / 2 + xangle, thickness / 2);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, (thickness - ParamC) / 2 + gapDimToPart * 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -ParamC / 2, paramBManual / 2, -thickness / 2,
                                        paramBManual / 2 + 1, -thickness / 2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если верхний и нижний допуск зазора одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -thickness / 2 - gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Горизонтальный угла                                
                                    LineDimension(lineDimensions, -xangle - paramBManual / 2, -thickness / 2, -paramBManual / 2, -ParamC / 2,
                                        -xangle / 2, ldParamB.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления                                
                                    ILineDimension ldParamCL = LineDimension(lineDimensions, -paramBManual / 2, ParamC / 2, -paramBManual / 2, -ParamC / 2,
                                        -xangle - extraLength - gapDimToPart - paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamAHL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2, ParamC / 2,
                                        ldParamCL.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHL).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHL.Update();
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength, -thickness / 2,
                                        ldParamCL.X3 - gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldThicknessL.X3 = ldParamCL.X3 - gapDimToDim * 1.5;
                                        ldThicknessL.Update();
                                    }
                                    SetDeviation((IDimensionText)ldParamCL, paramCTolerance);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, -ParamC / 2, -paramBManual / 2, ParamC / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, ParamC / 2, -paramBManual / 2 - xangle, thickness / 2);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, (thickness - ParamC) / 2 + gapDimToPart * 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                                break;
                            }
                        case LocationPart.Верх_Лево or LocationPart.Верх_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -thickness / 2, xangle + paramBManual / 2, -ParamC / 2, paramBManual / 2,
                                        -thickness / 2 - gapDimToPart, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2, -ParamC / 2, paramBManual / 2,
                                        ldParamA.X3, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCH = LineDimension(lineDimensions, -ParamC / 2, paramBManual / 2, ParamC / 2, paramBManual / 2,
                                        ParamC / 2 + 1, xangle + extraLength + gapDimToPart + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCH, paramCTolerance);
                                    //Линейный горизонтальный угла
                                    ILineDimension ldParamAHH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, -ParamC / 2, paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHH).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHH.Update();
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength,
                                        0, ldParamCH.Y3 + gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldThicknessH.Y3 = ldParamCH.Y3 + gapDimToDim * 1.5;
                                        ldThicknessH.Update();
                                    }
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, paramBManual / 2, ParamC / 2, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, paramBManual / 2, thickness / 2, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        (thickness - ParamC) / 2 + gapDimToPart * 2, xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                                break;
                            }
                        case LocationPart.Низ_Лево or LocationPart.Низ_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -thickness / 2, -xangle - paramBManual / 2, -ParamC / 2, -paramBManual / 2,
                                        -thickness / 2 - gapDimToPart, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2, -ParamC / 2, -paramBManual / 2,
                                        ldParamA.X3, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCD = LineDimension(lineDimensions, -ParamC / 2, -paramBManual / 2, ParamC / 2, -paramBManual / 2,
                                        ParamC / 2 + 1, -xangle - extraLength - gapDimToPart * 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCD, paramCTolerance);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCD.Y3 = -xangle - extraLength - gapDimToPart * 3 - paramBManual / 2;
                                        ldParamCD.Update();
                                    }
                                    //Линейный горизонтальный угла
                                    ILineDimension ldParamAHD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, -ParamC / 2, -paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCD.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHD).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHD.Update();
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength,
                                        0, ldParamCD.Y3 - gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, -paramBManual / 2, ParamC / 2, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, -paramBManual / 2, thickness / 2, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        (thickness - ParamC) / 2 + gapDimToPart * 2, -xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case ("5264-80" or "8713-79" or "14771-76", ConnectionTypeEnum.Тавровое, ShapePreparedEdgesEnum.С_двумя_симметричными_скосами, ShapePreparedEdgesEnum.Без_скоса_тавровое):
                    switch (locationPart)
                    {
                        case LocationPart.Право_Верх or LocationPart.Право_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        paramBManual / 2 + 1, -thickness / 2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если верхний и нижний допуск зазора одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -thickness / 2 - gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Горизонтальный угла                                
                                    LineDimension(lineDimensions, xangle + paramBManual / 2, thickness / 2, paramBManual / 2, ParamC / 2,
                                        xangle / 2, thickness / 2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления                                
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        xangle + extraLength + gapDimToPart * 2 + paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCR.X3 = xangle + extraLength + gapDimToPart * 3 + paramBManual / 2;
                                        ldParamCR.Update();
                                    }
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamAHR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2, ParamC / 2,
                                        ldParamCR.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHR).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHR.Update();
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength, -thickness / 2,
                                        ldParamCR.X3 + gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, -ParamC / 2, paramBManual / 2, ParamC / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, ParamC / 2, paramBManual / 2 + xangle, thickness / 2);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, (thickness - ParamC) / 2 + gapDimToPart * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        -paramBManual / 2 - 1, -thickness / 2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если верхний и нижний допуск зазора одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -thickness / 2 - gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Горизонтальный угла                                
                                    LineDimension(lineDimensions, -xangle - paramBManual / 2, thickness / 2, -paramBManual / 2, ParamC / 2,
                                        -xangle / 2, thickness / 2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления                                
                                    ILineDimension ldParamCL = LineDimension(lineDimensions, -paramBManual / 2, ParamC / 2, -paramBManual / 2, -ParamC / 2,
                                        -xangle - extraLength - gapDimToPart - paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamAHL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2, ParamC / 2,
                                        ldParamCL.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHL).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHL.Update();
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength, -thickness / 2,
                                        ldParamCL.X3 - gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldThicknessL.X3 = ldParamCL.X3 - gapDimToDim * 1.5;
                                        ldThicknessL.Update();
                                    }
                                    SetDeviation((IDimensionText)ldParamCL, paramCTolerance);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, -ParamC / 2, -paramBManual / 2, ParamC / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, ParamC / 2, -paramBManual / 2 - xangle, thickness / 2);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, (thickness - ParamC) / 2 + gapDimToPart * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Лево or LocationPart.Верх_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness / 2, xangle + paramBManual / 2, ParamC / 2, paramBManual / 2,
                                        thickness / 2 + gapDimToPart * 2, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2, -ParamC / 2, paramBManual / 2,
                                        -thickness / 2 - gapDimToPart, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCH = LineDimension(lineDimensions, -ParamC / 2, paramBManual / 2, ParamC / 2, paramBManual / 2,
                                        ParamC / 2 + 1, xangle + extraLength + gapDimToPart + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCH, paramCTolerance);
                                    //Линейный горизонтальный угла
                                    ILineDimension ldParamAHH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, -ParamC / 2, paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHH).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHH.Update();
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength,
                                        0, ldParamCH.Y3 + gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldThicknessH.Y3 = ldParamCH.Y3 + gapDimToDim * 1.5;
                                        ldThicknessH.Update();
                                    }
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, paramBManual / 2, ParamC / 2, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, paramBManual / 2, thickness / 2, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        (thickness - ParamC) / 2 + gapDimToPart * 2, xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Низ_Лево or LocationPart.Низ_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness / 2, -xangle - paramBManual / 2, ParamC / 2, -paramBManual / 2,
                                        thickness / 2 + gapDimToPart * 2, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -ParamC / 2, paramBManual / 2, -ParamC / 2, -paramBManual / 2,
                                        -thickness / 2 - gapDimToPart, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCD = LineDimension(lineDimensions, -ParamC / 2, -paramBManual / 2, ParamC / 2, -paramBManual / 2,
                                        ParamC / 2 + 1, -xangle - extraLength - gapDimToPart * 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCD, paramCTolerance);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCD.Y3 = -xangle - extraLength - gapDimToPart * 3 - paramBManual / 2;
                                        ldParamCD.Update();
                                    }
                                    //Линейный горизонтальный угла
                                    ILineDimension ldParamAHD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, -ParamC / 2, -paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCD.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHD).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHD.Update();
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength,
                                        0, ldParamCD.Y3 - gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, -paramBManual / 2, ParamC / 2, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, -paramBManual / 2, thickness / 2, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        (thickness - ParamC) / 2 + gapDimToPart * 2.5, -xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;

                case ("5264-80" or "8713-79" or "14771-76", ConnectionTypeEnum.Угловое, ShapePreparedEdgesEnum.С_двумя_симметричными_скосами, ShapePreparedEdgesEnum.Без_скоса_угловое):
                    switch (locationPart)
                    {
                        case LocationPart.Право_Верх:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, -thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        paramBManual / 2 + 1, -thickness / 2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если верхний и нижний допуск зазора одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -thickness / 2 - gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Горизонтальный угла                                
                                    LineDimension(lineDimensions, xangle + paramBManual / 2, thickness / 2, paramBManual / 2, ParamC / 2,
                                        xangle / 2, thickness / 2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления                                
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        xangle + extraLength + gapDimToPart * 2 + paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCR.X3 = xangle + extraLength + gapDimToPart * 3 + paramBManual / 2;
                                        ldParamCR.Update();
                                    }
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamAHR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2, ParamC / 2,
                                        ldParamCR.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHR).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHR.Update();
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength, -thickness / 2,
                                        ldParamCR.X3 + gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, -ParamC / 2, paramBManual / 2, ParamC / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, ParamC / 2, paramBManual / 2 + xangle, thickness / 2);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, (thickness - ParamC) / 2 + gapDimToPart * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Право_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, -thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        paramBManual / 2 + 1, -thickness / 2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если верхний и нижний допуск зазора одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -thickness / 2 - gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Горизонтальный угла                                
                                    LineDimension(lineDimensions, xangle + paramBManual / 2, thickness / 2, paramBManual / 2, ParamC / 2,
                                        xangle / 2, thickness / 2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления                                
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        xangle + extraLength + gapDimToPart * 2 + paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCR.X3 = xangle + extraLength + gapDimToPart * 3 + paramBManual / 2;
                                        ldParamCR.Update();
                                    }
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamAHR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2, ParamC / 2,
                                        ldParamCR.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHR).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHR.Update();
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength, -thickness / 2,
                                        ldParamCR.X3 + gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, -ParamC / 2, paramBManual / 2, ParamC / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, ParamC / 2, paramBManual / 2 + xangle, thickness / 2);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, (thickness - ParamC) / 2 + gapDimToPart * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Верх:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, -thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        -paramBManual / 2 - 1, -thickness / 2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если верхний и нижний допуск зазора одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -thickness / 2 - gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Горизонтальный угла                                
                                    LineDimension(lineDimensions, -xangle - paramBManual / 2, thickness / 2, -paramBManual / 2, ParamC / 2,
                                        -xangle / 2, thickness / 2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления                                
                                    ILineDimension ldParamCL = LineDimension(lineDimensions, -paramBManual / 2, ParamC / 2, -paramBManual / 2, -ParamC / 2,
                                        -xangle - extraLength - gapDimToPart - paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamAHL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2, ParamC / 2,
                                        ldParamCL.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHL).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHL.Update();
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength, -thickness / 2,
                                        ldParamCL.X3 - gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldThicknessL.X3 = ldParamCL.X3 - gapDimToDim * 1.5;
                                        ldThicknessL.Update();
                                    }
                                    SetDeviation((IDimensionText)ldParamCL, paramCTolerance);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, -ParamC / 2, -paramBManual / 2, ParamC / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, ParamC / 2, -paramBManual / 2 - xangle, thickness / 2);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, (thickness - ParamC) / 2 + gapDimToPart * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, -thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -ParamC / 2, paramBManual / 2, -ParamC / 2,
                                        -paramBManual / 2 - 1, -thickness / 2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если верхний и нижний допуск зазора одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -thickness / 2 - gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Горизонтальный угла                                
                                    LineDimension(lineDimensions, -xangle - paramBManual / 2, thickness / 2, -paramBManual / 2, ParamC / 2,
                                        -xangle / 2, thickness / 2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления                                
                                    ILineDimension ldParamCL = LineDimension(lineDimensions, -paramBManual / 2, ParamC / 2, -paramBManual / 2, -ParamC / 2,
                                        -xangle - extraLength - gapDimToPart - paramBManual / 2, -ParamC / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamAHL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2, ParamC / 2,
                                        ldParamCL.X3, (thickness + ParamC) / 4, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ((IDimensionText)ldParamAHL).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHL.Update();
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength, -thickness / 2,
                                        ldParamCL.X3 - gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldThicknessL.X3 = ldParamCL.X3 - gapDimToDim * 1.5;
                                        ldThicknessL.Update();
                                    }
                                    SetDeviation((IDimensionText)ldParamCL, paramCTolerance);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, -ParamC / 2, -paramBManual / 2, ParamC / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, ParamC / 2, -paramBManual / 2 - xangle, thickness / 2);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, (thickness - ParamC) / 2 + gapDimToPart * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Лево:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness / 2, xangle + paramBManual / 2, ParamC / 2, paramBManual / 2,
                                        thickness / 2 + gapDimToPart * 2, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2, -ParamC / 2, paramBManual / 2,
                                        -thickness / 2 - gapDimToPart, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCH = LineDimension(lineDimensions, -ParamC / 2, paramBManual / 2, ParamC / 2, paramBManual / 2,
                                        ParamC / 2 + 1, xangle + extraLength + gapDimToPart + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCH, paramCTolerance);
                                    //Линейный горизонтальный угла
                                    ILineDimension ldParamAHH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, -ParamC / 2, paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHH).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHH.Update();
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength,
                                        0, ldParamCH.Y3 + gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldThicknessH.Y3 = ldParamCH.Y3 + gapDimToDim * 1.5;
                                        ldThicknessH.Update();
                                    }
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, paramBManual / 2, ParamC / 2, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, paramBManual / 2, thickness / 2, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        (thickness - ParamC) / 2 + gapDimToPart * 2, xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness / 2, xangle + paramBManual / 2, ParamC / 2, paramBManual / 2,
                                        thickness / 2 + gapDimToPart * 2, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2, -ParamC / 2, paramBManual / 2,
                                        -thickness / 2 - gapDimToPart, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCH = LineDimension(lineDimensions, -ParamC / 2, paramBManual / 2, ParamC / 2, paramBManual / 2,
                                        ParamC / 2 + 1, xangle + extraLength + gapDimToPart + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCH, paramCTolerance);
                                    //Линейный горизонтальный угла
                                    ILineDimension ldParamAHH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, -ParamC / 2, paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHH).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHH.Update();
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessH = LineDimension(lineDimensions, -thickness / 2, paramBManual / 2 + xangle + extraLength, thickness / 2, paramBManual / 2 + xangle + extraLength,
                                        0, ldParamCH.Y3 + gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldThicknessH.Y3 = ldParamCH.Y3 + gapDimToDim * 1.5;
                                        ldThicknessH.Update();
                                    }
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, paramBManual / 2, ParamC / 2, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, paramBManual / 2, thickness / 2, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        (thickness - ParamC) / 2 + gapDimToPart * 2, xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Низ_Лево:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness / 2, -xangle - paramBManual / 2, ParamC / 2, -paramBManual / 2,
                                        thickness / 2 + gapDimToPart * 2, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -ParamC / 2, paramBManual / 2, -ParamC / 2, -paramBManual / 2,
                                        -thickness / 2 - gapDimToPart, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCD = LineDimension(lineDimensions, -ParamC / 2, -paramBManual / 2, ParamC / 2, -paramBManual / 2,
                                        ParamC / 2 + 1, -xangle - extraLength - gapDimToPart * 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCD, paramCTolerance);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCD.Y3 = -xangle - extraLength - gapDimToPart * 3 - paramBManual / 2;
                                        ldParamCD.Update();
                                    }
                                    //Линейный горизонтальный угла
                                    ILineDimension ldParamAHD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, -ParamC / 2, -paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCD.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHD).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHD.Update();
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength,
                                        0, ldParamCD.Y3 - gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, -paramBManual / 2, ParamC / 2, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, -paramBManual / 2, thickness / 2, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        (thickness - ParamC) / 2 + gapDimToPart * 2.5, -xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Низ_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Линейный вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness / 2, -xangle - paramBManual / 2, ParamC / 2, -paramBManual / 2,
                                        thickness / 2 + gapDimToPart * 2, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -ParamC / 2, paramBManual / 2, -ParamC / 2, -paramBManual / 2,
                                        -thickness / 2 - gapDimToPart, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCD = LineDimension(lineDimensions, -ParamC / 2, -paramBManual / 2, ParamC / 2, -paramBManual / 2,
                                        ParamC / 2 + 1, -xangle - extraLength - gapDimToPart * 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCD, paramCTolerance);
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamCD.Y3 = -xangle - extraLength - gapDimToPart * 3 - paramBManual / 2;
                                        ldParamCD.Update();
                                    }
                                    //Линейный горизонтальный угла
                                    ILineDimension ldParamAHD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, -ParamC / 2, -paramBManual / 2,
                                        -(thickness + ParamC) / 4, ldParamCD.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ((IDimensionText)ldParamAHD).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                    ldParamAHD.Update();
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessD = LineDimension(lineDimensions, -thickness / 2, -paramBManual / 2 - xangle - extraLength, thickness / 2, -paramBManual / 2 - xangle - extraLength,
                                        0, ldParamCD.Y3 - gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, -paramBManual / 2, ParamC / 2, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, -paramBManual / 2, thickness / 2, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        (thickness - ParamC) / 2 + gapDimToPart * 2.5, -xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;


                case ("5264-80" or "8713-79" or "14771-76", ConnectionTypeEnum.Стыковое, ShapePreparedEdgesEnum.Со_скосом_одной_кромки, ShapePreparedEdgesEnum.Со_скосом_одной_кромки):
                    switch (locationPart)
                    {
                        case LocationPart.Право_Верх or LocationPart.Лево_Верх:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Право_Верх:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                        break;
                                    case LocationPart.Лево_Верх:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                        break;
                                    default:
                                        break;
                                }
                                DrawingPart(view, thickness, locationPart, false, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Право_Верх:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                        break;
                                    case LocationPart.Лево_Верх:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                        break;
                                    default:
                                        break;
                                }
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamAL = LineDimension(lineDimensions, -xangle - paramBManual / 2, thickness, -paramBManual / 2, ParamC,
                                        -xangle / 2 - paramBManual / 2, thickness + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    LineDimension(lineDimensions, xangle + paramBManual / 2, thickness, paramBManual / 2, ParamC,
                                        xangle / 2 + paramBManual / 2, ldParamAL.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, ParamC, paramBManual / 2, ParamC,
                                        paramBManual / 2 + 1, ldParamAL.Y3 + gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, ParamC, paramBManual / 2, 0,
                                        -gapDimToPart - paramBManual / 2, -ParamC - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness, -paramBManual / 2 - xangle - extraLength, 0,
                                        -paramBManual / 2 - xangle - extraLength - gapDimToPart, thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness, paramBManual / 2 + xangle + extraLength, 0,
                                        paramBManual / 2 + xangle + extraLength + gapDimToPart * 2, thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamB.Y3 + gapDimToDim) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.Y3 + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        r1 = (ldParamB.Y3 + gapDimToDim * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                        r2 = Math.Sqrt(Math.Pow(ldParamB.Y3 + gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    }
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, ParamC, -paramBManual / 2 - xangle, thickness);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, ParamC, paramBManual / 2 + xangle, thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        0, ldParamB.Y3 + gapDimToDim, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Право_Низ or LocationPart.Лево_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Право_Низ:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                        break;
                                    case LocationPart.Лево_Низ:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                        break;
                                    default:
                                        break;
                                }
                                DrawingPart(view, thickness, locationPart, false, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                switch (locationPart)
                                {
                                    case LocationPart.Право_Низ:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                        break;
                                    case LocationPart.Лево_Низ:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                        break;
                                    default:
                                        break;
                                }
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamAL = LineDimension(lineDimensions, -xangle - paramBManual / 2, -thickness, -paramBManual / 2, -ParamC,
                                        -xangle / 2 - paramBManual / 2, -thickness - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    LineDimension(lineDimensions, xangle + paramBManual / 2, -thickness, paramBManual / 2, -ParamC,
                                        xangle / 2 + paramBManual / 2, ldParamAL.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -ParamC, paramBManual / 2, -ParamC,
                                        paramBManual / 2 + 1, ldParamAL.Y3 - gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = ldParamAL.Y3 - gapDimToDim * 1.5;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, -ParamC, paramBManual / 2, 0,
                                        -gapDimToPart - paramBManual / 2, ParamC + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessL = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, -thickness, -paramBManual / 2 - xangle - extraLength, 0,
                                        -paramBManual / 2 - xangle - extraLength - gapDimToPart, -thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, -thickness, paramBManual / 2 + xangle + extraLength, 0,
                                        paramBManual / 2 + xangle + extraLength + gapDimToPart * 2, -thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    double r1 = (ldParamB.Y3 - gapDimToDim) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.Y3 - gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, -ParamC, -paramBManual / 2 - xangle, -thickness);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, -ParamC, paramBManual / 2 + xangle, -thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        0, ldParamB.Y3 - gapDimToDim, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Право or LocationPart.Низ_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Верх_Право:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                        break;
                                    case LocationPart.Низ_Право:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                        break;
                                    default:
                                        break;
                                }
                                DrawingPart(view, thickness, locationPart, false, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Верх_Право:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                        break;
                                    case LocationPart.Низ_Право:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                        break;
                                    default:
                                        break;
                                }
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamAH = LineDimension(lineDimensions, thickness, xangle + paramBManual / 2, ParamC, paramBManual / 2,
                                        thickness + gapDimToPart * 2, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    LineDimension(lineDimensions, thickness, -xangle - paramBManual / 2, ParamC, -paramBManual / 2,
                                        ldParamAH.X3, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, ParamC, -paramBManual / 2, ParamC, paramBManual / 2,
                                        ldParamAH.X3 + gapDimToDim, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.X3 = ldParamAH.X3 + gapDimToDim * 1.5;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, 0, -paramBManual / 2, ParamC, -paramBManual / 2,
                                        -ParamC - 1, gapDimToPart + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessD = LineDimension(lineDimensions, thickness, -paramBManual / 2 - xangle - extraLength, 0, -paramBManual / 2 - xangle - extraLength,
                                        thickness / 2, -paramBManual / 2 - xangle - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ILineDimension ldThicknessH = LineDimension(lineDimensions, thickness, paramBManual / 2 + xangle + extraLength, 0, paramBManual / 2 + xangle + extraLength,
                                        thickness / 2, paramBManual / 2 + xangle + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    double r1 = (ldParamB.X3 + gapDimToDim) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.X3 + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, ParamC, -paramBManual / 2, thickness, -paramBManual / 2 - xangle);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC, paramBManual / 2, thickness, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamB.X3 + gapDimToDim, 0, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                                break;
                            }
                        case LocationPart.Верх_Лево or LocationPart.Низ_Лево:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Верх_Лево:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                        break;
                                    case LocationPart.Низ_Лево:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                        break;
                                    default:
                                        break;
                                }
                                DrawingPart(view, thickness, locationPart, false, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                //Что бы не плодить код, делаю поправку смещения группы тут.
                                switch (locationPart)
                                {
                                    case LocationPart.Верх_Лево:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                        break;
                                    case LocationPart.Низ_Лево:
                                        document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                        break;
                                    default:
                                        break;
                                }
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamAH = LineDimension(lineDimensions, -thickness, xangle + paramBManual / 2, -ParamC, paramBManual / 2,
                                        -thickness - gapDimToPart, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    LineDimension(lineDimensions, -thickness, -xangle - paramBManual / 2, -ParamC, -paramBManual / 2,
                                        ldParamAH.X3, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -ParamC, -paramBManual / 2, -ParamC, paramBManual / 2,
                                        ldParamAH.X3 - gapDimToDim, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, 0, -paramBManual / 2, -ParamC, -paramBManual / 2,
                                        ParamC + 1, gapDimToPart + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessD = LineDimension(lineDimensions, -thickness, -paramBManual / 2 - xangle - extraLength, 0, -paramBManual / 2 - xangle - extraLength,
                                        -thickness / 2, -paramBManual / 2 - xangle - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    ILineDimension ldThicknessH = LineDimension(lineDimensions, -thickness, paramBManual / 2 + xangle + extraLength, 0, paramBManual / 2 + xangle + extraLength,
                                        -thickness / 2, paramBManual / 2 + xangle + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    double r1 = (ldParamB.X3 - gapDimToDim) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.X3 - gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        r1 = (ldParamB.X3 - gapDimToDim * 1.7) / Math.Cos(ParamA * Math.PI / 180);
                                        r2 = Math.Sqrt(Math.Pow(ldParamB.X3 - gapDimToDim * 1.7, 2) + Math.Pow(xangle / 2, 2));
                                    }
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC, -paramBManual / 2, -thickness, -paramBManual / 2 - xangle);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -ParamC, paramBManual / 2, -thickness, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamB.X3 - gapDimToDim, 0, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;
                case ("5264-80" or "8713-79" or "14771-76", ConnectionTypeEnum.Стыковое, ShapePreparedEdgesEnum.Со_скосом_одной_кромки, ShapePreparedEdgesEnum.Без_скоса_стыковое):
                    switch (locationPart)
                    {
                        case LocationPart.Право_Верх:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, -thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, xangle + paramBManual / 2, thickness, paramBManual / 2, ParamC,
                                        xangle / 2 + paramBManual / 2, thickness + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, thickness, paramBManual / 2, ParamC,
                                        -paramBManual / 2 - 1, ldParamA.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, ParamC, paramBManual / 2, 0,
                                        gapDimToPart * 3 + paramBManual / 2, -ParamC - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness, paramBManual / 2 + xangle + extraLength, 0,
                                        paramBManual / 2 + xangle + extraLength + gapDimToPart * 2, thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamB.Y3 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.Y3 + gapDimToDim / 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, 0, paramBManual / 2, ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, ParamC, paramBManual / 2 + xangle, thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, ldParamB.Y3, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Право_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, -thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, xangle + paramBManual / 2, -thickness, paramBManual / 2, -ParamC,
                                        xangle / 2 + paramBManual / 2, -thickness - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, -thickness, paramBManual / 2, -ParamC,
                                        -paramBManual / 2 - 1, ldParamA.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamA.Y3 = -thickness - gapDimToPart * 3;
                                        ldParamA.Update();
                                        ldParamB.Y3 = ldParamA.Y3;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, -ParamC, paramBManual / 2, 0,
                                        gapDimToPart * 3 + paramBManual / 2, ParamC + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, -thickness, paramBManual / 2 + xangle + extraLength, 0,
                                        paramBManual / 2 + xangle + extraLength + gapDimToPart * 2, -thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamB.Y3 - gapDimToDim * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.Y3 - gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, 0, paramBManual / 2, -ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, -ParamC, paramBManual / 2 + xangle, -thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, ldParamB.Y3 - gapDimToDim * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Верх:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, -thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -xangle - paramBManual / 2, thickness, -paramBManual / 2, ParamC,
                                        -xangle / 2 - paramBManual / 2, thickness + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, paramBManual / 2, thickness, -paramBManual / 2, ParamC,
                                        paramBManual / 2 + 1, ldParamA.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -paramBManual / 2, ParamC, -paramBManual / 2, 0,
                                        -gapDimToPart * 2 - paramBManual / 2, -ParamC - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness, -paramBManual / 2 - xangle - extraLength, 0,
                                        -paramBManual / 2 - xangle - extraLength - gapDimToPart, thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamB.Y3 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.Y3 + gapDimToDim / 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, 0, -paramBManual / 2, ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, ParamC, -paramBManual / 2 - xangle, thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, ldParamB.Y3, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, -thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -xangle - paramBManual / 2, -thickness, -paramBManual / 2, -ParamC,
                                        -xangle / 2 - paramBManual / 2, -thickness - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamA.Y3 = -thickness - gapDimToPart * 3;
                                        ldParamA.Update();
                                    }
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, paramBManual / 2, -thickness, -paramBManual / 2, -ParamC,
                                        paramBManual / 2 + 1, ldParamA.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -paramBManual / 2, -ParamC, -paramBManual / 2, 0,
                                        -gapDimToPart * 2 - paramBManual / 2, ParamC + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, -thickness, -paramBManual / 2 - xangle - extraLength, 0,
                                        -paramBManual / 2 - xangle - extraLength - gapDimToPart, -thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamB.Y3 - gapDimToDim * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.Y3 - gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, 0, -paramBManual / 2, -ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, -ParamC, -paramBManual / 2 - xangle, -thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, ldParamB.Y3 - gapDimToDim * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness, xangle + paramBManual / 2, ParamC, paramBManual / 2,
                                        thickness + gapDimToPart * 2, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamA.X3 = thickness + gapDimToPart * 3;
                                        ldParamA.Update();
                                    }
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, thickness, -paramBManual / 2, ParamC, paramBManual / 2,
                                        ldParamA.X3, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, ParamC, paramBManual / 2, 0, paramBManual / 2,
                                        -ParamC - 1, gapDimToPart * 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, thickness, paramBManual / 2 + xangle + extraLength, 0, paramBManual / 2 + xangle + extraLength,
                                        thickness / 2, paramBManual / 2 + xangle + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamB.X3) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.X3, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, paramBManual / 2, ParamC, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC, paramBManual / 2, thickness, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamB.X3, paramBManual / 2 + xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Лево:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -thickness, xangle + paramBManual / 2, -ParamC, paramBManual / 2,
                                        -thickness - gapDimToPart, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -thickness, -paramBManual / 2, -ParamC, paramBManual / 2,
                                        ldParamA.X3, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -ParamC, paramBManual / 2, 0, paramBManual / 2,
                                        ParamC + 1, gapDimToPart * 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness, paramBManual / 2 + xangle + extraLength, 0, paramBManual / 2 + xangle + extraLength,
                                        -thickness / 2, paramBManual / 2 + xangle + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamB.X3 - gapDimToDim * 1.2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.X3 - gapDimToDim * 1.2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, paramBManual / 2, -ParamC, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -ParamC, paramBManual / 2, -thickness, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamB.X3 - gapDimToDim * 1.2, paramBManual / 2 + xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Низ_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness, -xangle - paramBManual / 2, ParamC, -paramBManual / 2,
                                        thickness + gapDimToPart * 2, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamA.X3 = thickness + gapDimToPart * 3;
                                        ldParamA.Update();
                                    }
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, thickness, paramBManual / 2, ParamC, -paramBManual / 2,
                                        ldParamA.X3, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, ParamC, -paramBManual / 2, 0, -paramBManual / 2,
                                        -ParamC - 1, -gapDimToPart * 3 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, thickness, -paramBManual / 2 - xangle - extraLength, 0, -paramBManual / 2 - xangle - extraLength,
                                        thickness / 2, -paramBManual / 2 - xangle - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamB.X3 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.X3 + gapDimToDim / 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -paramBManual / 2, ParamC, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC, -paramBManual / 2, thickness, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamB.X3 + gapDimToDim / 2, -paramBManual / 2 - xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Низ_Лево:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -thickness, -xangle - paramBManual / 2, -ParamC, -paramBManual / 2,
                                        -thickness - gapDimToPart, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -thickness, paramBManual / 2, -ParamC, -paramBManual / 2,
                                        ldParamA.X3, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -ParamC, -paramBManual / 2, 0, -paramBManual / 2,
                                        ParamC + 1, -gapDimToPart * 3 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness, -paramBManual / 2 - xangle - extraLength, 0, -paramBManual / 2 - xangle - extraLength,
                                        -thickness / 2, -paramBManual / 2 - xangle - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamB.X3 - gapDimToDim * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamB.X3 - gapDimToDim * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -paramBManual / 2, -ParamC, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -ParamC, -paramBManual / 2, -thickness, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamB.X3 - gapDimToDim * 2, -paramBManual / 2 - xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case ("5264-80" or "8713-79" or "14771-76", ConnectionTypeEnum.Тавровое, ShapePreparedEdgesEnum.Со_скосом_одной_кромки, ShapePreparedEdgesEnum.Без_скоса_тавровое):
                    switch (locationPart)
                    {
                        case LocationPart.Право_Верх:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, -thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, xangle + paramBManual / 2, thickness, paramBManual / 2, ParamC,
                                        xangle / 2 + paramBManual / 2, thickness + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, 0, paramBManual / 2, 0,
                                        paramBManual / 2 + 1, -gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, ParamC, paramBManual / 2, 0,
                                        gapDimToPart * 5 + paramBManual / 2, -ParamC - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness, paramBManual / 2 + xangle + extraLength, 0,
                                        paramBManual / 2 + xangle + extraLength + gapDimToPart * 2, thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.Y3 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.Y3 + gapDimToDim / 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, 0, paramBManual / 2, ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, ParamC, paramBManual / 2 + xangle, thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, ldParamA.Y3 + gapDimToDim / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Право_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, -thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, xangle + paramBManual / 2, -thickness, paramBManual / 2, -ParamC,
                                        xangle / 2 + paramBManual / 2, -thickness - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, 0, paramBManual / 2, 0,
                                        paramBManual / 2 + 1, gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, -ParamC, paramBManual / 2, 0,
                                        gapDimToPart * 5 + paramBManual / 2, ParamC + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, -thickness, paramBManual / 2 + xangle + extraLength, 0,
                                        paramBManual / 2 + xangle + extraLength + gapDimToPart * 2, -thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.Y3 - gapDimToDim * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.Y3 - gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, 0, paramBManual / 2, -ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, -ParamC, paramBManual / 2 + xangle, -thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, ldParamA.Y3 - gapDimToDim * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Верх:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, -thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -xangle - paramBManual / 2, thickness, -paramBManual / 2, ParamC,
                                        -xangle / 2 - paramBManual / 2, thickness + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, paramBManual / 2, 0, -paramBManual / 2, 0,
                                        -paramBManual / 2 - 1, -gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -paramBManual / 2, ParamC, -paramBManual / 2, 0,
                                        -gapDimToPart * 3 - paramBManual / 2, -ParamC - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness, -paramBManual / 2 - xangle - extraLength, 0,
                                        -paramBManual / 2 - xangle - extraLength - gapDimToPart, thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.Y3 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.Y3 + gapDimToDim / 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, 0, -paramBManual / 2, ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, ParamC, -paramBManual / 2 - xangle, thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, ldParamA.Y3, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, thickness / 2);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, -thickness / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -xangle - paramBManual / 2, -thickness, -paramBManual / 2, -ParamC,
                                        -xangle / 2 - paramBManual / 2, -thickness - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamA.Y3 = -thickness - gapDimToPart * 3;
                                        ldParamA.Update();
                                    }
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, paramBManual / 2, 0, -paramBManual / 2, 0,
                                        -paramBManual / 2 - 1, gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -paramBManual / 2, -ParamC, -paramBManual / 2, 0,
                                        -gapDimToPart * 3 - paramBManual / 2, ParamC + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, -thickness, -paramBManual / 2 - xangle - extraLength, 0,
                                        -paramBManual / 2 - xangle - extraLength - gapDimToPart, -thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.Y3 - gapDimToDim * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.Y3 - gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, 0, -paramBManual / 2, -ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, -ParamC, -paramBManual / 2 - xangle, -thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, ldParamA.Y3 - gapDimToDim * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness, xangle + paramBManual / 2, ParamC, paramBManual / 2,
                                        thickness + gapDimToPart * 2, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, 0, -paramBManual / 2, 0, paramBManual / 2,
                                        -gapDimToPart, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, ParamC, paramBManual / 2, 0, paramBManual / 2,
                                        -ParamC - 1, gapDimToPart * 3 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, thickness, paramBManual / 2 + xangle + extraLength, 0, paramBManual / 2 + xangle + extraLength,
                                        thickness / 2, paramBManual / 2 + xangle + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.X3) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.X3, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, paramBManual / 2, ParamC, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC, paramBManual / 2, thickness, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamA.X3, paramBManual / 2 + xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Лево:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -thickness, xangle + paramBManual / 2, -ParamC, paramBManual / 2,
                                        -thickness - gapDimToPart, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, 0, -paramBManual / 2, 0, paramBManual / 2,
                                        gapDimToPart * 2, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.X3 = gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -ParamC, paramBManual / 2, 0, paramBManual / 2,
                                        ParamC + 1, gapDimToPart * 3 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness, paramBManual / 2 + xangle + extraLength, 0, paramBManual / 2 + xangle + extraLength,
                                        -thickness / 2, paramBManual / 2 + xangle + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.X3 - gapDimToDim * 1.2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.X3 - gapDimToDim * 1.2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, paramBManual / 2, -ParamC, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -ParamC, paramBManual / 2, -thickness, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamA.X3 - gapDimToDim * 1.2, paramBManual / 2 + xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Низ_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness, -xangle - paramBManual / 2, ParamC, -paramBManual / 2,
                                        thickness + gapDimToPart * 2, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, 0, paramBManual / 2, 0, -paramBManual / 2,
                                        -gapDimToPart, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, ParamC, -paramBManual / 2, 0, -paramBManual / 2,
                                        -ParamC - 1, -gapDimToPart * 5 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, thickness, -paramBManual / 2 - xangle - extraLength, 0, -paramBManual / 2 - xangle - extraLength,
                                        thickness / 2, -paramBManual / 2 - xangle - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.X3 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.X3 + gapDimToDim / 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -paramBManual / 2, ParamC, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC, -paramBManual / 2, thickness, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamA.X3 + gapDimToDim / 2, -paramBManual / 2 - xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Низ_Лево:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, thickness / 2, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -thickness / 2, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -thickness, -xangle - paramBManual / 2, -ParamC, -paramBManual / 2,
                                        -thickness - gapDimToPart, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, 0, paramBManual / 2, 0, -paramBManual / 2,
                                        gapDimToPart * 2, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.X3 = gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -ParamC, -paramBManual / 2, 0, -paramBManual / 2,
                                        ParamC + 1, -gapDimToPart * 5 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness, -paramBManual / 2 - xangle - extraLength, 0, -paramBManual / 2 - xangle - extraLength,
                                        -thickness / 2, -paramBManual / 2 - xangle - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.X3 - gapDimToDim * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.X3 - gapDimToDim * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -paramBManual / 2, -ParamC, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -ParamC, -paramBManual / 2, -thickness, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamA.X3 - gapDimToDim * 2, -paramBManual / 2 - xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;
                case ("5264-80" or "8713-79" or "14771-76", ConnectionTypeEnum.Угловое, ShapePreparedEdgesEnum.Со_скосом_одной_кромки, ShapePreparedEdgesEnum.Без_скоса_угловое):
                    switch (locationPart)
                    {
                        case LocationPart.Право_Верх:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, xangle + paramBManual / 2, thickness, paramBManual / 2, ParamC,
                                        xangle / 2 + paramBManual / 2, thickness + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, 0, paramBManual / 2, 0,
                                        paramBManual / 2 + 1, -gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, ParamC, paramBManual / 2, 0,
                                        gapDimToPart * 5 + paramBManual / 2, -ParamC - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, thickness, paramBManual / 2 + xangle + extraLength, 0,
                                        paramBManual / 2 + xangle + extraLength + gapDimToPart * 2, thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.Y3 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.Y3 + gapDimToDim / 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, 0, paramBManual / 2, ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, ParamC, paramBManual / 2 + xangle, thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, ldParamA.Y3 + gapDimToDim / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Право_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual, 0);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual / 2, 0);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, xangle + paramBManual / 2, -thickness, paramBManual / 2, -ParamC,
                                        xangle / 2 + paramBManual / 2, -thickness - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, -paramBManual / 2, 0, paramBManual / 2, 0,
                                        paramBManual / 2 + 1, gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, paramBManual / 2, -ParamC, paramBManual / 2, 0,
                                        gapDimToPart * 5 + paramBManual / 2, ParamC + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, paramBManual / 2 + xangle + extraLength, -thickness, paramBManual / 2 + xangle + extraLength, 0,
                                        paramBManual / 2 + xangle + extraLength + gapDimToPart * 2, -thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.Y3 - gapDimToDim * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.Y3 - gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, paramBManual / 2, 0, paramBManual / 2, -ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, paramBManual / 2, -ParamC, paramBManual / 2 + xangle, -thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        paramBManual / 2 + xangle / 2, ldParamA.Y3 - gapDimToDim * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Верх:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -xangle - paramBManual / 2, thickness, -paramBManual / 2, ParamC,
                                        -xangle / 2 - paramBManual / 2, thickness + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, paramBManual / 2, 0, -paramBManual / 2, 0,
                                        -paramBManual / 2 - 1, -gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.Y3 = -gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -paramBManual / 2, ParamC, -paramBManual / 2, 0,
                                        -gapDimToPart * 3 - paramBManual / 2, -ParamC - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, thickness, -paramBManual / 2 - xangle - extraLength, 0,
                                        -paramBManual / 2 - xangle - extraLength - gapDimToPart, thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.Y3 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.Y3 + gapDimToDim / 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, 0, -paramBManual / 2, ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, ParamC, -paramBManual / 2 - xangle, thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, ldParamA.Y3, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Лево_Низ:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, -paramBManual, 0);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, paramBManual / 2, 0);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Горизонтальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -xangle - paramBManual / 2, -thickness, -paramBManual / 2, -ParamC,
                                        -xangle / 2 - paramBManual / 2, -thickness - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamA.Y3 = -thickness - gapDimToPart * 3;
                                        ldParamA.Update();
                                    }
                                    //Линейный горизонтальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, paramBManual / 2, 0, -paramBManual / 2, 0,
                                        -paramBManual / 2 - 1, gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -paramBManual / 2, -ParamC, -paramBManual / 2, 0,
                                        -gapDimToPart * 3 - paramBManual / 2, ParamC + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный вертикальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -paramBManual / 2 - xangle - extraLength, -thickness, -paramBManual / 2 - xangle - extraLength, 0,
                                        -paramBManual / 2 - xangle - extraLength - gapDimToPart, -thickness / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.Y3 - gapDimToDim * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.Y3 - gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -paramBManual / 2, 0, -paramBManual / 2, -ParamC);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -paramBManual / 2, -ParamC, -paramBManual / 2 - xangle, -thickness);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -paramBManual / 2 - xangle / 2, ldParamA.Y3 - gapDimToDim * 1.5, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness, xangle + paramBManual / 2, ParamC, paramBManual / 2,
                                        thickness + gapDimToPart * 2, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, 0, -paramBManual / 2, 0, paramBManual / 2,
                                        -gapDimToPart, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, ParamC, paramBManual / 2, 0, paramBManual / 2,
                                        -ParamC - 1, gapDimToPart * 3 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, thickness, paramBManual / 2 + xangle + extraLength, 0, paramBManual / 2 + xangle + extraLength,
                                        thickness / 2, paramBManual / 2 + xangle + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.X3) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.X3, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, paramBManual / 2, ParamC, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC, paramBManual / 2, thickness, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamA.X3, paramBManual / 2 + xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Верх_Лево:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -thickness, xangle + paramBManual / 2, -ParamC, paramBManual / 2,
                                        -thickness - gapDimToPart, xangle / 2 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, 0, -paramBManual / 2, 0, paramBManual / 2,
                                        gapDimToPart * 2, paramBManual / 2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.X3 = gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -ParamC, paramBManual / 2, 0, paramBManual / 2,
                                        ParamC + 1, gapDimToPart * 3 + paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness, paramBManual / 2 + xangle + extraLength, 0, paramBManual / 2 + xangle + extraLength,
                                        -thickness / 2, paramBManual / 2 + xangle + extraLength + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.X3 - gapDimToDim * 1.2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.X3 - gapDimToDim * 1.2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, paramBManual / 2, -ParamC, paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -ParamC, paramBManual / 2, -thickness, paramBManual / 2 + xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamA.X3 - gapDimToDim * 1.2, paramBManual / 2 + xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Низ_Право:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, thickness, -xangle - paramBManual / 2, ParamC, -paramBManual / 2,
                                        thickness + gapDimToPart * 2, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, 0, paramBManual / 2, 0, -paramBManual / 2,
                                        -gapDimToPart, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, ParamC, -paramBManual / 2, 0, -paramBManual / 2,
                                        -ParamC - 1, -gapDimToPart * 5 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, thickness, -paramBManual / 2 - xangle - extraLength, 0, -paramBManual / 2 - xangle - extraLength,
                                        thickness / 2, -paramBManual / 2 - xangle - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.X3 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.X3 + gapDimToDim / 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -paramBManual / 2, ParamC, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC, -paramBManual / 2, thickness, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamA.X3 + gapDimToDim / 2, -paramBManual / 2 - xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;
                        case LocationPart.Низ_Лево:
                            {
                                //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                //При это в размер забиваем вручную ноль
                                double paramBManual = 2;
                                if (ParamB != 0)
                                {
                                    paramBManual = ParamB;
                                }
                                DrawingPart(view, thickness, locationPart, true, false,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, -paramBManual);
                                DrawingPart(view, thickness, locationPart, false, true,
                                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, isCrossSection, isHatches, transitionData);
                                document2DAPI5.ksMoveObj(drawingGroup.Reference, 0, paramBManual / 2);
                                if (drawDimensions)
                                {
                                    //Размер скоса
                                    double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                    extraLength += xangle;
                                    extraLength = extraLength < 1 ? 1 : extraLength;
                                    //Вертикальный угла
                                    ILineDimension ldParamA = LineDimension(lineDimensions, -thickness, -xangle - paramBManual / 2, -ParamC, -paramBManual / 2,
                                        -thickness - gapDimToPart, -xangle / 2 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный вертикальный зазора в стыке
                                    ILineDimension ldParamB = LineDimension(lineDimensions, 0, paramBManual / 2, 0, -paramBManual / 2,
                                        gapDimToPart * 2, -paramBManual / 2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation((IDimensionText)ldParamB, paramBTolerance);
                                    //Если зазор в стыке равен нулю приходится для наглядности сделать его равным двум милиметрам
                                    //При это в размер забиваем вручную ноль
                                    if (ParamB == 0)
                                    {
                                        IDimensionText dtparamB = (IDimensionText)ldParamB;
                                        dtparamB.NominalValue = 0;
                                        ldParamB.Update();
                                    }
                                    if (Math.Abs(ParamBTolerance[0]) != Math.Abs(ParamBTolerance[1]))
                                    {
                                        ldParamB.X3 = gapDimToPart * 3;
                                        ldParamB.Update();
                                    }
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamCR = LineDimension(lineDimensions, -ParamC, -paramBManual / 2, 0, -paramBManual / 2,
                                        ParamC + 1, -gapDimToPart * 5 - paramBManual / 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation((IDimensionText)ldParamCR, paramCTolerance);
                                    //Линейный горизонтальный толщины
                                    ILineDimension ldThicknessR = LineDimension(lineDimensions, -thickness, -paramBManual / 2 - xangle - extraLength, 0, -paramBManual / 2 - xangle - extraLength,
                                        -thickness / 2, -paramBManual / 2 - xangle - extraLength - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Угол
                                    //Если верхний и нижний допуск притупления одинаков то расстояние до детали меньше чем при разных допусках
                                    double r1 = (ldParamA.X3 - gapDimToDim * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(ldParamA.X3 - gapDimToDim * 2, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                                               //Линии нужны для построения размера угла
                                    ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -paramBManual / 2, -ParamC, -paramBManual / 2);
                                    ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -ParamC, -paramBManual / 2, -thickness, -paramBManual / 2 - xangle);
                                    //Эти линии удалять нельзя. Компас вылетает с ошибкой.
                                    //Т.к. эти линии дублируют уже существующие то желательно удалить существующие.
                                    if (drawingGroup.Objects[0] is object[] obj)
                                    {
                                        foreach (var item in obj)
                                        {
                                            if (item is ILineSegment lineSegment)
                                            {
                                                if ((baseobjAngle1.X1 == lineSegment.X1 && baseobjAngle1.Y1 == lineSegment.Y1 && baseobjAngle1.X2 == lineSegment.X2 && baseobjAngle1.Y2 == lineSegment.Y2 && baseobjAngle1 != lineSegment)
                                                    || (baseobjAngle2.X1 == lineSegment.X1 && baseobjAngle2.Y1 == lineSegment.Y1 && baseobjAngle2.X2 == lineSegment.X2 && baseobjAngle2.Y2 == lineSegment.Y2 && baseobjAngle2 != lineSegment))
                                                {
                                                    lineSegment.Delete();
                                                }
                                            }
                                        }
                                    }
                                    IAngleDimension adParamA = AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        ldParamA.X3 - gapDimToDim * 2, -paramBManual / 2 - xangle / 2, angleDRadius);
                                    SetDeviation((IDimensionText)adParamA, ParamATolerance);
                                }
                            }
                            break;

                        default:
                            break;
                    }
                    break;




                default:
                    break;
            }
        }

        public void DrawingSeam()
        {

        }

        public void DrawingPart(IView view, double thickness, LocationPart locationPart, bool numberPar, bool drawDimensions, double gapDimToPart, double gapDimToDim,
            double gapDimToPartLeft, double extraLength, bool isCrossSection, bool isHatches, TransitionData transitionData)
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
            TransitionTypeEnum transitionType;
            if (numberPar)
            {
                shapePreparedEdges = ShapePreparedEdgesPart1;
                transitionType = transitionData.TransitionTypePart1;
            }
            else
            {
                shapePreparedEdges = ShapePreparedEdgesPart2;
                transitionType = transitionData.TransitionTypePart2;
            }
            switch (shapePreparedEdges)
            {
                case ShapePreparedEdgesEnum.НЕ_УКАЗАНО:
                    break;
                case ShapePreparedEdgesEnum.Без_скоса_стыковое:
                    extraLength += extraLengthNotChamfer / view.Scale;
                    switch (transitionType)
                    {
                        case TransitionTypeEnum.Без_перехода:
                            switch (locationPart)
                            {
                                case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2, 0, thickness / 2);
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, 0, -thickness / 2, -extraLength, -thickness / 2);
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, 0, thickness / 2, -extraLength, thickness / 2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = -extraLength;
                                        waveLine.Y1 = -thickness / 2;
                                        waveLine.X2 = -extraLength;
                                        waveLine.Y2 = thickness / 2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, -extraLength, -thickness / 2, -extraLength, thickness / 2, -extraLength - gapDimToPart,
                                                0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Верх or LocationPart.Право_Низ:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2, 0, thickness / 2);
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, 0, -thickness / 2, extraLength, -thickness / 2);
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, 0, thickness / 2, extraLength, thickness / 2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = extraLength;
                                        waveLine.Y1 = -thickness / 2;
                                        waveLine.X2 = extraLength;
                                        waveLine.Y2 = thickness / 2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, extraLength, -thickness / 2, extraLength, thickness / 2, extraLength + gapDimToPartLeft,0,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Верх_Лево or LocationPart.Верх_Право:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2, 0, thickness / 2, 0);
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, -thickness / 2, 0, -thickness / 2, extraLength);
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, thickness / 2, 0, thickness / 2 , extraLength);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = -thickness / 2;
                                        waveLine.Y1 = extraLength ;
                                        waveLine.X2 = thickness / 2 ;
                                        waveLine.Y2 = extraLength;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, -thickness / 2 , extraLength, thickness / 2 , extraLength, 0, extraLength + gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Лево or LocationPart.Низ_Право:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2, 0, thickness / 2, 0);
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, -thickness / 2, 0, -thickness / 2, -extraLength);
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, thickness / 2, 0, thickness / 2, -extraLength);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = -thickness / 2;
                                        waveLine.Y1 = -extraLength;
                                        waveLine.X2 = thickness / 2;
                                        waveLine.Y2 = -extraLength;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, -thickness / 2, -extraLength, thickness / 2, -extraLength, 0, -extraLength - gapDimToPartLeft,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                            }
                            break;
                        case TransitionTypeEnum.Симметричный:
                            switch (locationPart)
                            {
                                case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2, 0, thickness / 2);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, -transitionData.DimL, ls1.Y1 - transitionData.DimH);//Нижний скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2 - extraLength, ls2.Y2);//Нижнее удлимнение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, ls2.X2, -ls2.Y2);//Верхний скос
                                        ILineSegment ls5 = DrawLineSegment(lineSegments, ls4.X2, ls4.Y2, ls3.X2, -ls3.Y2);//Верхнее удлинение
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls5.X2;
                                        waveLine.Y2 = ls5.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(ls5, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls5.X2, ls5.Y2, ls3.X2 - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls1.X1, ls1.Y1, gapDimToPart * 2, ls2.Y2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ls4.X2, ls4.Y2, ls1.X2, ls1.Y2, ldH.X3, ls4.Y2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толищины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, ldH.X3, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls1.X2, ls1.Y2,
                                                -transitionData.DimL / 2, ls4.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Верх or LocationPart.Право_Низ:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2, 0, thickness / 2);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, transitionData.DimL, ls1.Y1 - transitionData.DimH);//Нижний скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2 + extraLength, ls2.Y2);//Нижнее удлимнение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, ls2.X2, -ls2.Y2);//Верхний скос
                                        ILineSegment ls5 = DrawLineSegment(lineSegments, ls4.X2, ls4.Y2, ls3.X2, -ls3.Y2);//Верхнее удлинение
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls5.X2;
                                        waveLine.Y2 = ls5.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(ls5, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls5.X2, ls5.Y2, ls3.X2 + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls1.X1, ls1.Y1, -gapDimToPart, ls2.Y2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ls4.X2, ls4.Y2, ls1.X2, ls1.Y2, ldH.X3, ls4.Y2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толищины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, ldH.X3, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls1.X2, ls1.Y2,
                                                transitionData.DimL / 2, ls4.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Верх_Лево or LocationPart.Верх_Право:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2, 0, thickness / 2, 0);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, ls1.X1 - transitionData.DimH, transitionData.DimL );//Левый скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2, ls2.Y2 + extraLength);//Левое удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, -ls2.X2, ls2.Y2);//Правый скос
                                        ILineSegment ls5 = DrawLineSegment(lineSegments, ls4.X2, ls4.Y2, -ls3.X2, ls3.Y2);//Правое удлинение
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls5.X2;
                                        waveLine.Y2 = ls5.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(ls5, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls5.X2, ls5.Y2, 0, ls3.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls1.X1, ls1.Y1, ls2.X2 - 1, -gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ls4.X2, ls4.Y2, ls1.X2, ls1.Y2, ls4.X2 + 1, ldH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толищины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, 0, ldH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls1.X2, ls1.Y2,
                                                ls4.X2 + gapDimToPart * 2, transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Лево or LocationPart.Низ_Право:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2, 0, thickness / 2, 0);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, ls1.X1 - transitionData.DimH, -transitionData.DimL);//Левый скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2, ls2.Y2 - extraLength);//Левое удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, -ls2.X2, ls2.Y2);//Правый скос
                                        ILineSegment ls5 = DrawLineSegment(lineSegments, ls4.X2, ls4.Y2, -ls3.X2, ls3.Y2);//Правое удлинение
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls5.X2;
                                        waveLine.Y2 = ls5.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(ls5, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls5.X2, ls5.Y2, 0, ls3.Y2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls1.X1, ls1.Y1, ls2.X2 - 1, gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ls4.X2, ls4.Y2, ls1.X2, ls1.Y2, ls4.X2 + 1, ldH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толищины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, 0, ldH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls1.X2, ls1.Y2,
                                                ls4.X2 + gapDimToPart * 2, -transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                            }
                            break;
                        case TransitionTypeEnum.Вверх:
                            switch (locationPart)
                            {
                                case LocationPart.Лево_Верх:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2, 0, thickness / 2);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, -transitionData.DimL, ls1.Y2 + transitionData.DimH);//Верхний скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2 - extraLength, ls2.Y2);//Верхнее удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, ls3.X2, ls1.Y1);//Нижняя линия
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls4.X2;
                                        waveLine.Y2 = ls4.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls4.X2, ls4.Y2, ls3.X2 - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls2.X1, ls2.Y1, gapDimToPart * 2, ls2.Y2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);                                            
                                            //Линейный вертикальный толщины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, ldH.X3, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ls2.X1, ls2.Y1, ls2.X2, ls2.Y2,
                                                -transitionData.DimL / 2, ls2.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Лево_Низ:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2, 0, thickness / 2);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, -transitionData.DimL, ls1.Y1 - transitionData.DimH);//Нижний скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2 - extraLength, ls2.Y2);//Нижнее удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, ls3.X2, ls1.Y2);//Верхняя линия
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls4.X2;
                                        waveLine.Y2 = ls4.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls4.X2, ls4.Y2, ls3.X2 - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls2.X1, ls2.Y1, gapDimToPart * 2, ls2.Y2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толщины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, ldH.X3, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ls2.X1, ls2.Y1, ls2.X2, ls2.Y2,
                                                -transitionData.DimL / 2, ls2.Y2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Верх:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2, 0, thickness / 2);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, transitionData.DimL, ls1.Y2 + transitionData.DimH);//Верхний скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2 + extraLength, ls2.Y2);//Верхнее удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, ls3.X2, ls1.Y1);//Нижняя линия
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls4.X2;
                                        waveLine.Y2 = ls4.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls4.X2, ls4.Y2, ls3.X2 + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls2.X1, ls2.Y1, -gapDimToPart, ls2.Y2 + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толщины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, ldH.X3, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ls2.X1, ls2.Y1, ls2.X2, ls2.Y2,
                                                transitionData.DimL / 2, ls2.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Низ:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2, 0, thickness / 2);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, transitionData.DimL, ls1.Y1 - transitionData.DimH);//Нижний скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2 + extraLength, ls2.Y2);//Нижнее удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, ls3.X2, ls1.Y2);//Верхняя линия
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls4.X2;
                                        waveLine.Y2 = ls4.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls4.X2, ls4.Y2, ls3.X2 + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls2.X1, ls2.Y1, -gapDimToPart, ls2.Y2 - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный толщины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, ldH.X3, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный перехода
                                            LineDimension(lineDimensions, ls2.X1, ls2.Y1, ls2.X2, ls2.Y2,
                                                transitionData.DimL / 2, ls2.Y2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        }
                                    }
                                    break;
                                case LocationPart.Верх_Лево:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2, 0, thickness / 2, 0);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, ls1.X1 - transitionData.DimH, transitionData.DimL);//Левый скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2, ls2.Y2 + extraLength);//Левое удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, ls1.X2, ls3.Y2);//Правая линия
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls4.X2;
                                        waveLine.Y2 = ls4.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls4.X2, ls4.Y2, 0, ls3.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls2.X1, ls2.Y1, ls2.X2 - 1, -gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, 0, ldH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ls2.X1, ls2.Y1, ls2.X2, ls2.Y2,
                                                ls2.X2 - gapDimToPart, transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Верх_Право:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2, 0, thickness / 2, 0);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, ls1.X2 + transitionData.DimH, transitionData.DimL);//Правый скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2, ls2.Y2 + extraLength);//Правое удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, ls1.X1, ls3.Y2);//Левая линия
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls4.X2;
                                        waveLine.Y2 = ls4.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls4.X2, ls4.Y2, 0, ls3.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls2.X1, ls2.Y1, ls2.X2 + 1, -gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, 0, ldH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ls2.X1, ls2.Y1, ls2.X2, ls2.Y2,
                                                ls2.X2 + gapDimToPart * 2, transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Лево:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2, 0, thickness / 2, 0);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, ls1.X1 - transitionData.DimH, -transitionData.DimL);//Левый скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2, ls2.Y2 - extraLength);//Левое удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, ls1.X2, ls3.Y2);//Правая линия
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls4.X2;
                                        waveLine.Y2 = ls4.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls4.X2, ls4.Y2, 0, ls3.Y2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls2.X1, ls2.Y1, ls2.X2 - 1, gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, 0, ldH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ls2.X1, ls2.Y1, ls2.X2, ls2.Y2,
                                                ls2.X2 - gapDimToPart, -transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Право:
                                    {
                                        ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2, 0, thickness / 2, 0);//Толщина в стыке
                                        ILineSegment ls2 = DrawLineSegment(lineSegments, ls1.X2, ls1.Y2, ls1.X2 + transitionData.DimH, -transitionData.DimL);//Правый скос
                                        ILineSegment ls3 = DrawLineSegment(lineSegments, ls2.X2, ls2.Y2, ls2.X2, ls2.Y2 - extraLength);//Правое удлинение
                                        ILineSegment ls4 = DrawLineSegment(lineSegments, ls1.X1, ls1.Y1, ls1.X1, ls3.Y2);//Левая линия
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = ls3.X2;
                                        waveLine.Y1 = ls3.Y2;
                                        waveLine.X2 = ls4.X2;
                                        waveLine.Y2 = ls4.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(ls1, false);
                                            contour.CopySegments(ls2, false);
                                            contour.CopySegments(ls3, false);
                                            contour.CopySegments(ls4, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, ls3.X2, ls3.Y2, ls4.X2, ls4.Y2, 0, ls3.Y2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldH = LineDimension(lineDimensions, ls2.X2, ls2.Y2, ls2.X1, ls2.Y1, ls2.X2 + 1, gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный толщины в стыке
                                            LineDimension(lineDimensions, ls1.X1, ls1.Y1, ls1.X2, ls1.Y2, 0, ldH.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, ls2.X1, ls2.Y1, ls2.X2, ls2.Y2,
                                                ls2.X2 + gapDimToPart * 2, -transitionData.DimL / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                        }
                                    }
                                    break;
                            }
                            break;
                        case TransitionTypeEnum.Вниз:
                            switch (locationPart)
                            {
                                case LocationPart.Лево_Верх:
                                    locationPart = LocationPart.Лево_Низ;
                                    break;
                                case LocationPart.Лево_Низ:
                                    locationPart = LocationPart.Лево_Верх;
                                    break;
                                case LocationPart.Право_Верх:
                                    locationPart = LocationPart.Право_Низ;
                                    break;
                                case LocationPart.Право_Низ:
                                    locationPart = LocationPart.Право_Верх;
                                    break;
                                case LocationPart.Верх_Лево:
                                    locationPart = LocationPart.Верх_Право;
                                    break;
                                case LocationPart.Верх_Право:
                                    locationPart = LocationPart.Верх_Лево;
                                    break;
                                case LocationPart.Низ_Лево:
                                    locationPart = LocationPart.Низ_Право;
                                    break;
                                case LocationPart.Низ_Право:
                                    locationPart = LocationPart.Низ_Лево;
                                    break;
                            }
                            goto case TransitionTypeEnum.Вверх;
                        case TransitionTypeEnum.Занижение:
                            break;
                    }
                    break;
                case ShapePreparedEdgesEnum.Без_скоса_угловое:
                    //Делаем минимальное удлинение детали
                    extraLength += extraLengthNotChamfer / view.Scale;
                    switch (locationPart)
                    {
                        case LocationPart.Лево_Верх:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, -0.7 * thickness, -extraLength, -0.7 * thickness, thickness);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, -0.7 * thickness, thickness, 0, thickness);
                                ILineSegment ls3 = DrawLineSegment(lineSegments, 0, thickness, 0, -extraLength);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = 0;
                                waveLine.Y1 = -extraLength;
                                waveLine.X2 = -0.7 * thickness;
                                waveLine.Y2 = -extraLength;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(ls3, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        case LocationPart.Лево_Низ:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, -0.7 * thickness, extraLength, -0.7 * thickness, -thickness);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, -0.7 * thickness, -thickness, 0, -thickness);
                                ILineSegment ls3 = DrawLineSegment(lineSegments, 0, -thickness, 0, extraLength);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = 0;
                                waveLine.Y1 = extraLength;
                                waveLine.X2 = -0.7 * thickness;
                                waveLine.Y2 = extraLength;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(ls3, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        case LocationPart.Право_Верх:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, 0.7 * thickness, -extraLength, 0.7 * thickness, thickness);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, 0.7 * thickness, thickness, 0, thickness);
                                ILineSegment ls3 = DrawLineSegment(lineSegments, 0, thickness, 0, -extraLength);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = 0;
                                waveLine.Y1 = -extraLength;
                                waveLine.X2 = 0.7 * thickness;
                                waveLine.Y2 = -extraLength;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(ls3, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        case LocationPart.Право_Низ:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, 0.7 * thickness, extraLength, 0.7 * thickness, -thickness);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, 0.7 * thickness, -thickness, 0, -thickness);
                                ILineSegment ls3 = DrawLineSegment(lineSegments, 0, -thickness, 0, extraLength);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = 0;
                                waveLine.Y1 = extraLength;
                                waveLine.X2 = 0.7 * thickness;
                                waveLine.Y2 = extraLength;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(ls3, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        case LocationPart.Верх_Право:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, -extraLength , 0.7 * thickness, thickness , 0.7 * thickness);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, thickness , 0.7 * thickness, thickness, 0);
                                ILineSegment ls3 = DrawLineSegment(lineSegments, thickness, 0, -extraLength, 0);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = -extraLength;
                                waveLine.Y1 = 0;
                                waveLine.X2 = -extraLength;
                                waveLine.Y2 = 0.7 * thickness ;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(ls3, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        case LocationPart.Верх_Лево:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, extraLength, 0.7 * thickness, -thickness, 0.7 * thickness);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, -thickness, 0.7 * thickness, -thickness, 0);
                                ILineSegment ls3 = DrawLineSegment(lineSegments, -thickness, 0, extraLength, 0);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = extraLength;
                                waveLine.Y1 = 0;
                                waveLine.X2 = extraLength;
                                waveLine.Y2 = 0.7 * thickness;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(ls3, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        case LocationPart.Низ_Право:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, -extraLength, -0.7 * thickness, thickness, -0.7 * thickness);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, thickness, -0.7 * thickness, thickness, 0);
                                ILineSegment ls3 = DrawLineSegment(lineSegments, thickness, 0, -extraLength, 0);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = -extraLength;
                                waveLine.Y1 = 0;
                                waveLine.X2 = -extraLength;
                                waveLine.Y2 = -0.7 * thickness;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(ls3, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        case LocationPart.Низ_Лево:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, extraLength, -0.7 * thickness, -thickness, -0.7 * thickness);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, -thickness, -0.7 * thickness, -thickness, 0);
                                ILineSegment ls3 = DrawLineSegment(lineSegments, -thickness, 0, extraLength, 0);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = extraLength;
                                waveLine.Y1 = 0;
                                waveLine.X2 = extraLength;
                                waveLine.Y2 = -0.7 * thickness;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(ls3, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                case ShapePreparedEdgesEnum.Без_скоса_тавровое:
                    //Делаем минимальное удлинение детали
                    extraLength += extraLengthNotChamfer / view.Scale;
                    switch (locationPart)
                    {
                        case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2 - extraLength, 0, thickness / 2 + extraLength);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, -thickness * 0.7, -thickness / 2 - extraLength, -thickness * 0.7, thickness / 2 + extraLength);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = ls1.X1;
                                waveLine.Y1 = ls1.Y1;
                                waveLine.X2 = ls2.X1;
                                waveLine.Y2 = ls2.Y1;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                IWaveLine waveLine1 = waveLines.Add();
                                waveLine1.X1 = ls1.X2;
                                waveLine1.Y1 = ls1.Y2;
                                waveLine1.X2 = ls2.X2;
                                waveLine1.Y2 = ls2.Y2;
                                waveLine1.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine1.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(waveLine, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(waveLine1, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        case LocationPart.Право_Верх or LocationPart.Право_Низ:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, 0, -thickness / 2 - extraLength, 0, thickness / 2 + extraLength);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, thickness * 0.7, -thickness / 2 - extraLength, thickness * 0.7, thickness / 2 + extraLength);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = ls1.X1;
                                waveLine.Y1 = ls1.Y1;
                                waveLine.X2 = ls2.X1;
                                waveLine.Y2 = ls2.Y1;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                IWaveLine waveLine1 = waveLines.Add();
                                waveLine1.X1 = ls1.X2;
                                waveLine1.Y1 = ls1.Y2;
                                waveLine1.X2 = ls2.X2;
                                waveLine1.Y2 = ls2.Y2;
                                waveLine1.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine1.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(waveLine, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(waveLine1, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;                        
                        case LocationPart.Верх_Лево or LocationPart.Верх_Право:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2 - extraLength, 0, thickness / 2 + extraLength, 0);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, -thickness / 2 - extraLength, thickness * 0.7 , thickness / 2 + extraLength, thickness * 0.7);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = ls1.X1;
                                waveLine.Y1 = ls1.Y1;
                                waveLine.X2 = ls2.X1;
                                waveLine.Y2 = ls2.Y1;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                IWaveLine waveLine1 = waveLines.Add();
                                waveLine1.X1 = ls1.X2;
                                waveLine1.Y1 = ls1.Y2;
                                waveLine1.X2 = ls2.X2;
                                waveLine1.Y2 = ls2.Y2;
                                waveLine1.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine1.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(waveLine, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(waveLine1, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        case LocationPart.Низ_Лево or LocationPart.Низ_Право:
                            {
                                ILineSegment ls1 = DrawLineSegment(lineSegments, -thickness / 2 - extraLength, 0, thickness / 2 + extraLength, 0);
                                ILineSegment ls2 = DrawLineSegment(lineSegments, -thickness / 2 - extraLength, -thickness * 0.7, thickness / 2 + extraLength, -thickness * 0.7);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = ls1.X1;
                                waveLine.Y1 = ls1.Y1;
                                waveLine.X2 = ls2.X1;
                                waveLine.Y2 = ls2.Y1;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                IWaveLine waveLine1 = waveLines.Add();
                                waveLine1.X1 = ls1.X2;
                                waveLine1.Y1 = ls1.Y2;
                                waveLine1.X2 = ls2.X2;
                                waveLine1.Y2 = ls2.Y2;
                                waveLine1.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine1.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(ls1, false);
                                    contour.CopySegments(waveLine, false);
                                    contour.CopySegments(ls2, false);
                                    contour.CopySegments(waveLine1, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                            }
                            break;
                        default:
                            break;
                    }

                    break;
                case ShapePreparedEdgesEnum.Без_притупления:
                    break;
                case ShapePreparedEdgesEnum.Со_скосом_одной_кромки:
                    switch (locationPart)
                    {
                        case LocationPart.Лево_Верх:
                            //Без переходов
                            if (true)
                            {
                                //Размер скоса
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                extraLength += xangle;
                                extraLength = extraLength < 1 ? 1 : extraLength;
                                //Чертим графику
                                //Создаём основу разделки
                                //Притупление
                                ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, 0, 0, ParamC);
                                //Угла
                                ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, 0, ParamC, -xangle, thickness);
                                //От угла к краю детали
                                ILineSegment lsExtra1 = DrawLineSegment(lineSegments, -xangle, thickness, -(xangle + extraLength), thickness);
                                //От нуля к краю детали
                                ILineSegment lsExtra2 = DrawLineSegment(lineSegments, 0, 0, -(xangle + extraLength), 0);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = -(xangle + extraLength);
                                waveLine.Y1 = 0;
                                waveLine.X2 = -(xangle + extraLength);
                                waveLine.Y2 = thickness;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(baseobjAngle1, false);
                                    contour.CopySegments(baseobjAngle2, false);
                                    contour.CopySegments(lsExtra1, false);
                                    contour.CopySegments(lsExtra2, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                                //Если разрез
                                if (!isCrossSection)
                                {
                                    DrawLineSegment(lineSegments, ParamB, thickness, -xangle, thickness);
                                    DrawLineSegment(lineSegments, ParamB, 0, ParamB, thickness);
                                    if (ParamB != 0)
                                    {
                                        DrawLineSegment(lineSegments, ParamB, 0, 0, 0);
                                    }
                                }
                                //Чертим размеры
                                if (drawDimensions)
                                {
                                    //Линейный вертикальный толщины
                                    LineDimension(lineDimensions, -(xangle + extraLength), 0, -(xangle + extraLength), thickness, -(xangle + extraLength + gapDimToPart), thickness / 2
                                        , ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный горзонтальный угла
                                    LineDimension(lineDimensions, 0, ParamC, -xangle, thickness, -xangle / 2, thickness + gapDimToPart,
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamC = LineDimension(lineDimensions, 0, 0, 0, ParamC, 0, ParamC + 1,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                    //Только для "левых видов"
                                    if (Math.Abs(ParamCTolerance[0]) == Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamC.X3 = gapDimToPart * 2;
                                    }
                                    else
                                    {
                                        ldParamC.X3 = gapDimToPart * 3;
                                    }
                                    SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                    double r1 = (thickness - ParamC + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(thickness - ParamC + gapDimToPart + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Угол
                                    IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -xangle / 2, thickness + gapDimToPart + gapDimToDim, angleDRadius);
                                    SetDeviation(dtParamA, ParamATolerance);
                                    if (!isCrossSection && ParamB != 0)
                                    {
                                        //Зазора в стыке
                                        IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, ParamB, thickness, 0, thickness, ParamB + 1, thickness + gapDimToPart,
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        SetDeviation(dtPatamB, paramBTolerance);
                                        //Двигаем размер притупления на величину зазора если выбран разрез
                                        ILineDimension ld_ParamB = ldParamC;
                                        ld_ParamB.X3 += ParamB;
                                        ld_ParamB.Update();
                                    }
                                }
                            }

                            //Обычный переход вверху

                            //Обычный переход внизу

                            //Обычный переход вверху и внизу

                            break;
                        case LocationPart.Лево_Низ:
                            //Без переходов
                            if (true)
                            {
                                //Размер скоса
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                extraLength += xangle;
                                extraLength = extraLength < 1 ? 1 : extraLength;
                                //Чертим графику
                                //Создаём основу разделки
                                //Притупление
                                ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, 0, 0, -ParamC);
                                //Угла
                                ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, 0, -ParamC, -xangle, -thickness);
                                //От угла к краю детали
                                ILineSegment lsExtra1 = DrawLineSegment(lineSegments, -xangle, -thickness, -(xangle + extraLength), -thickness);
                                //От нуля к краю детали
                                ILineSegment lsExtra2 = DrawLineSegment(lineSegments, 0, 0, -(xangle + extraLength), 0);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = -(xangle + extraLength);
                                waveLine.Y1 = 0;
                                waveLine.X2 = -(xangle + extraLength);
                                waveLine.Y2 = -thickness;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(baseobjAngle1, false);
                                    contour.CopySegments(baseobjAngle2, false);
                                    contour.CopySegments(lsExtra1, false);
                                    contour.CopySegments(lsExtra2, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                                //Если разрез
                                if (!isCrossSection)
                                {
                                    DrawLineSegment(lineSegments, ParamB, -thickness, -xangle, -thickness);
                                    DrawLineSegment(lineSegments, ParamB, 0, ParamB, -thickness);
                                    if (ParamB != 0)
                                    {
                                        DrawLineSegment(lineSegments, ParamB, 0, 0, 0);
                                    }
                                }
                                //Чертим размеры
                                if (drawDimensions)
                                {
                                    //Линейный вертикальный толщины
                                    LineDimension(lineDimensions, -(xangle + extraLength), 0, -(xangle + extraLength), -thickness, -(xangle + extraLength + gapDimToPartLeft), -thickness / 2
                                        , ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный горзонтальный угла
                                    LineDimension(lineDimensions, 0, -ParamC, -xangle, -thickness, -xangle / 2, -(thickness + gapDimToPart * 2),
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления
                                    ILineDimension ldParamC = LineDimension(lineDimensions, 0, 0, 0, -ParamC, gapDimToPart * 3, -(ParamC + 1),
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                    //Только для "левых видов"
                                    if (Math.Abs(ParamCTolerance[0]) == Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamC.X3 = gapDimToPart * 2;
                                    }
                                    else
                                    {
                                        ldParamC.X3 = gapDimToPart * 3;
                                    }
                                    SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                    double r1 = (thickness - ParamC + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(thickness - ParamC + gapDimToPart * 2 + gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Угол
                                    IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -xangle / 2, -(thickness + gapDimToPart * 2 + gapDimToDim * 1.5), angleDRadius);
                                    SetDeviation(dtParamA, ParamATolerance);
                                    if (!isCrossSection && ParamB != 0)
                                    {
                                        //Зазора в стыке
                                        IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, ParamB, -thickness, 0, -thickness, ParamB + 1, -(thickness + gapDimToPart * 2),
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        SetDeviation(dtPatamB, paramBTolerance);
                                        //Двигаем размер притупления на величину зазора если выбран разрез
                                        ILineDimension ld_ParamB = ldParamC;
                                        ld_ParamB.X3 += ParamB;
                                        ld_ParamB.Update();
                                    }
                                }
                            }

                            //Обычный переход вверху

                            //Обычный переход внизу

                            //Обычный переход вверху и внизу

                            break;
                        case LocationPart.Право_Верх:
                            //Без переходов
                            if (true)
                            {
                                //Размер скоса
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                extraLength += xangle;
                                extraLength = extraLength < 1 ? 1 : extraLength;
                                //Чертим графику
                                //Создаём основу разделки
                                //Притупление
                                ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, 0, 0, ParamC);
                                //Угла
                                ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, 0, ParamC, xangle, thickness);
                                //От угла к краю детали
                                ILineSegment lsExtra1 = DrawLineSegment(lineSegments, xangle, thickness, xangle + extraLength, thickness);
                                //От нуля к краю детали
                                ILineSegment lsExtra2 = DrawLineSegment(lineSegments, 0, 0, xangle + extraLength, 0);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = xangle + extraLength;
                                waveLine.Y1 = 0;
                                waveLine.X2 = xangle + extraLength;
                                waveLine.Y2 = thickness;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(baseobjAngle1, false);
                                    contour.CopySegments(baseobjAngle2, false);
                                    contour.CopySegments(lsExtra1, false);
                                    contour.CopySegments(lsExtra2, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                                //Если разрез
                                if (!isCrossSection)
                                {
                                    DrawLineSegment(lineSegments, 0 - ParamB, thickness, xangle, thickness);
                                    DrawLineSegment(lineSegments, 0 - ParamB, 0, 0 - ParamB, thickness);
                                    if (ParamB != 0)
                                    {
                                        DrawLineSegment(lineSegments, 0 - ParamB, 0, 0, 0);
                                    }
                                }
                                //Чертим размеры
                                if (drawDimensions)
                                {
                                    //Линейный вертикальный толщины
                                    LineDimension(lineDimensions, xangle + extraLength, 0, xangle + extraLength, thickness, xangle + extraLength + gapDimToPartLeft, thickness / 2
                                        , ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный горзонтальный угла
                                    LineDimension(lineDimensions, 0, ParamC, xangle, thickness, xangle / 2, thickness + gapDimToPart,
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления
                                    IDimensionText dtParamC = (IDimensionText)LineDimension(lineDimensions, 0, 0, 0, ParamC, - gapDimToPart, ParamC + 1,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation(dtParamC, paramCTolerance);
                                    double r1 = (thickness - ParamC + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(thickness - ParamC + gapDimToPart + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Угол
                                    IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        xangle / 2, thickness + gapDimToPart + gapDimToDim, angleDRadius);
                                    SetDeviation(dtParamA, ParamATolerance);
                                    if (!isCrossSection && ParamB != 0)
                                    {
                                        //Зазора в стыке
                                        IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, 0 - ParamB, thickness, 0, thickness, 0 - ParamB - 1, thickness + gapDimToPart,
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        SetDeviation(dtPatamB, paramBTolerance);
                                        //Двигаем размер притупления на величину зазора если выбран разрез
                                        ILineDimension ld_ParamB = (ILineDimension)dtParamC;
                                        ld_ParamB.X3 -= ParamB;
                                        ld_ParamB.Update();
                                    }
                                }
                            }

                            //Обычный переход вверху

                            //Обычный переход внизу

                            //Обычный переход вверху и внизу

                            break;
                        case LocationPart.Право_Низ:
                            //Без переходов
                            if (true)
                            {
                                //Размер скоса
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                extraLength += xangle;
                                extraLength = extraLength < 1 ? 1 : extraLength;
                                //Чертим графику
                                //Создаём основу разделки
                                //Притупление
                                ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, 0, 0, -ParamC);
                                //Угла
                                ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, 0, -ParamC, xangle, -thickness);
                                //От угла к краю детали
                                ILineSegment lsExtra1 = DrawLineSegment(lineSegments, xangle, -thickness, xangle + extraLength, -thickness);
                                //От нуля к краю детали
                                ILineSegment lsExtra2 = DrawLineSegment(lineSegments, 0, 0, xangle + extraLength, 0);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = xangle + extraLength;
                                waveLine.Y1 = 0;
                                waveLine.X2 = xangle + extraLength;
                                waveLine.Y2 = -thickness;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(baseobjAngle1, false);
                                    contour.CopySegments(baseobjAngle2, false);
                                    contour.CopySegments(lsExtra1, false);
                                    contour.CopySegments(lsExtra2, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }                                
                                //Если разрез
                                if (!isCrossSection)
                                {
                                    DrawLineSegment(lineSegments, 0 - ParamB, -thickness, xangle, -thickness);
                                    DrawLineSegment(lineSegments, 0 - ParamB, 0, 0 - ParamB, -thickness);
                                    if (ParamB != 0)
                                    {
                                        DrawLineSegment(lineSegments, 0 - ParamB, 0, 0, 0);
                                    }
                                }
                                //Чертим размеры
                                if (drawDimensions)
                                {
                                    //Линейный вертикальный толщины
                                    LineDimension(lineDimensions, xangle + extraLength, 0, xangle + extraLength, -thickness, xangle + extraLength + gapDimToPartLeft, -thickness / 2
                                        , ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный горзонтальный угла
                                    LineDimension(lineDimensions, 0, -ParamC, xangle, -thickness, xangle / 2, -(thickness + gapDimToPart * 2),
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный притупления
                                    IDimensionText dtParamC = (IDimensionText)LineDimension(lineDimensions, 0, 0, 0, -ParamC, -gapDimToPart, -(ParamC + 1),
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                    SetDeviation(dtParamC, paramCTolerance);
                                    double r1 = (thickness - ParamC + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(thickness - ParamC + gapDimToPart * 2 + gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Угол
                                    IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        xangle / 2, -(thickness + gapDimToPart * 2 + gapDimToDim * 1.5), angleDRadius);
                                    SetDeviation(dtParamA, ParamATolerance);
                                    if (!isCrossSection && ParamB != 0)
                                    {
                                        //Зазора в стыке
                                        IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, 0 - ParamB, -thickness, 0, -thickness, 0 - ParamB - 1, -(thickness + gapDimToPart * 2),
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                        SetDeviation(dtPatamB, paramBTolerance);
                                        //Двигаем размер притупления на величину зазора если выбран разрез
                                        ILineDimension ld_ParamB = (ILineDimension)dtParamC;
                                        ld_ParamB.X3 -= ParamB;
                                        ld_ParamB.Update();
                                    }
                                }
                            }

                            //Обычный переход вверху

                            //Обычный переход внизу

                            //Обычный переход вверху и внизу

                            break;
                        case LocationPart.Верх_Лево:
                            //Без переходов
                            if (true)
                            {
                                //Размер скоса
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                extraLength += xangle;
                                extraLength = extraLength < 1 ? 1 : extraLength;
                                //Чертим графику
                                //Создаём основу разделки
                                //Притупление
                                ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, 0, -ParamC, 0);
                                //Угла
                                ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -ParamC, 0, -thickness, xangle);
                                //От угла к краю детали
                                ILineSegment lsExtra1 = DrawLineSegment(lineSegments, -thickness, xangle, -thickness, xangle + extraLength);
                                //От нуля к краю детали
                                ILineSegment lsExtra2 = DrawLineSegment(lineSegments, 0, 0, 0, xangle + extraLength);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = 0;
                                waveLine.Y1 = xangle + extraLength;
                                waveLine.X2 = -thickness;
                                waveLine.Y2 = xangle + extraLength;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(baseobjAngle1, false);
                                    contour.CopySegments(baseobjAngle2, false);
                                    contour.CopySegments(lsExtra1, false);
                                    contour.CopySegments(lsExtra2, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                                //Если разрез
                                if (!isCrossSection)
                                {
                                    DrawLineSegment(lineSegments, -thickness, -ParamB, -thickness, xangle);
                                    DrawLineSegment(lineSegments, 0, -ParamB, -thickness, -ParamB);
                                    if (ParamB != 0)
                                    {
                                        DrawLineSegment(lineSegments, 0, -ParamB, 0, 0);
                                    }
                                }
                                //Чертим размеры
                                if (drawDimensions)
                                {
                                    //Линейный горизонтальный толщины
                                    LineDimension(lineDimensions, 0, xangle + extraLength, -thickness, xangle + extraLength, -thickness / 2, xangle + extraLength + gapDimToPartLeft
                                        , ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный угла
                                    LineDimension(lineDimensions, -ParamC, 0, -thickness, xangle, -(thickness + gapDimToPart), xangle / 2,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamC = LineDimension(lineDimensions, 0, 0, -ParamC, 0, -ParamC - 1, -gapDimToPart,
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                    //Только для "верхних видов"
                                    if (Math.Abs(ParamCTolerance[0]) == Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamC.Y3 = -gapDimToPart * 2;
                                    }
                                    else
                                    {
                                        ldParamC.Y3 = -gapDimToPart * 3;
                                    }
                                    SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                    double r1 = (thickness - ParamC + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(thickness - ParamC + gapDimToPart + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Угол
                                    IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -(thickness + gapDimToPart + gapDimToDim), xangle / 2, angleDRadius);
                                    SetDeviation(dtParamA, ParamATolerance);
                                    if (!isCrossSection && ParamB != 0)
                                    {
                                        //Зазора в стыке
                                        IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, -thickness, -ParamB, -thickness, 0, -(thickness + gapDimToPart), -ParamB - 1,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                        SetDeviation(dtPatamB, paramBTolerance);
                                        //Двигаем размер притупления на величину зазора если выбран разрез
                                        ILineDimension ld_ParamB = ldParamC;
                                        ld_ParamB.Y3 -= ParamB;
                                        ld_ParamB.Update();
                                    }
                                }
                            }

                            //Обычный переход вверху

                            //Обычный переход внизу

                            //Обычный переход вверху и внизу

                            break;
                        case LocationPart.Верх_Право:
                            //Без переходов
                            if (true)
                            {
                                //Размер скоса
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                extraLength += xangle;
                                extraLength = extraLength < 1 ? 1 : extraLength;
                                //Чертим графику
                                //Создаём основу разделки
                                //Притупление
                                ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, 0, ParamC, 0);
                                //Угла
                                ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC, 0, thickness, xangle);
                                //От угла к краю детали
                                ILineSegment lsExtra1 = DrawLineSegment(lineSegments, thickness, xangle, thickness, xangle + extraLength);
                                //От нуля к краю детали
                                ILineSegment lsExtra2 = DrawLineSegment(lineSegments, 0, 0, 0, xangle + extraLength);
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = 0;
                                waveLine.Y1 = xangle + extraLength;
                                waveLine.X2 = thickness;
                                waveLine.Y2 = xangle + extraLength;
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(baseobjAngle1, false);
                                    contour.CopySegments(baseobjAngle2, false);
                                    contour.CopySegments(lsExtra1, false);
                                    contour.CopySegments(lsExtra2, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                                //Если разрез
                                if (!isCrossSection)
                                {
                                    DrawLineSegment(lineSegments, thickness, -ParamB, thickness, xangle);
                                    DrawLineSegment(lineSegments, 0, -ParamB, thickness, -ParamB);
                                    if (ParamB != 0)
                                    {
                                        DrawLineSegment(lineSegments, 0, -ParamB, 0, 0);
                                    }
                                }
                                //Чертим размеры
                                if (drawDimensions)
                                {
                                    //Линейный горизонтальный толщины
                                    LineDimension(lineDimensions, 0, xangle + extraLength, thickness, xangle + extraLength, thickness / 2, xangle + extraLength + gapDimToPartLeft
                                        , ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный угла
                                    LineDimension(lineDimensions, ParamC, 0, thickness, xangle, thickness + gapDimToPart * 2, xangle / 2,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный горизонтальный притупления
                                    ILineDimension ldParamC = LineDimension(lineDimensions, 0, 0, ParamC, 0, ParamC + 1, -gapDimToPart * 2,
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                    //Только для "верхних видов"
                                    if (Math.Abs(ParamCTolerance[0]) == Math.Abs(ParamCTolerance[1]))
                                    {
                                        ldParamC.Y3 = -gapDimToPart * 2;
                                    }
                                    else
                                    {
                                        ldParamC.Y3 = -gapDimToPart * 3;
                                    }
                                    SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                    double r1 = (thickness - ParamC + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(thickness - ParamC + gapDimToPart * 2 + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Угол
                                    IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        thickness + gapDimToPart * 2 + gapDimToDim, xangle / 2, angleDRadius);
                                    SetDeviation(dtParamA, ParamATolerance);
                                    if (!isCrossSection && ParamB != 0)
                                    {
                                        //Зазора в стыке
                                        IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, thickness, -ParamB, thickness, 0, thickness + gapDimToPart * 2, - ParamB - 1,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                        SetDeviation(dtPatamB, paramBTolerance);
                                        //Двигаем размер притупления на величину зазора если выбран разрез
                                        ILineDimension ld_ParamB = ldParamC;
                                        ld_ParamB.Y3 -= ParamB;
                                        ld_ParamB.Update();
                                    }
                                }
                            }

                            //Обычный переход вверху

                            //Обычный переход внизу

                            //Обычный переход вверху и внизу

                            break;
                        case LocationPart.Низ_Лево:
                            //Без переходов
                            if (true)
                            {
                                //Размер скоса
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                extraLength += xangle;
                                extraLength = extraLength < 1 ? 1 : extraLength;
                                //Чертим графику
                                //Создаём основу разделки
                                //Притупление
                                ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, 0, -ParamC, 0);
                                //Угла
                                ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, -ParamC, 0, -thickness, -xangle);
                                //От угла к краю детали
                                ILineSegment lsExtra1 = DrawLineSegment(lineSegments, -thickness, -xangle, -thickness, -(xangle + extraLength));
                                //От нуля к краю детали
                                ILineSegment lsExtra2 = DrawLineSegment(lineSegments, 0, 0, 0, -(xangle + extraLength));
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = 0;
                                waveLine.Y1 = -(xangle + extraLength);
                                waveLine.X2 = -thickness;
                                waveLine.Y2 = -(xangle + extraLength);
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(baseobjAngle1, false);
                                    contour.CopySegments(baseobjAngle2, false);
                                    contour.CopySegments(lsExtra1, false);
                                    contour.CopySegments(lsExtra2, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                                //Если разрез
                                if (!isCrossSection)
                                {
                                    DrawLineSegment(lineSegments, -thickness, ParamB, -thickness, -xangle);
                                    DrawLineSegment(lineSegments, 0, ParamB, -thickness, ParamB);
                                    if (ParamB != 0)
                                    {
                                        DrawLineSegment(lineSegments, 0, ParamB, 0, 0);
                                    }
                                }
                                //Чертим размеры
                                if (drawDimensions)
                                {
                                    //Линейный горизонтальный толщины
                                    LineDimension(lineDimensions, 0, -(xangle + extraLength), -thickness, -(xangle + extraLength), -thickness / 2, -(xangle + extraLength + gapDimToPartLeft)
                                        , ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный угла
                                    LineDimension(lineDimensions, -ParamC, 0, -thickness, -xangle, -(thickness + gapDimToPart), -xangle / 2,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный горизонтальный притупления
                                    IDimensionText dtParamC = (IDimensionText)LineDimension(lineDimensions, 0, 0, -ParamC, 0, -ParamC - 1, gapDimToPart,
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation(dtParamC, paramCTolerance);
                                    double r1 = (thickness - ParamC + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(thickness - ParamC + gapDimToPart + gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Угол
                                    IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        -(thickness + gapDimToPart + gapDimToDim - 1.5), -xangle / 2, angleDRadius);
                                    SetDeviation(dtParamA, ParamATolerance);
                                    if (!isCrossSection && ParamB != 0)
                                    {
                                        //Зазора в стыке
                                        IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, -thickness, ParamB, -thickness, 0, -(thickness + gapDimToPart), ParamB + 1,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                        SetDeviation(dtPatamB, paramBTolerance);
                                        //Двигаем размер притупления на величину зазора если выбран разрез
                                        ILineDimension ld_ParamB = (ILineDimension)dtParamC;
                                        ld_ParamB.Y3 += ParamB;
                                        ld_ParamB.Update();
                                    }
                                }
                            }

                            //Обычный переход вверху

                            //Обычный переход внизу

                            //Обычный переход вверху и внизу

                            break;
                        case LocationPart.Низ_Право:
                            //Без переходов
                            if (true)
                            {
                                //Размер скоса
                                double xangle = (thickness - ParamC) * Math.Tan(ParamA * Math.PI / 180);
                                extraLength += xangle;
                                extraLength = extraLength < 1 ? 1 : extraLength;
                                //Чертим графику
                                //Создаём основу разделки
                                //Притупление
                                ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, 0, ParamC, 0);
                                //Угла
                                ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC, 0, thickness, -xangle);
                                //От угла к краю детали
                                ILineSegment lsExtra1 = DrawLineSegment(lineSegments, thickness, -xangle, thickness, -(xangle + extraLength));
                                //От нуля к краю детали
                                ILineSegment lsExtra2 = DrawLineSegment(lineSegments, 0, 0, 0, -(xangle + extraLength));
                                //Волнистая линия
                                IWaveLines waveLines = symbols2DContainer.WaveLines;
                                IWaveLine waveLine = waveLines.Add();
                                waveLine.X1 = 0;
                                waveLine.Y1 = -(xangle + extraLength);
                                waveLine.X2 = thickness;
                                waveLine.Y2 = -(xangle + extraLength);
                                waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                waveLine.Update();
                                if (isHatches)
                                {
                                    //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                    IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                    IDrawingContour drawingContour = drawingContours.Add();
                                    IContour contour = (IContour)drawingContour;
                                    //Добавляем в контур элементы которые ограничивают штриховку
                                    contour.CopySegments(baseobjAngle1, false);
                                    contour.CopySegments(baseobjAngle2, false);
                                    contour.CopySegments(lsExtra1, false);
                                    contour.CopySegments(lsExtra2, false);
                                    contour.CopySegments(waveLine, false);
                                    drawingContour.Update();
                                    //Штриховка
                                    IHatches hatches = drawingContainer.Hatches;
                                    IHatch hatch = hatches.Add();
                                    IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                    boundariesObject.AddBoundaries(drawingContour, true);
                                    hatch.Update();
                                }
                                //Если разрез
                                if (!isCrossSection)
                                {
                                    DrawLineSegment(lineSegments, thickness, ParamB, thickness, -xangle);
                                    DrawLineSegment(lineSegments, 0, ParamB, thickness, ParamB);
                                    if (ParamB != 0)
                                    {
                                        DrawLineSegment(lineSegments, 0, ParamB, 0, 0);
                                    }
                                }
                                //Чертим размеры
                                if (drawDimensions)
                                {
                                    //Линейный горизонтальный толщины
                                    LineDimension(lineDimensions, 0, -(xangle + extraLength), thickness, -(xangle + extraLength), thickness / 2, -(xangle + extraLength + gapDimToPartLeft)
                                        , ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    //Линейный вертикальный угла
                                    LineDimension(lineDimensions, ParamC, 0, thickness, -xangle, thickness + gapDimToPart * 2, -xangle / 2,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                    //Линейный горизонтальный притупления
                                    IDimensionText dtParamC = (IDimensionText)LineDimension(lineDimensions, 0, 0, ParamC, 0, ParamC + 1, gapDimToPart,
                                        ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                    SetDeviation(dtParamC, paramCTolerance);
                                    double r1 = (thickness - ParamC + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                    double r2 = Math.Sqrt(Math.Pow(thickness - ParamC + gapDimToPart * 2 + gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                    double angleDRadius = r1 > r2 ? r1 : r2;
                                    angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                    //Угол
                                    IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                        thickness + gapDimToPart * 2 + gapDimToDim * 1.5, -xangle / 2, angleDRadius);
                                    SetDeviation(dtParamA, ParamATolerance);
                                    if (!isCrossSection && ParamB != 0)
                                    {
                                        //Зазора в стыке
                                        IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, thickness, ParamB, thickness, 0, thickness + gapDimToPart * 2, ParamB + 1,
                                        ksLineDimensionOrientationEnum.ksLinDVertical);
                                        SetDeviation(dtPatamB, paramBTolerance);
                                        //Двигаем размер притупления на величину зазора если выбран разрез
                                        ILineDimension ld_ParamB = (ILineDimension)dtParamC;
                                        ld_ParamB.Y3 += ParamB;
                                        ld_ParamB.Update();
                                    }
                                }
                            }

                            //Обычный переход вверху

                            //Обычный переход внизу

                            //Обычный переход вверху и внизу

                            break;
                        default:
                            break;
                    }                    
                    break;
                case ShapePreparedEdgesEnum.С_двумя_симметричными_скосами:
                    switch (transitionType)
                    {
                        case TransitionTypeEnum.Без_перехода:
                            switch (locationPart)
                            {
                                case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -ParamC / 2, 0, ParamC / 2);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, 0, ParamC / 2, -xangle, thickness / 2);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, 0, -ParamC / 2, -xangle, -thickness / 2);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, -xangle, thickness / 2, -(xangle + extraLength), thickness / 2);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, -xangle, -thickness / 2, -(xangle + extraLength), -thickness / 2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = -(xangle + extraLength);
                                        waveLine.Y1 = -thickness / 2;
                                        waveLine.X2 = -(xangle + extraLength);
                                        waveLine.Y2 = thickness / 2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, ParamB, thickness / 2, -xangle, thickness / 2);
                                            DrawLineSegment(lineSegments, ParamB, -thickness / 2, -xangle, -thickness / 2);
                                            DrawLineSegment(lineSegments, ParamB, -thickness / 2, ParamB, thickness / 2);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, -(xangle + extraLength), -thickness / 2, -(xangle + extraLength), thickness / 2, -(xangle + extraLength + gapDimToPartLeft),
                                                0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горзонтальный угла
                                            ILineDimension dtLineHParamA = LineDimension(lineDimensions, 0, ParamC / 2, -xangle, thickness / 2, -xangle / 2, thickness / 2 + gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtLineHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtLineHParamA.Update();
                                            }
                                            //Линейный вертикальный угла
                                            ILineDimension dtLineVParamA = LineDimension(lineDimensions, 0, ParamC / 2, -xangle, thickness / 2, gapDimToPart, (thickness - ParamC) / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            ((IDimensionText)dtLineVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtLineVParamA.Update();
                                            //Линейный вертикальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, 0, -ParamC / 2, 0, ParamC / 2, gapDimToPart, -ParamC,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            //Только для "левых видов"
                                            if (Math.Abs(ParamCTolerance[0]) == Math.Abs(ParamCTolerance[1]))
                                            {
                                                ldParamC.X3 = gapDimToPart * 2;
                                                dtLineVParamA.X3 = gapDimToPart * 2;
                                            }
                                            else
                                            {
                                                ldParamC.X3 = gapDimToPart * 3;
                                                dtLineVParamA.X3 = gapDimToPart * 3;
                                            }
                                            dtLineVParamA.Update();
                                            ldParamC.Update();
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            double r1 = ((thickness - ParamC) / 2 + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                                -xangle / 2, (thickness - ParamC) / 2 + gapDimToPart + gapDimToDim, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, ParamB, thickness / 2, 0, ParamC / 2, ParamB + 1,
                                                    thickness / 2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                ILineDimension ld_ParamB = (ILineDimension)ldParamC;
                                                ld_ParamB.X3 += ParamB;
                                                ld_ParamB.Update();
                                                dtLineVParamA.X3 += ParamB;
                                                dtLineVParamA.Update();
                                            }
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Верх or LocationPart.Право_Низ:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, - ParamC / 2, 0, ParamC / 2);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, 0, ParamC / 2, xangle, thickness / 2);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, 0, -ParamC / 2, xangle, -thickness / 2);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, xangle, thickness / 2, xangle + extraLength, thickness / 2);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, xangle, -thickness / 2, xangle + extraLength, -thickness / 2);                                
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = xangle + extraLength;
                                        waveLine.Y1 = -thickness / 2;
                                        waveLine.X2 = xangle + extraLength;
                                        waveLine.Y2 = thickness / 2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, 0 - ParamB, thickness / 2, xangle, thickness / 2);
                                            DrawLineSegment(lineSegments, 0 - ParamB, -thickness / 2, xangle, -thickness / 2);
                                            DrawLineSegment(lineSegments, 0 - ParamB, -thickness / 2, 0 - ParamB, thickness / 2);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, xangle + extraLength, -thickness / 2, xangle + extraLength, thickness / 2, xangle + extraLength + gapDimToPartLeft,
                                                0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горзонтальный угла
                                            ILineDimension dtLineHParamA = LineDimension(lineDimensions, 0, ParamC / 2, xangle, thickness / 2, xangle / 2, thickness / 2 + gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtLineHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtLineHParamA.Update();
                                            }
                                            //Линейный вертикальный угла
                                            ILineDimension dtLineVParamA = LineDimension(lineDimensions, 0, ParamC / 2, xangle, thickness / 2, -gapDimToPart, (thickness - ParamC) / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            ((IDimensionText)dtLineVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtLineVParamA.Update();
                                            //Линейный вертикальный притупления
                                            IDimensionText dtParamC = (IDimensionText)LineDimension(lineDimensions, 0, -ParamC / 2, 0, ParamC / 2, -gapDimToPart, -ParamC,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation(dtParamC, paramCTolerance);
                                            double r1 = ((thickness - ParamC) / 2 + gapDimToPart) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                                xangle / 2, (thickness - ParamC) / 2 + gapDimToPart + gapDimToDim, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, 0 - ParamB, thickness / 2, 0, ParamC / 2, 0 - ParamB - 1,
                                                    thickness / 2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                ILineDimension ld_ParamB = (ILineDimension)dtParamC;
                                                ld_ParamB.X3 -= ParamB;
                                                ld_ParamB.Update();
                                                dtLineVParamA.X3 -= ParamB;
                                                dtLineVParamA.Update();
                                            }
                                        }
                                    }
                                    break;
                                case LocationPart.Верх_Право or LocationPart.Верх_Лево:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, 0, ParamC / 2, 0);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, 0, thickness / 2, xangle);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, -ParamC / 2, 0, -thickness / 2, xangle);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, thickness / 2, xangle, thickness / 2, xangle + extraLength);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, -thickness / 2, xangle, -thickness / 2, xangle + extraLength);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = -thickness / 2;
                                        waveLine.Y1 = xangle + extraLength;
                                        waveLine.X2 = thickness / 2;
                                        waveLine.Y2 = xangle + extraLength;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, thickness / 2, -ParamB, thickness / 2, xangle);
                                            DrawLineSegment(lineSegments, -thickness / 2, -ParamB, -thickness / 2, xangle);
                                            DrawLineSegment(lineSegments, -thickness / 2, -ParamB, thickness / 2, -ParamB);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, -thickness / 2, xangle + extraLength, thickness / 2, xangle + extraLength, 0,
                                                xangle + extraLength + gapDimToPartLeft, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный угла
                                            ILineDimension dtLineHParamA = LineDimension(lineDimensions, ParamC / 2, 0, thickness / 2, xangle, thickness / 2 + gapDimToPart * 2, xangle / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtLineHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtLineHParamA.Update();
                                            }
                                            //Линейный горизонтальный угла
                                            ILineDimension dtLineVParamA = LineDimension(lineDimensions, ParamC / 2, 0, thickness / 2, xangle, (thickness - ParamC) / 2, -gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            ((IDimensionText)dtLineVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtLineVParamA.Update();
                                            //Линейный горизонтальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, -ParamC / 2, 0, ParamC / 2, 0, -ParamC, -gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            //Только для "верхних видов"
                                            if (Math.Abs(ParamCTolerance[0]) == Math.Abs(ParamCTolerance[1]))
                                            {
                                                ldParamC.Y3 = -gapDimToPart * 2;
                                                dtLineVParamA.Y3 = -gapDimToPart * 2;
                                            }
                                            else
                                            {
                                                ldParamC.Y3 = -gapDimToPart * 3;
                                                dtLineVParamA.Y3 = -gapDimToPart * 3;
                                            }
                                            ldParamC.Update();
                                            dtLineVParamA.Update();
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2 + gapDimToDim, 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                                (thickness - ParamC) / 2 + gapDimToPart * 2 + gapDimToDim, xangle / 2, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                ILineDimension ldPatamB = LineDimension(lineDimensions, -thickness / 2, -ParamB, -ParamC / 2, 0, -(thickness / 2 + gapDimToPart),
                                                    ParamB + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                                SetDeviation((IDimensionText)ldPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                ILineDimension ld_ParamB = ldParamC;
                                                ld_ParamB.Y3 -= ParamB;
                                                ld_ParamB.Update();
                                                dtLineVParamA.Y3 -= ParamB;
                                                dtLineVParamA.Update();
                                            }
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Право or LocationPart.Низ_Лево:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, 0, ParamC / 2, 0);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, ParamC / 2, 0, thickness / 2, -xangle);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, -ParamC / 2, 0, -thickness / 2, -xangle);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, thickness / 2, -xangle, thickness / 2, -(xangle + extraLength));
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, -thickness / 2, -xangle, -thickness / 2, -(xangle + extraLength));
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = -thickness / 2;
                                        waveLine.Y1 = -(xangle + extraLength);
                                        waveLine.X2 = thickness / 2;
                                        waveLine.Y2 = -(xangle + extraLength);
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, thickness / 2, ParamB, thickness / 2, -xangle);
                                            DrawLineSegment(lineSegments, -thickness / 2, ParamB, -thickness / 2, -xangle);
                                            DrawLineSegment(lineSegments, -thickness / 2, ParamB, thickness / 2, ParamB);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горизонтальный толщины
                                            LineDimension(lineDimensions, -thickness / 2, -(xangle + extraLength), thickness / 2, -(xangle + extraLength), 0,
                                                -(xangle + extraLength + gapDimToPartLeft), ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный угла
                                            ILineDimension dtLineHParamA = LineDimension(lineDimensions, ParamC / 2, 0, thickness / 2, -xangle, thickness / 2 + gapDimToPart * 2, -xangle / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtLineHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtLineHParamA.Update();
                                            }
                                            //Линейный горизонтальный угла
                                            ILineDimension dtLineVParamA = LineDimension(lineDimensions, ParamC / 2, 0, thickness / 2, -xangle, (thickness - ParamC) / 2 , gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            ((IDimensionText)dtLineVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtLineVParamA.Update();
                                            //Линейный горизонтальный притупления
                                            IDimensionText dtParamC = (IDimensionText)LineDimension(lineDimensions, -ParamC / 2, 0, ParamC / 2, 0, -ParamC , gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation(dtParamC, paramCTolerance);
                                            double r1 = ((thickness - ParamC) / 2 + gapDimToPart * 2) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((thickness - ParamC) / 2 + gapDimToPart * 2 + gapDimToDim * 1.5, 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2,
                                                (thickness - ParamC) / 2 + gapDimToPart * 2 + gapDimToDim * 1.5, -xangle / 2, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                ILineDimension ldPatamB = LineDimension(lineDimensions, -thickness / 2, ParamB, -ParamC / 2, 0, -(thickness / 2 + gapDimToPart),
                                                    -ParamB - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                                SetDeviation((IDimensionText)ldPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                ILineDimension ld_ParamB = (ILineDimension)dtParamC;
                                                ld_ParamB.Y3 += ParamB;
                                                ld_ParamB.Update();
                                                dtLineVParamA.Y3 += ParamB;
                                                dtLineVParamA.Update();
                                            }
                                        }
                                    }
                                    break;
                            }
                            break;
                        case TransitionTypeEnum.Симметричный:
                            switch (locationPart)
                            {
                                case LocationPart.Лево_Верх or LocationPart.Лево_Низ:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -ParamC / 2, 0, ParamC / 2);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, -xangle, thickness / 2);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle2.X2, -baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, - transitionData.DimL, baseobjAngle2.Y2 + transitionData.DimH);
                                        ILineSegment lsTransition2 = DrawLineSegment(lineSegments, lsTransition1.X1, -lsTransition1.Y1, lsTransition1.X2, -lsTransition1.Y2);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2 - extraLength, lsTransition1.Y2);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, lsTransition1.X2, -lsTransition1.Y2, lsExtra1.X2, -lsTransition1.Y2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsTransition2, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, ParamB, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, ParamB, -lsTransition1.Y2, lsTransition1.X2, -lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, ParamB, -lsTransition1.Y2, ParamB, lsTransition1.Y2);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra1.X2, lsExtra2.Y2, lsExtra1.X2 - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горзонтальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, -xangle / 2, lsExtra1.Y2 + gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горзонтальный перехода
                                            LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, lsTransition2.X2, -lsTransition2.Y2, -transitionData.DimL / 2, lsTransition2.Y2 - gapDimToPart * 2,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный вертикальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, gapDimToPart * 2, (thickness - ParamC) / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                dtVParamA.X3 = gapDimToPart * 3;
                                                dtVParamA.Update();
                                            }
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, ParamB, lsTransition1.Y2, baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    ParamB + 1, dtHParamA.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.X3 += ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный вертикальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, dtVParamA.X3, -ParamC / 2 - 1,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                dtVParamA.X3 + gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                ldThicknessJoint.X3, (thickness + transitionData.DimH) / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            LineDimension(lineDimensions, baseobjAngle3.X2, baseobjAngle3.Y2, lsTransition2.X2, lsTransition2.Y2,
                                                ldThicknessJoint.X3, -(thickness + transitionData.DimH) / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.Y3 - ParamC / 2 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.Y3 - ParamC / 2 + gapDimToDim / 2), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2, -xangle / 2, 10, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);              
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Верх or LocationPart.Право_Низ:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -ParamC / 2, 0, ParamC / 2);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, xangle, thickness / 2);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle2.X2, -baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, transitionData.DimL, baseobjAngle2.Y2 + transitionData.DimH);
                                        ILineSegment lsTransition2 = DrawLineSegment(lineSegments, lsTransition1.X1, -lsTransition1.Y1, lsTransition1.X2, -lsTransition1.Y2);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2 + extraLength, lsTransition1.Y2);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, lsTransition1.X2, -lsTransition1.Y2, lsExtra1.X2, -lsTransition1.Y2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsTransition2, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, -ParamB, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, -ParamB, -lsTransition1.Y2, lsTransition1.X2, -lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, -ParamB, -lsTransition1.Y2, -ParamB, lsTransition1.Y2);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra1.X2, lsExtra2.Y2, lsExtra1.X2 + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горзонтальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, xangle / 2, lsExtra1.Y2 + gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горзонтальный перехода
                                            LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, lsTransition2.X2, -lsTransition2.Y2, transitionData.DimL / 2, lsTransition2.Y2 - gapDimToPart * 2,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный вертикальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, -gapDimToPart, (thickness - ParamC) / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, -ParamB, lsTransition1.Y2, baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    -ParamB - 1, dtHParamA.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.X3 -= ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный вертикальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, dtVParamA.X3, -ParamC / 2 - 1,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                dtVParamA.X3 - gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                ldThicknessJoint.X3 = dtVParamA.X3 - gapDimToDim * 1.5;
                                                ldThicknessJoint.Update();
                                            }
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                ldThicknessJoint.X3, (thickness + transitionData.DimH) / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            LineDimension(lineDimensions, baseobjAngle3.X2, baseobjAngle3.Y2, lsTransition2.X2, lsTransition2.Y2,
                                                ldThicknessJoint.X3, -(thickness + transitionData.DimH) / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.Y3 - ParamC / 2 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.Y3 - ParamC / 2 + gapDimToDim / 2), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2, xangle / 2, 10, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                        }
                                    }
                                    break;
                                case LocationPart.Верх_Право or LocationPart.Верх_Лево:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, 0, ParamC / 2, 0);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, thickness / 2, xangle);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, -baseobjAngle2.X2, baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle2.X2 + transitionData.DimH, transitionData.DimL);
                                        ILineSegment lsTransition2 = DrawLineSegment(lineSegments, -lsTransition1.X1, lsTransition1.Y1, -lsTransition1.X2, lsTransition1.Y2);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2 + extraLength);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, -lsTransition1.X2, lsTransition1.Y2, -lsTransition1.X2, lsExtra1.Y2 );
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsTransition2, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, lsTransition1.X2, -ParamB, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, -lsTransition1.X2, -ParamB , -lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, -lsTransition1.X2, -ParamB , lsTransition1.X2, -ParamB);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горзонтальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra2.X2, lsExtra1.Y2, 0, lsExtra1.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, lsExtra1.X2 + gapDimToPart * 2, xangle / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, -lsTransition2.X2, lsTransition2.Y2, lsTransition2.X2 - gapDimToPart, transitionData.DimL / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный горзонтальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, (thickness - ParamC) / 2, -gapDimToPart * 2,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                dtVParamA.Y3 = -gapDimToPart * 3;
                                                dtVParamA.Update();
                                            }
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, -lsTransition1.X2, -ParamB, -baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    lsTransition2.X2 - gapDimToPart, -ParamB - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.Y3 -= ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный горзонтальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, -ParamC / 2 - 1, dtVParamA.Y3,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный горзонтальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                0, dtVParamA.Y3 - gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);                               
                                            //Линейный горзонтальный перехода
                                            LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                (thickness + transitionData.DimH) / 2, ldThicknessJoint.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            LineDimension(lineDimensions, baseobjAngle3.X2, baseobjAngle3.Y2, lsTransition2.X2, lsTransition2.Y2,
                                                -(thickness + transitionData.DimH) / 2, ldThicknessJoint.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.X3 - ParamC / 2 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.X3 - ParamC / 2 + gapDimToDim / 2), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2, 10, xangle / 2, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Право or LocationPart.Низ_Лево:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, 0, ParamC / 2, 0);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, thickness / 2, -xangle);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, -baseobjAngle2.X2, baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle2.X2 + transitionData.DimH, -transitionData.DimL);
                                        ILineSegment lsTransition2 = DrawLineSegment(lineSegments, -lsTransition1.X1, lsTransition1.Y1, -lsTransition1.X2, lsTransition1.Y2);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2 - extraLength);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, -lsTransition1.X2, lsTransition1.Y2, -lsTransition1.X2, lsExtra1.Y2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsTransition2, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, lsTransition1.X2, ParamB, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, -lsTransition1.X2, ParamB, -lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, -lsTransition1.X2, ParamB, lsTransition1.X2, ParamB);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горзонтальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra2.X2, lsExtra1.Y2, 0, lsExtra1.Y2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, lsExtra1.X2 + gapDimToPart * 2, -xangle / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, -lsTransition2.X2, lsTransition2.Y2, lsTransition2.X2 - gapDimToPart, -transitionData.DimL / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный горзонтальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, (thickness - ParamC) / 2, gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, -lsTransition1.X2, ParamB, -baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    lsTransition2.X2 - gapDimToPart, ParamB + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.Y3 += ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный горзонтальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, -ParamC / 2 - 1, dtVParamA.Y3,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный горзонтальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                0, dtVParamA.Y3 + gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                ldThicknessJoint.Y3 = dtVParamA.Y3 + gapDimToDim * 1.5;
                                                ldThicknessJoint.Update();
                                            }
                                            //Линейный горзонтальный перехода
                                            LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                (thickness + transitionData.DimH) / 2, ldThicknessJoint.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            LineDimension(lineDimensions, baseobjAngle3.X2, baseobjAngle3.Y2, lsTransition2.X2, lsTransition2.Y2,
                                                -(thickness + transitionData.DimH) / 2, ldThicknessJoint.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.X3 - ParamC / 2 + gapDimToDim / 2) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.X3 - ParamC / 2 + gapDimToDim / 2), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2, 10, -xangle / 2, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                        }
                                    }
                                    break;
                            }
                            break;
                        case TransitionTypeEnum.Вверх:
                            switch (locationPart)
                            {
                                case LocationPart.Лево_Верх:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -ParamC / 2, 0, ParamC / 2);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, -xangle, thickness / 2);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle2.X2, -baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, -transitionData.DimL, baseobjAngle2.Y2 + transitionData.DimH);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2 - extraLength, lsTransition1.Y2);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, baseobjAngle3.X2, baseobjAngle3.Y2, lsExtra1.X2, baseobjAngle3.Y2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, ParamB, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, ParamB, baseobjAngle3.Y2, baseobjAngle3.X2, baseobjAngle3.Y2);
                                            DrawLineSegment(lineSegments, ParamB, baseobjAngle3.Y2, ParamB, lsTransition1.Y2);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra1.X2, lsExtra2.Y2, lsExtra1.X2 - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle3.X1, baseobjAngle3.Y1, baseobjAngle3.X2, baseobjAngle3.Y2, -xangle / 2, baseobjAngle3.Y2 - gapDimToPart * 2,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горзонтальный перехода
                                            ILineDimension ldTransition = LineDimension(lineDimensions, baseobjAngle1.X2, baseobjAngle1.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                -transitionData.DimL / 2, lsTransition1.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный вертикальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, gapDimToPart * 2, (thickness - ParamC) / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                dtVParamA.X3 = gapDimToPart * 3;
                                                dtVParamA.Update();
                                            }
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, ParamB, lsTransition1.Y2, baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    ParamB + 1, ldTransition.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.X3 += ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный вертикальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, dtVParamA.X3, -ParamC / 2 - 1,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                dtVParamA.X3 + gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                ldThicknessJoint.X3, (thickness + transitionData.DimH) / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.Y3 + ParamC / 2 - gapDimToDim * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.Y3 + ParamC / 2 - gapDimToDim * 1.5), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle3, baseobjAngle1, -xangle / 2, -10, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                        }
                                    }
                                    break;
                                case LocationPart.Лево_Низ:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -ParamC / 2, 0, ParamC / 2);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, -xangle, thickness / 2);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle2.X2, -baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle3.X2, baseobjAngle3.Y2, -transitionData.DimL, baseobjAngle3.Y2 - transitionData.DimH);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2 - extraLength, lsTransition1.Y2);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, lsExtra1.X2, baseobjAngle2.Y2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, ParamB, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, ParamB, baseobjAngle2.Y2, baseobjAngle2.X2, baseobjAngle2.Y2);
                                            DrawLineSegment(lineSegments, ParamB, baseobjAngle2.Y2, ParamB, lsTransition1.Y2);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra1.X2, lsExtra2.Y2, lsExtra1.X2 - gapDimToPart, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горизонтальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2,
                                                -xangle / 2, baseobjAngle2.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горизонтальный перехода
                                            ILineDimension ldTransition = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, lsTransition1.X2, lsTransition1.Y2,
                                                -transitionData.DimL / 2, lsTransition1.Y2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный вертикальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, gapDimToPart * 2, (thickness - ParamC) / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                dtVParamA.X3 = gapDimToPart * 3;
                                                dtVParamA.Update();
                                            }
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, ParamB, baseobjAngle2.Y2, baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    ParamB + 1, dtHParamA.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.X3 += ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный вертикальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, dtVParamA.X3, -ParamC / 2 - 1,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                dtVParamA.X3 + gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, baseobjAngle3.X2, baseobjAngle3.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                ldThicknessJoint.X3, -(thickness + transitionData.DimH) / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.Y3 - ParamC / 2 + gapDimToDim) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.Y3 - ParamC / 2 + gapDimToDim), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2, -xangle / 2, 10, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Верх:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -ParamC / 2, 0, ParamC / 2);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, xangle, thickness / 2);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle2.X2, -baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, transitionData.DimL, baseobjAngle2.Y2 + transitionData.DimH);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2 + extraLength, lsTransition1.Y2);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, baseobjAngle3.X2, baseobjAngle3.Y2, lsExtra1.X2, baseobjAngle3.Y2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, -ParamB, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, -ParamB, baseobjAngle3.Y2, baseobjAngle3.X2, baseobjAngle3.Y2);
                                            DrawLineSegment(lineSegments, -ParamB, baseobjAngle3.Y2, -ParamB, lsTransition1.Y2);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra1.X2, lsExtra2.Y2, lsExtra1.X2 + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горзонтальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle3.X1, baseobjAngle3.Y1, baseobjAngle3.X2, baseobjAngle3.Y2, xangle / 2, baseobjAngle3.Y2 - gapDimToPart * 2,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горзонтальный перехода
                                            ILineDimension ldTransition = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, lsTransition1.X2, lsTransition1.Y2, transitionData.DimL / 2, lsTransition1.Y2 + gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный вертикальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, -gapDimToPart, (thickness - ParamC) / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, -ParamB, lsTransition1.Y2, baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    -ParamB - 1, ldTransition.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.X3 -= ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный вертикальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, dtVParamA.X3, -ParamC / 2 - 1,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                dtVParamA.X3 - gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                ldThicknessJoint.X3 = dtVParamA.X3 - gapDimToDim * 1.5;
                                                ldThicknessJoint.Update();
                                            }
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                ldThicknessJoint.X3, (thickness + transitionData.DimH) / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.Y3 + ParamC / 2 - gapDimToDim * 1.5) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.Y3 + ParamC / 2 - gapDimToDim * 1.5), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle3, xangle / 2, -10, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                        }
                                    }
                                    break;
                                case LocationPart.Право_Низ:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, 0, -ParamC / 2, 0, ParamC / 2);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, xangle, thickness / 2);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle2.X2, -baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle3.X2, baseobjAngle3.Y2, transitionData.DimL, baseobjAngle3.Y2 - transitionData.DimH);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2 + extraLength, lsTransition1.Y2);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, lsExtra1.X2, baseobjAngle2.Y2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, -ParamB, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, -ParamB, baseobjAngle2.Y2, baseobjAngle2.X2, baseobjAngle2.Y2);
                                            DrawLineSegment(lineSegments, -ParamB, baseobjAngle2.Y2, -ParamB, lsTransition1.Y2);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный вертикальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra1.X2, lsExtra2.Y2, lsExtra1.X2 + gapDimToPart * 2, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный горзонтальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, xangle / 2, baseobjAngle2.Y2 + gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горзонтальный перехода
                                            ILineDimension ldTransition = LineDimension(lineDimensions, baseobjAngle3.X1, baseobjAngle3.Y1, lsTransition1.X2, lsTransition1.Y2,
                                                transitionData.DimL / 2, lsTransition1.Y2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный вертикальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, -gapDimToPart, (thickness - ParamC) / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, -ParamB, baseobjAngle2.Y2, baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    -ParamB - 1, dtHParamA.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.X3 -= ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный вертикальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, dtVParamA.X3, -ParamC / 2 - 1,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный вертикальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                dtVParamA.X3 - gapDimToDim, 0, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                ldThicknessJoint.X3 = dtVParamA.X3 - gapDimToDim * 1.5;
                                                ldThicknessJoint.Update();
                                            }
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, baseobjAngle3.X2, baseobjAngle3.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                ldThicknessJoint.X3, -(thickness + transitionData.DimH) / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.Y3 - ParamC / 2 + gapDimToDim) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.Y3 - ParamC / 2 + gapDimToDim), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle2, xangle / 2, 10, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                        }
                                    }
                                    break;




                                case LocationPart.Верх_Право or LocationPart.Верх_Лево:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, 0, ParamC / 2, 0);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, thickness / 2, xangle);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, -baseobjAngle2.X2, baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle2.X2 + transitionData.DimH, transitionData.DimL); 
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2 + extraLength);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, baseobjAngle3.X2, baseobjAngle3.Y2, baseobjAngle3
                                            .X2, lsExtra1.Y2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, lsTransition1.X2, -ParamB, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, baseobjAngle3.X2, -ParamB, baseobjAngle3.X2, baseobjAngle3.Y2);
                                            DrawLineSegment(lineSegments, lsExtra2.X2, -ParamB, lsExtra1.X2, -ParamB);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горзонтальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra2.X2, lsExtra1.Y2, 0, lsExtra1.Y2 + gapDimToPart, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle3.X1, baseobjAngle3.Y1, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                baseobjAngle3.X2 - gapDimToPart, xangle / 2, ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2 + gapDimToPart * 2, transitionData.DimL / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный горзонтальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, (thickness - ParamC) / 2, -gapDimToPart * 2,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                dtVParamA.Y3 = -gapDimToPart * 3;
                                                dtVParamA.Update();
                                            }
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, lsExtra2.X2, -ParamB, -baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    dtHParamA.X3, -ParamB - 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.Y3 -= ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный горзонтальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, -ParamC / 2 - 1, dtVParamA.Y3,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный горзонтальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                0, dtVParamA.Y3 - gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный горзонтальный перехода
                                            LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                (thickness + transitionData.DimH) / 2, ldThicknessJoint.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.X3 + ParamC / 2 - gapDimToDim) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.X3 + ParamC / 2 - gapDimToDim), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle3, -10, xangle / 2, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                        }
                                    }
                                    break;
                                case LocationPart.Низ_Право or LocationPart.Низ_Лево:
                                    {
                                        //Размер скоса
                                        double xangle = (thickness - ParamC) / 2 * Math.Tan(ParamA * Math.PI / 180);
                                        extraLength += xangle;
                                        extraLength = extraLength < 1 ? 1 : extraLength;
                                        //Чертим графику
                                        //Создаём основу разделки
                                        //Притупление
                                        ILineSegment baseobjAngle1 = DrawLineSegment(lineSegments, -ParamC / 2, 0, ParamC / 2, 0);
                                        //Угла
                                        ILineSegment baseobjAngle2 = DrawLineSegment(lineSegments, baseobjAngle1.X2, baseobjAngle1.Y2, thickness / 2, -xangle);
                                        ILineSegment baseobjAngle3 = DrawLineSegment(lineSegments, baseobjAngle1.X1, baseobjAngle1.Y1, -baseobjAngle2.X2, baseobjAngle2.Y2);
                                        //Переход
                                        ILineSegment lsTransition1 = DrawLineSegment(lineSegments, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle2.X2 + transitionData.DimH, -transitionData.DimL);
                                        //От угла к краю детали
                                        ILineSegment lsExtra1 = DrawLineSegment(lineSegments, lsTransition1.X2, lsTransition1.Y2, lsTransition1.X2, lsTransition1.Y2 - extraLength);
                                        ILineSegment lsExtra2 = DrawLineSegment(lineSegments, baseobjAngle3.X2, baseobjAngle3.Y2, baseobjAngle3.X2, lsExtra1.Y2);
                                        //Волнистая линия
                                        IWaveLines waveLines = symbols2DContainer.WaveLines;
                                        IWaveLine waveLine = waveLines.Add();
                                        waveLine.X1 = lsExtra1.X2;
                                        waveLine.Y1 = lsExtra1.Y2;
                                        waveLine.X2 = lsExtra2.X2;
                                        waveLine.Y2 = lsExtra2.Y2;
                                        waveLine.Style = (int)ksCurveStyleEnum.ksCSBrokenLine;
                                        waveLine.Update();
                                        if (isHatches)
                                        {
                                            //Создаём контур для штриховки. При создании на прямую из линий штриховка вызывает ошибку
                                            IDrawingContours drawingContours = drawingContainer.DrawingContours;
                                            IDrawingContour drawingContour = drawingContours.Add();
                                            IContour contour = (IContour)drawingContour;
                                            //Добавляем в контур элементы которые ограничивают штриховку
                                            contour.CopySegments(baseobjAngle1, false);
                                            contour.CopySegments(baseobjAngle2, false);
                                            contour.CopySegments(baseobjAngle3, false);
                                            contour.CopySegments(lsTransition1, false);
                                            contour.CopySegments(lsExtra2, false);
                                            contour.CopySegments(lsExtra1, false);
                                            contour.CopySegments(waveLine, false);
                                            drawingContour.Update();
                                            //Штриховка
                                            IHatches hatches = drawingContainer.Hatches;
                                            IHatch hatch = hatches.Add();
                                            IBoundariesObject boundariesObject = (IBoundariesObject)hatch;
                                            boundariesObject.AddBoundaries(drawingContour, true);
                                            hatch.Update();
                                        }
                                        //Если разрез
                                        if (!isCrossSection)
                                        {
                                            DrawLineSegment(lineSegments, lsTransition1.X2, ParamB, lsTransition1.X2, lsTransition1.Y2);
                                            DrawLineSegment(lineSegments, baseobjAngle3.X2, ParamB, baseobjAngle3.X2, baseobjAngle3.Y2);
                                            DrawLineSegment(lineSegments, baseobjAngle3.X2, ParamB, lsTransition1.X2, ParamB);
                                        }
                                        //Чертим размеры
                                        if (drawDimensions)
                                        {
                                            //Линейный горзонтальный толщины
                                            LineDimension(lineDimensions, lsExtra1.X2, lsExtra1.Y2, lsExtra2.X2, lsExtra1.Y2, 0, lsExtra1.Y2 - gapDimToPart * 2, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Линейный вертикальный угла
                                            ILineDimension dtHParamA = LineDimension(lineDimensions, baseobjAngle3.X1, baseobjAngle3.Y1, baseobjAngle3.X2, baseobjAngle3.Y2, baseobjAngle3.X2 - gapDimToPart, -xangle / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Линейный вертикальный перехода
                                            LineDimension(lineDimensions, lsTransition1.X2, lsTransition1.Y2, baseobjAngle2.X1, baseobjAngle2.Y1, lsTransition1.X2 + gapDimToPart * 2, -transitionData.DimL / 2,
                                                ksLineDimensionOrientationEnum.ksLinDVertical);
                                            //Если угол равен 45 то оба размера угла делаем с десятыми
                                            if (ParamA == 45)
                                            {
                                                ((IDimensionText)dtHParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                                dtHParamA.Update();
                                            }
                                            //Линейный горзонтальный угла
                                            ILineDimension dtVParamA = LineDimension(lineDimensions, baseobjAngle2.X1, baseobjAngle2.Y1, baseobjAngle2.X2, baseobjAngle2.Y2, (thickness - ParamC) / 2, gapDimToPart,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            ((IDimensionText)dtVParamA).Accuracy = ksAccuracyEnum.ksAccuracy1;
                                            dtVParamA.Update();
                                            //Если разрез
                                            if (!isCrossSection && ParamB != 0)
                                            {
                                                //Зазора в стыке
                                                IDimensionText dtPatamB = (IDimensionText)LineDimension(lineDimensions, baseobjAngle3.X2, ParamB, -baseobjAngle1.X2, baseobjAngle1.Y2,
                                                    dtHParamA.X3, ParamB + 1, ksLineDimensionOrientationEnum.ksLinDVertical);
                                                SetDeviation(dtPatamB, paramBTolerance);
                                                //Двигаем размер притупления и линейный угла на величину зазора если выбран разрез
                                                dtVParamA.Y3 += ParamB;
                                                dtVParamA.Update();
                                            }
                                            //Линейный горзонтальный притупления
                                            ILineDimension ldParamC = LineDimension(lineDimensions, baseobjAngle1.X1, baseobjAngle1.Y1, baseobjAngle1.X2, baseobjAngle1.Y2, -ParamC / 2 - 1, dtVParamA.Y3,
                                                ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            SetDeviation((IDimensionText)ldParamC, paramCTolerance);
                                            //Линейный горзонтальный толщины в стыке
                                            ILineDimension ldThicknessJoint = LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, baseobjAngle3.X2, baseobjAngle3.Y2,
                                                0, dtVParamA.Y3 + gapDimToDim, ksLineDimensionOrientationEnum.ksLinDHorizontal);
                                            //Если верхний и нижний допуск на притупление одинаков то расстояние до детали меньше чем при разных допусках
                                            if (Math.Abs(ParamCTolerance[0]) != Math.Abs(ParamCTolerance[1]))
                                            {
                                                ldThicknessJoint.Y3 = dtVParamA.Y3 + gapDimToDim * 1.5;
                                                ldThicknessJoint.Update();
                                            }
                                            //Линейный горзонтальный перехода
                                            LineDimension(lineDimensions, baseobjAngle2.X2, baseobjAngle2.Y2, lsTransition1.X2, lsTransition1.Y2,
                                                (thickness + transitionData.DimH) / 2, ldThicknessJoint.Y3, ksLineDimensionOrientationEnum.ksLinDHorizontal);                                            
                                            //Расчёты для угла
                                            double r1 = (dtHParamA.X3 + ParamC / 2 - gapDimToDim * 2) / Math.Cos(ParamA * Math.PI / 180);
                                            double r2 = Math.Sqrt(Math.Pow((dtHParamA.X3 + ParamC / 2 - gapDimToDim * 2), 2) + Math.Pow(xangle / 2, 2));
                                            double angleDRadius = r1 > r2 ? r1 : r2;
                                            angleDRadius *= view.Scale;//Радиус будто бы должен задаваться в масштабе 1:1
                                            //Угол
                                            IDimensionText dtParamA = (IDimensionText)AngleDimension(angleDimensions, baseobjAngle1, baseobjAngle3, -10, -xangle / 2, angleDRadius);
                                            SetDeviation(dtParamA, ParamATolerance);
                                        }
                                    }
                                    break;
                            }
                            break;
                        case TransitionTypeEnum.Вниз:
                            break;
                        case TransitionTypeEnum.Занижение:
                            break;
                    }
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
        /// Создание углового размера по опорным линиям
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
