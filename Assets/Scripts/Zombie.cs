using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ZombieAI : MonoBehaviour
{
    public GameObject objetivo;  // Jugador
    private NavMeshAgent agente;
    private Animator animator;   // Referencia al Animator del Zombie
    public float attackRange = 2.0f; // Rango de ataque
    public int vida = 100; // Vida del Zombie
    private bool estaMuerto = false; // Indica si el Zombie est� muerto
    public int attackDamage = 10; // Da�o del ataque
    public float attackCooldown = 1.0f; // Tiempo entre ataques
    private float lastAttackTime = 0f; // Tiempo del �ltimo ataque

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obt�n el componente Animator
    }

    void Update()
    {
        if (estaMuerto || objetivo == null) return; // Si el Zombie est� muerto o no hay objetivo, no hace nada

        float distanceToPlayer = Vector3.Distance(transform.position, objetivo.transform.position);

        if (distanceToPlayer <= attackRange)
        {
            // Detiene el movimiento y ataca
            agente.isStopped = true;
            agente.ResetPath();
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsRunning", false);

            // Ataca si ha pasado el tiempo de cooldown
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Atacar();
                lastAttackTime = Time.time; // Registra el tiempo del �ltimo ataque
            }
        }
        else
        {
            // Si est� fuera de rango, camina o corre hacia el jugador
            agente.isStopped = false;
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsRunning", distanceToPlayer > 10f); // Corre si est� lejos del jugador
            animator.SetBool("IsAttacking", false); // Resetea el par�metro de ataque
            agente.SetDestination(objetivo.transform.position);
        }
    }

    void Atacar()
    {
        Debug.Log("Zombie est� atacando"); // Mensaje de depuraci�n

        // Aplica da�o al jugador
        PlayerHealth playerHealth = objetivo.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Zombie atac� al jugador. Da�o: " + attackDamage);
        }

        // Activa la animaci�n de ataque
        animator.SetBool("IsAttacking", true); // Activa el bool para la animaci�n de ataque

        // Inicia una corrutina para resetear el par�metro despu�s de un tiempo
        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Espera 0.5 segundos (ajusta este valor seg�n la duraci�n de la animaci�n)
        animator.SetBool("IsAttacking", false); // Resetea el par�metro de ataque
    }

    public void RecibirDa�o(int cantidad)
    {
        if (estaMuerto) return; // Si ya est� muerto, no recibe m�s da�o

        vida -= cantidad; // Reduce la vida del Zombie

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("Zombie ha muerto"); // Mensaje de depuraci�n
        estaMuerto = true;
        animator.SetBool("IsDead", true); // Activa la animaci�n de muerte
        agente.isStopped = true;
        agente.ResetPath();
        Destroy(gameObject, 5f); // Destruye el objeto despu�s de 5 segundos (opcional)
    }

    public int ObtenerVida()
    {
        return vida;
    }
}