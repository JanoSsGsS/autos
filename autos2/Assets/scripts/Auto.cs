using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Auto : MonoBehaviour
{
    public long puntos;
    public float Combustible = 100;
    public bool chocado = false;
    private Vector3 posicionRespawn;
    private Quaternion rotacionRespawn;
    private DateTime tiempo;
    private DateTime tiempoChoque;
    private DateTime ultimaPosicion = new DateTime();
    public GameObject Explosion;
    private AudioSource audio;
    public List<AudioClip> Sonidos = new List<AudioClip>();
    public float VelocidadMaxima = 20;
    public float velocidad = 0;
    public float velocidadHorizontal = 0.5f;
    public float velocidadFreno =0.5f;
    public float velocidadRotacion = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        audio = this.GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Juego.Jugando && Combustible > 0)
        {
            if (!chocado)
            {
                //Guardar un pos válida
                if ((DateTime.Now - ultimaPosicion).TotalSeconds > 4)
                {
                    ultimaPosicion = DateTime.Now;
                    posicionRespawn = this.transform.position;
                    rotacionRespawn = this.transform.rotation;
                }
                if(Input.GetAxis("Acelerar") >= 0)
                {
                    EncenderLuces (false);
                    ReproducirSonidoMotor();
                    if(velocidad < VelocidadMaxima)
                    {
                        velocidad += Input.GetAxis("Acelerar");
                    }
                }
                else
                {
                    if(Input.GetAxis("Frenar")>0)
                    {
                        ReproducirSonidoFrenada();
                        velocidad -= velocidadFreno;
                        EncenderLuces(true);
                    }
                    else
                    {
                        VelocidadMaxima -=0.25f;
                    }
                    if(velocidad<=0)
                    {
                        DetenerSonido();
                        velocidad =0;
                        EncenderLuces(false);
                    }
                }
                if (velocidad>0)
                {
                    this.transform.Rotate(new Vector3(0f,Input.GetAxis("Horizontal") * velocidadRotacion,0f));
                    this.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * velocidadHorizontal,0, velocidad * Time.deltaTime),Space.Self);
                }
                DescontarCombustible();
            }
            else
            {
                respawn();
            }
        }
        else 
        {
            EncenderLuces(false);
        }
    }
    public void DescontarCombustible()
    {
        if(!Juego.Jugando && Combustible > 0)
        {
            Combustible--;
        }
    }
    public void CargarCombustible()
        {
            Combustible +=10;
        }
    public void EncenderLuces(bool valor)
    {

    }
    public void DetenerSonido()
    {
        if(audio.isPlaying)
        {
            audio.Stop();
        }
    }
    private void SetearSonido(int indice)
        {
            if(audio.clip != Sonidos[indice])
            {
                audio.clip = Sonidos [indice];
            }
        }
        public void ReproducirSonidoExplosion()
        {
            if(!audio.isPlaying || audio.clip != Sonidos[2])
            {
                SetearSonido(2);
                audio.Play();
            }
        }
        
        public void ReproducirSonidoMotor()
        {
            if(!audio.isPlaying || audio.clip != Sonidos[0])
            {
                SetearSonido(0);
                audio.Play();
            }
        }
        
        public void ReproducirSonidoFrenada()
        {
            if(!audio.isPlaying || audio.clip != Sonidos[1])
            {
                SetearSonido(1);
                audio.Play();
            }
        }
private void OnCollisionEnter(Collision col)
{
    if (col.gameObject.tag != "ruta" && col.gameObject.tag != "Terreno")
    {
        velocidad = 0;
        Explotar();
        foreach(MeshRenderer render in this.GetComponentsInChildren<MeshRenderer>())
        {
            render.enabled = false;
        }
        tiempoChoque = DateTime.Now;
        chocado = true;
    }

}
private void Explotar()
{
    GameObject explosion = Instantiate(Explosion, this.transform.position, this.transform.rotation);
    Destroy(explosion,3);
}
private void respawn()
{
    if (chocado && (DateTime.Now - tiempoChoque).TotalSeconds >= 3)
    {
        this.transform.position = posicionRespawn;
        this.transform.rotation = rotacionRespawn;
        chocado = false;
        foreach(MeshRenderer render in this.GetComponentsInChildren<MeshRenderer>())
        {
            render.enabled = true;
        }
    }
}
                
    }

