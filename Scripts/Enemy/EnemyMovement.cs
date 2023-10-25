//Uso de librerias de UNITY
using UnityEngine;
using System.Collections;

//Declaracion de clase publica EnemyMovement
public class EnemyMovement : MonoBehaviour
{
    Transform player;                   // Referencia a la posicion del jugador
    //PlayerHealth playerHealth;
    //EnemyHealth enemyHealth;
    UnityEngine.AI.NavMeshAgent nav;    // Referencia al nav mesh agent.

    //Declaracion de funcion Awake
    void Awake ()
    {
        //Configuracion de referencias
        player = GameObject.FindGameObjectWithTag ("Player").transform;
        //playerHealth = player.GetComponent <PlayerHealth> ();
        //enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <UnityEngine.AI.NavMeshAgent> ();
    }

    //Declaracion de funcion Update
    void Update ()
    {
        //if(enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        //{
            //Asigna destino del nav mesh agent al jugador
            nav.SetDestination (player.position);
        //}
        //else
        //{
        //    nav.enabled = false;
        //}
    }
}
