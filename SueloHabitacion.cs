using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SueloHabitacion : MonoBehaviour
{
    // Variables
    public List<GameObject> enemies = new List<GameObject>(); //Lista de enemigos en la sala
    public bool openWhenEnemiesCleared; // Abrir las puertas cuando se matan a los enemigos
    public Habitacion theRoom;

    void Start()
    {
        if(openWhenEnemiesCleared)
        {
            theRoom.closeDoors = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (enemies.Count > 0 && theRoom.roomActive /* && openDoors */)
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
                theRoom.OpenDoors();
            }
        }
    }
}
