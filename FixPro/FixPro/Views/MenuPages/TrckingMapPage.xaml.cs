using Acr.UserDialogs;
using Acr.UserDialogs.Infrastructure;
using FixPro.Helpers;
using FixPro.Models;
using FixPro.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using System.Xml;
using Xamarin.Essentials;
using Syncfusion.SfCalendar.XForms;


namespace FixPro.Views.MenuPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TrckingMapPage : Controls.CustomsPage
    {
        EmployeesViewModel ViewModel { get => BindingContext as EmployeesViewModel; set => BindingContext = value; }

        public EmployeeModel OneEmployee { get; set; }

        public TrckingMapPage()
        {
            InitializeComponent();
        }

        public TrckingMapPage(DataMapsModel dataMap)
        {
            InitializeComponent();

            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                   new Position(double.Parse(dataMap.Lat), double.Parse(dataMap.Long)), Distance.FromMeters(200)));
        }

        private void myMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(
                new Position(double.Parse(ViewModel.LastListmap.LastOrDefault().Lat), double.Parse(ViewModel.LastListmap.LastOrDefault().Long)), Distance.FromMeters(100)));
        }



        //int id = 0;
        //double lat = 29.7066156;
        //double log = -95.8789401;
        //private async void GetDataEmployee()
        //{
        //    //string uri = "https://projectservices.engprosoft.com/XMLData/61_2023-01-24.xml";

        //    //UserDialogs.Instance.ShowLoading();
        //    //XDocument document = XDocument.Load(uri);
        //    //UserDialogs.Instance.HideLoading();

        //    //DataSet ds = new DataSet();
        //    //ds.ReadXml(new XmlTextReader(new StringReader(document.ToString())));

        //    //DataTable employeesTable = ds.Tables[0];
        //    //List<DataMapsModel> List = (from DataRow dr in employeesTable.Rows
        //    //                            where dr["EmployeeId"].ToString() == OneEmployee.Id.ToString()
        //    //                            select new DataMapsModel()
        //    //                            {
        //    //                                Id = int.Parse(dr["Tracking_id"].ToString()),
        //    //                                EmployeeId = int.Parse(dr["EmployeeId"].ToString()),
        //    //                                Lat = dr["lat"].ToString(),
        //    //                                Long = dr["log"].ToString(),
        //    //                                Time = dr["time"].ToString(),
        //    //                                CreateDate = dr["date"].ToString(),
        //    //                                MPosition = new Position(double.Parse(dr["lat"].ToString()), double.Parse(dr["log"].ToString())),
        //    //                            }).OrderBy(c => c.Id).ToList();

        //    id += 1;

        //    lat += .0000400;
        //    log -= .0000400;


        //    DataMapsModel oMap = new DataMapsModel
        //    {
        //        Id = id,
        //        EmployeeId = 69,
        //        Lat = lat,
        //        Long = log,
        //        MPosition = new Position(lat, log),
        //    };

        //    //var cc = List;
        //    LastListmap.Clear();
        //    LastListmap.Add(oMap);


        //    ViewModel.LastListmap.Clear();
        //    ViewModel.LastListmap.Add(oMap);

        //    //myMap.ItemsSource = LastListmap;

        //    //myMap.Pins.Clear();
        //    //Pin pin = new Pin
        //    //{
        //    //    Label = id.ToString(),
        //    //    Type = PinType.Place,
        //    //    Position = new Position(lat, log)

        //    //    //Label = "1",
        //    //    //ClassId = "1",
        //    //    //StyleId = "1",
        //    //    //Type = PinType.Place,
        //    //    //Position = new Position(double.Parse(LastListmap.LastOrDefault().Lat), double.Parse(LastListmap.LastOrDefault().Long))
        //    //};
        //    //myMap.Pins.Add(pin);


        //    //MapsModel = List.LastOrDefault();

        //}

    }
}