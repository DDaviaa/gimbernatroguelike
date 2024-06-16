using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorCamara : MonoBehaviour
{
    // Variables
    public static ControladorCamara instance; // Instancia 
    public float moveSpeed; // Velocidad de cambio de la camara
    public Transform roomTarget; // Pivote central de la habitación

    void Awake()
    {
        instance = this; //Generamos la instancia
    }

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        if (roomTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(roomTarget.position.x, roomTarget.position.y, transform.position.z), moveSpeed * Time.deltaTime);
        }
    }

    public void ChangeTarget(Transform newTarget)
    {
        roomTarget = newTarget;
    }
}
