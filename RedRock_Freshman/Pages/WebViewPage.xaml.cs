using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace RedRock_Freshman.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class WebViewPage : Page
    {
        private Uri uri;

        public WebViewPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                if (e.Parameter is Model.yuanchuang)
                {
                    title.Text = (e.Parameter as Model.yuanchuang).name;
                    uri = new Uri((e.Parameter as Model.yuanchuang).video_url);
                }
            }
        }

        private void webview_Loaded(object sender, RoutedEventArgs e)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
            httpRequestMessage.Headers.Append("User-Agent", "Mozilla/5.0 (Windows Phone 10.0; Android 4.2.1; Microsoft; Lumia950) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/46.0.2486.0 Mobile Safari/537.36 Edge/13.10586");
            webview.NavigateWithHttpRequestMessage(httpRequestMessage);
        }

        private void back_but_Click(object sender, RoutedEventArgs e)
        {
            FengCaiPage.fengcaipage.WebView_Back();
        }
    }
}
