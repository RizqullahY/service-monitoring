# Feat Monitoring Service 3
# Feat Monitoring Service 3

## Changelog
- Memisahkan antar menjadi beberapa module, yakni utilities
- Issue : menghapus data file json yang di kirim ke api
- Issue : beberapa data json tidak terkirim
- Use `dotnet publish MonitoringServiceApp.csproj -c Release -o ./publish` for publish
- Change from server timestamp -> lokal timestamp 