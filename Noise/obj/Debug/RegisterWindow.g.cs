﻿#pragma checksum "..\..\RegisterWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B69767C6C6E7574ABC293D0E6DAF6FB343D5FD1874FAC68CAB8A8E7B7D8D58DE"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Noise;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Noise {
    
    
    /// <summary>
    /// RegisterWindow
    /// </summary>
    public partial class RegisterWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid MainContent;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox usernameBox;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox emailBox;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox passwordBox;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock secondTimeText;
        
        #line default
        #line hidden
        
        
        #line 97 "..\..\RegisterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock errorMessage;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Noise;component/registerwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\RegisterWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\RegisterWindow.xaml"
            ((Noise.RegisterWindow)(target)).MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.dragAWindow);
            
            #line default
            #line hidden
            return;
            case 2:
            this.MainContent = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.usernameBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 41 "..\..\RegisterWindow.xaml"
            this.usernameBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.usernameBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.emailBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 60 "..\..\RegisterWindow.xaml"
            this.emailBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.emailBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.passwordBox = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 80 "..\..\RegisterWindow.xaml"
            this.passwordBox.KeyDown += new System.Windows.Input.KeyEventHandler(this.passwordBox_KeyDown);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 86 "..\..\RegisterWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SignUp_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.secondTimeText = ((System.Windows.Controls.TextBlock)(target));
            
            #line 93 "..\..\RegisterWindow.xaml"
            this.secondTimeText.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.secondTimeText_PreviewMouseDown);
            
            #line default
            #line hidden
            return;
            case 8:
            this.errorMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

