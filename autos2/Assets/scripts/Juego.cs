using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class Juego : MonoBehaviour
{

    public static bool Jugando = false;
    private static bool victoria = false;
    private static bool derrota = false;
    private static DateTime ultimoTiempo;
    private static int contador = 3;

    void Start()
    {
        

    }

    public static void Victoria()
    {
        victoria = true;
        Jugando = false;
        ultimoTiempo = DateTime.Now;
    }
    public static void GameOver()
    {
        derrota = true;
        Jugando = false;
        ultimoTiempo = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if (derrota || victoria)
        {
            if((DateTime.Now - ultimoTiempo).TotalSeconds >=1 )
            {
                ultimoTiempo = DateTime.Now;
                contador--;
                if(contador == 0)
                {
                    contador = 3;
                    derrota = false;
                    victoria = false;
                    Scene escena = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(escena.name);
                }
            }
        }

    }
}
        