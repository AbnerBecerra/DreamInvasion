//Uso de librerias de UNITY
using UnityEngine;
using System.Collections;

//Declaracion de clase publica EnemyAttack
public class EnemyAttack : MonoBehaviour
{
    //Declaracion de variables publicas
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 15;

    //Declaracion de variables booleanas y referencias
    Animator anim;
    GameObject player;
    PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    bool playerInRange;
    float timer;

    //Declaracion de funcion Awake
    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        //enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();
    }

    //Declaracion de funcion OnTriggerEnter con atributo
    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
    }

    //Declaracion de funcion OnTriggerExit con atributo
    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
    }

    //Declaracion de funcion Update
    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && playerInRange/* && enemyHealth.currentHealth > 0*/)
        {
            Attack ();
        }

        if(playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }
    }

    //Declaracion de funcion Attack
    void Attack ()
    {
        timer = 0f;

        if(playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage (attackDamage);
        }
    }
}
