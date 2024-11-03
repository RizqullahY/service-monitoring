<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class MonitoringSystem extends Model
{
    use HasFactory;

    // Tentukan nama tabel jika tidak sesuai dengan konvensi
    protected $table = 'monitoring_systems';

    // Tentukan atribut yang dapat diisi
    protected $fillable = [
        'device_name',
        'battery_status',
        'estimated_charge_remaining',
        'estimated_run_time',
        'last_boot_time',
        'system_uptime',
        'ssid',
        'state',
        'signal_quality',
        'radio_type',
        'authentication',
        'temperature',
    ];

    // Jika Anda ingin mengatur format tanggal secara otomatis
    protected $dates = [
        'last_boot_time',
    ];
}
