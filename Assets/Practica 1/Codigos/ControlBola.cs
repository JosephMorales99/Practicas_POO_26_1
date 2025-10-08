using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ControlBola : MonoBehaviour
{

    public Rigidbody rb;

    //Variables para apuntar
    public float velocidadDeApuntado = 5f;
    public float limiteIzquierdo = -2F;
    public float limiteDerecho = 2f;


    public float fuerzaDeLanzamiento = 1000f;

    private bool haSidoLanzada = false;

    //TODO: Referencia a la camara y score
    public CameraFollow cameraFollow;
    public ScoreManager scoreManager;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {      //Expresion:mientras que haSidoLanzada sea falso puedes disparar
        if (haSidoLanzada==false)
        {
            Apuntar();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Lanzar();
            }
        }
    }

    void Apuntar()
    {
        // 1. Leer un input Horizontal de tipo Axis, te permite registrar
        //entradas co0n las teclas A y D, y flecha izquierda y flecha Derecha
        float inputHorizontal = Input.GetAxis("Horizontal");

        //2. Mover la bola hacia los lados
        transform.Translate(Vector3.right * inputHorizontal * velocidadDeApuntado * Time.deltaTime);

        //3. Delimitar el movimiento de la bola 
        Vector3 posicionActual = transform.position;
        //transform.position me permite saber cual es la posicion actual de la bola en la escena

        posicionActual.x = Mathf.Clamp(posicionActual.x, limiteIzquierdo, limiteDerecho);

        transform.position = posicionActual;
        
    }


    void Lanzar()
    {
        haSidoLanzada = true;
        rb.AddForce(Vector3.forward * fuerzaDeLanzamiento);

        if(cameraFollow !=null)cameraFollow.IniciarSeguimiento();
        //else 
        //{
            //Debug.LogWarning("El Rigidbody no esta asignado en" + gameObject.name); 
        //}

        //if(CamaraPrincipal != null)
        //{
           // CamaraPrincipal.SetParent(transform);
        //}
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pin"))
        {
            if(cameraFollow != null) cameraFollow.DetenerSeguimiento();

            if (scoreManager != null) Invoke("CalcularPuntaje", 2f);
        }
        }

    void CalcularPuntaje()
    {
        scoreManager.CalcularPuntaje();
    }

}//Bienvenido a la entrada al infierno >:O
