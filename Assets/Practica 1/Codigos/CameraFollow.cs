using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform objetivo;

    //Retoma el componente Transform position x y z
    public Vector3 offset = new Vector3(0f, 3f, -6f);

    private bool seguir = false;
    void Start()
    {
      
    }

    
    void LateUpdate()
    {
        if(seguir && objetivo != null)
        {
            transform.position = objetivo.position + offset;
        }
    }

    public void IniciarSeguimiento()
    {
        seguir = true;
    }

    public void DetenerSeguimiento()
    {
        seguir = false;
    }

}//Bienvenido a la entrada al infierno >:O
