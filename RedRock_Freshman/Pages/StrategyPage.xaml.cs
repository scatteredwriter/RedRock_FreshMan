using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedRock_Freshman.Model;
using RedRock_Freshman.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RedRock_Freshman.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StrategyPage : Page
    {
        private int pivot_index;


        StrategyViewModel viewmodel = new StrategyViewModel();

        public StrategyPage()
        {

            this.InitializeComponent();

            SPivot.SelectedIndex = 0;
            this.DataContext = viewmodel;

        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            base.OnNavigatedTo(e);
            if (e.NavigationMode == NavigationMode.New)
            {
                viewmodel.Anquan = "";
                viewmodel.Jiangxuejin = "";
                viewmodel.Ruxue = "";
                viewmodel.Xueshengshouce = "";

                await PivotItem1_First_Step();

                await Task.Delay(100);
            }
        }
        private void SPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (SPivot.SelectedIndex < 0)
                {
                    SPivot.SelectedIndex = pivot_index = 0;
                }
                (((SPivot.Items[pivot_index] as PivotItem).Header as Grid).Children[0] as TextBlock).Foreground = App.APPTheme.Content_Header_Color_Brush;
                (((SPivot.Items[pivot_index] as PivotItem).Header as Grid).Children[1] as Line).Visibility = Visibility.Collapsed;
                pivot_index = SPivot.SelectedIndex;
                (((SPivot.Items[pivot_index] as PivotItem).Header as Grid).Children[0] as TextBlock).Foreground = App.APPTheme.APP_Color_Brush;
                (((SPivot.Items[pivot_index] as PivotItem).Header as Grid).Children[1] as Line).Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                return;
            }
        }
        private async Task PivotItem1_First_Step()
        {
            StorageFile file;
            string json = "";
            JObject json_object;
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Json/strategy_header_lists.json", UriKind.Absolute));
            json = await FileIO.ReadTextAsync(file);
            json_object = (JObject)JsonConvert.DeserializeObject(json);
            JArray headers = (JArray)json_object["header_lists"];
            ObservableCollection<Model.StrategyHeader> header_lists = new ObservableCollection<StrategyHeader>();
            for (int i = 0; i < headers.Count; i++)
            {
                Model.StrategyHeader item = new Model.StrategyHeader();
                item.header = headers[i]["header"].ToString();
                header_lists.Add(item);
            }
            viewmodel.Header = header_lists;

            Ruxue_Get();

        }


        async void Ruxue_Get()
        {
            #region 入学须知
            StorageFile anquan_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/anquan.txt", UriKind.Absolute));
            StorageFile ruxue_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/ruxue.txt", UriKind.Absolute));
            StorageFile jiangxuejin_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/jiangxuejin.txt", UriKind.Absolute));
            StorageFile xueshengshouce_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/xueshengshouce.txt", UriKind.Absolute));


            viewmodel.Anquan = await FileIO.ReadTextAsync(anquan_File);
            viewmodel.Ruxue = await FileIO.ReadTextAsync(ruxue_File);
            viewmodel.Jiangxuejin = await FileIO.ReadTextAsync(jiangxuejin_File);
            viewmodel.Xueshengshouce = await FileIO.ReadTextAsync(xueshengshouce_File);

            #endregion
        }

        private async void anquanButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.isReduced[0])
            {

                App.isReduced[0] = false;

            }
            else
            {
                App.isReduced[0] = true;
            }
            StorageFile anquan_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/anquan.txt", UriKind.Absolute));
            viewmodel.Anquan = await FileIO.ReadTextAsync(anquan_File);
        }

        private async void ruxueButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.isReduced[1])
            {

                App.isReduced[1] = false;

            }
            else
            {
                App.isReduced[1] = true;
            }
            StorageFile ruxue_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/ruxue.txt", UriKind.Absolute));
            viewmodel.Ruxue = await FileIO.ReadTextAsync(ruxue_File);
        }

        private async void jiangxuejinButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.isReduced[2])
            {

                App.isReduced[2] = false;

            }
            else
            {
                App.isReduced[2] = true;
            }
            StorageFile jiangxuejin_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/jiangxuejin.txt", UriKind.Absolute));
            viewmodel.Jiangxuejin = await FileIO.ReadTextAsync(jiangxuejin_File);

        }

        private async void xueshengshouceButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.isReduced[3])
            {

                App.isReduced[3] = false;
                JDhtml.Visibility = Visibility.Visible;
                jidianGrid.Height =new GridLength(170);
            }
            else
            {
                App.isReduced[3] = true;
                JDhtml.Visibility = Visibility.Collapsed;
                jidianGrid.Height = new GridLength(0);

            }
            StorageFile xueshengshouce_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/xueshengshouce.txt", UriKind.Absolute));
            viewmodel.Xueshengshouce = await FileIO.ReadTextAsync(xueshengshouce_File);
        }
    }
}
