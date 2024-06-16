using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour
{
    //Variables
    public float speed = 7.5f; //Velocidad del proyectil *por defecto 7.5u por segundo*
    private Rigidbody2D rb; //Fisicas de la bala
    public int damage = 50; //Daño de la bala *por defecto 50*
    public GameObject impactParticle; //Efecto de particula al impactar con algo

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Físicas de la bala
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemigoControl>().DamageEnemy(damage);
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
