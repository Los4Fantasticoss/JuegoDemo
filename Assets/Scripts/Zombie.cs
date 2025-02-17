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
    private bool estaMuerto = false; // Indica si el Zombie está muerto
    public int attackDamage = 10; // Daño del ataque
    public float attackCooldown = 1.0f; // Tiempo entre ataques
    private float lastAttackTime = 0f; // Tiempo del último ataque

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtén el componente Animator
    }

    void Update()
    {
        if (estaMuerto || objetivo == null) return; // Si el Zombie está muerto o no hay objetivo, no hace nada

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
                lastAttackTime = Time.time; // Registra el tiempo del último ataque
            }
        }
        else
        {
            // Si está fuera de rango, camina o corre hacia el jugador
            agente.isStopped = false;
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsRunning", distanceToPlayer > 10f); // Corre si está lejos del jugador
            animator.SetBool("IsAttacking", false); // Resetea el parámetro de ataque
            agente.SetDestination(objetivo.transform.position);
        }
    }

    void Atacar()
    {
        Debug.Log("Zombie está atacando"); // Mensaje de depuración

        // Aplica daño al jugador
        PlayerHealth playerHealth = objetivo.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage);
            Debug.Log("Zombie atacó al jugador. Daño: " + attackDamage);
        }

        // Activa la animación de ataque
        animator.SetBool("IsAttacking", true); // Activa el bool para la animación de ataque

        // Inicia una corrutina para resetear el parámetro después de un tiempo
        StartCoroutine(ResetAttack());
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(0.5f); // Espera 0.5 segundos (ajusta este valor según la duración de la animación)
        animator.SetBool("IsAttacking", false); // Resetea el parámetro de ataque
    }

    public void RecibirDaño(int cantidad)
    {
        if (estaMuerto) return; // Si ya está muerto, no recibe más daño

        vida -= cantidad; // Reduce la vida del Zombie

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        Debug.Log("Zombie ha muerto"); // Mensaje de depuración
        estaMuerto = true;
        animator.SetBool("IsDead", true); // Activa la animación de muerte
        agente.isStopped = true;
        agente.ResetPath();
        Destroy(gameObject, 5f); // Destruye el objeto después de 5 segundos (opcional)
    }

    public int ObtenerVida()
    {
        return vida;
    }
}