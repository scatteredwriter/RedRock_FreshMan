using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Windows.UI.Core;
using Windows.UI.Xaml.Shapes;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace RedRock_Freshman.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FengCaiPage : Page
    {
        private int pivot_index;
        private int zuzhi_listview_index;
        private double[] pivotitem1_ver_offest;

        private ViewModel.FengCaiViewModel viewmodel;

        public FengCaiPage()
        {
            this.InitializeComponent();
            pivot_index = 0;
            zuzhi_listview_index = 0;
            viewmodel = new ViewModel.FengCaiViewModel();
            this.DataContext = viewmodel;
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                await PivotItem1_First_Step();
                pivotitem1_ver_offest = new double[viewmodel.ZuZhi.Count];
                Dispatcher?.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    PivotItem1_Add_Content(1);
                });
                await Task.Delay(100);
                zuzhi_listview.SelectedIndex = pivot.SelectedIndex = 0;
            }
        }

        private async Task PivotItem1_First_Step()
        {
            StorageFile file;
            string json = "";
            JObject json_object;

            #region 得到Header列表
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Json/fengcai_header_lists.json", UriKind.Absolute));
            json = await FileIO.ReadTextAsync(file);
            json_object = (JObject)JsonConvert.DeserializeObject(json);
            JArray headers = (JArray)json_object["header_lists"];
            ObservableCollection<Model.fengcaiheaders> header_lists = new ObservableCollection<Model.fengcaiheaders>();
            for (int i = 0; i < headers.Count; i++)
            {
                Model.fengcaiheaders item = new Model.fengcaiheaders();
                item.header = headers[i]["header"].ToString();
                header_lists.Add(item);
            }
            viewmodel.Header = header_lists;
            #endregion

            #region 得到组织列表
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Json/fengcai_zuzhi_lists.json", UriKind.Absolute));
            json = await FileIO.ReadTextAsync(file);
            json_object = (JObject)JsonConvert.DeserializeObject(json);
            JArray zuzhis = (JArray)json_object["zuzhi_lists"];
            ObservableCollection<Model.zuzhi> zuzhi_lists = new ObservableCollection<Model.zuzhi>();
            for (int i = 0; i < zuzhis.Count; i++)
            {
                Model.zuzhi item = new Model.zuzhi();
                item.zuzhi_name = zuzhis[i]["zuzhi"].ToString();
                zuzhi_lists.Add(item);
            }
            viewmodel.ZuZhi = zuzhi_lists;
            #endregion

            #region 得到组织介绍
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Json/fengcai_zuzhi_intro.json", UriKind.Absolute));
            json = await FileIO.ReadTextAsync(file);
            json_object = (JObject)JsonConvert.DeserializeObject(json);
            JArray zuzhi_intros = (JArray)json_object["zuzhi_intro"];
            ObservableCollection<Model.zuzhi_intro> intro_lists = new ObservableCollection<Model.zuzhi_intro>();
            for (int i = 0; i < zuzhi_intros.Count; i++)
            {
                Model.zuzhi_intro item = new Model.zuzhi_intro();
                item.zuzhi = new ObservableCollection<string>();
                JArray zuzhi_item = (JArray)zuzhi_intros[i]["zuzhi"];
                for (int j = 0; j < zuzhi_item.Count; j++)
                {
                    item.zuzhi.Add(zuzhi_item[j]["duanluo"].ToString());
                }
                intro_lists.Add(item);
            }
            viewmodel.Zuzhi_Intro = intro_lists;
            #endregion

            #region 得到最美重邮文字内容
            file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Json/fengcai_zuimei.json", UriKind.Absolute));
            json = await FileIO.ReadTextAsync(file);
            json_object = (JObject)JsonConvert.DeserializeObject(json);
            JArray zuimei = (JArray)json_object["zuimei"];
            ObservableCollection<string> zuimei_lists = new ObservableCollection<string>();
            for (int i = 0; i < zuimei.Count; i++)
            {
                string content = zuimei[i]["content"].ToString();
                zuimei_lists.Add(content);
            }
            viewmodel.ZuiMei = zuimei_lists;
            #endregion
        }

        private void PivotItem1_Add_Content(int p)
        {
            zuzhi_content.Children.Clear();
            if (p == 1)
            {
                for (int i = 0; i < viewmodel.Zuzhi_Intro[0].zuzhi.Count; i++)
                {
                    if (viewmodel.Zuzhi_Intro[0].zuzhi[i].Contains("【"))
                    {
                        zuzhi_content.Children.Add(New_TextBlock(1, viewmodel.Zuzhi_Intro[0].zuzhi[i]));
                    }
                    else
                    {
                        zuzhi_content.Children.Add(New_TextBlock(2, viewmodel.Zuzhi_Intro[0].zuzhi[i]));
                    }
                }
            }
            else if (p == 2)
            {
                for (int i = 0; i < viewmodel.Zuzhi_Intro[zuzhi_listview.SelectedIndex].zuzhi.Count; i++)
                {
                    if (viewmodel.Zuzhi_Intro[zuzhi_listview.SelectedIndex].zuzhi[i].Contains("【"))
                    {
                        zuzhi_content.Children.Add(New_TextBlock(1, viewmodel.Zuzhi_Intro[zuzhi_listview.SelectedIndex].zuzhi[i]));
                    }
                    else
                    {
                        zuzhi_content.Children.Add(New_TextBlock(2, viewmodel.Zuzhi_Intro[zuzhi_listview.SelectedIndex].zuzhi[i]));
                    }
                }
            }
        }

        private TextBlock New_TextBlock(int p, string content)
        {
            TextBlock tb = new TextBlock();
            switch (p)
            {
                case 1: //较重标题
                    {
                        tb.Text = content.Substring(1, (content.LastIndexOf('】') - content.IndexOf('【') - 1));
                        tb.Foreground = App.APPTheme.Content_Header_Color_Brush;
                        tb.FontSize = 16;
                        tb.Margin = new Thickness(0, 3, 0, 8);
                    }; break;
                case 2: //普通内容
                    {
                        tb.Text = content;
                        tb.Foreground = App.APPTheme.Gary_Color_Brush;
                        tb.FontSize = 15;
                        tb.LineHeight = 26;
                    }; break;
            }
            tb.CharacterSpacing = 100;
            tb.TextWrapping = TextWrapping.Wrap;
            return tb;
        }

        private void pivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (pivot.SelectedIndex < 0)
                {
                    pivot.SelectedIndex = pivot_index = 0;
                }
                (((pivot.Items[pivot_index] as PivotItem).Header as Grid).Children[0] as TextBlock).Foreground = App.APPTheme.Content_Header_Color_Brush;
                (((pivot.Items[pivot_index] as PivotItem).Header as Grid).Children[1] as Line).Visibility = Visibility.Collapsed;
                pivot_index = pivot.SelectedIndex;
                (((pivot.Items[pivot_index] as PivotItem).Header as Grid).Children[0] as TextBlock).Foreground = App.APPTheme.APP_Color_Brush;
                (((pivot.Items[pivot_index] as PivotItem).Header as Grid).Children[1] as Line).Visibility = Visibility.Visible;
            }
            catch (Exception)
            {
                return;
            }
        }

        private void zuzhi_listview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pivotitem1_ver_offest[zuzhi_listview_index] = zuzhi_sc.VerticalOffset;
            PivotItem1_Add_Content(2);
            if (pivotitem1_ver_offest[zuzhi_listview.SelectedIndex] == 0.0)
            {
                zuzhi_sc.ChangeView(null, 0.0, null, true);
            }
            zuzhi_listview_index = zuzhi_listview.SelectedIndex;
        }
    }
}
