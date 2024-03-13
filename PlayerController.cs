using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;


public class PlayerController : MonoBehaviour
{
 
    private Rigidbody rb; //Componente Rigibody de mi player
    private int count;// Contador de objetos recogidos
    private float movementX; //Creo movimiento X(horizontal)
    private float movementY; //Creo movimiento Y(vertical)
    private float force = 7.0f;// Fuerza del salto
    private float speed = 10.0f; // Velociadad del player
    public TextMeshProUGUI countText; // Texto de la UI
    public GameObject winTextObject; // Mensaje de victoria
    private float ballMass = 1.5f;// Masa de la bola
    private Renderer playerRenderer;// Renderer del player

        
    // Start is called before the first frame update
    void Start(){ 
        rb = GetComponent <Rigidbody>();
        playerRenderer = GetComponent<Renderer>();//Cojo el renderer del player
		rb.mass = ballMass;
        count = 0; 
        SetCountText();
        winTextObject.SetActive(false);
        Debug.Log("Hola, soy un mensaje de debug en Start");
        
    }
    
    //Dar un salto con la tecla Fire (boton derecho del raton)
    void OnFire(){
       Debug.Log("Hola, soy un mensaje de debug en OnFire");
       rb.AddForce(Vector3.up * force,  ForceMode.Impulse); // Añado una fuerza hacia arriba
	   playerRenderer.material.color = Random.ColorHSV();// Cambio el color del player cada vez que salta
    }

    private void FixedUpdate() 
    {
       //Dar un salto con la tecla espacio
       if (Input.GetKeyDown(KeyCode.Space)){
          OnFire();
       }
       Vector3 movement = new Vector3 (movementX,0.0f, movementY); // Tiene 3 dimensiones
	   rb.AddForce(movement * speed, ForceMode.Force); // Añado fuerza al player. ()
        
        //Debug de los valores de movimiento
        Debug.Log("Hola, estoy en el FixedUpdate");
       
    }

    void OnTriggerEnter(Collider other){
       if (other.gameObject.CompareTag("PickUp")){
           other.gameObject.SetActive(false);
          count = count + 1;    
          SetCountText();
       }
   }
    
    void OnMove (InputValue movementValue){
        //Recojo valor del input system
        Vector2 movementVector = movementValue.Get<Vector2>(); //Tiene 2 dimensiones
        
        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }
    
    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
        if (count >= 10)
        {
            winTextObject.SetActive(true);
			GameIsOver();
        }
    }

	public bool GameIsOver()
    {
        return count >= 10;
    }

}