using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemigoControl : MonoBehaviour
{
    // Variables
    private Rigidbody2D rb; // Componente RigidBody
    public float moveSpeed; // Velocidad del enemigo
    public float rangeDetectionPlayer; // Rango de detección del jugador
    private Vector2 moveDirection; // Dirección de movimiento del enemigo
    public Animator anim; // Animación
    public int health = 150; // Vida del enemigo *por defecto 150*
    public GameObject deathParticle; // Partículas al morir
    public bool shouldShoot; // ¿Debería el enemigo poder disparar?
    public GameObject bullet; // Proyectil del enemigo
    public Transform firePoint; // "Boca del enemigo" (de donde sale el proyectil que dispara)
    public float fireRate; // Cadencia de fuego en segundos
    private float nextFireTime; // Tiempo para el próximo disparo
    public SpriteRenderer body; // El cuerpo del enemigo
    public bool shouldDrop; // ¿Debería el enemigo dropear items?
    public GameObject itemToDrop; // Item a dropear
    public float itemDropPercent; // Porcentaje de drop
    public bool shouldPatrol; // ¿Debería patrullar?
    public Transform[] patrolPoints; // Puntos de patrulla
    private int currentPatrolIndex; // Índice del punto de patrulla actual

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Físicas del enemigo
        if (shouldPatrol)
        {
            currentPatrolIndex = Random.Range(0, patrolPoints.Length); // Seleccionar un punto de patrulla inicial aleatorio
        }
    }

    private void FixedUpdate()
    {
        if (body.isVisible && JoystickMove.instance.gameObject.activeInHierarchy) // Si el cuerpo del enemigo se muestra en pantalla procede 
        {
            float distanceToPlayer = Vector2.Distance(transform.position, JoystickMove.instance.transform.position);
            if (distanceToPlayer < rangeDetectionPlayer)
            {
                moveDirection = (JoystickMove.instance.transform.position - transform.position).normalized; // Guardar la dirección hacia el jugador
                rb.velocity = moveDirection * moveSpeed; // Mover el enemigo en esa la dirección

                FlipTowardsPlayer(); // Mirar hacia el jugador

                if (shouldShoot && Time.time >= nextFireTime)
                {
                    Instantiate(bullet, firePoint.position, firePoint.rotation);
                    ControladorAudio.instance.PlaySFX(10);
                    nextFireTime = Time.time + fireRate; // Actualizar el tiempo para el próximo disparo
                }

                if (moveSpeed != 0)
                {
                    anim.SetBool("Movimiento", true);
                }
            }
            else
            {
                if (shouldPatrol) // Si el enemigo patrulla
                {
                    moveDirection = (patrolPoints[currentPatrolIndex].position - transform.position).normalized;
                    rb.velocity = moveDirection * moveSpeed;
                    FlipTowardsPoint(patrolPoints[currentPatrolIndex].position);

                    if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.5f)
                    {
                        currentPatrolIndex = Random.Range(0, patrolPoints.Length); // Seleccionar un nuevo punto de patrulla aleatorio
                    }

                    anim.SetBool("Movimiento", true);
                }
                else
                {
                    rb.velocity = Vector2.zero; // Parar el movimiento del enemigo si no patrulla
                    anim.SetBool("Movimiento", false);
                }
            }
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void FlipTowardsPlayer()
    {
        Vector3 scale = transform.localScale;
        if (JoystickMove.instance.transform.position.x < transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z); // Mirar hacia la izquierda
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z); // Mirar hacia la derecha
        }
    }

    private void FlipTowardsPoint(Vector3 point)
    {
        Vector3 scale = transform.localScale;
        if (point.x < transform.position.x)
        {
            transform.localScale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z); // Mirar hacia la izquierda
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(scale.x), scale.y, scale.z); // Mirar hacia la derecha
        }
    }

    public void DamageEnemy(int damage) //Función para el daño
    {
        Debug.Log("Enemigo recibe daño: " + damage);
        health -= damage; //El daño que recibe se resta de la vida
        ControladorAudio.instance.PlaySFX(1);
        StartCoroutine(FlashDamage()); // Iniciar la corrutina para el flash

        if (health <= 0) //Si la vida es menor a 0 el enemigo desaparece
        {
            Instantiate(deathParticle, transform.position, transform.rotation);
            if (shouldDrop)
            {
                float dropChance = Random.Range(0f, 100f); // Generar un número aleatorio entre 0 y 100
                if (dropChance <= itemDropPercent) // Comparar con el porcentaje de drop
                {
                    Instantiate(itemToDrop, transform.position, Quaternion.identity); // Dropear el item
                }
            }
            ControladorAudio.instance.PlaySFX(0);
            Destroy(gameObject);
        }
    }

    private IEnumerator FlashDamage() //Corrutina para el flash
    {
        body.color = new Color(1f, 0f, 0f, .5f);
        yield return new WaitForSeconds(0.1f); 
        body.color = new Color(1f, 1f, 1f, 1f); // Volver al color original
    }
}