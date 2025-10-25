using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Kompas6API5;
using Kompas6Constants;
using KompasAPI7;
using KompasTools.Classes.Sundry.Welding;
using KompasTools.Utils;
using Microsoft.WindowsAPICodePack.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using static KompasTools.Classes.Sundry.Welding.WeldEnum;
using static System.TimeZoneInfo;

namespace KompasTools.ViewModels.Sundry
{
    public partial class Welding : ObservableValidator
    {
        /// <summary>
        /// Толщина t1
        /// </summary>
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [RegularExpression(@"^(\d+(,\d+)?)$")]
        private string _thickness1Str = "0";
        partial void OnThickness1StrChanged(string value)
        {
            if (GetErrors(nameof(Thickness1Str)).Any())
            {
                Thickness1 = 0;
            }
            else if(!double.TryParse(Thickness1Str, out Thickness1))
            {
                Thickness1 = 0;
            }
            ComparisonThicknesses();
        }
        /// <summary>
        /// Толщина t2
        /// </summary>
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [RegularExpression(@"^(\d+(,\d+)?)$")]
        private string _thickness2Str = "0";
        partial void OnThickness2StrChanged(string value)
        {
            if (GetErrors(nameof(Thickness2Str)).Any())
            {
                Thickness2 = 0;
            }
            else if (!double.TryParse(Thickness2Str, out Thickness2))
            {
                Thickness2 = 0;
            }
            ComparisonThicknesses();
        }
        /// <summary>
        /// Толщина в стыке
        /// </summary>
        private double Thickness = 0;
        /// <summary>
        /// Толщина первой детали
        /// </summary>
        private double Thickness1 = 0;
        /// <summary>
        /// Толщина второй детали
        /// </summary>
        private double Thickness2 = 0;
        /// <summary>
        /// Отображаемый список сварных швов. Отфильтрован
        /// </summary>
        [ObservableProperty]
        private WeldData[]? _weldDates;
        [ObservableProperty]
        private WeldData? _selectWeldDates;
        /// <summary>
        /// Изначальный список сварных швов
        /// </summary>
        private WeldData[]? OrigWeldDates;
        /// <summary>
        /// Списов гостов
        /// </summary>
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
            WeldingMethod = WeldDates?.Select(n => n.WeldingMethod).Distinct().ToArray();
        }
        /// <summary>
        /// Список условных обозначений швов
        /// </summary>
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
        /// <summary>
        /// Способ сварки
        /// </summary>
        [ObservableProperty]
        private WeldingMethodEnum[]? _weldingMethod;
        [ObservableProperty]
        private WeldingMethodEnum? _selectWeldingMethod = null;
        partial void OnSelectWeldingMethodChanged(WeldingMethodEnum? value)
        {
            Filter();
        }
        partial void OnSelectWeldDatesChanged(WeldData? value)
        {
            string path = $"Resources\\Sundry\\Welding\\DataWeld\\{SelectWeldDates?.ConnectionType}.frw";
            if(File.Exists(path)) PathImage = ShellFile.FromFilePath(path).Thumbnail.BitmapSource;
        }
        /// <summary>
        /// Тип расположения деталей сварного шва
        /// </summary>
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
        private TransitionTypeEnum[] _transitionTypes = (TransitionTypeEnum[])Enum.GetValues(typeof(TransitionTypeEnum));
        /// <summary>
        /// Выбранный тип перехода
        /// </summary>
        [ObservableProperty]
        private TransitionTypeEnum _selectTransitionTypes = TransitionTypeEnum.Без_перехода;
        partial void OnSelectTransitionTypesChanged(TransitionTypeEnum value)
        {
            ComparisonThicknesses();
        }
        /// <summary>
        /// Длина или кофициент длины перехода
        /// </summary>
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [RegularExpression(@"^(\d+(,\d+)?)$")]
        private string _transitionDimLorK = "8";
        /// <summary>
        /// Ввод длины перехода
        /// </summary>
        [ObservableProperty]
        private bool _istransitionDimL = false;
        /// <summary>
        /// Чертить размеры?
        /// </summary>
        [ObservableProperty]
        private bool _isDrawingDimensions = true;
        /// <summary>
        /// Объединить в макроэлемент?
        /// </summary>
        [ObservableProperty]
        private bool _isMacro = true;
        /// <summary>
        /// Чертить штриховку?
        /// </summary>
        [ObservableProperty]
        private bool _isHatches = true;
        /// <summary>
        /// Имя сечения
        /// </summary>
        [ObservableProperty]
        private string _nameCut = "1-1"; //TODO сделать равным ""
        /// <summary>
        /// Подобрать вид по масштабу? Если true то подбираем. Если false то вставляем в активный вид
        /// </summary>
        [ObservableProperty]
        private bool _isSearchView = false;
        /// <summary>
        /// Левая часть масштаба
        /// </summary>
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [RegularExpression(@"^(\d+(,\d+)?)$")]
        private string _leftScale = "1";
        /// <summary>
        /// Правая часть масштаба
        /// </summary>
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [RegularExpression(@"^(\d+(,\d+)?)$")]
        private string _rightScale = "1";
        /// <summary>
        /// Расстояние между размерами
        /// </summary>
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [RegularExpression(@"^(\d+(,\d+)?)$")]
        private string _gapDim = "8";
        /// <summary>
        /// Изменение длины детали
        /// </summary>
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required]
        [RegularExpression(@"^(-?\d+(,\d+)?)$")]
        private string _extraLength = "0";
        /// <summary>
        /// Создавать сечение?
        /// </summary>
        [ObservableProperty]
        private bool _isCrossSection= true;
        /// <summary>
        /// Путь к файлу для получения миниатюры
        /// </summary>
        [ObservableProperty]
        private BitmapSource? _pathImage;

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        #region Методы
        /// <summary>
        /// Фильтр списка сварных швов
        /// </summary>
        public void Filter()
        {
            if (OrigWeldDates != null)
            {
                WeldDates = OrigWeldDates;
                if (SelectGOST != null) WeldDates = WeldDates.Where(n => n.NameGost == SelectGOST).ToArray();
                if (SelectNameWeldJoints != null && WeldDates != null) WeldDates = WeldDates.Where(n => n.NameWeldJoint == SelectNameWeldJoints).ToArray();
                if (SelectWeldingMethod != null && WeldDates != null) WeldDates = WeldDates.Where(n => n.WeldingMethod == SelectWeldingMethod).ToArray();

                if (Thickness > 0 && WeldDates != null) WeldDates = WeldDates.Where(n => n.CheckThickness(Thickness)).ToArray();
            }
        }
        /// <summary>
        /// Сравнение толщин и запись наименьшей в толщину стыка
        /// </summary>
        private void ComparisonThicknesses()
        {
            if (SelectTransitionTypes != TransitionTypeEnum.Без_перехода)
            {
                if (Thickness1 < Thickness2)
                {
                    Thickness = Thickness1;
                }
                else
                {
                    Thickness = Thickness2;
                }
            }
            else
            {
                Thickness = Thickness1;
            }
        Filter();
        }
        #endregion


        #region Команды
        /// <summary>
        /// Действия при загрузке закладки
        /// </summary>
        [RelayCommand]
        public void LoadedTab()
        {
            #region Загрузка и парсинг данных. Так же заполнение  списков.
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Resources\\Sundry\\Welding\\Параметры сварки.csv");
            string text;
            int columnCount = 29; //Правильное количество параметров шва
            List<WeldData> data = new();
            using (StreamReader reader = new(path))
            {
                text = reader.ReadToEnd();
            }
            string[] temp = text.Split("\n", StringSplitOptions.RemoveEmptyEntries);
            //Заполняем данными
            foreach (string row in temp)
            {
                string[] cells = row.Split("\t");
                if (cells.Length != columnCount) continue;
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
                                weldData.ParamCTolerance[0] = paramCTolerance1;
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
                                weldData.ParamATolerance[0] = paramATolerance1;
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
                                weldData.ParamBTolerance[0] = paramBTolerance1;
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
                                weldData.ParamETolerance[0] = paramETolerance1;
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
                                weldData.ParamE1Tolerance[0] = paramE1Tolerance1;
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
                                weldData.ParamGTolerance[0] = paramGTolerance1;
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
                                weldData.ParamG1Tolerance[0] = paramG1Tolerance1;
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
            //Заполняем список гостов
            if (WeldDates != null)
            {
                WeldGOSTs = OrigWeldDates.Select(n => n.NameGost).Distinct().ToArray();
            }
            #endregion
            Thickness1Str = "16"; //TODO Удалить
            Thickness2Str = "10"; //TODO Удалить
            SelectTransitionTypes = TransitionTypeEnum.Вверх;
            Filter();
        }
        /// <summary>
        /// Чертим
        /// </summary>
        /// <param name="TypeElement"></param>
        [RelayCommand]
        public void Drawing(string TypeElement)
        {
            if (SelectWeldDates == null)
            {
                MessageBox.Show("Выберите сварной шов");
                return;
            }
            if (GetErrors(nameof(Thickness1Str)).Any() || Thickness <= 0)
            {
                MessageBox.Show("Толщина должна быть числом и оно должно быть больше нуля. Дробная часть должна разделяться запятой.");
                return;
            }
            if (!double.TryParse(LeftScale, out double leftScale))
            {
                MessageBox.Show("В левую часть масштаба введено не число. Число должно быть больше нуля. Дробная часть должна разделяться запятой.");
                return;
            }
            if (!double.TryParse(RightScale, out double rightScale))
            {
                MessageBox.Show("В правую часть масштаба введено не число. Число должно быть больше нуля. Дробная часть должна разделяться запятой.");
                return;
            }
            if (!double.TryParse(GapDim, out double gapDim))
            {
                MessageBox.Show("Расстояние между размерами должно быть числом. Число должно быть больше нуля. Дробная часть должна разделяться запятой.");
                return;
            }
            if (!double.TryParse(TransitionDimLorK, out double transitionDimLorK) && (SelectTransitionTypes != TransitionTypeEnum.Без_перехода))
            {
                MessageBox.Show("Длина/коффициент перехода должно быть числом. Число должно быть больше нуля. Дробная часть должна разделяться запятой.");
                return;
            }
            KompasObject? kompas = ExMarshal.GetActiveObject("KOMPAS.Application.5") as KompasObject;
            if (kompas == null)
            {
                MessageBox.Show("Запустите компас");
                return;
            }
            IApplication application = (IApplication)kompas.ksGetApplication7();
            IKompasDocument? activeKD = application.ActiveDocument;
            ksDocument2D? document2DAPI5 = kompas.ActiveDocument2D() as ksDocument2D;
            IKompasDocument2D kompasDocument2D = (IKompasDocument2D)activeKD;
            IKompasDocument2D1 kompasDocument2D1 = (IKompasDocument2D1)activeKD;
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
            //Переключиться на компас
            Process[] processes = Process.GetProcessesByName("KOMPAS");
            IntPtr handle = processes[0].MainWindowHandle;
            SetForegroundWindow(handle);
            document2DAPI5.ksUndoContainer(true);
            IViewsAndLayersManager viewsAndLayersManager = kompasDocument2D.ViewsAndLayersManager;
            IViews views = viewsAndLayersManager.Views;
            IView view = views.ActiveView;
            //Подбираем вид в зависимости от масштаба
            if (IsSearchView && kompasDocument2D.DocumentType == DocumentTypeEnum.ksDocumentDrawing)
            {
                double scale = leftScale / rightScale;
                bool isview = false;
                foreach (IView view1 in views)
                {
                    if (view1.Scale == scale)
                    {
                        view = view1;
                        view.Current = true;
                        view.Update();
                        isview = true;
                        break;
                    }
                }
                //Если вид не найден, создаём новый
                if (!isview)
                {
                    IView newview = views.Add(LtViewType.vt_Normal);
                    newview.Scale = scale;
                    newview.Current = true;
                    newview.Update();
                    newview.Name = $"Вид {newview.Number}";
                    newview.Update();
                    view = newview;
                }
            }
            IDrawingContainer drawingContainer = (IDrawingContainer)view;
            gapDim /= view.Scale;
            double gapDimToPart = gapDim / 2; //Расстояние до детали находящейся снизу или справа
            double gapDimToDim = gapDim; //Расстояние между размерами
            double gapDimToPartLeft = gapDim; //Расстояние до детали находящейся слева
            if (!Double.TryParse(ExtraLength, out double extraLength))//Длина детали от скоса
            {
                extraLength = 0;
            }
            extraLength /= view.Scale;
            IDrawingGroups drawingGroups = kompasDocument2D1.DrawingGroups;
            IDrawingGroup drawingGroup = drawingGroups.Add(true, "Сварка");
            drawingGroup.Open();
            TransitionData transitionData = new TransitionData(SelectTransitionTypes, Thickness1, Thickness2, transitionDimLorK, IstransitionDimL);
            //Чертим
            switch (TypeElement)
            {
                case "Part":
                    if (NumberPart)
                    {
                        SelectWeldDates?.DrawingPart(view, Thickness, IsLocationPart, NumberPart, IsDrawingDimensions,
                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, IsCrossSection, IsHatches, transitionData);
                    }
                    else
                    {
                        SelectWeldDates?.DrawingPart(view, Thickness, IsLocationPart, NumberPart, IsDrawingDimensions,
                            gapDimToPart, gapDimToDim, gapDimToPartLeft, extraLength, IsCrossSection, IsHatches, transitionData);
                    }
                    break;
                case "Joint":
                    SelectWeldDates?.DrawingJoint(kompas, view, Thickness, IsLocationPart, IsDrawingDimensions, drawingGroup, gapDimToPart,
                        gapDimToDim, gapDimToPartLeft, extraLength, IsCrossSection, IsHatches, transitionData);
                    break;
                case "Seam":
                    MessageBox.Show("Шов");
                    break;
                case "3D":
                    MessageBox.Show("3D");
                    break;
                default:
                    break;
            }
            if (drawingGroup.ObjectsCount == 0)
            {
                drawingGroup.Close();
                drawingGroup.Delete();
                document2DAPI5.ksUndoContainer(false);
                return;
            }
            //Создаём текст названия сечения
            if (NameCut.Trim() != "" && IsDrawingDimensions)
            {
                ksRectParam rectParam = (ksRectParam)kompas.GetParamStruct(15); //Параметры прямоугольника
                document2DAPI5.ksGetObjGabaritRect(drawingGroup.Reference, rectParam); //Получение габаритного прямоугольника фигуры, полученной через площадь

                IDrawingTexts drawingTexts = drawingContainer.DrawingTexts;
                IDrawingText drawingText = drawingTexts.Add();
                IText text = (IText)drawingText;
                text.Str = NameCut;
                ITextLine textLine = text.TextLine[0];
                ITextItem textItem = textLine.TextItem[0];
                ITextFont textFont = (ITextFont)textItem;
                textFont.Underline = true;
                textItem.Update();
                ksMathPointParam pointBot = (ksMathPointParam)rectParam.GetpBot();
                ksMathPointParam pointTop = (ksMathPointParam)rectParam.GetpTop();
                document2DAPI5.ksSheetToView(pointBot.x, pointBot.y, out double xBot, out _);
                document2DAPI5.ksSheetToView(pointTop.x, pointTop.y, out double xTop, out double yTop);
                drawingText.X = (xBot + xTop) / 2;
                drawingText.Y = yTop + gapDimToDim / 2;
                drawingText.Update();
            }
            drawingGroup.Close();


            double xpaste = 0; double ypaste = 0;
            ksPhantom phantom = (ksPhantom)kompas.GetParamStruct(6);
            phantom.phantom = 1; //Указываем тип фантом "Фантом для сдвига группы"
            ksType1 type1 = (ksType1)phantom.GetPhantomParam();
            type1.gr = drawingGroup.Reference;
            //Вызываем курсор для указания точки вставки. Если была нажата Esc, прерываем вставку.
            if (document2DAPI5.ksCursorEx(null, ref xpaste, ref ypaste, phantom, null) == 0)
            {
                document2DAPI5.ksDeleteObj(type1.gr);
                document2DAPI5.ksUndoContainer(false);
                return;
            }
            document2DAPI5.ksMoveObj(drawingGroup.Reference, xpaste, ypaste);
            drawingGroup.Store();
            //Создаём макроэлемент
            if (IsMacro)
            {
                document2DAPI5.ksMacro(0);
                int macro = document2DAPI5.ksEndObj();
                document2DAPI5.ksAddObjectToMacro(macro, drawingGroup.Reference);
                //TODO Разобраться почему не работает создание макроэлемента в API7
                #region Создание макроэлемента в API7
                //IMacroObjects macroObjects = drawingContainer.MacroObjects;
                //IMacroObject macroObject = macroObjects.Add(false);
                //macroObject.DoubleClickEditable = true;
                //macroObject.HotPointsEditable = false;
                //macroObject.ExternalEditable = false;
                //macroObject.PropertyObjectEditable = false;
                //MessageBox.Show($"{macroObject.AddObjects(drawingGroup)}");
                //macroObject.Update(); 
                #endregion
            }
            drawingGroup?.Clear(true);

            document2DAPI5.ksUndoContainer(false);
        }         
        #endregion
    }
}
