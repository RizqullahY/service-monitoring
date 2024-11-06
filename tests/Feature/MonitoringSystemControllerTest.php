<?php

namespace Tests\Feature;

use Tests\TestCase;

class MonitoringSystemControllerTest extends TestCase
{
    public function test_store_data(): void
    {
        $payload = [
            'DeviceName' => 'Device 1',
            'BatteryInfo' => [
                'BatteryStatus' => 1,
                'EstimatedChargeRemaining' => 80,
                'EstimatedRunTime' => 120,
            ],
            'SystemInfo' => [
                'LastBootTime' => '2024-11-01T12:00:00',
                'SystemUptime' => '2 hours',
            ],
            'WiFiInfo' => [
                'SSID' => 'HomeNetwork',
                'State' => 'Connected',
                'SignalQuality' => 'Strong',
                'RadioType' => '802.11ac',
                'Authentication' => 'WPA2',
            ],
            'TemperatureInfo' => [
                'Temperature' => 35,
            ],
        ];

        $response = $this->postJson('/api/system-info', $payload);

        $response->assertStatus(201);
    }
}
