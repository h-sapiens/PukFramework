using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDebugEscena : MonoBehaviour {

    private void Awake()
    {
        /// Buscar si existe el objeto main controller y 
        if (GameObject.Find("_MainController") == null)
        {
            //Debug.Log("Main controller no existe, usar temporales de debuguear");

        }
        else
        {
            //Debug.Log("Usar variables globales, como juego normal");
            Destroy(this.gameObject);
        }
    }
}
