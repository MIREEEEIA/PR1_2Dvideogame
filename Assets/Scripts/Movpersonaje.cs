using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movpersonaje : MonoBehaviour
{
    public float multiplicador = 5f;

    public float multiplicadorSalto = 5f;

    private bool puedoSaltar = true;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float movTeclas = Input.GetAxis("Horizontal"); //(a -1f - d 1f)
        //float movTeclasY = Input.GetAxis("Vertical"); //(a -1f - d 1f)


        float miDeltaTime = Time.deltaTime;

        //movimiento personaje
        rb.velocity = new Vector2(movTeclas*multiplicador, rb.velocity.y);

        //salto
        if(Input.GetKeyDown(KeyCode.Space) && puedoSaltar ){
          rb.AddForce(
            new Vector2(0,multiplicadorSalto),
            ForceMode2D.Impulse
         );
         puedoSaltar = false;
        }
    }

    void OnCollisionEnter2D(){
        puedoSaltar = true;
        Debug.Log("Collision");
    }
    

    //Esto es un comentario para actualizar github
}
