using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControladorNivel : MonoBehaviour
{
    // Variables
    public static ControladorNivel instance; // Instancia
    public float waitLevelTransition = 3f; //Espera transición para cambiar de nivel
    public string levelTransition; // Nivel a cambiar

    void Awake()
    {
        instance = this; //Generamos la instancia
    } 

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        
    }

    public IEnumerator LevelEnd() //Corrutina 
    {
        ControladorAudio.instance.PlayGameWin(); // Suena el sonido de la victoria
        JoystickMove.instance.freeze = true; // Freezeamos al jugador
        ControladorInterfaz.instance.StartFadeIn(); // La pantalla se oscurece
        yield return new WaitForSeconds(waitLevelTransition);
        SceneManager.LoadScene(levelTransition); // Se cambia de escena 
        JoystickMove.instance.freeze = false; // Freezeamos al jugador
    }
}
