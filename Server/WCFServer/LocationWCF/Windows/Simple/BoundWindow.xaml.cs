﻿
using Location.TModel.Location.AreaAndDev;
using LocationServices.Converters;
using LocationServices.Locations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Point = Location.TModel.Location.AreaAndDev.Point;

namespace LocationServer.Windows.Simple
{
    /// <summary>
    /// BoundWindow.xaml 的交互逻辑
    /// </summary>
    public partial class BoundWindow : Window
    {
        public BoundWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var bll = AppContext.GetLocationBll();
            bll.Bounds.Edit(Area.InitBound.ToDbModel());
        }

        //public Bound Bound { get; set; }
        public PhysicalTopology Area { get; internal set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DrawArea();
        }

        private void DrawArea()
        {
            if (Area == null) return;
            var bound = Area.InitBound;
            if(bound.MaxX==0)
                bound.SetMinMaxXY();

            PropertyGrid1.SelectedObject = bound;

            ListBox1.ItemsSource = null;
            ListBox1.ItemsSource = Area.GetPoints();

            ListBox2.ItemsSource = null;
            ListBox2.ItemsSource = Area.GetAbsolutePoints();

            
            BoundCanvas1.Show(bound);


            //pcCenter.X=bound.
            pcSize.X = bound.GetSizeX();
            pcSize.Y = bound.GetSizeY();

            pcCenter.X = bound.GetCenterX();
            pcCenter.Y = bound.GetCenterY();
        }

        private void MenuAdd_Click(object sender, RoutedEventArgs e)
        {
            var bound = Area.InitBound;
            

            //var point = ListBox1.SelectedItem as Location.TModel.Location.AreaAndDev.Point;
            var point = new Point();
            var win = new ItemInfoWindow(point);
            win.SaveEvent += (w, p) =>
            {
                var p1=p as Point;
                var bll = AppContext.GetLocationBll();
                var r=new AreaService(bll).AddPoint(Area,p1);
                return r != null;
            };
            if (win.ShowDialog()==true)
            {
                DrawArea();
            }
        }

        private void MenuEdit_Click(object sender, RoutedEventArgs e)
        {
            var point = ListBox1.SelectedItem as Location.TModel.Location.AreaAndDev.Point;
            //var point = new DbModel.Location.AreaAndDev.Point();
            var win = new ItemInfoWindow(point);
            win.SaveEvent += (w, p) =>
            {
                var p1 = p as Point;
                var bll = AppContext.GetLocationBll();
                var r = new AreaService(bll).EditPoint(p1);
                return r != null;
            };
            if (win.ShowDialog() == true)
            {
                DrawArea();
            }
        }

        private void MenuDelete_Click(object sender, RoutedEventArgs e)
        {
            var point = ListBox1.SelectedItem as Location.TModel.Location.AreaAndDev.Point;
            Area.InitBound.Points.Remove(point);
            var bll = AppContext.GetLocationBll();
            bll.Points.DeleteById(point.Id);
            DrawArea();


        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var point = ListBox1.SelectedItem as Point;
            BoundCanvas1.SelectPoint(point);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BoundCanvas1.RefreshBound();
        }

        private void BtnModifySize_Click(object sender, RoutedEventArgs e)
        {
            //pcSize.X = bound.GetSizeX();
            //pcSize.Y = bound.GetSizeY();

            //pcCenter.X = bound.GetCenterX();
            //pcCenter.Y = bound.GetCenterY();

            var bound = Area.InitBound;

            //bound.ModifSize(pcCenter.X, pcCenter.Y, pcSize.X, pcSize.Y);

            new AreaService().ModifySize(bound,pcCenter.X, pcCenter.Y, pcSize.X, pcSize.Y);
        }
    }
}
