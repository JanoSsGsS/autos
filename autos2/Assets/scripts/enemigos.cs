using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigos : MonoBehaviour
{
    // Start is called before the first frame update
    public float velocidad;
    public List<Transform> puntos = new List<Transform>();
    public GameObject ruta;
    public int indice = 0;
    private Transform target;
    private Vector3 Direccion;
    private Quaternion delta;
    private Collider ultimoTarget;
    void Start()
    {
        for(int i = 0; i < ruta.transform.childCount; i++)
        {
            puntos.Add(ruta.transform.GetChild(i));
        }
        target = puntos [indice];
        var lookat = Quaternion.LookRotation(target.position - this.transform.position);
        delta = Quaternion.Inverse(lookat)* this.transform.rotation;
    }
        

    // Update is called once per frame
    void Update()
    {
        if(Juego.Jugando)
        {
            transform.rotation = Quaternion.LookRotation(target.position - this.transform.position)*delta;
            transform.position = Vector3.MoveTowards(transform.position,target.position,velocidad * Time.deltaTime);

        }
    }
    private void OntriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player")
        {

        }
        else
        {
            if(col.gameObject.tag == "Respawn" )
            {
                
                try 
                {
                    if(col !=ultimoTarget )
                    {
                        indice = col.gameObject.GetComponent <indice>().numero + 1;
                        target = puntos[indice];
                        ultimoTarget = col;
                    }
                } 
                catch
                {
                    
                }
            }
        }
            
    }
}
