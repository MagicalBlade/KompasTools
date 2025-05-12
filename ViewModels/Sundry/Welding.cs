using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DocumentFormat.OpenXml.EMMA;
using Kompas6API5;
using KompasAPI7;
using KompasTools.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace KompasTools.ViewModels.Sundry
{
    internal partial class Welding : ObservableObject
    {
        [ObservableProperty]
        private string _thickness = "20";

        [RelayCommand]
        public void Test()
        {
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
            LType lType = new("С12-А", "ГОСТ 14771", activeView, thx, 50);
            
            document2DAPI5.ksUndoContainer(false);



        }
        class Weld_Cutting
        {
            private string _name;
            private string _gostName;
            private IView _view;
            private double _thickness;
            private double _angle;

            private protected string Name { get => _name; set => _name = value; }
            private protected string GostName { get => _gostName; set => _gostName = value; }
            private protected IView View { get => _view; set => _view = value; }
            private protected double Thickness { get => _thickness; set => _thickness = value; }
            private protected double Angle { get => _angle; set => _angle = value; }

            public Weld_Cutting(string name, string gostName, IView view, double thickness, double angle)
            {
                Name = name;
                GostName = gostName;
                View = view;
                Thickness = thickness;
                Angle = angle;
            }


            /// <summary>
            /// Начертить линию
            /// </summary>
            /// <param name="x1"></param>
            /// <param name="y1"></param>
            /// <param name="x2"></param>
            /// <param name="y2"></param>
            private protected void DrawLineSegment(double x1, double y1, double x2, double y2)
            {
                IDrawingContainer drawingContainer = (IDrawingContainer)View;
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
            private protected void DrawLineDimension(double x1, double y1, double x2, double y2, double x3, double y3)
            {
                ISymbols2DContainer symbols2DContainer = (ISymbols2DContainer)View;
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
        }

        class KType : Weld_Cutting
        {
            public KType(string name, string gostName, IView view, double thickness, double angle) : base(name, gostName, view, thickness, angle)
            {
            }

        }

        class LType : Weld_Cutting
        {
            public LType(string name, string gostName, IView view, double thickness, double angle) : base(name, gostName, view, thickness, angle)
            {
                double ldop = 20; //Удлинение вида разделки
                double dimGap = 8 / View.Scale;//Зазор до размерной линии
                DrawLineSegment(0, 0, 0, 2);
                double xangle = (thickness - 2) * Math.Tan(angle * Math.PI / 180);
                DrawLineSegment(0, 2, xangle, thickness);
                DrawLineSegment(xangle, thickness, xangle + ldop, thickness);
                DrawLineSegment(0, 0, xangle + ldop, 0);

                DrawLineDimension(0, 0, 0, 2, 0 - dimGap, -1);
            }
        }
    }    
}
