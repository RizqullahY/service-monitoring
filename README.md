# Feat Monitoring Service Milestone

## Changelog
- Memisahkan antar menjadi beberapa module, yakni utilities
- Issue : menghapus data file json yang di kirim ke api
- Issue : beberapa data json tidak terkirim
- Use `dotnet publish MonitoringServiceApp.csproj -c Release -o ./publish` for publish

- 10/6/2024 Change from server timestamp -> lokal timestamp 
- 11/6/2024 - 11:15 | coba git ignore obj, publish dan bin tapi malah error anjay

- 11/6/2024 - 12:40 | Issue kecocokan data
###### Dictionary<string, DiskInfo> GetDiskStatus()
```
"DiskInfo": {
    "TotalSize": "231 GB",
    "FreeSpace": "73 GB",
    "UsedSpace": "157 GB",
    "PercentUsed": "68 %"
},
```

###### DiskInfo GetTotalDiskStatus()
```
"DiskInfo": {
    "C:": {
      "TotalSize": "179 GB",
      "FreeSpace": "26 GB",
      "UsedSpace": "153 GB",
      "PercentUsed": "85 %"
    },
    "D:": {
      "TotalSize": "51 GB",
      "FreeSpace": "46 GB",
      "UsedSpace": "4 GB",
      "PercentUsed": "8 %"
    },
}
```
- 11/6/2024 - 14:20 | Im Sorry Bro... epic conflict jadi branching baru dari feat/MS-3 