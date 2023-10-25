//Uso de libreria UnityEngine de UNITY
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;


    Animator anim;

    //Declaracion de funcion Awake
    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //Declaracion de funcion Update
    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        }
    }
}
