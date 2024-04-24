using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovPersonaje : MonoBehaviour
{
    private float multiplicador = 5f;

    private float multiplicadorSalto = 5f;

    float movTeclas;

    private bool puedoSaltar = true;

    private bool activaSaltoFixed = false;

    public bool miraDerecha = true;

    private Rigidbody2D rb;

    private Animator animatorController;

    GameObject respawn;

    bool soyAzul;


    // Start is called before the first frame update
    void Start()
    {
       rb = GetComponent<Rigidbody2D>();

       animatorController = this.GetComponent<Animator>();

       respawn = GameObject.Find("Respawn");
      
       transform.position = respawn.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

         if(GameManager.estoyMuerto) return;

         float miDeltaTime = Time.deltaTime;

        //movimiento personaje
        movTeclas = Input.GetAxis("Horizontal"); //(a -1f - d 1f) 
        //float movTeclasY = Input.GetAxis("Vertical"); //(a -1f - d 1f)


        //izq <--
        if(movTeclas < 0){
          this.GetComponent<SpriteRenderer>().flipX = true;  
          miraDerecha = false;
        }else if(movTeclas > 0){

        //dcha
          this.GetComponent<SpriteRenderer>().flipX = false;  
          miraDerecha = true;
        }
        
        //Animacion walking
        if(movTeclas != 0){
          animatorController.SetBool("activaCamina", true);
        }else{
          animatorController.SetBool("activaCamina", false);
        }
        


        //salto
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.5f);
        Debug.DrawRay(transform.position, Vector2.down, Color.magenta);

        if(hit){
            puedoSaltar = true;
            //Debug.Log(hit.collider.name);
        }else{
            puedoSaltar = false;
        }

        if(Input.GetKeyDown(KeyCode.Space) && puedoSaltar ){
          activaSaltoFixed = true;

          //PuedoSaltarFixed
          /*rb.AddForce(
            new Vector2(0,multiplicadorSalto),
            ForceMode2D.Impulse
         );*/
        }

       //Comprobar si me he salido de la pantalla por abajo
       if(transform.position.y <= -7){
        AudioManager.Instance.SonarClipUnaVez(AudioManager.Instance.fxDead);
        Respawnear();
       }

       // 0 vidas

       if(GameManager.vidas <= 0)
       {
        GameManager.estoyMuerto = true;
       }

    }

    void FixedUpdate(){
         rb.velocity = new Vector2(movTeclas*multiplicador, rb.velocity.y);

         if(activaSaltoFixed == true){
           rb.AddForce(
            new Vector2(0,multiplicadorSalto),
            ForceMode2D.Impulse);

            activaSaltoFixed = false;
         }
 
    }

    public void Respawnear(){

      Debug.Log("vidas: "+GameManager.vidas);
      GameManager.vidas = GameManager.vidas - 1;
      Debug.Log("vidas: "+GameManager.vidas);

      transform.position = respawn.transform.position;
    }

    public void CambiarColor(){

      if(soyAzul){
        this.GetComponent<SpriteRenderer>().color = Color.white;
        soyAzul = false;
      }else{
        this.GetComponent<SpriteRenderer>().color = Color.blue;
        soyAzul = true;
      }
      
    }

    void OnTrigerEnter2D(Collider2D col){

      if(col.gameObject.name == "Tunel"){
        //disparo tunel
        AudioManager.Instance.IniciarEfectoTunel();
      }

       if(col.gameObject.name == "Burbuja"){
        //disparo tunel
        AudioManager.Instance.IniciarEfectoBurbuja();
      }
    }

    void OnTrigerExit2D(Collider2D col){

      if(col.gameObject.name == "Tunel" || col.gameObject.name =="Burbuja"){
        AudioManager.Instance.IniciarEfectoDefault();
      }

    }

    
}
