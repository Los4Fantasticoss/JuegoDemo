using UnityEngine;
using System.Collections.Generic;

public class GunScript : MonoBehaviour
{
    public GameObject muzzleFlash;
    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    private AudioSource audioSource; // Ahora es privado
    public AudioClip fireSound;

    public bool isPlayerAlive = true; // Variable para controlar si el jugador está vivo

    private List<GameObject> activeBullets = new List<GameObject>(); // Lista para rastrear las balas activas

    void Start()
    {
        // Obtener el AudioSource automáticamente
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Solo disparar si el jugador está vivo
        if (isPlayerAlive && Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot()
    {
        muzzleFlash.SetActive(true);
        Invoke("DisableMuzzleFlash", 0.05f);

        // Reproducir sonido
        if (audioSource != null && fireSound != null)
        {
            audioSource.PlayOneShot(fireSound);
        }

        if (bulletPrefab != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            activeBullets.Add(bullet); // Añadir la bala a la lista de balas activas
        }
    }

    void DisableMuzzleFlash()
    {
        muzzleFlash.SetActive(false);
    }

    // Método para desactivar el disparo cuando el jugador muere
    public void DisableShooting()
    {
        isPlayerAlive = false;

        // Desactivar todas las balas activas
        foreach (GameObject bullet in activeBullets)
        {
            if (bullet != null)
            {
                Destroy(bullet); // Destruir la bala
            }
        }
        activeBullets.Clear(); // Limpiar la lista de balas activas
    }
}
