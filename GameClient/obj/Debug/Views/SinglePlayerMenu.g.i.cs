﻿#pragma checksum "..\..\..\Views\SinglePlayerMenu.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "DFA130587E44B7BACD67C0E3725F0DB7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GameClient.Views;
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


namespace GameClient.Views {
    
    
    /// <summary>
    /// SinglePlayerMenu
    /// </summary>
    public partial class SinglePlayerMenu : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\Views\SinglePlayerMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock mazeNameBlock;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\Views\SinglePlayerMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox MazeNameBox;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\Views\SinglePlayerMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock NumOfRows;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\Views\SinglePlayerMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button okButton;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\Views\SinglePlayerMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock title;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\SinglePlayerMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock numOfRowsBlock;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\Views\SinglePlayerMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox numOfRowsBox;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\Views\SinglePlayerMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock numOfColsBlock;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\SinglePlayerMenu.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox numOfCols;
        
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
            System.Uri resourceLocater = new System.Uri("/GameClient;component/views/singleplayermenu.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\SinglePlayerMenu.xaml"
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
            this.mazeNameBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.MazeNameBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.NumOfRows = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.okButton = ((System.Windows.Controls.Button)(target));
            
            #line 15 "..\..\..\Views\SinglePlayerMenu.xaml"
            this.okButton.Click += new System.Windows.RoutedEventHandler(this.okButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.title = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            this.numOfRowsBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.numOfRowsBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.numOfColsBlock = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.numOfCols = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
