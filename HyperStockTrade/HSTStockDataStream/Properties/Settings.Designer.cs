﻿//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはツールによって生成されました。
//     ランタイム バージョン:4.0.30319.18444
//
//     このファイルへの変更は、以下の状況下で不正な動作の原因になったり、
//     コードが再生成されるときに損失したりします。
// </auto-generated>
//------------------------------------------------------------------------------

namespace HSTStockDataStream.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://k-db.com/site/download.aspx?date={0}-{1}-{2}&p=stockT&download=csv")]
        public string StockDataDownloadSiteURL1 {
            get {
                return ((string)(this["StockDataDownloadSiteURL1"]));
            }
            set {
                this["StockDataDownloadSiteURL1"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("StockData\\RSSStockDataFull\\StockData")]
        public string DownloadFolderPathFromDesktop {
            get {
                return ((string)(this["DownloadFolderPathFromDesktop"]));
            }
            set {
                this["DownloadFolderPathFromDesktop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("StockData\\RSSStockDataFull\\StockData\\Complete")]
        public string InsertCompleteFolderPathFromDesktop {
            get {
                return ((string)(this["InsertCompleteFolderPathFromDesktop"]));
            }
            set {
                this["InsertCompleteFolderPathFromDesktop"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("StockData\\RSSStockDataFull\\StockData\\Abort")]
        public string InsertAbortFolderPathFromDesktop {
            get {
                return ((string)(this["InsertAbortFolderPathFromDesktop"]));
            }
            set {
                this["InsertAbortFolderPathFromDesktop"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.SpecialSettingAttribute(global::System.Configuration.SpecialSetting.ConnectionString)]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=POOHACE-VAIO\\SQL2012EXPRESS;Initial Catalog=StockData;Integrated Secu" +
            "rity=True")]
        public string StockDataConnectionString {
            get {
                return ((string)(this["StockDataConnectionString"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("http://k-db.com/stocks/{0}-{1}-{2}?download=csv")]
        public string StockDataDownloadSiteURL2 {
            get {
                return ((string)(this["StockDataDownloadSiteURL2"]));
            }
            set {
                this["StockDataDownloadSiteURL2"] = value;
            }
        }
    }
}
