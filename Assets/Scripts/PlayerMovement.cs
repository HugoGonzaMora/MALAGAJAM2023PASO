using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Componentes
    [Header("Inputs")]
    private Rigidbody2D rb;
    private SpriteRenderer characterPlayer;
    private GameObject piesPlayer; //ground check
    private Animator animacionJugador;

    //Ground Check
    private LayerMask capa;

    [Header("Movimiento")]
    public float velocidadMovimiento = 5;

    [Header("Dasheo")]
    private bool puedoDashear = true;
    private bool estaDasheando;
    [SerializeField]private float fuerzaDasheo = 10f;
    private float tiempoDasheo = 0.2f;
    private float dashCooldown = 1f;
    [SerializeField] private TrailRenderer tr;


    [Header("Inputs")]
    private float ejeVertical = 0;
    private float ejeHorizontal = 0;
    private bool saltoInput = false;
    


    [Header("Salto")]
    public float fuerzaSalto = 5f;
    private Vector3 vectorSalto = Vector3.up;

    private void Awake()
    {
        //Funciones de inicio
        rb = GetComponent<Rigidbody2D>();
        characterPlayer = GameObject.Find("CharacterPlayer").GetComponent<SpriteRenderer>();
        piesPlayer = GameObject.Find("PiesPlayer");
        capa = LayerMask.GetMask("Suelo");
        animacionJugador = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //no permitir que este haciendo otras acciones mientras dashea
        if (estaDasheando)
        {
            return;
        }


        GetInput();
        Movimiento();
        VoltearSprite();
        CalcularSalto();
        CambiarAnimacion();
    }

    private void CambiarAnimacion()
    {
        if (!IsSuelo())
        {
            animacionJugador.Play("PlayerJump");
        }
        else if (IsSuelo() && ejeHorizontal != 0)
        {
            animacionJugador.Play("PlayerRun");
        }
        else if (IsSuelo() && ejeHorizontal == 0)
        {
            animacionJugador.Play("PlayerIdle");
        }
        else
        {

        }
     
    }

    private void FixedUpdate()
    {
        //no permitir que este haciendo otras acciones mientras dashea
        if (estaDasheando)
        {
            return;
        }

        Salto();
    }
    void GetInput()
    {
        //movimiento en los ejes
        ejeVertical = Input.GetAxisRaw("Vertical");
        ejeHorizontal = Input.GetAxisRaw("Horizontal") * Time.timeScale;

        //Activacion del salto
        saltoInput = Input.GetKeyDown(KeyCode.UpArrow);


        if (Input.GetKeyDown(KeyCode.LeftShift) && puedoDashear)
        {
            
            StartCoroutine(Dash());
        }
    }
    private void Movimiento()
    {
        transform.position += Vector3.right * ejeHorizontal * velocidadMovimiento * Time.deltaTime;
    }
    private void VoltearSprite()
    {
        if (ejeHorizontal > 0)characterPlayer.flipX = false;
        else if (ejeHorizontal < 0) characterPlayer.flipX = true;
    }

    private bool IsSuelo()
    {
        return Physics2D.Raycast(piesPlayer.transform.position, Vector3.down, 0.5f, capa);
    }

    private void CalcularSalto()
    {
        if(saltoInput && IsSuelo())
        {

            vectorSalto = Vector3.up * fuerzaSalto;
        }
        else if(!IsSuelo())
        {
            vectorSalto = Vector3.zero;
        }

    }

    private void Salto()
    {
        rb.AddForce(vectorSalto, ForceMode2D.Impulse);
    }

    //Create a new coroutine for dashing
    private IEnumerator Dash()
    {
        puedoDashear = false;
        estaDasheando = true;

        //No gravity affects during dash
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        animacionJugador.Play("PlayerDash");
        if (characterPlayer.flipX)
        {
            rb.velocity = new Vector2(transform.localScale.x * fuerzaDasheo * -1, 0f);
        }
        else
        {
            rb.velocity = new Vector2(transform.localScale.x * fuerzaDasheo, 0f);
        }
        

        //activate the trail
        tr.emitting = true;

        //stop dashing after an amount of time
        yield return new WaitForSeconds(tiempoDasheo);
        tr.emitting = false;
        rb.gravityScale = originalGravity;
        estaDasheando = false;

        //implementing cooldown
        yield return new WaitForSeconds(dashCooldown);
        puedoDashear = true;

        
    }
}
