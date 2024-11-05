package models

type BatteryInfo struct {
	BatteryStatus            int `json:"BatteryStatus"`
	EstimatedChargeRemaining int `json:"EstimatedChargeRemaining"`
	EstimatedRunTime         int `json:"EstimatedRunTime"` // dalam menit
}

type SystemInfo struct {
	LastBootTime string `json:"LastBootTime"` // format ISO 8601
	SystemUptime string `json:"SystemUptime"` // e.g., "2 hours 15 minutes"
}

type WiFiInfo struct {
	Ssid           string `json:"SSID"`
	State          string `json:"State"`
	SignalQuality  string `json:"SignalQuality"`
	RadioType      string `json:"RadioType"`
	Authentication string `json:"Authentication"`
}

type TemperatureInfo struct {
	Temperature int `json:"Temperature"` // dalam Celsius atau Fahrenheit
}

type MonitoringSystems struct {
    DeviceName      string          `json:"DeviceName"`
	BatteryInfo     BatteryInfo     `json:"BatteryInfo" gorm:"embedded"` 
	SystemInfo      SystemInfo      `json:"SystemInfo" gorm:"embedded"` 
	WiFiInfo        WiFiInfo        `json:"WiFiInfo" gorm:"embedded"` 
	TemperatureInfo TemperatureInfo  `json:"TemperatureInfo" gorm:"embedded"` 
    Created_At      string          `json:"Created_At"`
}
