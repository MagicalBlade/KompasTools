using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
using Kompas6API5;
using Kompas6Constants;
using KompasAPI7;
using KompasTools.Classes.Sundry.Welding;
using KompasTools.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;
using static KompasTools.Classes.Sundry.Welding.WeldEnum;

namespace KompasTools.ViewModels.Sundry
{
    public partial class Welding : ObservableObject
    {
        /// <summary>
        /// Толщина
        /// </summary>
        [ObservableProperty]        
        private string? _thickness; //TODO подумать про конвертацию в double
        partial void OnThicknessChanged(string? value)
        {
            Filter();
        }

        [ObservableProperty]
        private WeldData[]? _weldDates;
        [ObservableProperty]
        private WeldData? _selectWeldDates;

        private WeldData[]? OrigWeldDates;

        [ObservableProperty]
        private string[]? _weldGOSTs;
        [ObservableProperty]
        private string? _selectGOST = null;
        partial void OnSelectGOSTChanged(string? value)
        {
            SelectNameWeldJoints = null;
            SelectWeldingMethod = null;
            Filter();
            NameWeldJoints = WeldDates?.Select(n => n.NameWeldJoint).Distinct().ToArray();
        }

        [ObservableProperty]
        private string[]? _nameWeldJoints;
        [ObservableProperty]
        private string? _selectNameWeldJoints = null;
        partial void OnSelectNameWeldJointsChanged(string? value)
        {
            SelectWeldingMethod = null;
            Filter();
            WeldingMethod = WeldDates?.Select(n => n.WeldingMethod).Distinct().ToArray();
        }

        [ObservableProperty]
        private WeldingMethodEnum[]? _weldingMethod;
        [ObservableProperty]
        private WeldingMethodEnum? _selectWeldingMethod = null;
        partial void OnSelectWeldingMethodChanged(WeldingMethodEnum? value)
        {
            Filter();
        }
        public void Filter()
        {
            if (OrigWeldDates != null)
            {
                if (SelectGOST != null) WeldDates = OrigWeldDates.Where(n => n.NameGost == SelectGOST).ToArray();
                if (SelectNameWeldJoints != null && WeldDates != null) WeldDates = WeldDates.Where(n => n.NameWeldJoint == SelectNameWeldJoints).ToArray();
                if (SelectWeldingMethod!= null && WeldDates != null) WeldDates = WeldDates.Where(n => n.WeldingMethod == SelectWeldingMethod).ToArray();
                if (Thickness != "" && Thickness != null && WeldDates != null) WeldDates = WeldDates.Where(n => n.CheckThickness(Convert.ToDouble(Thickness))).ToArray();                
            }
        }


        [ObservableProperty]
        private LocationPart _isLocationPart = LocationPart.Право_Верх;
        /// <summary>
        /// Номер детали
        /// </summary>
        [ObservableProperty]
        private bool _numberPart = true;
        /// <summary>
        /// Тип перехода
        /// </summary>
        [ObservableProperty]
        private TransitionTypeEnum[] _transitionTypes = new TransitionTypeEnum[]
        {
            TransitionTypeEnum.Без_перехода,
            TransitionTypeEnum.Обычный,
            TransitionTypeEnum.Занижение
        };
        [ObservableProperty]
        private TransitionTypeEnum _selectTransitionTypesFirstUP = TransitionTypeEnum.Без_перехода;
        [ObservableProperty]
        private TransitionTypeEnum _selectTransitionTypesFirstBottom = TransitionTypeEnum.Без_перехода;
        [ObservableProperty]
        private TransitionTypeEnum _selectTransitionTypesSecondUP = TransitionTypeEnum.Без_перехода;
        [ObservableProperty]
        private TransitionTypeEnum _selectTransitionTypesSecondBottom = TransitionTypeEnum.Без_перехода;

        [ObservableProperty]
        private bool _isDrawingDimensions = true;



