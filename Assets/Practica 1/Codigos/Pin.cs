using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private float umbralCaida = 5f;

    void Start()
    {
        
}
    public bool EstaCaido()
    {
        float angulo = Vector3.Angle(transform.up, Vector3.up);
        return angulo > umbralCaida; 
    }

   
    void Update()
    {
        
    }
}
