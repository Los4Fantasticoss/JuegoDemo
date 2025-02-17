using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 5f; // Fuerza del salto
    private CharacterController controller; // Referencia al CharacterController
    private float verticalVelocity = 0f; // Velocidad vertical
    private bool isGrounded; // Indica si el jugador está en el suelo

    void Start()
    {
        // Obtén el componente CharacterController
        controller = GetComponent<CharacterController>();

        // Verifica si el CharacterController está adjunto
        if (controller == null)
        {
            Debug.LogError("No se encontró un componente CharacterController en el objeto " + gameObject.name);
        }
    }

    void Update()
    {
        // Verifica si el jugador está en el suelo
        isGrounded = controller.isGrounded;

        // Si el jugador está en el suelo y presiona la barra espaciadora, salta
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            verticalVelocity = jumpForce; // Aplica la fuerza del salto
            Debug.Log("Saltando con fuerza: " + verticalVelocity);
        }

        // Aplica la gravedad
        if (!isGrounded)
        {
            verticalVelocity -= 9.8f * Time.deltaTime; // Gravedad
        }
        else
        {
            // Si está en el suelo, asegúrate de que la velocidad vertical no sea negativa
            verticalVelocity = -0.1f; // Un pequeño valor negativo para mantener al jugador pegado al suelo
        }

        // Aplica el movimiento vertical
        Vector3 moveDirection = new Vector3(0, verticalVelocity, 0);
        controller.Move(moveDirection * Time.deltaTime);
    }
}