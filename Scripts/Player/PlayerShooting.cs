//Uso de librerias de UNITY
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //Declaracion de clase publica PlayerShooting
    public int damagePerShot = 20;                  // Daño infligido por cada bala.
    public float timeBetweenBullets = 0.15f;        // Tiempo entre cada disparo.
    public float range = 100f;                      // Distancia que el arma puede disparar.

    //Declaracion de Variables y referencias
    float timer;                                    // Temporizador para determinar cuando disparar.
    Ray shootRay;                                   // Rayo desde el extremo del arma hacia adelante.
    RaycastHit shootHit;                            // Golpe de raycast para obtener información sobre lo que fue golpeado.
    int shootableMask;                              // Mascara de capa para que el raycast solo golpee cosas en la capa disparable.
    ParticleSystem gunParticles;                    // Referencia al sistema de particulas.
    LineRenderer gunLine;                           // Referencia al representador de linea.
    AudioSource gunAudio;                           // Referencia a fuente de audio.
    Light gunLight;                                 // Referencia al componente de luz.
    float effectsDisplayTime = 0.2f;                // Proporción de tiempo entre balas durante la cual se muestran los efectos.

    //Declaracion de funcion Awake
    void Awake ()
    {
        // Creacion de máscara de capa para capa Shootable.
        shootableMask = LayerMask.GetMask ("Shootable");
        // Configuracion de referencias.
        gunParticles = GetComponent<ParticleSystem> ();
        gunLine = GetComponent <LineRenderer> ();
        gunAudio = GetComponent<AudioSource> ();
        gunLight = GetComponent<Light> ();
    }

    //Declaracion de funcion Update
    void Update ()
    {   // Añade tiempo transcurrido desde la última vez que se llamó a Update a temporizador.
        timer += Time.deltaTime;
        // Si presiona el botón Disparo1 y es hora de disparar entonces
		if(Input.GetButton ("Fire1") && timer >= timeBetweenBullets && Time.timeScale != 0)
        {   // LLama funcion Dispara.
            Shoot ();
        }
        // Si hay entrada en la palanca de dirección de disparo y es hora de disparar
        if(timer >= timeBetweenBullets * effectsDisplayTime)
        {   // LLama funcion Desactivar Efectos.
            DisableEffects ();
        }
    }

    //Declaracion de funcion publica DisableEffects
    public void DisableEffects ()
    {   // Deshabilita renderizador de línea y la luz.
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    //Declaracion de funcion Shoot
    void Shoot ()
    {
        timer = 0f; // Reinicia temporizador.

        gunAudio.Play ();   // Reproduce clip de audio del disparo.

        gunLight.enabled = true;    // Habilita las luces.
        // Detiene las partículas, luego las inicia.
        gunParticles.Stop ();
        gunParticles.Play ();
        // Habilita renderizador de línea y configura su primera posición para que sea el final del arma.
        gunLine.enabled = true;
        gunLine.SetPosition (0, transform.position);
        // Configura shootRay para que comience en el extremo del arma y apunte hacia adelante desde el cañón.
        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;
        // Realiza raycast contra gameobjects en la capa disparable y si golpea algo
        if(Physics.Raycast (shootRay, out shootHit, range, shootableMask))
        {   //Encuentra un script de EnemyHealth en el hit de gameobject.
            EnemyHealth enemyHealth = shootHit.collider.GetComponent <EnemyHealth> ();
            //Si existe componente EnemyHealt
            if(enemyHealth != null)
            {       //El enemigo recibe daño.
                enemyHealth.TakeDamage (damagePerShot, shootHit.point);
            }   // Establece segunda posición del renderizador de línea en el punto en que golpeó el raycast.
            gunLine.SetPosition (1, shootHit.point);
        }
        else    // Si raycast no golpeó nada en la capa disparable
        {   // Establece segunda posición del renderizador de línea en la máxima extensión del alcance del arma.
            gunLine.SetPosition (1, shootRay.origin + shootRay.direction * range);
        }
    }
}
