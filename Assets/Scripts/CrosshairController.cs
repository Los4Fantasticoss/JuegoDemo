using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshairImage; // Referencia a la imagen del crosshair
    public Color normalColor = Color.white; // Color normal del crosshair
    public Color shootColor = Color.red; // Color cuando se dispara

    void Start()
    {
        // Asegúrate de que la imagen del crosshair esté asignada
        if (crosshairImage == null)
        {
            crosshairImage = GetComponent<Image>();
        }
        crosshairImage.color = normalColor; // Establece el color inicial
    }

    // Función para cambiar el color del crosshair al disparar
    public void OnShoot()
    {
        crosshairImage.color = shootColor; // Cambia el color al disparar
        Invoke("ResetCrosshair", 0.1f); // Vuelve al color normal después de 0.1 segundos
    }

    // Función para restablecer el color del crosshair
    void ResetCrosshair()
    {
        crosshairImage.color = normalColor;
    }
}
