using System;
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

namespace Eve.Sandbox
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private EveRepository ds = new EveRepository(null, null);

    public MainWindow()
    {
      InitializeComponent();
    }

    public class Foo
    {
      MainWindow t;

      public Foo(MainWindow w)
      {
        this.t = w;
      }

      public void Bar()
      {
        t.textBox1.Text = "nothing";
      }

      public void Bar(string a)
      {
        t.textBox1.Text = "string";
      }
    }

    private void Button_Click_1(object sender, RoutedEventArgs e)
    {
      EveDbContext ct = new EveDbContext();

      var result = ds.GetEveTypes(q => q.Where(x => x.Name == "Condor")).Single();

      foreach (var t in result.Materials)
      {
        textBox1.AppendText(t.ToString() + Environment.NewLine);
      }
    }
  }
}