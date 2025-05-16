using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.EMMA;
using DocumentFormat.OpenXml.Wordprocessing;
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
        private string _thickness = "20";

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
            Filter();
        }

        [ObservableProperty]
        private string[]? _nameWeldJoints;
        [ObservableProperty]
        private string? _selectNameWeldJoints = null;
        partial void OnSelectNameWeldJointsChanged(string? value)
        {
            Filter();
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
                NameWeldJoints = OrigWeldDates.Select(n => n.NameWeldJoint).Distinct().ToArray();
                WeldingMethod = OrigWeldDates.Select(n => n.WeldingMethod).Distinct().ToArray();

                if (SelectGOST != null) WeldDates = OrigWeldDates.Where(n => n.NameGost == SelectGOST).ToArray();
                if (SelectNameWeldJoints != null && WeldDates != null) WeldDates = WeldDates.Where(n => n.NameWeldJoint == SelectNameWeldJoints).ToArray();
                if (SelectWeldingMethod!= null && WeldDates != null) WeldDates = WeldDates.Where(n => n.WeldingMethod == SelectWeldingMethod).ToArray();
            }
        }

        //[ObservableProperty]
        //private List<WeldGOST>? _weldGOSTs;
        //[ObservableProperty]
        //private WeldGOST? _selectGOST = null;

        //[ObservableProperty]
        //private WeldSeam? _selectWeldSeam = null;

        //[ObservableProperty]
        //private WeldingMethod? _selectWeldingMethod;




        /// <summary>
        /// Форма подготовленных кромок
        /// </summary>
        public enum ShapePreparedEdges
        {
            [EnumMember(Value = "Без скоса")]
            Без_скоса,
            [EnumMember(Value = "Со скосом одной кромки")]
            Скос_одной_кромки,
            [EnumMember(Value = "С двумя симетричными скосами")]
            Два_симметричные_скоса,
            [EnumMember(Value = "С двумя не симетричными скосами")]
            Два_не_симметричные_скоса
        }

        [RelayCommand]
        public void LoadedTab()
        {
            WeldDates = new WeldData[]{
                new WeldData()
                {
                    NameGost = "14771",
                    NameWeldJoint = "C15",
                    WeldingMethod = WeldingMethodEnum.ИП
                },
                new WeldData()
                {
                    NameGost = "14771",
                    NameWeldJoint = "C18",
                    WeldingMethod = WeldingMethodEnum.ИП
                },
                new WeldData()
                {
                    NameGost = "14771",
                    NameWeldJoint = "C18",
                    WeldingMethod = WeldingMethodEnum.УП
                },
                new WeldData()
                {
                    NameGost = "23518",
                    NameWeldJoint = "У1",
                    WeldingMethod = WeldingMethodEnum.ИП
                },
                new WeldData()
                {
                    NameGost = "23518",
                    NameWeldJoint = "У1",
                    WeldingMethod = WeldingMethodEnum.ИНп
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

           


            /*
            WeldSeam t8ip = new()
            {
                Name = "T8",
                WeldDatas =
                new WeldData[]
                {
                    new WeldData()
                    {
                        WeldingMethod = WeldingMethod.ИНп,
                        RangeThicknessMin = 6,
                        RangeThicknessMax = 20

                    }
                }
            };

            WeldGOST weldGOST14771 = new()
            {
                Name = "ГОСТ 14771",
                WeldingMethods = new WeldingMethod[]
                {
                    WeldingMethod.ИП,
                    WeldingMethod.УП,
                    WeldingMethod.ИН,
                    WeldingMethod.ИНп
                },
                WeldSeams = new WeldSeam[] { t8ip}
            };

            WeldGOSTs = new List<WeldGOST> { weldGOST14771 };
            */
        }

        public class WeldData
        {
            private string nameGost = "";
            private string nameWeldJoint = "";
            private WeldingMethodEnum weldingMethod;

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


            public enum WeldingMethodEnum
            {
                ИП,
                УП,
                ИН,
                ИНп
            }
        }




        #region Третья проба
        /*
        /// <summary>
        /// ГОСТ: Соединения сварные
        /// </summary>
        public class WeldGOST
        {
            private string name = "";
            private WeldSeam[]? weldSeams;
            private WeldingMethod[]? weldingMethods;

            /// <summary>
            /// Наименование госта
            /// </summary>
            public string Name { get => name; set => name = value; }
            /// <summary>
            /// Список условных обозначений сварных соединений
            /// </summary>
            public WeldSeam[]? WeldSeams { get => weldSeams; set => weldSeams = value; }
            /// <summary>
            /// Способ сварки
            /// </summary>
            public WeldingMethod[]? WeldingMethods { get => weldingMethods; set => weldingMethods = value; }




        }
        /// <summary>
        /// Сварное соединение
        /// </summary>
        public class WeldSeam
        {
            private string name = "";
            private WeldData[]? weldDatas;

            public string Name { get => name; set => name = value; }
            public WeldData[]? WeldDatas { get => weldDatas; set => weldDatas = value; }



            public void DrawingJoint(IView view, double thickness, LocationPart locationPart, bool drawDimensions, double extraLength = 20)
            {

            }

            public void DrawingSeam(IView view, double thickness, LocationPart locationPart, bool drawDimensions, double extraLength = 20)
            {

            }

            public void DrawingPart(IView view, double thickness, WeldingMethod weldingMethod, LocationPart locationPart, bool drawDimensions, double extraLength = 20)
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
                WeldData[]? weldData = null;
                weldData = weldDatas?.Where(p => p.RangeThicknessMin <= thickness && p.RangeThicknessMax >= thickness
                                            && p.WeldingMethod == weldingMethod).ToArray();

                IDrawingContainer drawingContainer = (IDrawingContainer)view;
                ISymbols2DContainer symbols2DContainer = (ISymbols2DContainer)view;
                ILineSegments lineSegments = drawingContainer.LineSegments;
                switch (weldData?.ShapePreparedEdges)
                {
                    case ShapePreparedEdges.Без_скоса:
                        break;
                    case ShapePreparedEdges.Скос_одной_кромки:
                        {
                            //double xangle = (thickness - Prit) * Math.Tan(angle * Math.PI / 180);
                            //DrawLineSegment(0, 0, );
                        }
                        break;
                    case ShapePreparedEdges.Два_симметричные_скоса:
                        break;
                    case ShapePreparedEdges.Два_не_симметричные_скоса:
                        break;
                }

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
            /// Тип соединения
            /// </summary>
            public enum WeldSeamType
            {
                Стыковой,
                Тавровы,
                Угловой,
                Нахлёсточный
            }


        }

        public class WeldData
        {
            private double rangeThicknessMin;
            private double rangeThicknessMax;
            private WeldingMethod weldingMethod;

            private ShapePreparedEdges shapePreparedEdges;

            public double RangeThicknessMin { get => rangeThicknessMin; set => rangeThicknessMin = value; }
            public double RangeThicknessMax { get => rangeThicknessMax; set => rangeThicknessMax = value; }
            public WeldingMethod WeldingMethod { get => weldingMethod; set => weldingMethod = value; }
            public ShapePreparedEdges ShapePreparedEdges { get => shapePreparedEdges; set => shapePreparedEdges = value; }
        }
        */
        #endregion
        #region Вторая проба
        /*
        public interface IWeldSeam
        {
            //Параметры сварного шва
            /// <summary>
            /// Условное обозначение сварного соединения
            /// </summary>
            string? Name { get; set; }
            /// <summary>
            /// Форма подготовленных кромок
            /// </summary>
            public ShapePreparedEdges? ShapePrepared { get; set; }
            /// <summary>
            /// Тип соединения
            /// </summary>
            public WeldSeamType? WeldSeamType1 { get; set; }
            /// <summary>
            /// Способ сварки
            /// </summary>
            public WeldingMethod? WeldingMethod1 { get; set; }
            /// <summary>
            /// Начертить стык
            /// </summary>
            /// <param name="view"></param>
            /// <param name="thickness"></param>
            public void DrawingJoint(IView view, double thickness, LocationPart locationPart, bool drawDimensions, double extraLength = 20);


            /// <summary>
            /// Начертить стык с отображением шва
            /// </summary>
            /// <param name="view"></param>
            /// <param name="thickness"></param>
            public void DrawingSeam(IView view, double thickness, LocationPart locationPart, bool drawDimensions, double extraLength = 20);

            /// <summary>
            /// Начертить разделку детали
            /// </summary>
            /// <param name="view"></param>
            /// <param name="thickness"></param>
            public void DrawingPart(IView view, double thickness, LocationPart locationPart, bool drawDimensions, double extraLength = 20);

            /// <summary>
            /// Начертить разделку деталя для 3D
            /// </summary>
            /// <param name="view"></param>
            /// <param name="thickness"></param>
            public void DrawingPart3D(IView view, double thickness, LocationPart locationPart);

        }

        public class WeldSeamGost14771 : IWeldSeam
        {
            private string? name;

            //Список диапозонов толщин

            private WeldingMethod? weldingMethod;


            private WeldSeamType? weldSeamType;
            private ShapePreparedEdges? shapePrepared;

            private double? angleMax;
            private double? angleMin;
            private double? angle;

            public string? Name { get => name; set => name = value; }
            public WeldingMethod? WeldingMethod1 { get => weldingMethod; set => weldingMethod = value; }
            public WeldSeamType? WeldSeamType1 { get => weldSeamType; set => weldSeamType = value; }
            public ShapePreparedEdges? ShapePrepared { get => shapePrepared; set => shapePrepared = value; }

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
                switch (ShapePrepared)
                {
                    case ShapePreparedEdges.Без_скоса:
                        break;
                    case ShapePreparedEdges.Скос_одной_кромки:
                        {
                            //double xangle = (thickness - Prit) * Math.Tan(angle * Math.PI / 180);
                            //DrawLineSegment(0, 0, );
                        }
                        break;
                    case ShapePreparedEdges.Два_симметричные_скоса:
                        break;
                    case ShapePreparedEdges.Два_не_симметричные_скоса:
                        break;
                }

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
            /// Тип соединения
            /// </summary>
            public enum WeldSeamType
            {
                Стыковой,
                Тавровы,
                Угловой,
                Нахлёсточный
            }
            /// <summary>
            /// Способ сварки
            /// </summary>
            public enum WeldingMethod
            {
                [EnumMember(Value = "ИН - в инертных газах, неплавящимся электродом без присадочного металла")]
                ИН,
                [EnumMember(Value = "ИНп - в инертных газах, неплавящимся электродом с присадочным металлом")]
                ИНп,
                [EnumMember(Value = "ИП - в инертных газах и их смесях с углекислым газом и кислородом плавящимся электродом")]
                ИП,
                [EnumMember(Value = "ИН - в углекислом газе и его смеси с кислородом плавящимся электродом")]
                УП
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
                [EnumMember(Value = "С двумя симетричными скосами")]
                Два_симметричные_скоса,
                [EnumMember(Value = "С двумя не симетричными скосами")]
                Два_не_симметричные_скоса
            }
        } 
        */
        #endregion

        #region Первая проба
        /*
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
            IKompasDocument activeKD = application.ActiveDocument;
            ksDocument2D? document2DAPI5 = kompas.ActiveDocument2D() as ksDocument2D;
            if (activeKD == null || document2DAPI5 == null)
            {
                System.Windows.Forms.MessageBox.Show("Запустите чертеж или фрагмент");
                return;
            }
            if (activeKD.DocumentType != Kompas6Constants.DocumentTypeEnum.ksDocumentDrawing
                && activeKD.DocumentType != Kompas6Constants.DocumentTypeEnum.ksDocumentFragment)
            {
                System.Windows.Forms.MessageBox.Show("Программа работает только в чертеже или фрагменте");
                return;
            }
            IKompasDocument2D activeKD2D = (IKompasDocument2D)activeKD;
            IViewsAndLayersManager viewsAndLayersManager = activeKD2D.ViewsAndLayersManager;
            IViews views = viewsAndLayersManager.Views;
            IView activeView = views.ActiveView;
            document2DAPI5.ksUndoContainer(true);

            double thx = Convert.ToDouble(Thickness);
            double angle = 50;
            double ldop = 20; //Удлинение вида разделки
            double dimGap = 8 / activeView.Scale;//Зазор до размерной линии
            WeldGOST WG_14771 = new()
            {
                Name = "ГОСТ 14771",
            };

            VType s29_a = new()
            {
                Name = "С18-ИП",
                GostName = "ГОСТ 14771",
                Angle1 = 30,
                Angle2 = 20,
                ThicknessAngle = 11,
                Prit = 2
            };
            WG_14771.Cutting.Add(s29_a);

            VType s30_a = new()
            {
                Name = "С30-А",
                GostName = "ГОСТ 14771",
                Angle1 = 40,
                Angle2 = 50,
                ThicknessAngle = 50,
                Prit = 4
            };
            WG_14771.Cutting.Add(s30_a);


            WeldGOSTs = new List<WeldGOST>() { WG_14771 };
            s29_a.Draw(activeView, thx);

            XmlSerializer xmlSerializer = new(WeldGOSTs.GetType(), new[] { typeof(WeldGOST), typeof(KType), typeof(VType) });
            if (!Directory.Exists($"{currentDirectory}\\Sundry\\Welding"))
            {
                Directory.CreateDirectory($"{currentDirectory}\\Sundry\\Welding");
            }
            using (FileStream fs = new($"{currentDirectory}\\Sundry\\Welding\\GOST_14771.xml", FileMode.Create))
            {
                xmlSerializer.Serialize(fs, WeldGOSTs);
            }


            document2DAPI5.ksUndoContainer(false);



        }
        [Serializable]
        [XmlRoot(ElementName = "Фаска")]
        public class Weld_Chamfer
        {
            private string _name = "";
            private string _gostName = "";
            private double _angle1;
            private double _angle2;
            private double _thicknessAngle;
            private double _prit;

            public string Name { get => _name; set => _name = value; }
            public string GostName { get => _gostName; set => _gostName = value; }
            public double Angle1 { get => Angle1; set => _angle1 = value; }
            public double Angle2 { get => Angle2; set => _angle2 = value; }
            public double Prit { get => _prit; set => _prit = value; }
            /// <summary>
            /// Максимальная толщина для максимального угла. Обычно это первый угол.
            /// </summary>
            public double ThicknessAngle { get => _thicknessAngle; set => _thicknessAngle = value; }


            /// <summary>
            /// Начертить линию
            /// </summary>
            /// <param name="x1"></param>
            /// <param name="y1"></param>
            /// <param name="x2"></param>
            /// <param name="y2"></param>
            private protected void DrawLineSegment(IView view, double x1, double y1, double x2, double y2)
            {
                IDrawingContainer drawingContainer = (IDrawingContainer)view;
                ILineSegments lineSegments = drawingContainer.LineSegments;
                ILineSegment lineSegment = lineSegments.Add();
                lineSegment.X1 = x1;
                lineSegment.Y1 = y1;
                lineSegment.X2 = x2;
                lineSegment.Y2 = y2;
                lineSegment.Update();
            }
            /// <summary>
            /// Начертить линейный размер
            /// </summary>
            /// <param name="x1"></param>
            /// <param name="y1"></param>
            /// <param name="x2"></param>
            /// <param name="y2"></param>
            /// <param name="x3"></param>
            /// <param name="y3"></param>
            private protected void DrawLineDimension(IView view, double x1, double y1, double x2, double y2, double x3, double y3)
            {
                ISymbols2DContainer symbols2DContainer = (ISymbols2DContainer)view;
                ILineDimensions lineDimensions = symbols2DContainer.LineDimensions;
                ILineDimension lineDimension = lineDimensions.Add();
                IDimensionParams dimensionParams = (IDimensionParams)lineDimension;
                dimensionParams.InitDefaultValues();
                lineDimension.X1 = x1;
                lineDimension.Y1 = y1;
                lineDimension.X2 = x2;
                lineDimension.Y2 = y2;
                lineDimension.X3 = x3;
                lineDimension.Y3 = y3;
                lineDimension.Update();
            }

            public double GetAngle(double thickness)
            {
                if (thickness <= ThicknessAngle)
                {
                    return _angle1;
                }
                else
                {
                    return _angle2;
                }
            }

        }

        [Serializable]
        [XmlRoot(ElementName = "Фаска K типа")]
        public class KType : Weld_Chamfer
        {

        }

        [Serializable]
        [XmlRoot(ElementName = "Фаска V типа")]
        public class VType : Weld_Chamfer
        {
            public bool Draw(IView view, double thickness)
            {
                double angle = GetAngle(thickness);
                if (view == null || thickness <= 0 || angle <= 0 || Prit <= 0 || ThicknessAngle <= 0)
                {
                    return false;
                }
                double ldop = 20; //Удлинение вида разделки
                double dimGap = 8 / view.Scale;//Зазор до размерной линии
                DrawLineSegment(view, 0, 0, 0, Prit);
                double xangle = (thickness - Prit) * Math.Tan(angle * Math.PI / 180);
                DrawLineSegment(view, 0, Prit, xangle, thickness);
                DrawLineSegment(view, xangle, thickness, xangle + ldop, thickness);
                DrawLineSegment(view, 0, 0, xangle + ldop, 0);

                DrawLineDimension(view, 0, 0, 0, Prit, 0 - dimGap, -1);
                return true;
            }
        }

        [Serializable]
        [XmlRoot(ElementName = "ГОСТ")]
        public class WeldGOST
        {
            private string name;
            List<Weld_Chamfer> cutting = new();

            public string Name { get => name; set => name = value; }
            [XmlArrayItem("Список_разделок")]
            public List<Weld_Chamfer> Cutting { get => cutting; set => cutting = value; }
        } 
        */
        #endregion
    }
}
