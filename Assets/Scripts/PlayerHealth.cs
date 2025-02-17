using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100; // Vida m�xima del jugador
    public int currentHealth; // Vida actual del jugador
    public Slider healthBar; // Referencia a la barra de vida
    public TextMeshProUGUI deathMessage; // Para TextMeshPro

    public GunScript gunScript; // Referencia al script de disparo

    void Start()
    {
        currentHealth = maxHealth; // Inicializa la vida al m�ximo
        UpdateHealthBar(); // Actualiza la barra de vida al inicio
        deathMessage.gameObject.SetActive(false); // Aseg�rate de que el mensaje est� desactivado al inicio
    }

    // Funci�n para recibir da�o
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce la vida del jugador
        if (currentHealth < 0)
        {
            currentHealth = 0; // Aseg�rate de que la vida no sea negativa
        }

        UpdateHealthBar(); // Actualiza la barra de vida

        // Si la vida llega a 0, el jugador muere
        if (currentHealth == 0)
        {
            Die();
        }
    }

    // Funci�n para actualizar la barra de vida
    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            healthBar.value = currentHealth; // Actualiza el valor de la barra de vida
        }
    }

    // Funci�n para manejar la muerte del jugador
    void Die()
    {
        Debug.Log("El jugador ha muerto");

        // Muestra el mensaje "HAS MUERTO"
        if (deathMessage != null)
        {
            deathMessage.gameObject.SetActive(true);
        }

        // Desactiva el disparo y destruye las balas activas
        if (gunScript != null)
        {
            gunScript.DisableShooting();
        }

        // Reinicia el juego despu�s de un breve retraso
        Invoke("RestartGame", 3f); // Reinicia el juego despu�s de 3 segundos
    }

    // Funci�n para reiniciar el juego
    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Recarga la escena actual
    }
}