        [RelayCommand]
        public void LoadedTab()
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Sundry\\Welding\\Параметры сварки.csv");
            string text;
            int columnCount = 29;
            List<WeldData> data = new List<WeldData>();
            using (StreamReader reader = new (path))
            {
                text = reader.ReadToEnd();
            }
            string[] temp = text.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            foreach (string row in temp)
            {
                string[] cells = row.Split("\t");
                if(cells.Length != columnCount) continue;
                WeldData weldData = new()
                {
                    NameGost = cells[0],
                    NameWeldJoint = cells[1]
                };
                if (Enum.TryParse(cells[2], out ConnectionTypeEnum connectionType) && connectionType != ConnectionTypeEnum.НЕ_УКАЗАНО)
                {
                    weldData.ConnectionType = connectionType;
                }
                else continue;
                if (Enum.TryParse(cells[3], out ShapePreparedEdgesEnum shapePreparedEdges1) && shapePreparedEdges1 != ShapePreparedEdgesEnum.НЕ_УКАЗАНО)
                {
                    weldData.ShapePreparedEdgesPart1 = shapePreparedEdges1;
                }
                else continue;
                if (Enum.TryParse(cells[4], out ShapePreparedEdgesEnum shapePreparedEdges2) && shapePreparedEdges2 != ShapePreparedEdgesEnum.НЕ_УКАЗАНО)
                {
                    weldData.ShapePreparedEdgesPart2 = shapePreparedEdges2;
                }
                else continue;
                if (Enum.TryParse(cells[5], out WeldingMethodEnum weldingMethod) && weldingMethod != WeldingMethodEnum.НЕ_УКАЗАНО)
                {
                    weldData.WeldingMethod = weldingMethod;
                }
                else continue;
                if (Double.TryParse(cells[6], out double thicknessMin))
                {
                    weldData.ThicknessMin = thicknessMin;
                }
                else continue;
                if (Double.TryParse(cells[7], out double thicknessMax))
                {
                    weldData.ThicknessMax = thicknessMax;
                }
                else continue;
                if (Double.TryParse(cells[8], out double paramC))
                {
                    weldData.ParamC = paramC;
                }
                else continue;
                string[] paramCTolerance = cells[9].Split(';');
                switch (paramCTolerance.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        {
                            if (Double.TryParse(paramCTolerance[0], out double paramCTolerance1))
                            {
                                weldData.ParamCTolerance[0] = paramCTolerance1; //TODO проверить сработает ли если в размер передать верхний и нижний одинаковый допуск
                                weldData.ParamCTolerance[1] = -paramCTolerance1;
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Double.TryParse(paramCTolerance[0], out double paramCTolerance1))
                            {
                                weldData.ParamCTolerance[0] = paramCTolerance1;
                            }
                            if (Double.TryParse(paramCTolerance[1], out double paramCTolerance2))
                            {
                                weldData.ParamCTolerance[1] = paramCTolerance2;
                            } 
                        }
                        break;
                    default:
                        continue;
                }
                if (Double.TryParse(cells[10], out double paramA))
                {
                    weldData.ParamA = paramA;
                }
                else continue;
                string[] paramATolerance = cells[11].Split(';');
                switch (paramATolerance.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        {
                            if (Double.TryParse(paramATolerance[0], out double paramATolerance1))
                            {
                                weldData.ParamATolerance[0] = paramATolerance1; //TODO проверить сработает ли если в размер передать верхний и нижний одинаковый допуск
                                weldData.ParamATolerance[1] = -paramATolerance1;
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Double.TryParse(paramATolerance[0], out double paramATolerance1))
                            {
                                weldData.ParamATolerance[0] = paramATolerance1;
                            }
                            if (Double.TryParse(paramATolerance[1], out double paramATolerance2))
                            {
                                weldData.ParamATolerance[1] = paramATolerance2;
                            }
                        }
                        break;
                    default:
                        continue;
                }
                if (Double.TryParse(cells[12], out double paramH))
                {
                    weldData.ParamH = paramH;
                }
                else continue;
                if (Double.TryParse(cells[13], out double paramB))
                {
                    weldData.ParamB = paramB;
                }
                else continue;
                string[] paramBTolerance = cells[14].Split(';');
                switch (paramBTolerance.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        {
                            if (Double.TryParse(paramBTolerance[0], out double paramBTolerance1))
                            {
                                weldData.ParamBTolerance[0] = paramBTolerance1; //TODO проверить сработает ли если в размер передать верхний и нижний одинаковый допуск
                                weldData.ParamBTolerance[1] = -paramBTolerance1;
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Double.TryParse(paramBTolerance[0], out double paramBTolerance1))
                            {
                                weldData.ParamBTolerance[0] = paramBTolerance1;
                            }
                            if (Double.TryParse(paramBTolerance[1], out double paramBTolerance2))
                            {
                                weldData.ParamBTolerance[1] = paramBTolerance2;
                            }
                        }
                        break;
                    default:
                        continue;
                }
                if (Double.TryParse(cells[15], out double paramN))
                {
                    weldData.ParamN = paramN;
                }
                else continue;
                if (Double.TryParse(cells[16], out double minUserAngle))
                {
                    weldData.MinUserAngle = minUserAngle;
                }
                else continue;
                if (Double.TryParse(cells[17], out double maxUserAngle))
                {
                    weldData.MaxUserAngle = maxUserAngle;
                }
                else continue;
                if (Enum.TryParse(cells[18], out WeldTypeEnum weldType) && weldType != WeldTypeEnum.НЕ_УКАЗАНО)
                {
                    weldData.WeldType = weldType;
                }
                else continue;
                if (Enum.TryParse(cells[19], out NatureSeamPerformedEnum natureSeamPerformed) && natureSeamPerformed != NatureSeamPerformedEnum.НЕ_УКАЗАНО)
                {
                    weldData.NatureSeamPerformed = natureSeamPerformed;
                }
                else continue;
                if (Double.TryParse(cells[20], out double paramE))
                {
                    weldData.ParamE = paramE;
                }
                else continue;
                string[] paramETolerance = cells[21].Split(';');
                switch (paramETolerance.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        {
                            if (Double.TryParse(paramETolerance[0], out double paramETolerance1))
                            {
                                weldData.ParamETolerance[0] = paramETolerance1; //TODO проверить сработает ли если в размер передать верхний и нижний одинаковый допуск
                                weldData.ParamETolerance[1] = -paramETolerance1;
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Double.TryParse(paramETolerance[0], out double paramETolerance1))
                            {
                                weldData.ParamETolerance[0] = paramETolerance1;
                            }
                            if (Double.TryParse(paramETolerance[1], out double paramETolerance2))
                            {
                                weldData.ParamETolerance[1] = paramETolerance2;
                            }
                        }
                        break;
                    default:
                        continue;
                }
                if (Double.TryParse(cells[22], out double paramE1))
                {
                    weldData.ParamE1 = paramE1;
                }
                else continue;
                string[] paramE1Tolerance = cells[23].Split(';');
                switch (paramE1Tolerance.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        {
                            if (Double.TryParse(paramE1Tolerance[0], out double paramE1Tolerance1))
                            {
                                weldData.ParamE1Tolerance[0] = paramE1Tolerance1; //TODO проверить сработает ли если в размер передать верхний и нижний одинаковый допуск
                                weldData.ParamE1Tolerance[1] = -paramE1Tolerance1;
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Double.TryParse(paramE1Tolerance[0], out double paramE1Tolerance1))
                            {
                                weldData.ParamE1Tolerance[0] = paramE1Tolerance1;
                            }
                            if (Double.TryParse(paramE1Tolerance[1], out double paramE1Tolerance2))
                            {
                                weldData.ParamE1Tolerance[1] = paramE1Tolerance2;
                            }
                        }
                        break;
                    default:
                        continue;
                }
                if (Double.TryParse(cells[24], out double paramG))
                {
                    weldData.ParamG = paramG;
                }
                else continue;
                string[] paramGTolerance = cells[25].Split(';');
                switch (paramGTolerance.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        {
                            if (Double.TryParse(paramGTolerance[0], out double paramGTolerance1))
                            {
                                weldData.ParamGTolerance[0] = paramGTolerance1; //TODO проверить сработает ли если в размер передать верхний и нижний одинаковый допуск
                                weldData.ParamGTolerance[1] = -paramGTolerance1;
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Double.TryParse(paramGTolerance[0], out double paramGTolerance1))
                            {
                                weldData.ParamGTolerance[0] = paramGTolerance1;
                            }
                            if (Double.TryParse(paramGTolerance[1], out double paramGTolerance2))
                            {
                                weldData.ParamGTolerance[1] = paramGTolerance2;
                            }
                        }
                        break;
                    default:
                        continue;
                }
                if (Double.TryParse(cells[26], out double paramG1))
                {
                    weldData.ParamG1 = paramG1;
                }
                else continue;
                string[] paramG1Tolerance = cells[27].Split(';');
                switch (paramG1Tolerance.Length)
                {
                    case 0:
                        continue;
                    case 1:
                        {
                            if (Double.TryParse(paramG1Tolerance[0], out double paramG1Tolerance1))
                            {
                                weldData.ParamG1Tolerance[0] = paramG1Tolerance1; //TODO проверить сработает ли если в размер передать верхний и нижний одинаковый допуск
                                weldData.ParamG1Tolerance[1] = -paramG1Tolerance1;
                            }
                        }
                        break;
                    case 2:
                        {
                            if (Double.TryParse(paramG1Tolerance[0], out double paramG1Tolerance1))
                            {
                                weldData.ParamG1Tolerance[0] = paramG1Tolerance1;
                            }
                            if (Double.TryParse(paramG1Tolerance[1], out double paramG1Tolerance2))
                            {
                                weldData.ParamG1Tolerance[1] = paramG1Tolerance2;
                            }
                        }
                        break;
                    default:
                        continue;
                }
                if (Enum.TryParse(cells[28], out DependenceSeamThicknessEnum dependenceSeamThickness) && dependenceSeamThickness != DependenceSeamThicknessEnum.НЕ_УКАЗАНО)
                {
                    weldData.DependenceSeamThickness = dependenceSeamThickness;
                }

                data.Add(weldData);
            }
            OrigWeldDates = data.ToArray();
            WeldDates = OrigWeldDates;
            if (WeldDates != null)
            {
                WeldGOSTs = OrigWeldDates.Select(n => n.NameGost).Distinct().ToArray();
            }
        }

