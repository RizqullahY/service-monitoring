# Monitoring System Service 1.0

Program .NET yang berjalan di latar belakang sebagai Windows Service untuk memantau sistem.

## Build dan Publish

Gunakan perintah berikut untuk membangun dan mem-publish aplikasi ini:

```bash
dotnet publish -c Release -o ./publish
```

Output akan berada dalam folder `publish`, di mana file executable (`.exe`) dan dependensi lainnya akan dibundel.

## Menjadikan Aplikasi Sebagai Windows Service

Setelah aplikasi dipublish, gunakan perintah berikut untuk membuat aplikasi ini sebagai Windows Service:

```bash
sc create MonitoringSystemService binPath= "\"E:\...path_the_folder\publish\MonitoringServiceApp.exe\""
```

**Catatan:** Gantilah `E:\...path_the_folder\publish\` dengan path lengkap tempat Anda mem-publish aplikasi ini.

## Debugging dan Menjalankan Service

Berikut adalah beberapa perintah yang dapat digunakan untuk mengelola service:

- **Debugging**  
  Untuk menjalankan aplikasi secara manual tanpa service, gunakan:
  ```bash
  dotnet publish\MonitoringServiceApp.dll
  ```

- **Menghapus Service**  
  Untuk menghapus service:
  ```bash
  sc delete MonitoringSystemService
  ```

- **Menghentikan Service**  
  Untuk menghentikan service:
  ```bash
  sc stop MonitoringSystemService
  ```

- **Memulai Service**  
  Untuk memulai service:
  ```bash
  sc start MonitoringSystemService
  ```

- **Memantau Status Service**  
  Untuk memeriksa status service:
  ```bash
  sc query MonitoringSystemService
  ```

## Paket NuGet yang Digunakan

Berikut adalah paket-paket NuGet yang diinstal untuk aplikasi ini:

```bash
dotnet add package System.ServiceProcess.ServiceController
dotnet add package Microsoft.Extensions.Hosting.WindowsServices
dotnet add package System.Windows.Forms
dotnet add package Microsoft.Windows.SDK.Contracts
dotnet add package System.Management
dotnet add package System.Windows.Extensions
dotnet add package Newtonsoft.Json
dotnet add package System.Diagnostics.PerformanceCounter
```

## Struktur Direktori

Pastikan aplikasi dipublish ke struktur direktori berikut:

```
MonitoringServiceApp
├── publish
│   └── MonitoringServiceApp.exe
```

## Changelog
```
dotnet publish MonitoringServiceApp.csproj -c Release -o ./publish

```