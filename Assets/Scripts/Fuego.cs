using UnityEngine;

public class Fuego : MonoBehaviour
{
    public float damageAmount = 5f; // Daño por disparo
    public float range = 150f; // Rango del disparo
    public Camera PlayerCam; // Cámara del jugador
    public CrosshairController crosshairController; // Referencia al controlador del crosshair

    void Update()
    {
        if (Input.GetButtonDown("Fire1")) // Detecta cuando el jugador dispara (clic izquierdo)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        RaycastHit hit;

        // Cambia el color del crosshair al disparar
        if (crosshairController != null)
        {
            crosshairController.OnShoot();
        }

        // Lanza un raycast desde la cámara en la dirección en la que apunta
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out hit, range))
        {
            bool hitEnemy = false; // Bandera para verificar si se golpeó a un enemigo

            // Verifica si el objeto golpeado es el Troll
            EnemyAI troll = hit.transform.GetComponent<EnemyAI>();
            if (troll != null)
            {
                troll.RecibirDaño((int)damageAmount); // Reduce la vida del Troll
                Debug.Log("Menos " + damageAmount + " de vida al troll. Vida restante: " + troll.ObtenerVida());
                hitEnemy = true; // Se golpeó a un enemigo
            }

            // Verifica si el objeto golpeado es el Zombie
            ZombieAI zombie = hit.transform.GetComponent<ZombieAI>();
            if (zombie != null)
            {
                zombie.RecibirDaño((int)damageAmount); // Reduce la vida del Zombie
                Debug.Log("Menos " + damageAmount + " de vida al zombie. Vida restante: " + zombie.ObtenerVida());
                hitEnemy = true; // Se golpeó a un enemigo
            }

            // Si no se golpeó a ningún enemigo, muestra "Bala fallida"
            if (!hitEnemy)
            {
                Debug.Log("Bala fallida");
            }
        }
        else
        {
            // Si el raycast no golpea nada, muestra "Bala fallida"
            Debug.Log("Bala fallida");
        }
    }
}