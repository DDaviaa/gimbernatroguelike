using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorAudio : MonoBehaviour
{
    // Variables
    public static ControladorAudio instance; // Instanciar el script de la musica que se escuchara durante el juego
    public AudioSource levelMusic, gameOverMusic, gameWinMusic; // Diferentes canciones/sonidos que se escucharan mientras jugamos
    public AudioSource[] sfx; // Array de los diferentes sonidos del juego

    private void Awake()
    {
        instance = this; //Generamos la instancia
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        
    }

    public void PlayGameOver()
    {
        levelMusic.Stop(); // Paramos la musica del nivel actual
        gameOverMusic.Play(); // Ponemos el game over
    }

    public void PlayGameWin()
    {
        levelMusic.Stop(); // Paramos la musica del nivel actual
        gameWinMusic.Play(); // Ponemos el game over
    }

    public void PlaySFX(int sfxNumber) // Se llamara a esta función pasando un numero del elemento de los diferentes sonidos que hay en el array
    {
        sfx[sfxNumber].Stop(); //Stop para que el sonido empiece desde 0
        sfx[sfxNumber].Play(); //Play sonido
    }

}
