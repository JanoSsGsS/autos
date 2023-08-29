using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class meta : MonoBehaviour
{
    public GameObject cubo; // Referencia al cubo desactivado en el inspector
    private bool cuboVisible = false; // Estado del cubo

    private List<string> posiciones = new List<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Auto"))
        {
            posiciones.Add(other.transform.name);

            int puntaje = 0;
            switch (posiciones.Count)
            {
                case 1: puntaje = 500; break;
                case 2: puntaje = 400; break;
                case 3: puntaje = 300; break;
                case 4: puntaje = 200; break;
                case 5: puntaje = 100; break;
            }

            int puntajeActual = PlayerPrefs.GetInt(other.transform.name + "_puntaje", 0);
            puntaje += puntajeActual;

            PlayerPrefs.SetInt(other.transform.name + "_puntaje", puntaje);

            if (other.transform.name == "auto azul")
            {
                ToggleCubeVisibility(); // Activa/desactiva el cubo al cruzar la meta con "auto azul"
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }
    }

    private void ToggleCubeVisibility()
    {
        cuboVisible = !cuboVisible;
        cubo.SetActive(cuboVisible);
    }

    private void ResetScene()
    {
        // Clear the positions
        posiciones.Clear();

        // Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 150, 300));
        GUILayout.Label("Posiciones:");

        foreach (string autoName in posiciones)
        {
            int puntaje = PlayerPrefs.GetInt(autoName + "_puntaje", 0);
            GUILayout.Label(autoName + " - Puntaje: " + puntaje);
        }

        GUILayout.EndArea();
    }
}