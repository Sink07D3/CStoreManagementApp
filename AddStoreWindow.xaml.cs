using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace StationTankManagementProject
{
    /// <summary>
    /// Interaction logic for AddStoreWindow.xaml
    /// </summary>
    public partial class AddStoreWindow : Window
    {
        public AddStoreWindow()
        {
            InitializeComponent();
        }









        public CStore RetrieveNewStoreData()
        {
            CStore returnStore = new CStore();



            return returnStore;
        }

    }
}
