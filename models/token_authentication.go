package models

import (
	"time"
	"gorm.io/gorm"
)

type TokenAuthentication struct {
	ID        uint           `gorm:"primaryKey"`
	Token     string         `gorm:"unique;not null"`
	CreatedAt time.Time
	UpdatedAt time.Time
	DeletedAt gorm.DeletedAt `gorm:"index"`
}
