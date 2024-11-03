<?php

namespace App\Http\Controllers\Api;

use App\Models\MonitoringSystem;
use App\Http\Requests\StoreMonitoringSystemRequest;
use App\Http\Requests\UpdateMonitoringSystemRequest;
use Illuminate\Http\JsonResponse;
use App\Http\Controllers\Controller;

class MonitoringSystemController extends Controller
{
    // Menyimpan data monitoring sistem
    public function store(StoreMonitoringSystemRequest $request): JsonResponse
    {
        // Menyimpan data ke database
        $monitoringSystem = MonitoringSystem::create($this->prepareData($request));

        return response()->json(['message' => 'Monitoring system data saved successfully!', 'data' => $monitoringSystem], 201);
    }

    // Mendapatkan semua data monitoring sistem
    public function index(): JsonResponse
    {
        $monitoringSystems = MonitoringSystem::all();
        return response()->json($monitoringSystems, 200);
    }

    // Mendapatkan data monitoring sistem berdasarkan ID
    public function show($id): JsonResponse
    {
        $monitoringSystem = MonitoringSystem::find($id);

        if (!$monitoringSystem) {
            return response()->json(['message' => 'Monitoring system not found'], 404);
        }

        return response()->json($monitoringSystem, 200);
    }

    // Mengupdate data monitoring sistem
    public function update(UpdateMonitoringSystemRequest $request, $id): JsonResponse
    {
        $monitoringSystem = MonitoringSystem::find($id);

        if (!$monitoringSystem) {
            return response()->json(['message' => 'Monitoring system not found'], 404);
        }

        // Mengupdate data
        $monitoringSystem->update($this->prepareData($request));

        return response()->json(['message' => 'Monitoring system data updated successfully!', 'data' => $monitoringSystem], 200);
    }

    // Menghapus data monitoring sistem
    public function destroy($id): JsonResponse
    {
        $monitoringSystem = MonitoringSystem::find($id);

        if (!$monitoringSystem) {
            return response()->json(['message' => 'Monitoring system not found'], 404);
        }

        $monitoringSystem->delete();
        return response()->json(['message' => 'Monitoring system data deleted successfully!'], 200);
    }

    // Memformat data dari request untuk penyimpanan
    protected function prepareData($request): array
    {
        return [
            'device_name' => $request->DeviceName,
            'battery_status' => $request->BatteryInfo['BatteryStatus'],
            'estimated_charge_remaining' => $request->BatteryInfo['EstimatedChargeRemaining'],
            'estimated_run_time' => $request->BatteryInfo['EstimatedRunTime'],
            'last_boot_time' => $request->SystemInfo['LastBootTime'],
            'system_uptime' => $request->SystemInfo['SystemUptime'],
            'ssid' => $request->WiFiInfo['SSID'],
            'state' => $request->WiFiInfo['State'],
            'signal_quality' => $request->WiFiInfo['SignalQuality'],
            'radio_type' => $request->WiFiInfo['RadioType'],
            'authentication' => $request->WiFiInfo['Authentication'],
            'temperature' => $request->TemperatureInfo['Temperature'],
        ];
    }
}
