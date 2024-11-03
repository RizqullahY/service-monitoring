---

# Mengambil Data Service Laptop

Aplikasi ini bertujuan untuk mengambil berbagai informasi dari laptop, seperti status baterai, informasi sistem, status WiFi, dan suhu perangkat. Saat dijalankan, aplikasi akan menghasilkan file JSON yang berisi data-data ini di direktori yang sama dengan file program.

## Cara Menjalankan Aplikasi

1. Pastikan .NET SDK telah terpasang di komputer Anda. Unduh .NET SDK di [sini](https://dotnet.microsoft.com/download) jika belum tersedia.
  
2. Buka terminal atau command prompt, lalu arahkan ke direktori proyek.

3. Jalankan perintah berikut untuk menjalankan aplikasi:
   ```bash
   dotnet run
   ```

4. Setelah perintah di atas dijalankan, data JSON akan dihasilkan di folder tersebut. Contoh output JSON adalah sebagai berikut:

   ```json
   {
     "DeviceName": "ASCE-3D2Y",
     "BatteryInfo": {
       "BatteryStatus": 2,
       "EstimatedChargeRemaining": 77,
       "EstimatedRunTime": 71582788
     },
     "SystemInfo": {
       "LastBootTime": "2024-11-03T14:07:56.501589+07:00",
       "SystemUptime": "01:26:21.6854186"
     },
     "WiFiInfo": {
       "SSID": "Zalfaa",
       "State": "connected",
       "SignalQuality": "100%",
       "RadioType": "802.11n",
       "Authentication": "WPA2-Personal"
     },
     "TemperatureInfo": {
       "Temperature": 45
     }
   }
   ```

## Changelog

### Versi 1.0.1
- FIX: Masalah JSON yang tidak terstruktur dengan benar telah diperbaiki.
- FIX: Perbaikan error pada serialisasi data.

### Versi 1.0.2
- FIX: Penambahan `DeviceName` yang sempat hilang dalam output JSON.
- Pembaruan: Implementasi `DeviceName` dalam service untuk penarikan data yang lebih akurat.

---

Aplikasi ini berguna untuk monitoring informasi dasar sistem laptop Anda dan dapat digunakan sebagai alat diagnostik ringan.