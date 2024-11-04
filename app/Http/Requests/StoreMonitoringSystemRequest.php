<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StoreMonitoringSystemRequest extends FormRequest
{
    public function authorize()
    {
        return true; // Set true jika Anda ingin mengizinkan semua pengguna untuk mengakses request ini
    }

    public function rules()
    {
        return [
            'DeviceName' => 'required|string',
            'BatteryInfo.BatteryStatus' => 'required|integer',
            'BatteryInfo.EstimatedChargeRemaining' => 'required|integer',
            'BatteryInfo.EstimatedRunTime' => 'required|integer',
            'SystemInfo.LastBootTime' => 'required|date',
            'SystemInfo.SystemUptime' => 'required|string',
            'WiFiInfo.SSID' => 'nullable|string',
            'WiFiInfo.State' => 'nullable|string',
            'WiFiInfo.SignalQuality' => 'nullable|string',
            'WiFiInfo.RadioType' => 'nullable|string',
            'WiFiInfo.Authentication' => 'nullable|string',
            'TemperatureInfo.Temperature' => 'required|integer',
        ];
    }
    
}
