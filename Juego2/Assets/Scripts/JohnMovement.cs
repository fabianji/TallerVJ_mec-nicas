using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JohnMovement : MonoBehaviour
{
    public float Speed;
    public float JumpForce;
    public GameObject BulletPrefab;
    public bool emparejado = false;
    //public bool encasa = false;
    

    private Rigidbody2D Rigidbody2D;
    private Animator Animator;
    private float Horizontal;
    //private bool Grounded;
    //private float LastShoot;
    //private int Health = 5;
    
    //public GameObject pareja;

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        transform.localScale = new Vector3(0.2f, 0.2f,1.0f);
    }   

    private void Update()
    {
        // Movimiento
        //transform.localScale = new Vector3(0.2f*transform.localScale.x, 0.2f*transform.localScale.y,1.0f);
        Horizontal = Input.GetAxisRaw("Horizontal");


        if (Horizontal < 0.0f) transform.localScale = new Vector3(-0.2f, 0.2f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(0.2f, 0.2f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);

        // Detectar Suelo
        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        

        // Salto
        if (Input.GetKeyDown(KeyCode.W) )
        {
            Jump();
        }


        // Tecla1
        if (Input.GetKeyDown(KeyCode.Alpha1) )
        {
            if (emparejado ==true ) {
                Debug.Log("Puedes usar tecla 1");
                //ejecutar set active..
                //SceneManager.LoadScene(1);
        //-- ejecutar set active  

            }
            else{
                Debug.Log("No puedes usar tecla1");
            }

            
        }

        //Debug.Log(transform.position.x);
       // Debug.Log("emparejado");
       //Debug.Log(emparejado);
        //Debug.Log("encasa");
        //Debug.Log(encasa);



        if( transform.position.x <= -1 && emparejado == false) Debug.Log("tefaltapareja");
        //else if (transform.position.x > -1 && emparejado == true) Debug.Log("Lleva tu pareja a tu casa");
        else if (transform.position.x <= -1 && emparejado == true)
        {
            Debug.Log("ganaron!");
            SceneManager.LoadScene(3);
        } 
        //else Debug.Log("Busca a tu parejita y llega a casa con ella");

        if (transform.position.y < -3)
        {
            Debug.Log("Se murio :C");
            SceneManager.LoadScene(2);
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
        //transform.localScale = new Vector3(0.2f, 0.2f,1.0f);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
    }

    public void Hit()
    {
        emparejado = true;
    }

    
    
    
}