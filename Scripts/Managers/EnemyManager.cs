//Uso de librerias de UNITY
using UnityEngine;

//Declaracion de clase publica EnemyManager
public class EnemyManager : MonoBehaviour
{
    //Declaracion de variables y refereancias publicas
    public PlayerHealth playerHealth;
    public GameObject enemy;
    public float spawnTime = 3f;
    public Transform[] spawnPoints;

    //Declaracion de funcion Start
    void Start ()
    {
        InvokeRepeating ("Spawn", spawnTime, spawnTime);
    }

    //Declaracion de funcion Spawn
    void Spawn ()
    {
        if(playerHealth.currentHealth <= 0f)
        {
            return;
        }

        int spawnPointIndex = Random.Range (0, spawnPoints.Length);

        Instantiate (enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
    }
}
