package controllers

import (
	"net/http"
	"ms/backend-api/models"
	"github.com/gin-gonic/gin"
)

func GetAllData(c *gin.Context) {

	var data []models.MonitoringSystems
	models.DB.Find(&data)

	c.JSON(200, gin.H{
		"success": true,
		"message": "Listing of monitoring data system",
		"data": data,
	})
}

func StoreData(c *gin.Context) {
	var monitoringData models.MonitoringSystems
	
	// Bind the incoming JSON to the monitoringData struct
	if err := c.ShouldBindJSON(&monitoringData); err != nil {
		c.JSON(http.StatusBadRequest, gin.H{
			"success": false,
			"message": "Invalid input",
			"error":   err.Error(),
		})
		return
	}

	// Save the data to the database
	if err := models.DB.Create(&monitoringData).Error; err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{
			"success": false,
			"message": "Failed to save data",
			"error":   err.Error(),
		})
		return
	}

	c.JSON(http.StatusOK, gin.H{
		"success": true,
		"message": "Monitoring data created successfully",
		"data":    monitoringData,
	})
}