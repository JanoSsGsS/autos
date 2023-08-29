using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Semaforo : MonoBehaviour
{
    public List<Light> lucesRojas = new List<Light>();
    public List<Light> lucesAmarillas = new List<Light>();
    public List<Light> lucesVerdes = new List<Light>();

    public int contador = 3;

    private DateTime TiempoDetenido;
    // Start is called before the first frame update
    void Start()
    {
        TiempoDetenido = DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        if ((DateTime.Now - TiempoDetenido).TotalSeconds >=1)
        {
            contador --;
            TiempoDetenido = DateTime.Now;
            switch(contador)
            {
                case 1:
                {
                    Cambiar(lucesAmarillas,true);
                    break;
                }
                case 0:
                {
                    Cambiar(lucesAmarillas,false);
                    Cambiar(lucesRojas,false);
                    Cambiar(lucesVerdes,true);
                    Juego.Jugando = true;
                    break;
                }
            }
        }
    }

    private void Cambiar(List<Light> luces, bool valor)
    {
        foreach (Light Luz in luces)
        {
            Luz.enabled = valor;
        }
    }
}
