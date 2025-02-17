using UnityEngine;

public class Fuego : MonoBehaviour
{
    public float damageAmount = 5f; // Da�o por disparo
    public float range = 150f; // Rango del disparo
    public Camera PlayerCam; // C�mara del jugador
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

        // Lanza un raycast desde la c�mara en la direcci�n en la que apunta
        if (Physics.Raycast(PlayerCam.transform.position, PlayerCam.transform.forward, out hit, range))
        {
            bool hitEnemy = false; // Bandera para verificar si se golpe� a un enemigo

            // Verifica si el objeto golpeado es el Troll
            EnemyAI troll = hit.transform.GetComponent<EnemyAI>();
            if (troll != null)
            {
                troll.RecibirDa�o((int)damageAmount); // Reduce la vida del Troll
                Debug.Log("Menos " + damageAmount + " de vida al troll. Vida restante: " + troll.ObtenerVida());
                hitEnemy = true; // Se golpe� a un enemigo
            }

            // Verifica si el objeto golpeado es el Zombie
            ZombieAI zombie = hit.transform.GetComponent<ZombieAI>();
            if (zombie != null)
            {
                zombie.RecibirDa�o((int)damageAmount); // Reduce la vida del Zombie
                Debug.Log("Menos " + damageAmount + " de vida al zombie. Vida restante: " + zombie.ObtenerVida());
                hitEnemy = true; // Se golpe� a un enemigo
            }

            // Si no se golpe� a ning�n enemigo, muestra "Bala fallida"
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