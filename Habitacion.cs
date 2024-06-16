using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Habitacion : MonoBehaviour
{
    // Variables
    public bool closeDoors; /* openDoors */ // Indica si se deben cerrar las puertas al entrar a una sala
    public GameObject[] doors; // Array de Puertas
    // public List<GameObject> enemies = new List<GameObject>(); //Lista de enemigos en la sala
    [HideInInspector]
    public bool roomActive; // Indica si la sala está activa (el jugador está dentro)

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /* if (enemies.Count > 0 && roomActive && openDoors )
        {
            for (int i=0; i < enemies.Count; i++)
            {
                if(enemies[i] == null)
                {
                    enemies.RemoveAt(i);
                    i--;
                }
            }

            if (enemies.Count == 0)
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(false); //Desactiva las puertas
                    closeDoors = false; // Marcador indicando que ya hemos limpiado la sala
                }
            }
        } */
    }

    public void OpenDoors()
    {
        foreach(GameObject door in doors)
        {
            door.SetActive(false); //Desactiva las puertas
            closeDoors = false; // Marcador indicando que ya hemos limpiado la sala
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") // Si el jugador colisiona con el collider de una habitación
        {
            ControladorCamara.instance.ChangeTarget(transform); // La camara cambia a esa habitación
            if (closeDoors) // Si se tienen que cerrar las puertas en esa sala
            {
                foreach(GameObject door in doors)
                {
                    door.SetActive(true); //Activa las puertas
                }
            }
            roomActive = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            roomActive = false;
        }
    }
}
