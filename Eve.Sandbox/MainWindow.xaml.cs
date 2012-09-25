using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
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

using FreeNet.Data.Entity;

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
      //textBox1.AppendText(Eve.Meta.EveCodeGenerator.Default.GenerateEnumGroupId(connection, null, null));
      //return;

      MarketGroup group = Eve.General.DataSource.Get<MarketGroup>(x => x.Id == MarketGroupId.Ships_Frigates_StandardFrigates_Caldari).Single();

      foreach (ItemType item in group.ItemTypes) {
        textBox1.AppendText(item.Name + " (" + item.Category.Name + ")" + Environment.NewLine);
      }

      //IList<Race> races = Eve.General.DataSource.GetAll<Race>();

      //foreach (Race race in races) {
      //  textBox1.AppendText(race.Id.ToString() + ", " + race.Name + " , " + race.Description + " , " + race.ShortDescription + Environment.NewLine);
      //}
    }
  }
}