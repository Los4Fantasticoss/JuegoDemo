using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject objetivo;  // Cápsula (Jugador)
    private NavMeshAgent agente;
    private Animator animator;   // Referencia al Animator del Troll
    public float attackRange = 2.0f; // Rango de ataque
    public float timeBetweenAttacks = 1.0f; // Tiempo entre ataques
    private float timeSinceLastAttack = 0f; // Tiempo desde el último ataque
    private bool isAttacking = false; // Indica si el Troll está atacando
    public int vida = 100; // Vida del Troll
    private bool estaMuerto = false; // Indica si el Troll está muerto
    public int attackDamage = 10; // Daño normal del ataque
    public int strongAttackDamage = 20; // Daño del tercer ataque
    private int attackCount = 0; // Contador de ataques
    public AudioSource audioSource; // Referencia al AudioSource
    public AudioClip angrySound;    // Sonido cuando el troll está cerca
    public float maxSoundDistance = 20f; // Distancia máxima para escuchar el sonido

    void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>(); // Obtén el componente Animator
    }

    void Update()
    {
        if (estaMuerto) return; // Si el Troll está muerto, no hace nada

        if (objetivo != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, objetivo.transform.position);

            if (distanceToPlayer <= maxSoundDistance)
            {
                audioSource.volume = 1 - (distanceToPlayer / maxSoundDistance);
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
            else
            {
                audioSource.volume = 0;
            }

            if (distanceToPlayer <= attackRange)
            {
                agente.isStopped = true;
                animator.SetBool("IsWalking", false);

                if (!isAttacking)
                {
                    StartAttacking();
                }

                if (isAttacking)
                {
                    timeSinceLastAttack += Time.deltaTime;

                    if (timeSinceLastAttack >= timeBetweenAttacks)
                    {
                        StartAttacking();
                    }
                }
            }
            else
            {
                agente.isStopped = false;
                animator.SetBool("IsWalking", true);
                animator.ResetTrigger("Attack1");
                animator.ResetTrigger("Attack2");
                animator.ResetTrigger("Attack3");
                isAttacking = false;
                agente.SetDestination(objetivo.transform.position);
            }
        }
    }

    void StartAttacking()
    {
        attackCount++;
        int damage = (attackCount % 3 == 0) ? strongAttackDamage : attackDamage;

        PlayerHealth playerHealth = objetivo.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Debug.Log("Troll atacó al jugador. Daño: " + damage);
        }
        else
        {
            Debug.LogError("PlayerHealth no encontrado en el objetivo.");
        }

        int attackIndex = Random.Range(1, 4);
        switch (attackIndex)
        {
            case 1:
                animator.SetTrigger("Attack1");
                break;
            case 2:
                animator.SetTrigger("Attack2");
                break;
            case 3:
                animator.SetTrigger("Attack3");
                break;
        }

        isAttacking = true;
        timeSinceLastAttack = 0f;
    }

    public void RecibirDaño(int cantidad)
    {
        if (estaMuerto) return;

        vida -= cantidad;

        if (vida <= 0)
        {
            Morir();
        }
    }

    void Morir()
    {
        estaMuerto = true;
        animator.SetTrigger("Death");
        agente.isStopped = true;
        audioSource.Stop();
        Destroy(gameObject, 5f);
    }

    public int ObtenerVida()
    {
        return vida;
    }
}