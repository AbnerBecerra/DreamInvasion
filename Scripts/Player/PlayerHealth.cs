//Uso de librerias de UNITY
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

//Declaracion de clase publica PlayerHealth
public class PlayerHealth : MonoBehaviour
{
    //Declaracion de Variables publicas
    public int startingHealth = 100;                            // De tipo entero para asignar la cantidad de vida con la que empieza el jugador. 
    public int currentHealth;                                   // De tipo entero para el nivel de vida actual del jugador.
    public Slider healthSlider;                                 // Referencia a la barra de salud de interfaz de usuario.
    public Image damageImage;                                   // Referencia a imagen para que parpadee en la pantalla al ser herido.
    public AudioClip deathClip;                                 // Clip de audio que se reproducirá cuando el jugador muera.
    public float flashSpeed = 5f;                               // Velocidad a la que se desvanece la imagen de daño.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // Color en el que está configurada la imagen de daño, para que parpadee.

    //Declaracion de referencias y variables booleanas
    Animator anim;                                              // Referencia al componente Animator
    AudioSource playerAudio;                                    // Referencia al componente Fuente de Audio
    PlayerMovement playerMovement;                              // Referencia al componente Movimiento del jugador
    //PlayerShooting playerShooting;
    bool isDead;                                                // Variable booleana true si el jugador esta muerto.
    bool damaged;                                               // Variable booleana true si el jugador esta herido.

    //Declaracion de funcion Awake
    void Awake ()
    {
        // Configuracion de Referencias.
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        // Establece salud inicial del jugador.
        currentHealth = startingHealth;
    }

    //Declaracion de funcion Update
    void Update ()
    {
        // Si el jugador esta herido
        if(damaged)
        {   // Muestra color de imagen de daño en el color del flash.
            damageImage.color = flashColour;
        }
        else    // Sino
        {   // Cambia del color de nuevo a claro.
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        // Restablece bandera dañada.
        damaged = false;
    }

    //Declaracion de funcion publica TakeDamage con parametro entero
    public void TakeDamage (int amount)
    {
        // Establece bandera dañada para que la pantalla parpadee.
        damaged = true;
        // Reduce salud actual por la cantidad de daño.
        currentHealth -= amount;
        // Asigna valor de la barra de vida en valor de vida actual.
        healthSlider.value = currentHealth;
        // Reproduce efecto de sonido herido.
        playerAudio.Play ();
        // Si el jugador ha perdido su salud y la bandera de la muerte no se ha establecido
        if(currentHealth <= 0 && !isDead)
        {
            Death ();   //Llama a la funcion Death.
        }
    }

    //Declaracion de funcion Death
    void Death ()
    {   // Confirma que murio para que la función Death() no se vuelva a llamar.
        isDead = true;

        //playerShooting.DisableEffects ();
        // Indica al animator que el jugador está muerto.
        anim.SetTrigger ("Die");
        // Configura fuente de audio para que reproduzca el audio de muerte (detendiendo el audio de herido).
        playerAudio.clip = deathClip;
        playerAudio.Play ();
        // Desactiva guiones de movimiento y disparo.
        playerMovement.enabled = false;
        //playerShooting.enabled = false;
    }

    //Declaracion de funcion publica RestartLevel
    public void RestartLevel ()
    {   // Reinicia el nivel.
        SceneManager.LoadScene (0);
    }
}
