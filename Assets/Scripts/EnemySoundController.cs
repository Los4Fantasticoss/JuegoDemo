using UnityEngine;

public class EnemySoundController : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform player; // Referencia al jugador
    public float maxDistance = 20f; // Distancia máxima para escuchar el sonido

    void Update()
    {
        // Calcula la distancia entre el enemigo y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Ajusta el volumen basado en la distancia
        if (distanceToPlayer <= maxDistance)
        {
            audioSource.volume = 1 - (distanceToPlayer / maxDistance);
        }
        else
        {
            audioSource.volume = 0; // Silencia el sonido si está muy lejos
        }
    }
}