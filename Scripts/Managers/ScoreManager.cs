//Uso de librerias de UNITY
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public static int score;//Declaracion de variable publica estatica tipo entero 
    Text text;//Referencia al texto

    //Declaracion de funcion Awake
    void Awake ()
    {
        text = GetComponent <Text> (); //Obtiene el componente Texto
        score = 0;//Asigna 0 al valor de variable score
    }

    //Declaracion de funcion Update
    void Update ()
    {
        text.text = "Score: " + score; // Muestra en el texto el valor del puntaje
    }
}
