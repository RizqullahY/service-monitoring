<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    /**
     * Run the migrations.
     */
    public function up(): void
    {
        Schema::create('monitoring_systems', function (Blueprint $table) {
            $table->id();
            $table->string('device_name');
            $table->integer('battery_status');
            $table->integer('estimated_charge_remaining');
            $table->integer('estimated_run_time');
            $table->string('last_boot_time');
            $table->string('system_uptime');
            $table->string('ssid')->nullable();
            $table->string('state')->nullable();
            $table->string('signal_quality')->nullable();
            $table->string('radio_type')->nullable();
            $table->string('authentication')->nullable();
            $table->integer('temperature');
            $table->timestamps();
        });
    }

    /**
     * Reverse the migrations.
     */
    public function down(): void
    {
        Schema::dropIfExists('monitoring_systems');
    }
};
