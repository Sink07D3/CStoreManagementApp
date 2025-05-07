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
            this.loadedStore = new CStore();
        }



        public void LoadNewCStore(CStore newCStore)
        {
            this.DisableMainWindow();
            //disable input
            //clear visible data
            this.loadedStore = newCStore;
            this.RefreshLoadedCStoreData();
            //enable input
            this.EnableMainWindow();
        }

        private void DisableMainWindow()
        {
            this.buttonAddStore.IsEnabled = false;
            this.buttonGenerateTagOrder.IsEnabled= false;
            this.buttonSearchStores.IsEnabled = false;
            this.textboxPONumber.IsEnabled = false;
            this.textboxStoreNumber.IsEnabled = false;
            this.textboxStoreShipDate.IsEnabled = false;
            this.textboxStoreNumber.IsEnabled= false;
            this.textboxStoreShippedDate.IsEnabled = false;
            this.textboxStoreState.IsEnabled = false;
        }

        private void EnableMainWindow()
        {
            this.buttonAddStore.IsEnabled = true;
            this.buttonGenerateTagOrder.IsEnabled = true;
            this.buttonSearchStores.IsEnabled = true;
            this.textboxPONumber.IsEnabled = true;
            this.textboxStoreNumber.IsEnabled = true;
            this.textboxStoreShipDate.IsEnabled = true;
            this.textboxStoreNumber.IsEnabled = true;
            this.textboxStoreShippedDate.IsEnabled = true;
            this.textboxStoreState.IsEnabled = true;
        }

        private void ClearLoadedData()
        {
            //textbox
            this.textboxStoreNumber.Content = string.Empty;
            this.textboxPONumber.Content = string.Empty;
            this.textboxStoreShipDate.Content = string.Empty;
            this.textboxStoreShippedDate.Content = string.Empty;
            this.textboxStoreState.Content = string.Empty;
            this.textboxStreetAddress.Content = string.Empty;
            //tank dataGrid
        }

        public void RefreshLoadedCStoreData()
        {
            this.ClearLoadedData();
            this.PopulateLoadedData();

        }

        private void PopulateLoadedData()
        {
            //textbox
            this.textboxStoreNumber.Content = this.loadedStore.StoreNumber;
            this.textboxPONumber.Content = this.loadedStore.StorePONumber;
            this.textboxStoreShipDate.Content = this.loadedStore.StoreShipDate;
            this.textboxStoreShippedDate.Content = this.loadedStore.StoreShippedDate;
            this.textboxStoreState.Content = this.loadedStore.StoreState;
            this.textboxStreetAddress.Content = this.loadedStore.StoreAddress;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveLoadedCStoreChanges(object sender, RoutedEventArgs e)
        {

        }

        private void TestCStoreMethod(object sender, RoutedEventArgs e)
        {
            CStore testStore = new CStore(752, "Your mom's house", "UT", DateTime.Now, null, new List<FuelTank>(), "PowerM420");


            this.LoadNewCStore(testStore);
        }
    }
}