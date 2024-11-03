# Laptop Service Data Collector

Aplikasi **Laptop Service Data Collector** ini adalah aplikasi berbasis .NET yang bertujuan untuk mengambil informasi perangkat laptop secara real-time, seperti status baterai, informasi sistem, status WiFi, dan suhu perangkat. Data yang diambil akan ditampilkan langsung di console menggunakan `Console.WriteLine`, memungkinkan pengguna untuk memantau kondisi perangkat secara instan.

## Fitur Utama

- **Informasi Baterai**: Menampilkan status baterai, perkiraan sisa daya, dan waktu yang tersisa sebelum baterai habis.
- **Informasi Sistem**: Menyediakan waktu boot terakhir dan total waktu uptime sistem.
- **Informasi WiFi**: Menunjukkan SSID, status koneksi, kualitas sinyal, tipe jaringan, dan metode otentikasi WiFi yang digunakan.
- **Informasi Suhu**: Memantau suhu perangkat untuk memastikan sistem tidak mengalami overheat.

## Contoh Output

Setelah aplikasi dijalankan, data perangkat akan ditampilkan di console sebagai berikut:

```
Device Name: ASCE-3D2Y
Battery Status: 2
Estimated Charge Remaining: 77%
Estimated Run Time: 71582788 seconds

Last Boot Time: 2024-11-03T14:07:56
System Uptime: 01:26:21

WiFi Info:
  SSID: Zalfaa
  State: connected
  Signal Quality: 100%
  Radio Type: 802.11n
  Authentication: WPA2-Personal

Temperature: 45°C
```

## Cara Menjalankan Aplikasi

### Prasyarat

- **.NET SDK**  
  Pastikan Anda sudah menginstal .NET SDK. Anda dapat mengunduhnya di [sini](https://dotnet.microsoft.com/download).

### Langkah-langkah

1. **Clone atau Unduh Proyek**  
   Clone repositori ini ke komputer Anda atau unduh file kode sumbernya.

2. **Navigasi ke Folder Proyek**  
   Buka terminal atau command prompt, kemudian arahkan ke direktori proyek yang telah diunduh.

3. **Jalankan Aplikasi**  
   Jalankan aplikasi dengan perintah berikut:
   ```bash
   dotnet run
   ```

4. **Lihat Output di Console**  
   Setelah dijalankan, aplikasi akan menampilkan data perangkat secara real-time di console.

## Teknologi yang Digunakan

- **.NET 6** (atau versi lain yang kompatibel)
- **System.Management** untuk mengakses informasi perangkat secara langsung di lingkungan Windows.

## Pengembangan Lebih Lanjut

Aplikasi ini dirancang untuk mendukung pengembangan lebih lanjut, seperti:
- Menambahkan lebih banyak informasi diagnostik perangkat.
- Menyimpan data secara otomatis ke file untuk monitoring berkelanjutan.
- Mengimplementasikan antarmuka pengguna (GUI) untuk visualisasi data yang lebih baik.

---

Aplikasi **Laptop Service Data Collector** ini ideal untuk Anda yang ingin mendapatkan informasi cepat mengenai kondisi perangkat laptop tanpa perlu membuka aplikasi tambahan.