        [RelayCommand]
        public void Test()
        {
            KompasObject kompas = (KompasObject)ExMarshal.GetActiveObject("KOMPAS.Application.5");
            if (kompas == null)
            {
                MessageBox.Show("Запустите компас");
                return;
            }
            IApplication application = (IApplication)kompas.ksGetApplication7();
            IKompasDocument? activeKD = application.ActiveDocument;
            ksDocument2D? document2DAPI5 = kompas.ActiveDocument2D() as ksDocument2D;
            if (activeKD == null || document2DAPI5 == null)
            {
                MessageBox.Show("Запустите чертеж или фрагмент");
                return;
            }
            if (activeKD.DocumentType != DocumentTypeEnum.ksDocumentDrawing
                && activeKD.DocumentType != DocumentTypeEnum.ksDocumentFragment)
            {
                MessageBox.Show("Программа работает только в чертеже или фрагменте");
                return;
            }
            IKompasDocument2D activeKD2D = (IKompasDocument2D)activeKD;
            IViewsAndLayersManager viewsAndLayersManager = activeKD2D.ViewsAndLayersManager;
            IViews views = viewsAndLayersManager.Views;
            IView activeView = views.ActiveView;
            document2DAPI5.ksUndoContainer(true);

            double thickness = Convert.ToDouble(Thickness);
            if (NumberPart)
            {
                SelectWeldDates?.DrawingPart(activeView, thickness, IsLocationPart, NumberPart, IsDrawingDimensions, SelectTransitionTypesFirstUP, SelectTransitionTypesFirstBottom);
            }
            else
            {
                SelectWeldDates?.DrawingPart(activeView, thickness, IsLocationPart, NumberPart, IsDrawingDimensions, SelectTransitionTypesSecondUP, SelectTransitionTypesSecondBottom);
            }

            document2DAPI5.ksUndoContainer(false);
            application.MessageBoxEx("Работа со сварным швом завершена", "", 64);
        }        
    }
}
