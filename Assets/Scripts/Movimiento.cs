using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 20f;

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * x + transform.forward * z;
        controller.Move(movement * speed * Time.deltaTime);
    }
}