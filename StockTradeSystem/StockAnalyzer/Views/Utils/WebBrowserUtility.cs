using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace StockAnalyzer.Views.Utils
{
    public static class WebBrowserUtility
    {
        public static readonly DependencyProperty BindableSourceProperty =
            DependencyProperty.RegisterAttached("BindableSource", typeof(string), typeof(WebBrowserUtility), new UIPropertyMetadata(null, BindableSourcePropertyChanged));

        public static string GetBindableSource(DependencyObject obj)
        {
            return (string)obj.GetValue(BindableSourceProperty);
        }

        public static void SetBindableSource(DependencyObject obj, string value)
        {
            obj.SetValue(BindableSourceProperty, value);
        }

        public static void BindableSourcePropertyChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            var browser = o as WebBrowser;
            if (browser != null)
            {
                string uri = e.NewValue as string;
                browser.Source = !String.IsNullOrEmpty(uri) ? new Uri(uri) : null;

                SetRestrictErrorPopup(browser);
            }
        }

        /// <summary>
        /// JavaScriptのエラーのポップアップを抑止
        /// http://msyi303.blog130.fc2.com/blog-entry-59.html
        /// </summary>
        /// <param name="browser"></param>
        private static void SetRestrictErrorPopup(WebBrowser browser)
        {
            // IWebBrowser2 の取得 プロパティから
            var axIWebBrowser2 = typeof(WebBrowser).GetProperty("AxIWebBrowser2", BindingFlags.Instance | BindingFlags.NonPublic);
            var comObj = axIWebBrowser2.GetValue(browser, null);

            // 値の設定
            comObj.GetType()
                  .InvokeMember
                  (
                     "Silent",
                     BindingFlags.SetProperty,
                     null,
                     comObj,
                    new object[] { true }
                  );

        }

    }
}
