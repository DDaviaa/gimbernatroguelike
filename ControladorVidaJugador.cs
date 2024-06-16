using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorVidaJugador : MonoBehaviour
{
    //Variables
    public static ControladorVidaJugador instance; //Instancia el script del control de la vida para detectar la vida del jugador a tiempo real
    public int actualLives; //Vidas actuales
    public int maxLives; //Vida máxima
    public float invulnerabilityTime = 1f; // Tiempo de invulnerabilidad en segundos
    private bool isInvulnerable = false; // Booleano de la invulnerabilidad
    public SpriteRenderer body; // El cuerpo del enemigo

    private void Awake()
    {
        instance = this; //Generamos la instancia
    }

    private void Start()
    {
        actualLives = maxLives; // Cuando empieza la partida la vida máxima será la actual

        ControladorInterfaz.instance.livesSlider.maxValue = maxLives;
        ControladorInterfaz.instance.livesSlider.value = actualLives;
        ControladorInterfaz.instance.livesText.text = actualLives.ToString() + " / " + maxLives.ToString();
    }

    private void FixedUpdate()
    {

    }

    public void DamagePlayer()
    {
        if(isInvulnerable == false)
        {
            actualLives--; // Si el personaje recibe un golpe se le resta una vida
            ControladorAudio.instance.PlaySFX(8);
            StartCoroutine(Invulnerability());

            if(actualLives <= 0)
            {
                ControladorAudio.instance.PlaySFX(6);
                JoystickMove.instance.gameObject.SetActive(false); // El personaje "muere"
                ControladorInterfaz.instance.deathScreen.SetActive(true); // Pantalla de muerte
                ControladorAudio.instance.PlayGameOver();
            }

            ControladorInterfaz.instance.livesSlider.value = actualLives;
            ControladorInterfaz.instance.livesText.text = actualLives.ToString() + " / " + maxLives.ToString();
        }
    }

    private IEnumerator Invulnerability() // Corrutina para la invulnerabilidad
    {
        isInvulnerable = true;
        body.color = new Color(1f, 1f, 1f, .5f); // Transparente
        yield return new WaitForSeconds(invulnerabilityTime);
        isInvulnerable = false;
        body.color = new Color(1f, 1f, 1f, 1f); // Volver al color original
    }

    public void HealPlayer(int healAmount)
    {
        if (actualLives < maxLives)
        {
            actualLives += healAmount;
            if (actualLives > maxLives) 
            {
                actualLives = maxLives; // Asegurarse de que no exceda el máximo de vidas
            }
            ControladorInterfaz.instance.livesSlider.value = actualLives;
            ControladorInterfaz.instance.livesText.text = actualLives.ToString() + " / " + maxLives.ToString();
        }
    }
}
