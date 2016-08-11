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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace RedRock_Freshman.Pages
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class FirstPage : Page
    {
        private ViewModel.FirstPageViewModel viewmodel = new ViewModel.FirstPageViewModel();

        public static FirstPage firstpage;

        public FirstPage()
        {
            this.InitializeComponent();
            this.NavigationCacheMode = NavigationCacheMode.Enabled;
            this.DataContext = viewmodel;
            firstpage = this;
            go_back_sb.Completed += Go_back_sb_Completed;
            this.SizeChanged += FirstPage_SizeChanged;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (e.NavigationMode == NavigationMode.New)
            {
                viewmodel.Page_Height = this.Height;
                viewmodel.Page_Width = this.Width;
            }
        }

        public void Second_Page_Forwoard() //页面前进方法
        {
            (go_forward_sb.Children[0] as DoubleAnimation).From = second_frame_trans.X;
            go_forward_sb.Begin();
        }

        public void Second_Page_Back() //页面后退方法
        {
            (go_back_sb.Children[0] as DoubleAnimation).To = second_frame.ActualWidth;
            go_back_sb.Begin();
        }

        private void Go_back_sb_Completed(object sender, object e)
        {
            second_frame.SetNavigationState("1,0");
        }

        private void FirstPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            viewmodel.Page_Height = e.NewSize.Height;
            viewmodel.Page_Width = e.NewSize.Width;
            if (second_frame.GetNavigationState() == "1,0")
            {
                second_frame_trans.X = e.NewSize.Width;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int index = 0;
            for (int i = 0; i < ((sender as Button).Parent as Grid).Children.Count; i++)
            {
                if (((sender as Button).Parent as Grid).Children[i] == sender as Button)
                {
                    index = i;
                }
            }
            Second_Page_Forwoard();
            switch (index)
            {
                case 0:
                    {
                        second_frame.Navigate(typeof(StrategyPage));
                    }; break;
                case 1:
                    {

                    }; break;
                case 2:
                    {
                        second_frame.Navigate(typeof(FengCaiPage));
                    }; break;
            }
        }

        private void back_but_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
