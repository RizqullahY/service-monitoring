package controllers

import (
	"crypto/rand"
	"encoding/hex"
	"net/http"

	"github.com/gin-gonic/gin"
	"ms/backend-api/models"
)

// GetAllTokens - Mengambil semua token dari database
func GetAllTokens(c *gin.Context) {
	var tokens []models.TokenAuthentication
	if err := models.DB.Find(&tokens).Error; err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}
	c.JSON(http.StatusOK, tokens)
}

// GenerateToken - Membuat token baru dan menyimpannya di database
func GenerateToken(c *gin.Context) {
	token := generateRandomToken(32) // Panjang token 32 byte
	newToken := models.TokenAuthentication{Token: token}

	if err := models.DB.Create(&newToken).Error; err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}
	c.JSON(http.StatusOK, gin.H{"message": "Token generated", "token": newToken.Token})
}

// DeleteToken - Menghapus token berdasarkan ID
func DeleteToken(c *gin.Context) {
	var token models.TokenAuthentication
	id := c.Param("id")

	if err := models.DB.First(&token, id).Error; err != nil {
		c.JSON(http.StatusNotFound, gin.H{"error": "Token not found"})
		return
	}

	if err := models.DB.Delete(&token).Error; err != nil {
		c.JSON(http.StatusInternalServerError, gin.H{"error": err.Error()})
		return
	}
	c.JSON(http.StatusOK, gin.H{"message": "Token deleted"})
}

// generateRandomToken - Membuat token acak dengan panjang tertentu
func generateRandomToken(length int) string {
	bytes := make([]byte, length)
	_, err := rand.Read(bytes)
	if err != nil {
		panic(err) // Pastikan menangani error dengan benar dalam produksi
	}
	return hex.EncodeToString(bytes)
}
