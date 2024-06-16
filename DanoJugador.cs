using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanoJugador : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
    // Funciones que dañan al jugador al colisionar con GameObjects que tengan este script
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ControladorVidaJugador.instance.DamagePlayer();
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ControladorVidaJugador.instance.DamagePlayer();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ControladorVidaJugador.instance.DamagePlayer();
        }
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ControladorVidaJugador.instance.DamagePlayer();
        }
    }
}
