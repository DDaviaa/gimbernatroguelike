using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    // Variables
    public static JoystickMove instance; // Instancia el script de movimiento que se usará para detectar la ubicación actual del jugador
    private Rigidbody2D rb; // Componente RigidBody
    public Joystick movementJoystick; // Joystick movimiento
    public Animator anim; // Animación
    public float playerSpeed; // Velocidad del personaje
    public bool freeze = false; // Congelar al jugador

    private void Awake()
    {
        instance = this; // Generamos la instancia
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Físicas del personaje
    }

    private void FixedUpdate()
    {
        if (!freeze) // Solo permite el movimiento si freeze es false
        {
            // Movimiento del personaje
            if (movementJoystick.Direction != Vector2.zero) // Si el joystick de movimiento se mueve
            {
                anim.SetBool("Movimiento", true);
                rb.velocity = new Vector2(movementJoystick.Direction.x * playerSpeed, movementJoystick.Direction.y * playerSpeed);
            }
            else
            {
                anim.SetBool("Movimiento", false);
                rb.velocity = Vector2.zero; // Si no hay movimiento, la velocidad es cero
            }
        }
        else
        {
            // Si freeze es true, la velocidad es cero
            rb.velocity = Vector2.zero;
            anim.SetBool("Movimiento", false);
        }
    }
}
