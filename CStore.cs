using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StationTankManagementProject
{
    /// <summary>
    /// CStore object for storing data relating to convenience stores, for manipulating and later storing via the program.
    /// </summary>
    public class CStore
    {
        /// <summary>
        /// 
        /// </summary>
        public int StoreNumber {  get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StoreAddress { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string StoreState { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime StoreShipDate { get; set; }
        /// <summary>
        ///
        /// </summary>
        public DateTime? StoreShippedDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public IList<FuelTank> StoreTanks { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StorePONumber { get; set; }

        /// <summary>
        /// Default CStore constructor. Creates a "blank" CStore object with a store number of zero, an empty store address string, and a new, empty, fuel tank list.
        /// </summary>
        public CStore()
        {
            this.StoreNumber = 0;
            this.StoreAddress = string.Empty;
            this.StoreState = string.Empty;
            this.StoreShipDate = DateTime.Now;
            this.StoreShippedDate = DateTime.Now;
            this.StoreTanks = new List<FuelTank>();
            this.StorePONumber = string.Empty;
        }

        /// <summary>
        /// CStore constructor for constructing a CStore object with all required information.
        /// </summary>
        /// <param name="storeNumber">An integer that is the store number of the desired CStore.</param>
        /// <param name="storeAddress">A string containing the address of the desired CStore.</param>
        /// <param name="tankList">A list of FuelTanks that represent the fuel tanks at this CStore.</param>
        public CStore (int storeNumber, string storeAddress, string storeState, DateTime storeShipDate, DateTime? storeShippedDate, List<FuelTank> tankList, string PONum)
        {
            this.StoreNumber = storeNumber;
            this.StoreAddress = storeAddress;
            this.StoreState = storeState;
            this.StoreShipDate = storeShipDate;
            this.StoreShippedDate = storeShippedDate;
            this.StoreTanks = tankList;
            this.StorePONumber = PONum;
        }

    }

    /// <summary>
    /// FuelTank object, used for storing data pertaining to the fuel tanks associated with a convenience store.
    /// </summary>
    public class FuelTank
    {
        /// <summary>
        /// 
        /// </summary>
        public int TankNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public FuelType TankFuel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int TankCapacity { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool HasVapor { get; set; }

        /// <summary>
        /// The only FuelTank constructor. Will populate every field for a FuelTank object.
        /// </summary>
        /// <param name="tankNum">An integer corresponding to the assigned tank number for this tank at its particular store.</param>
        /// <param name="storedFuel">An enum with a value corresponding to the type of fuel stored in this tank.</param>
        /// <param name="tankCapacity">An integer corresponding to the amount of fuel (in gallons) this tank stores.</param>
        /// <param name="vapor">A bool that states whether or not this tank has a vapor lid.</param>
        public FuelTank(int tankNum, FuelType storedFuel, int tankCapacity, bool vapor)
        {
            TankNumber = tankNum;
            TankFuel = storedFuel;
            TankCapacity = tankCapacity;
            HasVapor = vapor;
        }

    }

    /// <summary>
    /// An enum for the various fuel types that might be stored in a fuel tank.
    /// </summary>
    public enum FuelType
    {
        UNL_GASOLINE,
        PREM_GASOLINE,
        DIESEL_NUM2,
        ETHANOL_FREE_GASOLINE,
        ECO_E15_GASOLINE,
        E85_GASOLINE,
        MID_GRADE_GASOILNE,
        BIODIESEL_B99,
        DEF
    }

}
