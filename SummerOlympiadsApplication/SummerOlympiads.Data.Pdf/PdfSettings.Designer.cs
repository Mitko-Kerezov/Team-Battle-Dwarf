﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SummerOlympiads.Data.Pdf {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "12.0.0.0")]
    internal sealed partial class PdfSettings : global::System.Configuration.ApplicationSettingsBase {
        
        private static PdfSettings defaultInstance = ((PdfSettings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new PdfSettings())));
        
        public static PdfSettings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("../../../../PdfReports/")]
        public string ReportsPath {
            get {
                return ((string)(this["ReportsPath"]));
            }
            set {
                this["ReportsPath"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Verdana")]
        public string fontFamily {
            get {
                return ((string)(this["fontFamily"]));
            }
            set {
                this["fontFamily"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("12")]
        public int fontSize {
            get {
                return ((int)(this["fontSize"]));
            }
            set {
                this["fontSize"] = value;
            }
        }
    }
}
