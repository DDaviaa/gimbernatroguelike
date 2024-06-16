using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControladorInterfaz : MonoBehaviour
{
    //Variables
    public static ControladorInterfaz instance; // Instanciar interfaz
    public Slider livesSlider; // Slider de las vidas visual (barra verde)
    public Text livesText; // Contador de vidas en texto
    public GameObject deathScreen; // Pantalla de muerte
    public Image fadeScreen; // Imagen de transición
    public float fadeSpeed; // Velocidad de transición
    private bool fadeIn, fadeOut; // Transición

    private void Awake()
    {
        instance = this; //Generamos la instancia
    }

    private void Start()
    {
        fadeOut = true; // Se desvanace la imagen
        fadeIn = false; 
    }

    private void FixedUpdate()
    {
        if (fadeOut)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 0f)
            {
                fadeOut = false;
                fadeScreen.gameObject.SetActive(false); // Desactiva el GameObject cuando el desvanecimiento está completo

            }
        }

        if (fadeIn)
        {
            fadeScreen.color = new Color(fadeScreen.color.r, fadeScreen.color.g, fadeScreen.color.b, Mathf.MoveTowards(fadeScreen.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (fadeScreen.color.a == 1f)
            {
                fadeIn = false;
            }
        }
    }

    public void StartFadeIn()
    {
        fadeScreen.gameObject.SetActive(true); // Asegura que el GameObject esté activo al comenzar el desvanecimiento
        fadeIn = true;
        fadeOut = false;
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Menu Inicial");
    }

}
