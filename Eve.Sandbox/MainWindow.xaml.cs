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
using FreeNet.Data.Entity.Extensions;

using Eve;
using Eve.Data;
using Eve.Meta;
using Eve.Universe;

namespace Eve.Sandbox {
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window {
    public MainWindow() {
      InitializeComponent();
    }

    private void Button_Click_1(object sender, RoutedEventArgs e) {
      //IDbConnection connection = Eve.Data.EveDbContext.Default.Database.Connection;
      //connection.Open();
      //textBox1.AppendText(Eve.Meta.EveCodeGenerator.Default.GenerateEnumSkillId(connection, null, null));
      //return;

      DbConnection connection = (DbConnection) Eve.Data.EveDbContext.Default.Database.Connection;
      connection.Open();

      Schema s = new Schema(connection, true);
      textBox1.AppendText(s.Name + Environment.NewLine);

      foreach (SchemaTable table in s.Tables) {
        textBox1.AppendText("  " + table.Name + Environment.NewLine);

        foreach (SchemaColumn column in table.Columns) {
          textBox1.AppendText("    " + column.Description + Environment.NewLine);
        }
      }

      return;

      Group group = Eve.General.DataSource.GetGroupById(GroupId.EnergyDestabilizer);

      foreach (ItemType item in group.ItemTypes) {
        textBox1.AppendText(item.ToString());
      }


      //AttributeValue value = Eve.General.DataSource.Get<AttributeValue>(x => ((x.ItemTypeId == 18) && (x.AttributeId == (AttributeId) 182))).First();
      //textBox1.AppendText(value.ToString());

      //IList<Race> races = Eve.General.DataSource.GetAll<Race>();

      //foreach (Race race in races) {
      //  textBox1.AppendText(race.Id.ToString() + ", " + race.Name + " , " + race.Description + " , " + race.ShortDescription + Environment.NewLine);
      //}
    }

    private void Button_Click_2(object sender, RoutedEventArgs e) {
      Eve.General.Cache.Clear();
    }
  }
}