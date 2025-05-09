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

        public MainWindow()
        {
            InitializeComponent();
            //this.DataContext = new MainWindowObject();
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            //this.loadedStore = new CStore();
            //this.datagridStoreTanks.AutoGenerateColumns = false;
            this.datagridStoreTanks.CanUserAddRows = true;
            this.datagridStoreTanks.ItemsSource = Enum.GetValues(FuelType.UNL_GASOLINE.GetType());
            //setup data bindings?
            this.StoreGrid.DataContext = new CStore();



        }



        public void LoadNewCStore(CStore newCStore)
        {
            this.DisableMainWindow();
            this.StoreGrid.DataContext = newCStore;
            //this.datagridStoreTanks.ItemsSource = GenerateStoreTankTable(newCStore).DefaultView;
            this.datagridStoreTanks.DataContext = newCStore.StoreTanks;
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

            ///datagrid

            //textbox
            this.textboxPONumber.SetBinding(TextBox.TextProperty, new Binding("StorePONumber"));
            this.textboxStoreNumber.SetBinding(TextBox.TextProperty, new Binding("StoreNumber"));



            //datagrid columns are tank number, product, capacity, hasVapor

            DataGridTextColumn tankNumCol = new DataGridTextColumn();
            tankNumCol.Binding = new Binding("TankNum");
            tankNumCol.Header = "Tank Number";
            DataGridComboBoxColumn tankProdCol = new DataGridComboBoxColumn();
            tankProdCol.SelectedValueBinding = new Binding("TankProd");
            tankProdCol.SelectedValuePath = "TankProd";
            tankProdCol.ItemsSource = fuelTypes;
            tankProdCol.Header = "Tank Product";
            tankProdCol.DisplayMemberPath = "TankProd";
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
            //DataTable storeTankTable = this.GenerateStoreTankTable(this.DataContext);
            //this.datagridStoreTanks.ItemsSource = storeTankTable.DefaultView;
            
            //this.datagridStoreTanks.DataContext = storeTankTable;
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

            CStore testStore = new CStore(752, "Your mom's house", "AZ", DateTime.Now, null, fuelTanks, "PowerM420");


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
                Console.WriteLine(ex.Message);
                return new DataTable(); //return an empty data table if an exception is thrown
            }
        }
    }

    public class MainWindowObject
    {
        //properties
        public CStore LoadedStore {  get; set; }
        public string WindowName { get; set; }
        public MainWindowObject()
        {
            this.LoadedStore = new CStore();
            this.WindowName = "Tank Management";
        }

        public MainWindowObject(CStore newCStore)
        {
            this.LoadedStore = newCStore;
            this.WindowName = "Tank Management";
        }
    }


}