namespace MonitoringSystemApp.Models
{
    public class BatteryInfo
    {
        public int BatteryStatus { get; set; }
        public int EstimatedChargeRemaining { get; set; }
        public int EstimatedRunTime { get; set; }
        public bool IsCharging { get; set; }
        public int TimeToFullCharge { get; set; }
    }
}
