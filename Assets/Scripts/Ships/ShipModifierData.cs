namespace Ships
{
    /// <summary>
    /// Holds all the data of crew members on the ship
    /// </summary>
    public class ShipModifierData
    {
        #region Non Combat Roles

        public int SwabbieCount;
        public float SwabbieStatAverage;
        public int QuartermasterCount;
        public float QuartermasterStatAverage;
        public int CookCount;
        public float CookStatAverage;
        public int BoatswainCount;
        public float BoatswainStatAverage;
        public int LookoutCount;
        public float LookoutStatAverage;
        public int DoctorCount;
        public float DoctorStatAverage;
        public int ShantyManCount;
        public float ShantyManStatAverage;
        public int SailHandCount;
        public float SailHandStatAverage;

        #endregion

        #region Naval Combat Roles

        public int CombatSupportCount;
        public float CombatSupportStatAverage;
        public int CommanderCount;
        public float CommanderStatAverage;
        public int GunnerCount;
        public float GunnerStatAverage;
        public int EmergencyMedicCount;
        public float EmergencyMedicStatAverage;
        public int EmergencyRepairManCount;
        public float EmergencyRepairManStatAverage;
        public int CombatLookoutCount;
        public float CombatLookoutStatAverage;
        public int CombatSailHandCount;
        public float CombatSailHandStatAverage;
        public int PowderMonkeyCount;
        public float PowderMonkeyStatAverage;

        #endregion
    }
}
