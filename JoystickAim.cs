using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickAim : MonoBehaviour
{
    //Variables
    private Rigidbody2D rb; //Componente RigidBody
    public Joystick aimJoystick; //Joystick apuntado
    public GameObject bullet; //Proyectil del arma
    public Transform firePoint; //Punto final del arma (de donde sale la bala)
    public Transform gunArm; //Mano con el arma
    public float fireRate = 0.5f; // Cadencia de fuego en segundos *por defecto 0.5balas por segundo*
    private float nextFireTime; // Tiempo para el próximo disparo

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Físicas del personaje
    }

    private void FixedUpdate()
    {
        if (aimJoystick.Direction != Vector2.zero) // Si el joystick de apuntado se mueve
        {
            // Rotación del brazo del arma
            float angle = Mathf.Atan2(aimJoystick.Direction.y, aimJoystick.Direction.x) * Mathf.Rad2Deg;
            gunArm.rotation = Quaternion.Euler(0, 0, angle);

            //Disparos
            if (Time.time >= nextFireTime)
            {
                Instantiate(bullet, firePoint.position, firePoint.rotation);
                ControladorAudio.instance.PlaySFX(9);
                nextFireTime = Time.time + fireRate; // Actualizar el tiempo para el próximo disparo
            }
            
            if (aimJoystick.Direction.x < 0)  // Apuntando hacia la izquierda
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); 
                gunArm.localScale = new Vector3(-0.75f, -0.75f, 1f); 
            }
            else if (aimJoystick.Direction.x > 0)  // Apuntando hacia la derecha
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
                gunArm.localScale = new Vector3(0.75f, 0.75f, 1f); 
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verificar si el jugador entra en contacto con una nueva pistola
        if (other.gameObject.CompareTag("Gun"))
        {
            // Obtener el script de la nueva pistola
            Gun newGun = other.gameObject.GetComponent<Gun>();

            if (newGun != null)
            {
                // Cambiar el proyectil y la cadencia de disparo
                bullet = newGun.bullet;
                fireRate = newGun.fireRate;

                // Actualizar el sprite del arma del jugador
                ControladorAudio.instance.PlaySFX(4);
                SpriteRenderer gunRenderer = gunArm.GetComponent<SpriteRenderer>();
                SpriteRenderer newGunRenderer = other.GetComponent<SpriteRenderer>();
                if (gunRenderer != null && newGunRenderer != null)
                {
                    gunRenderer.sprite = newGunRenderer.sprite;
                }

                // Destruir la pistola del suelo (si es necesario)
                Destroy(other.gameObject);
            }
        }
    }
}
