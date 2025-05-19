using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
using Irony.Parsing;
using Kompas6API5;
using Kompas6Constants;
using KompasAPI7;
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
using static KompasTools.ViewModels.Sundry.Welding.WeldData;

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
            WeldDates = new WeldData[]{
                new WeldData()
                {
                    NameGost = "14771",
                    NameWeldJoint = "C15",
                    WeldingMethod = WeldingMethodEnum.ИП,
                    ThicknessMin = 8,
                    ThicknessMax = 11
                },
                new WeldData()
                {
                    NameGost = "14771",
                    NameWeldJoint = "C15",
                    WeldingMethod = WeldingMethodEnum.ИП,
                    ThicknessMin = 12,
                    ThicknessMax = 14
                },
                new WeldData()
                {
                    NameGost = "14771",
                    NameWeldJoint = "C18",
                    WeldingMethod = WeldingMethodEnum.ИП,
                    ThicknessMin = 7,
                    ThicknessMax = 8
                },
                new WeldData()
                {
                    NameGost = "14771",
                    NameWeldJoint = "C18",
                    WeldingMethod = WeldingMethodEnum.УП,
                    ThicknessMin = 50,
                    ThicknessMax = 56
                },
                new WeldData()
                {
                    NameGost = "23518",
                    NameWeldJoint = "У1",
                    WeldingMethod = WeldingMethodEnum.ИП,
                    ThicknessMin = 6,
                    ThicknessMax = 30
                },
                new WeldData()
                {
                    NameGost = "23518",
                    NameWeldJoint = "У1",
                    WeldingMethod = WeldingMethodEnum.ИНп,
                    ThicknessMin = 0.8,
                    ThicknessMax = 2
                }

            };
            OrigWeldDates = WeldDates;
            if (WeldDates != null)
            {
                WeldGOSTs = WeldDates.Select(n => n.NameGost).Distinct().ToArray();
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

        public class WeldData
        {
            private string nameGost = "";
            private string nameWeldJoint = "";
            private WeldingMethodEnum weldingMethod;
            private double thicknessMin;
            private double thicknessMax;
            private string? rangeThickness;

            /// <summary>
            /// Наименование ГОСТа
            /// </summary>
            public string NameGost { get => nameGost; set => nameGost = value; }
            /// <summary>
            /// Услоавное обозначение сварного соединения
            /// </summary>
            public string NameWeldJoint { get => nameWeldJoint; set => nameWeldJoint = value; }
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
            /// Проверка вхождения толщины детали в диапазон толщин
            /// </summary>
            /// <param name="thickness"></param>
            /// <returns></returns>
            public bool CheckThickness(double thickness)
            {
                if(thickness >= thicknessMin &&  thickness < thicknessMax)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            public enum WeldingMethodEnum
            {
                ИП,
                УП,
                ИН,
                ИНп
            }
            /// <summary>
            /// Форма подготовленных кромок
            /// </summary>
            public enum ShapePreparedEdges
            {
                [EnumMember(Value = "Без скоса")]
                Без_скоса,
                [EnumMember(Value = "Со скосом одной кромки")]
                Скос_одной_кромки,
                [EnumMember(Value = "С двумя симметричными скосами")]
                Два_симметричные_скоса,
                [EnumMember(Value = "С двумя не симметричными скосами")]
                Два_не_симметричные_скоса
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
}
