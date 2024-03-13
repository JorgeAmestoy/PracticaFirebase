using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

public GameObject player;//Creo objeto de tipo GameObject. Arrastro en UNity el player a la derecha para darle "valor" al GAmeObject
private Vector3 offset;//Vector de 3 posiciones. ES la distancia entre la cámara y el player
public float moveSpeed = 15f; // Velocidad de movimiento de la cámara
public float turnSpeed = 30f; // Velocidad de rotación de la cámara alrededor del jugador

    // Start is called before the first frame update
    void Start()
    {

 	offset = transform.position - player.transform.position; //COn transform. position sé la posicion del objeto, en este caso cámara pq estoy e el script de la cámara. COn el player.transform.position cojo la posicionm del player.
        
    }

    // Cambio Update por LateUpdate siendo LAteUpdate otro método predefinido de Unity. (mayusuculas)
    void Update(){
     /*
        if(Input.GetKey(KeyCode.W))// Para que al pulsar W la cámara se mueva hacia adelante
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);//Mover la cámara hacia adelante

       
        if(Input.GetKey(KeyCode.S))// Para que al pulsar S la cámara se mueva hacia atrás
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);//Mover la cámara hacia atrás*/
        
        
         /*Mover cámara a la derecha o izquierda
        if(Input.GetKey(KeyCode.A))// Para que al pulsar A la cámara se mueva hacia adelante
            transform.Translate(-Vector3.right * moveSpeed * Time.deltaTime);//Mover la cámara hacia adelante

       
        if(Input.GetKey(KeyCode.D))// Para que al pulsar D la cámara se mueva hacia atrás
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);//Mover la cámara hacia atrás*/
        
        
        /*Rotar cámara
        if(Input.GetKey(KeyCode.A))
            transform.Rotate(Vector3.up, -20.0f * Time.deltaTime);

        if(Input.GetKey(KeyCode.D))
            transform.Rotate(Vector3.up, 20.0f * Time.deltaTime);*/
        

       /* Rotar cámara alrededor del jugador
        if (Input.GetKey(KeyCode.A))// Para que al pulsar A la cámara rote alrededor del jugador .
            transform.RotateAround(player.transform.position, Vector3.up, -turnSpeed * Time.deltaTime);
		
      
        if (Input.GetKey(KeyCode.D))// Para que al pulsar D la cámara rote alrededor del jugador 
            transform.RotateAround(player.transform.position, Vector3.up, turnSpeed * Time.deltaTime);*/
       
       }

    void LateUpdate() {//LateUpdte se llama una vez por frame después de que se haya completado todas las funciones Update
        
        // Esto es para que la cámara siga al jugador. Si está desilenciada, no uso las teclas W y S
        transform.position = player.transform.position + offset;//Mantener el mismo desplazamiento entre la cámara y el jugador a lo largo del juego
        
      
    }
/*
    void RotateCameraAndPlayer(float angle){
        
        //Rotar la camara alrededor del jugador
        transform.RotateAround(player.transform.position, Vector3.up, angle);
        
        //Rotar el jugador
        player.transform.Rotate(Vector3.up, angle);
        
    }*/
    
}
