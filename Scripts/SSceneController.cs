using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SSceneController : MonoBehaviour/*, ISceneController*/
{
    /// <summary>
    /// Por hacer: 
    /// Estandarizar la carga de las escenas mediante el uso de este método. 
    /// También a futuro pudiera usarse otro método para cuando se necesite usar otra configuración 
    /// para cargar una escena, como la variante de cargar escenda dentro de la escena.
    /// </summary>
    /// <param name="escena">Escena a cargar según los build settings</param>
    public void CargarEscena(short escena)
    {
        // todo: 
        SceneManager.LoadScene(escena, LoadSceneMode.Single);
    }

    private void Awake()
    {
        
    }
}