using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlJugador : MonoBehaviour
{
    // Movimiento
    public float velocidad = 5f; //velocidad para el movimiento del jugador
    public float gravedad = -9.8f; //Para controlar velocidad o fuerza aplicada a la gravedad del jugador
    private CharacterController controller; // Es la pieza de Lego que nos va a permitir movimiento en el juego
    private Vector3 velocidadVertical; //Nos permite saber que tan rapido caemos

    //Variables vista
    public Transform camara; //Es para registrar que camara va a funcionar como los ojos del jugador
    public float sensibilidadMouse = 200f; //Que tan rapido gira el mouse para voltear a ver en diferentes direcciones
    private float rotacionXVertical = 0f; //Para indicar cuantos grados va a poder voltear a ver arriba o abajo el jugador

    void Start()
    {
        controller = GetComponent<CharacterController>();

        //Esta linea bloquea el puntero del mouse en los limites de la pantalla
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        ManejadorVista();
        ManejadorMovimiento();
    }

    void ManejadorVista()
    {
        // 1. Leer el input del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensibilidadMouse * Time.deltaTime; 
        float mouseY = Input.GetAxis("Mouse Y") * sensibilidadMouse * Time.deltaTime;

        // 2. Construir la rotacion horizontal 
        transform.Rotate(Vector3.up * mouseX);

        // 3. Registro de la rotacion vertical
        rotacionXVertical -= mouseY;

        // 4. Limitar la rotacion vertical

        Mathf.Clamp(rotacionXVertical, -90f, 90f);

        // 5. Aplicar la rotacion
        //son los ejes X  Y  Z
        camara.localRotation = Quaternion.Euler(rotacionXVertical, 0, 0); 

    }

    void ManejadorMovimiento()
    {
        // 1. Leer el input de movimiento (WASD o las flechas de direccion)
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        // 2. Crear el vector de movimiento
        // Se almacena de forma local el registro de direccion de movimiento
        Vector3 direccion = transform.right * inputX + transform.forward * inputZ;
        //registro de direccion en la cual se movera el personaje

        // 3. Mover el CharacterController
        controller.Move(direccion * velocidad * Time.deltaTime);

        // 4. Aplicar la gravedad
        //Registro si estoy en el piso para un futuro comportamiento de salto
        if(controller.isGrounded && velocidadVertical.y < 0)
        {
            velocidadVertical.y = -2f; //Una pequeña fuerza hacia abajo para mantenerlo pegado al piso
        }

        //Aplicamos la aceleracion de la gravedad
        velocidadVertical.y += gravedad + Time.deltaTime;

        //Movemos el controlador hacia abajo
        controller.Move(velocidadVertical * Time.deltaTime);
    }

}//hell
