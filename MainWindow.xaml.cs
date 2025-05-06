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

namespace StationTankManagementProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CStore loadedStore;


        public MainWindow()
        {
            InitializeComponent();
            loadedStore = new CStore();
        }



        public void LoadNewCStore(CStore newCStore)
        {
            //disable input
            //clear visible data
            //change loadedStore
            //enable input
        }

        public void ClearLoadedData()
        {
            //
        }

    }
}