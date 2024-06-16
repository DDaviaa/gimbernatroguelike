using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneracionProcedural : MonoBehaviour
{
    // Variables
    public GameObject layoutRoom; // Habitación para generar
    public int roomsToExplore; // Numero de habitaciones totales que se generaran hasta la ultima
    public Color startColor, endColor; // Habitación inicial y habitacón final
    public Transform generationPoint; // Punto central donde se genera la habitación
    public enum Direction {up, right, down, left}; // Posibles direcciones hacia donde se van a generar las habitaciones
    public Direction selectedDirection; //Dirección hacia donde se va a generar una habitación.
    private float xOffeset = 18f, yOffSet = 10; //Distancias estandar entre las habitaciones
    public LayerMask whatIsRoom; // Layout habitaciones
    private GameObject endRoom; // La habitación de escape/final
    private List<GameObject> layoutRoomObjects = new List<GameObject>(); // Lista de habitaciones para generar
    public RoomFrefabs rooms; // Todos los prefabs de las habitaciones
    private List<GameObject> generatedOutlines = new List<GameObject>(); // Lista de habitaciones generadas
    public SueloHabitacion centerStart, centerEnd; //Suelos inicio y final que serán estáticos
    public SueloHabitacion[] potentialsCenters; //Pool de suelos

    void Start()
    {
        Instantiate(layoutRoom, generationPoint.position, generationPoint.rotation).GetComponent<SpriteRenderer>().color = startColor; //Genera la primera habitación con el color inicial
        selectedDirection = (Direction)Random.Range(0, 4); // Selecciona una dirección aleatoria para seguir generando las habitaciones
        MoveGenerationPoint();

        for (int i = 0; i < roomsToExplore; i++)
        {
            GameObject newRoom = Instantiate(layoutRoom, generationPoint.position, generationPoint.rotation); //Generar una habitación
            layoutRoomObjects.Add(newRoom); //Añadirla al layout
            if (i + 1 == roomsToExplore) // Generar la ultima habitación
            {
                newRoom.GetComponent<SpriteRenderer>().color = endColor;
                layoutRoomObjects.RemoveAt(layoutRoomObjects.Count - 1);
                endRoom = newRoom;
            }
            selectedDirection = (Direction)Random.Range(0, 4); // Selecciona una dirección aleatoria para seguir generando las habitaciones
            MoveGenerationPoint();
            while (Physics2D.OverlapCircle(generationPoint.position, .2f, whatIsRoom)) // Si dentro del layout de las habitaciones cuando se mueve el generation point se detecta una colisión sigue moviendo el punto hasta que haya un espacio libre
            {
                MoveGenerationPoint();
            }
        }

        // Crear la forma que tendra la habitacion
        CreateRoomOutline(Vector3.zero); //Inicial
        foreach(GameObject room in layoutRoomObjects) // Para cada habitación que se ha generado
        {
            CreateRoomOutline(room.transform.position);
        }
        CreateRoomOutline(endRoom.transform.position); //Final

        foreach(GameObject outline in generatedOutlines) // Generar el suelo de la habitación
        {
            bool generateCenter = true;

            if (outline.transform.position == Vector3.zero) // Habitación inicial
            {
                Instantiate(centerStart, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Habitacion>();
                generateCenter = false;
            }

            if (outline.transform.position == endRoom.transform.position) // Habitación final
            {
                Instantiate(centerEnd, outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Habitacion>();
                generateCenter = false;
            }

            if(generateCenter) // Habitación random del array potential centers
            {
                int centerSelect = Random.Range(0, potentialsCenters.Length); //Selecciona un suelo random del array de suelos
                Instantiate(potentialsCenters[centerSelect], outline.transform.position, transform.rotation).theRoom = outline.GetComponent<Habitacion>();
            }
        }
    }

    void FixedUpdate()
    {
        
    }

    public void MoveGenerationPoint() // Mover el punto central de generación
    {
        switch(selectedDirection)
        {
            case Direction.up:
                generationPoint.position += new Vector3(0f, yOffSet, 0f);
                break;
            case Direction.down:
                generationPoint.position += new Vector3(0f, -yOffSet, 0f);
                break;
            case Direction.right:
                generationPoint.position += new Vector3(xOffeset, 0f, 0f);
                break;
            case Direction.left:
                generationPoint.position += new Vector3(-xOffeset, 0f, 0f);
                break;
        }
    }

    public void CreateRoomOutline(Vector3 roomPosition) // Crear la forma que tendra la habitacion
    {
        bool roomUp = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, yOffSet, 0f), .2f, whatIsRoom); // Check si hay una habitacion arriba
        bool roomDown = Physics2D.OverlapCircle(roomPosition + new Vector3(0f, -yOffSet, 0f), .2f, whatIsRoom); // Check si hay una habitacion abajo
        bool roomLeft = Physics2D.OverlapCircle(roomPosition + new Vector3(-xOffeset, 0f, 0f), .2f, whatIsRoom); // Check si hay una habitacion izquierda
        bool roomRight = Physics2D.OverlapCircle(roomPosition + new Vector3(xOffeset, 0f, 0f), .2f, whatIsRoom); // Check si hay una habitacion derecha
        int directionDetection = 0;

        // Detección de habitaciones que esten al lado, si hay una ++ para saber las posibles salidas
        if (roomUp)
        {
            directionDetection ++;
        }
        if (roomDown)
        {
            directionDetection ++;
        }
        if (roomLeft)
        {
            directionDetection ++;
        }
        if (roomRight)
        {
            directionDetection ++;
        }

        switch (directionDetection)
        {
            case 0:
                Debug.LogError("No se ha encontrado ninguna habitación para generar.");
                break;
            case 1:
                if (roomUp)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleUp, roomPosition, transform.rotation));
                }
                if (roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleDown, roomPosition, transform.rotation));
                }
                if (roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleRight, roomPosition, transform.rotation));
                }
                if (roomLeft)
                {
                    generatedOutlines.Add(Instantiate(rooms.singleLeft, roomPosition, transform.rotation));
                }
                break;
            case 2:
                if (roomUp && roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpDown, roomPosition, transform.rotation));
                }
                if (roomLeft && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftRight, roomPosition, transform.rotation));
                }
                if (roomUp && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleUpRight, roomPosition, transform.rotation));
                }
                if (roomDown && roomRight)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleRightDown, roomPosition, transform.rotation));
                }
                if (roomLeft && roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleDownLeft, roomPosition, transform.rotation));
                }
                if (roomLeft && roomUp)
                {
                    generatedOutlines.Add(Instantiate(rooms.doubleLeftUp, roomPosition, transform.rotation));
                }               
                break;
            case 3:
                if (roomUp && roomRight && roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleUpRightDown, roomPosition, transform.rotation));
                }
                if (roomLeft && roomRight && roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleRightDownLeft, roomPosition, transform.rotation));
                }
                if (roomDown && roomLeft && roomUp)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleDownLeftUp, roomPosition, transform.rotation));
                }
                if (roomLeft && roomRight && roomUp)
                {
                    generatedOutlines.Add(Instantiate(rooms.tripleLeftUpRight, roomPosition, transform.rotation));
                }    
                break;
            case 4:
                if (roomLeft && roomRight && roomUp && roomDown)
                {
                    generatedOutlines.Add(Instantiate(rooms.quadrupleUpRightDownLeft, roomPosition, transform.rotation));
                }    
                break;
        }
    }
}

[System.Serializable]
public class RoomFrefabs //Clase de todos los tipos de habitación
{
    public GameObject singleUp, singleDown, singleLeft, singleRight,
    doubleUpDown, doubleLeftRight, doubleUpRight, doubleRightDown, doubleDownLeft, doubleLeftUp,
    tripleUpRightDown, tripleRightDownLeft, tripleDownLeftUp, tripleLeftUpRight,
    quadrupleUpRightDownLeft;
}