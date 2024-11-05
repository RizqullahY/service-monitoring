package main

import (
	"github.com/gin-gonic/gin"
	"ms/backend-api/controllers"
	"ms/backend-api/models"
)

func main() {
	router := gin.Default()

	models.ConnectDatabase()

	router.GET("/", func(c *gin.Context) {

		//return response JSON
		c.JSON(200, gin.H{
			"message": "Hello World!",
		})
	})

	router.GET("/api/system-info", controllers.GetAllData)
    router.POST("/api/system-info", controllers.StoreData)

	router.Run(":8000")
}

