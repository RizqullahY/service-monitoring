package models


type BatteryInfo struct {
	BatteryStatus            int    `json:"BatteryStatus"`
	EstimatedChargeRemaining string `json:"EstimatedChargeRemaining"`
	EstimatedRunTime         string `json:"EstimatedRunTime"`
}

type SystemInfo struct {
	LastBootTime string `json:"LastBootTime"`
	SystemUptime string `json:"SystemUptime"`
}

type WiFiInfo struct {
	Ssid           string `json:"SSID"`
	State          string `json:"State"`
	SignalQuality  string `json:"SignalQuality"`
	RadioType      string `json:"RadioType"`
	Authentication string `json:"Authentication"`
}

type TemperatureInfo struct {
	Temperature int `json:"Temperature"`
}

type DiskInfo struct {
	TotalSize   string `json:"TotalSize"`
	FreeSpace   string `json:"FreeSpace"`
	UsedSpace   string `json:"UsedSpace"`
	PercentUsed string `json:"PercentUsed"`
}

type MonitoringSystems struct {
	Id 				int				`json:"Id"`
	DeviceName      string          `json:"DeviceName"`
	BatteryInfo     BatteryInfo     `json:"BatteryInfo" gorm:"embedded"`
	SystemInfo      SystemInfo      `json:"SystemInfo" gorm:"embedded"`
	WiFiInfo        WiFiInfo        `json:"WiFiInfo" gorm:"embedded"`
	DiskInfo        DiskInfo        `json:"DiskInfo" gorm:"embedded"`
	TemperatureInfo TemperatureInfo `json:"TemperatureInfo" gorm:"embedded"`
	Created_At      string          `json:"Created_At"`
	SendToApiOnTime bool            `json:"SendToApiOnTime"`
}
