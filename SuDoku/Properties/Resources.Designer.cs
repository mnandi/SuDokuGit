﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SuDoku.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SuDoku.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Error.
        /// </summary>
        internal static string MB_caption_err {
            get {
                return ResourceManager.GetString("MB_caption_err", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Exit.
        /// </summary>
        internal static string MB_caption_exit {
            get {
                return ResourceManager.GetString("MB_caption_exit", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Információ.
        /// </summary>
        internal static string MB_caption_info {
            get {
                return ResourceManager.GetString("MB_caption_info", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Eredmény.
        /// </summary>
        internal static string MB_caption_result {
            get {
                return ResourceManager.GetString("MB_caption_result", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Egy megoldás van!.
        /// </summary>
        internal static string MB_msg_1result {
            get {
                return ResourceManager.GetString("MB_msg_1result", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Több, legalább {0} megoldás is van!.
        /// </summary>
        internal static string MB_msg_MANYresult {
            get {
                return ResourceManager.GetString("MB_msg_MANYresult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A tábla nem oldható meg.
        /// </summary>
        internal static string MB_msg_NOresult {
            get {
                return ResourceManager.GetString("MB_msg_NOresult", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A tábla megoldása\r\nBefejezem.
        /// </summary>
        internal static string MB_msg_result {
            get {
                return ResourceManager.GetString("MB_msg_result", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized resource of type System.Drawing.Icon similar to (Icon).
        /// </summary>
        internal static System.Drawing.Icon SuDoku {
            get {
                object obj = ResourceManager.GetObject("SuDoku", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
    }
}
