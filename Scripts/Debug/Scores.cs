using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PukFramework
{
    public class Scores : MonoBehaviour {

        [SerializeField]
        internal Componentes.CVidasTotales Vidas = new Componentes.CVidasTotales();

		//[SerializeField]
		//internal Componentes.CTimer tiempo = new Componentes.CTimer();

        // Update is called once per frame
        void Update() {
            Vidas = SVidas.VidasJuego;
			//tiempo = STimer.TiempoJuego;
        }
    }
}