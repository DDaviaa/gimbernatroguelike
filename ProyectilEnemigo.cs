using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProyectilEnemigo : MonoBehaviour
{
    //Variables
    public float speed = 7.5f; //Velocidad del proyectil *por defecto 7.5u por segundo*
    private Vector3 direction; //Dirección del proyectil
    public GameObject impactParticle; //Efecto de particula al impactar con algo

    private void Start()
    {
        direction = (JoystickMove.instance.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            ControladorVidaJugador.instance.DamagePlayer();
        }
        //Debug.Log("Colisión detectada con: " + other.gameObject.name);
        Instantiate(impactParticle, transform.position, transform.rotation);
        ControladorAudio.instance.PlaySFX(3);
        Destroy(gameObject);
    }

    private void OnBecameInvisible() //Si sale de la pantalla el proyectil se destruirá
    {
        Destroy(gameObject);
    }

}
