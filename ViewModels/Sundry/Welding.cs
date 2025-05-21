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
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using System.Xml.Linq;
using System.Xml.Serialization;
using static KompasTools.Classes.Sundry.Welding.WeldEnum;

namespace KompasTools.ViewModels.Sundry
{
    public partial class Welding : ObservableObject
    {
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
                data.Add(weldData);
            }
            OrigWeldDates = data.ToArray();
            WeldDates = OrigWeldDates;
            if (WeldDates != null)
            {
                WeldGOSTs = OrigWeldDates.Select(n => n.NameGost).Distinct().ToArray();
            }

            //    string currentDirectory = Directory.GetCurrentDirectory();
            //    string pathsettings = $"{currentDirectory}\\Sundry\\Welding\\GOST_14771.xml";
            //    XmlSerializer xmlSerializer = new(typeof(List<WeldGOST>), new[] { typeof(WeldGOST), typeof(KType), typeof(VType) });
            //    if (File.Exists(pathsettings))
            //    {
            //        using FileStream fs = new(pathsettings, FileMode.Open);
            //        WeldGOSTs = xmlSerializer.Deserialize(fs) as List<WeldGOST>;
            //    }
        }

        [RelayCommand]
        public void Test()
        {
            string currentDirectory = Directory.GetCurrentDirectory();



            KompasObject kompas = (KompasObject)ExMarshal.GetActiveObject("KOMPAS.Application.5");
            if (kompas == null)
            {
                System.Windows.Forms.MessageBox.Show("Запустите компас");
                return;
            }
            IApplication application = (IApplication)kompas.ksGetApplication7();
            application.MessageBoxEx("Работа со сварным швом завершена", "", 64);
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
                System.Windows.Forms.MessageBox.Show("Программа работает только в чертеже или фрагменте");
                return;
            }
            IKompasDocument2D activeKD2D = (IKompasDocument2D)activeKD;
            IViewsAndLayersManager viewsAndLayersManager = activeKD2D.ViewsAndLayersManager;
            IViews views = viewsAndLayersManager.Views;
            IView activeView = views.ActiveView;
            document2DAPI5.ksUndoContainer(true);

            double thickness = Convert.ToDouble(Thickness);

            IDrawingContainer drawingContainer = (IDrawingContainer)activeView;
            ISymbols2DContainer symbols2DContainer = (ISymbols2DContainer)activeView;
            ILineSegments lineSegments = drawingContainer.LineSegments;
            //ILineSegment lineSegment = lineSegments.Add();
            //lineSegment.X1 = 0;
            //lineSegment.Y1 = 0;
            //lineSegment.X2 = 0;
            //lineSegment.Y2 = 0;
            //lineSegment.Update();
        }


    }
}
