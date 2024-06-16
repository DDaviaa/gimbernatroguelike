using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour
{
    // Variables
    public string levelTransition; // Nivel a cargar

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(levelTransition); // Ir al nivel inicial
    }

    public void ExitGame()
    {
        Application.Quit(); // Salir de la app
    }
}
