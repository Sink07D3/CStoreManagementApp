using System.Data;
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
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            this.loadedStore = new CStore();
            this.datagridStoreTanks.AutoGenerateColumns = false;
            this.datagridStoreTanks.CanUserAddRows = true;
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
            this.datagridStoreTanks.Columns.Clear();
            
        }

        public void RefreshLoadedCStoreData()
        {
            this.ClearLoadedData();
            this.PopulateLoadedData();

        }

        private void PopulateLoadedData()
        {
            FuelType[] fuelTypes =
            {
                FuelType.UNL_GASOLINE,
                FuelType.PREM_GASOLINE,
                FuelType.DIESEL_NUM2,
                FuelType.ETHANOL_FREE_GASOLINE,
                FuelType.ECO_E15_GASOLINE,
                FuelType.E85_GASOLINE,
                FuelType.MID_GRADE_GASOILNE,
                FuelType.BIODIESEL_B99,
                FuelType.DEF
            };
            //textbox
            this.textboxStoreNumber.Content = this.loadedStore.StoreNumber;
            this.textboxPONumber.Content = this.loadedStore.StorePONumber;
            this.textboxStoreShipDate.Content = this.loadedStore.StoreShipDate;
            this.textboxStoreShippedDate.Content = this.loadedStore.StoreShippedDate;
            this.textboxStoreState.Content = this.loadedStore.StoreState;
            this.textboxStreetAddress.Content = this.loadedStore.StoreAddress;
            //datagrid
            //datagrid columns are tank number, product, capacity, hasVapor

            DataGridTextColumn tankNumCol = new DataGridTextColumn();
            tankNumCol.Binding = new Binding("TankNum");
            tankNumCol.Header = "Tank Number";
            DataGridComboBoxColumn tankProdCol = new DataGridComboBoxColumn();
            tankProdCol.SelectedValueBinding = new Binding("TankProd");
            tankProdCol.SelectedValuePath = "TankProd";
            tankProdCol.ItemsSource = fuelTypes;
            tankProdCol.Header = "Tank Product";
            //tankProdCol.DisplayMemberPath = fuelTypes;
            DataGridTextColumn tankCapCol = new DataGridTextColumn();
            tankCapCol.Binding = new Binding("TankCap");
            tankCapCol.Header = "Tank Capacity (Gals)";
            DataGridCheckBoxColumn tankVapCol = new DataGridCheckBoxColumn();
            tankVapCol.Binding = new Binding("TankVapor");
            tankVapCol.Header = "Has Vapor?";

            this.datagridStoreTanks.Columns.Add(tankNumCol);
            this.datagridStoreTanks.Columns.Add(tankProdCol);
            this.datagridStoreTanks.Columns.Add(tankCapCol);
            this.datagridStoreTanks.Columns.Add(tankVapCol);
            DataTable storeTankTable = this.GenerateStoreTankTable(this.loadedStore);
            this.datagridStoreTanks.ItemsSource = storeTankTable.DefaultView;
            
            this.datagridStoreTanks.DataContext = storeTankTable;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveLoadedCStoreChanges(object sender, RoutedEventArgs e)
        {
            //query the database and update the entry for the loaded store
        }

        private void TestCStoreMethod(object sender, RoutedEventArgs e)
        {
            List<FuelTank> fuelTanks = new List<FuelTank>();
            fuelTanks.Add(new FuelTank(1, FuelType.UNL_GASOLINE, 20000, true));
            fuelTanks.Add(new FuelTank(2, FuelType.DIESEL_NUM2, 40000, false));
            fuelTanks.Add(new FuelTank(3, FuelType.PREM_GASOLINE, 10000, true));
            fuelTanks.Add(new FuelTank(4, FuelType.ETHANOL_FREE_GASOLINE, 10000, true));
            fuelTanks.Add(new FuelTank(5, FuelType.DEF, 6000, false));

            CStore testStore = new CStore(752, "Your mom's house", "UT", DateTime.Now, null, fuelTanks, "PowerM420");


            this.LoadNewCStore(testStore);
        }

        private void OpenAddStore(object sender, RoutedEventArgs e)
        {
            AddStoreWindow addStore = new AddStoreWindow();
            bool? addStoreResult = addStore.ShowDialog();

            if (addStoreResult == true)
            {
                this.LoadNewCStore(addStore.RetrieveNewStoreData());
            }
            else
            {
                //do nothing if the addStoreResult was not true
            }


                

        }

        


        public DataTable GenerateStoreTankTable(CStore aStore)
        {
            try
            {
                DataTable dt = new DataTable();

                dt.Columns.Add(new DataColumn("TankNum"));
                dt.Columns.Add(new DataColumn("TankProd"));
                dt.Columns.Add(new DataColumn("TankCap"));
                dt.Columns.Add(new DataColumn("TankVapor"));

                foreach (FuelTank tank in aStore.StoreTanks)
                {
                    DataRow row = dt.NewRow();
                    row["TankNum"] = tank.TankNumber;
                    row["TankProd"] = tank.TankFuel;
                    row["TankCap"] = tank.TankCapacity;
                    row["TankVapor"] = tank.HasVapor;

                    dt.Rows.Add(row);

                }

                return dt;
            }
            catch (Exception ex)
            {
                return new DataTable(); //return an empty data table if an exception is thrown
            }
        }
    }
}