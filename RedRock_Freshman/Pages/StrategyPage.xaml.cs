﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedRock_Freshman.Model;
using RedRock_Freshman.Resource;
using RedRock_Freshman.ViewModel;
using SQLite.Net;
using SQLite.Net.Platform.WinRT;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
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
        string XSQString = "";
        string LXQString = "";


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
                #region 初始化
                for (int i = 0; i < 8; i++)
                {
                    App.isLoading[i] = false;
                }
                for (int i = 0; i < 4; i++)
                {
                    App.isReduced[i] = true;
                }
                viewmodel.Anquan = "";
                viewmodel.Jiangxuejin = "";
                viewmodel.Ruxue = "";
                viewmodel.Xueshengshouce = "";

                viewmodel.Icon = new ObservableCollection<string>();
                viewmodel.Text = new ObservableCollection<string>();
                viewmodel.Icon.Add("");
                viewmodel.Icon.Add("");
                viewmodel.Icon.Add("");
                viewmodel.Icon.Add("");
                viewmodel.Text.Add("展开");
                viewmodel.Text.Add("展开");
                viewmodel.Text.Add("展开");
                viewmodel.Text.Add("展开");

                viewmodel.AllQQInfo = new Allstring();
                viewmodel.QdContent = new ObservableCollection<qindan_content>();
                viewmodel.QsIntroduce = new ObservableCollection<qinshiIntroduce>();
                viewmodel.RichangContent = new ObservableCollection<richangshenghuo>();
                viewmodel.MsContent = new ObservableCollection<zhoubianmeishi>();
                viewmodel.MjContent = new ObservableCollection<zhoubianmeijing>();
                #endregion
                await PivotItem1_First_Step();

                //await Task.Delay(100);
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
            if (SPivot.SelectedIndex == 4 && !App.isLoading[4])
            {
                XSQ_qqGroup();
                viewmodel.AllQQInfo.XYQ_All = XSQString;

                LXQ_qqGroup();
                viewmodel.AllQQInfo.LXQ_All = LXQString;
                App.isLoading[4] = true;
            }
            else if (SPivot.SelectedIndex == 3 && !App.isLoading[3])
            {
                Qindan_Get();
                App.isLoading[3] = true;
            }
            else if (SPivot.SelectedIndex == 2 && !App.isLoading[2])
            {
                qinshi_Get();
                App.isLoading[2] = true;
            }
            else if (SPivot.SelectedIndex == 5 && !App.isLoading[5])
            {
                richang_Get();
                App.isLoading[5] = true;
            }
            else if (SPivot.SelectedIndex == 6 && !App.isLoading[6])
            {
                meishi_Get();
                App.isLoading[6] = true;
            }
            else if(SPivot.SelectedIndex==7&&!App.isLoading[7])
            {
                meijing_Get();
                App.isLoading[7] = true;
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

        async void Qindan_Get()
        {
            StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Json/必备清单.json", UriKind.Absolute));
            string json = await FileIO.ReadTextAsync(file);
            JObject json_object = JObject.Parse(json);
            JArray jArray = (JArray)json_object["qindan"];
            ObservableCollection<Model.qindan_content> qindanTemp = new ObservableCollection<qindan_content>();
            qindanTemp.Add(new qindan_content { content = jArray[0]["body1"][0]["content"].ToString() });
            qindanTemp.Add(new qindan_content { content = jArray[1]["body2"][0]["content1"].ToString() });
            qindanTemp.Add(new qindan_content { content = jArray[1]["body2"][1]["content2"].ToString() });
            qindanTemp.Add(new qindan_content { content = jArray[1]["body2"][2]["content3"].ToString() });
            qindanTemp.Add(new qindan_content { content = jArray[1]["body2"][3]["content4"].ToString() });
            qindanTemp.Add(new qindan_content { content = jArray[1]["body2"][4]["content5"].ToString() });
            for (int i = 0; i < 6; i++)
            {
                viewmodel.QdContent.Add(new qindan_content { content = qindanTemp[i].content.ToString() });
            }

        }
        List<KeyValuePair<string, string>> paramListMJ = new List<KeyValuePair<string, string>>();
        public async Task<string> mjHttpClient()
        {
            paramListMJ.Add(new KeyValuePair<string, string>("page", "0"));
            paramListMJ.Add(new KeyValuePair<string, string>("size", "8"));
            string content = "";
            await Task.Run(() =>
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage responese;
                responese = httpClient.PostAsync(new Uri(Api.zhoubianmeijing_api), new FormUrlEncodedContent(paramListMJ)).Result;
                if (responese.StatusCode == HttpStatusCode.OK)
                    content = responese.Content.ReadAsStringAsync().Result;
            });
            return content;
        }
        private async void meijing_Get()
        {
            string content = "";
            content = await mjHttpClient();
            JObject json = JObject.Parse(content);
            for (int i = 0; i < 8; i++)
            {
                viewmodel.MjContent.Add(new zhoubianmeijing
                {
                    Name = json["data"][i]["name"].ToString(),
                    Tourroute = json["data"][i]["tourroute"].ToString(),
                    Introduction = json["data"][i]["introduction"].ToString(),
                    Uri = json["data"][i]["photo"][0]["photo_src"].ToString()
                });
            }
        }

        List<KeyValuePair<string, string>> paramListRC = new List<KeyValuePair<string, string>>();
        public async Task<string> rcHttpClient()
        {
            paramListRC.Add(new KeyValuePair<string, string>("page", "0"));
            paramListRC.Add(new KeyValuePair<string, string>("size", "19"));
            string content = "";
            await Task.Run(() =>
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response;
                response = httpClient.PostAsync(new Uri(Api.richangshenghuo_api), new FormUrlEncodedContent(paramListRC)).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    content = response.Content.ReadAsStringAsync().Result;

            });
            return content;
        }
        List<KeyValuePair<string, string>> paramListMS = new List<KeyValuePair<string, string>>();
        public async Task<string> msHttpClient()
        {
            paramListMS.Add(new KeyValuePair<string, string>("page", "0"));
            paramListMS.Add(new KeyValuePair<string, string>("size", "60"));
            string content = "";
            await Task.Run(() =>
            {
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response;
                response = httpClient.PostAsync(new Uri(Api.zhoubianmeishi_api), new FormUrlEncodedContent(paramListMS)).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                    content = response.Content.ReadAsStringAsync().Result;
            });
            return content;

        }
        private async void meishi_Get()
        {
            string content = "";
            content = await msHttpClient();
            JObject json = JObject.Parse(content);
            for (int i = 0; i < 60; i++)
            {
                viewmodel.MsContent.Add(new zhoubianmeishi
                {
                    Name = json["data"][i]["name"].ToString(),
                    Address = json["data"][i]["address"].ToString(),
                    Introduction = json["data"][i]["introduction"].ToString(),
                    Uri = json["data"][i]["photo"][0]["photo_src"].ToString()
                });
            }
        }
        private async void richang_Get()
        {
            string content = await rcHttpClient();
            JObject json = JObject.Parse(content);
            for (int i = 0; i < 19; i++)
            {
                try
                {
                    viewmodel.RichangContent.Add(new richangshenghuo
                    {
                        Name = json["data"][i]["name"].ToString(),
                        Address = json["data"][i]["address"].ToString(),
                        Uri = json["data"][i]["photo"][0]["photo_src"].ToString(),
                    });
                }
                catch
                {

                }

            }

        }
        public async Task<string> qsHttpClient()
        {
            string content = "";
            try
            {
                return await Task.Run(() =>
                {
                    HttpClient httpClient = new HttpClient();
                    HttpResponseMessage response = httpClient.GetAsync(new Uri(Api.susheqinkuang_api)).Result;
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                        content = response.Content.ReadAsStringAsync().Result;
                    return content;
                });
            }
            catch
            {
                Debug.Write("请求失败");
                return "";
            }


        }
        private async void qinshi_Get()
        {
            string content = await qsHttpClient();
            JObject json = JObject.Parse(content);
            int total = int.Parse(json["total"].ToString());
            for (int i = 0; i < total; i++)
            {
                try
                {

                    viewmodel.QsIntroduce.Add(new qinshiIntroduce { Introduction = json["data"][i]["introduction"].ToString(), Uri = json["data"][i]["photo"][0]["photo_src"].ToString() });
                }
                catch
                {
                    viewmodel.QsIntroduce.Add(new qinshiIntroduce { Introduction = json["data"][i]["introduction"].ToString(), Uri = "ms-appx:///Assets/StoreLogo.png" });

                }

            };
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
                viewmodel.Text[0] = "收起";
                viewmodel.Icon[0] = "";
            }
            else
            {
                App.isReduced[0] = true;
                viewmodel.Text[0] = "展开";
                viewmodel.Icon[0] = "";
            }
            StorageFile anquan_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/anquan.txt", UriKind.Absolute));
            viewmodel.Anquan = await FileIO.ReadTextAsync(anquan_File);
        }

        private async void ruxueButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.isReduced[1])
            {

                App.isReduced[1] = false;
                viewmodel.Text[1] = "收起";
                viewmodel.Icon[1] = "";

            }
            else
            {
                App.isReduced[1] = true;
                viewmodel.Text[1] = "展开";
                viewmodel.Icon[1] = "";
            }
            StorageFile ruxue_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/ruxue.txt", UriKind.Absolute));
            viewmodel.Ruxue = await FileIO.ReadTextAsync(ruxue_File);
        }

        private async void jiangxuejinButton_Click(object sender, RoutedEventArgs e)
        {
            if (App.isReduced[2])
            {

                App.isReduced[2] = false;
                viewmodel.Text[2] = "收起";
                viewmodel.Icon[2] = "";

            }
            else
            {
                App.isReduced[2] = true;
                viewmodel.Text[2] = "展开";
                viewmodel.Icon[2] = "";
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
                jidianGrid.Height = new GridLength(170);
                viewmodel.Text[3] = "收起";
                viewmodel.Icon[3] = "";
            }
            else
            {
                App.isReduced[3] = true;
                JDhtml.Visibility = Visibility.Collapsed;
                jidianGrid.Height = new GridLength(0);
                viewmodel.Text[3] = "展开";
                viewmodel.Icon[3] = "";

            }
            StorageFile xueshengshouce_File = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///Notepad/xueshengshouce.txt", UriKind.Absolute));
            viewmodel.Xueshengshouce = await FileIO.ReadTextAsync(xueshengshouce_File);
        }
        private void LXQ_qqGroup()
        {
            using (var conn = Model.getDB.GetDblxqConnection())
            {

                int i = 0;
                var query = conn.Table<LXQ>();
                foreach (var q in query)
                {

                    LXQString += q.name + "   " + q.qq_num;
                    if (i < query.Count() - 1)
                        LXQString += '\n';
                    i++;
                }

            }

        }
        private void XSQ_qqGroup()
        {
            using (var conn = Model.getDB.GetDbxsqConnection())
            {
                int i = 0;
                var query = conn.Table<XYXS>();
                foreach (var q in query)
                {
                    XSQString += q.name + "   " + q.qq_num;
                    if (i < query.Count() - 1)
                        XSQString += '\n';
                    i++;
                }
            }

        }
        Point Point_new = new Point();
        Point Point_old = new Point();
        private void back_but_Click(object sender, RoutedEventArgs e)
        {
            FirstPage.firstpage.Second_Page_Back();
        }
        private void bigImage_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            isPoint = false;

        }
        bool isPoint = false;
        private void bigImage_PointerMoved(object sender, PointerRoutedEventArgs e)
        {
            if (isPoint)
            {
                Point_new = e.GetCurrentPoint(scrollViewer).Position;
                scrollViewer.ChangeView(scrollViewer.HorizontalOffset - Point_new.X + Point_old.X, scrollViewer.VerticalOffset - Point_new.Y + Point_old.Y, scrollViewer.ZoomFactor, true);
                //scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - Point_new.X + Point_old.X);
                //scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - Point_new.Y + Point_old.Y);
                Point_old = Point_new;
            }
        }
        private void bigImage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            Point_old = e.GetCurrentPoint(scrollViewer).Position;
            isPoint = true;
        }

        private void bigImage_PointerReleased(object sender, PointerRoutedEventArgs e)
        {
            isPoint = false;
        }
        private void bigImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            showimage.Visibility = Visibility.Collapsed;

        }

        private void Rectangle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ImageBrush s = ((sender as Rectangle).Fill as ImageBrush);
            scrollViewer.ChangeView(scrollViewer.HorizontalOffset, scrollViewer.VerticalOffset, (float)0.7);
            bigImage.Source = s.ImageSource;

            showimage.Visibility = Visibility.Visible;
        }
    }
}
