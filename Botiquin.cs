using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : MonoBehaviour
{
    //Variables
    public int livesHealed = 1; // Vidas curadas por el botiquin, se puede alterar en un prefab

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && ControladorVidaJugador.instance.actualLives < ControladorVidaJugador.instance.maxLives) //Si el jugador coge el botiquin y le falta vida lo pilla y lo cura
        {
            ControladorVidaJugador.instance.HealPlayer(livesHealed);
            ControladorAudio.instance.PlaySFX(5);
            Destroy(gameObject);
        }
    }
}
