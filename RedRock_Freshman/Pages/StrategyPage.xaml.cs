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

             PivotItem1_First_Step();
            SPivot.SelectedIndex = 0;
            this.DataContext = viewmodel;

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
        private async void PivotItem1_First_Step()
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


        }
    }
}
