using UnityEngine;
using UnityEngine.UI;

public class CrosshairController : MonoBehaviour
{
    public Image crosshairImage; // Referencia a la imagen del crosshair
    public Color normalColor = Color.white; // Color normal del crosshair
    public Color shootColor = Color.red; // Color cuando se dispara

    void Start()
    {
        // Aseg�rate de que la imagen del crosshair est� asignada
        if (crosshairImage == null)
        {
            crosshairImage = GetComponent<Image>();
        }
        crosshairImage.color = normalColor; // Establece el color inicial
    }

    // Funci�n para cambiar el color del crosshair al disparar
    public void OnShoot()
    {
        crosshairImage.color = shootColor; // Cambia el color al disparar
        Invoke("ResetCrosshair", 0.1f); // Vuelve al color normal despu�s de 0.1 segundos
    }

    // Funci�n para restablecer el color del crosshair
    void ResetCrosshair()
    {
        crosshairImage.color = normalColor;
    }
}
