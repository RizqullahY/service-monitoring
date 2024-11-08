package models

import (
	"gorm.io/driver/postgres"
	"gorm.io/gorm"
)

var DB *gorm.DB

func ConnectDatabase() {
	dsn := "postgres://postgres@127.0.0.1:5432/monitoring_system?sslmode=disable"

	database, err := gorm.Open(postgres.Open(dsn), &gorm.Config{})
	if err != nil {
		panic("failed to connect database")
	}

	database.AutoMigrate(&MonitoringSystems{})
	database.AutoMigrate(&TokenAuthentication{})

	DB = database
}
