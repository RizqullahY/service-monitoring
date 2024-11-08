package main

import (
	"github.com/gin-gonic/gin"
	"ms/backend-api/controllers"
	"ms/backend-api/models"
	"ms/backend-api/middleware"
)

func main() {
	router := gin.Default()

	models.ConnectDatabase()

	router.GET("/", func(c *gin.Context) {
		c.JSON(200, gin.H{
			"message": "Hello World!",
		})
	})

	router.GET("/api/system-info", controllers.GetAllMonitoringSystemData)
	router.POST("/api/system-info", middleware.TokenAuthMiddleware(), controllers.StoreMonitoringSystemData)

	router.GET("/api/tokens", controllers.GetAllTokens)
    router.POST("/api/generate-token", controllers.GenerateToken)
    router.DELETE("/api/tokens/:id", controllers.DeleteToken)

	router.Run(":8000")
}

