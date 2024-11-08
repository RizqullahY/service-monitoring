package middleware

import (
	"net/http"
	"github.com/gin-gonic/gin"
	"ms/backend-api/models"
)

// TokenAuthMiddleware untuk memverifikasi token yang ada di database
func TokenAuthMiddleware() gin.HandlerFunc {
	return func(c *gin.Context) {
		// Ambil token dari header Authorization
		authHeader := c.GetHeader("Authorization")
		if authHeader == "" {
			c.JSON(http.StatusUnauthorized, gin.H{
				"success": false,
				"message": "Authorization token is required",
			})
			c.Abort()
			return
		}

		token := authHeader

		// Cek apakah token ada di database
		var tokenRecord models.TokenAuthentication
		if err := models.DB.Where("token = ?", token).First(&tokenRecord).Error; err != nil {
			c.JSON(http.StatusUnauthorized, gin.H{
				"success": false,
				"message": "Invalid or expired token",
			})
			c.Abort()
			return
		}

		// Lanjutkan request jika token valid
		c.Next()
	}
}
