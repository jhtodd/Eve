﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Microsoft.CSharp;

using FreeNet.Data;
using FreeNet.Data.Entity;
using FreeNet.Data.Entity.Utilities;

using Eve;
using Eve.Character;
using Eve.Data;
using Eve.Data.Entities;
using Eve.Universe;
using System.Reflection;
using YamlDotNet.RepresentationModel;
using System.IO;

namespace Eve.Sandbox {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    IEveRepository ds = new EveRepository(null, null);

    public MainWindow() {
      InitializeComponent();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e) {
      var i1 = ds.GetItemById(14);
      var i2 = ds.GetItemById(15);
      var i3 = ds.GetItemById(1007);

      textBox1.AppendText(i3.Name + Environment.NewLine);
      textBox1.AppendText(object.ReferenceEquals(i1.Owner, i2.Owner).ToString() + Environment.NewLine);

      //foreach (var a in t.Jumps)
      //{
      //  textBox1.AppendText(a.ToConstellation.Name + Environment.NewLine);
      //}
    }
  }
}