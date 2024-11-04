<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class UpdateMonitoringSystemRequest extends FormRequest
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
            'WiFiInfo.SSID' => 'string',
            'WiFiInfo.State' => 'string',
            'WiFiInfo.SignalQuality' => 'string',
            'WiFiInfo.RadioType' => 'string',
            'WiFiInfo.Authentication' => 'string',
            'TemperatureInfo.Temperature' => 'required|integer',
        ];
    }
}
