using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PukFramework.Entidades;
using PukFramework.Componentes;

namespace PukFramework
{
	/// <summary>
	/// Clase timer que detecta cuando la aplicación se pausa y detiene el timer.
	/// Cuando la aplicación regresa del pausa, continúa con el timer.
	/// </summary>
	public class STimerConPause : STimer, ITimer{

	    /// <summary>
	    /// Llamar este método Inicializar the objeto, desde el controlador para guardar el orden de ejecución de los eventos.
	    /// </summary>
		//internal void InicializarObjeto() {
		new internal void Awake(){
			base.Awake ();
	    }

		/// <summary>
		/// Pausa el timer mientras la app está en segundo plano y lo reinicia cuando la app se reabre.
		/// </summary>
		/// <param name="pauseStatus">If set to <c>true</c> pause status.</param>
		void OnApplicationPause(bool pauseStatus) {
			//if (timerCorriendo) { // todo: debug
				// todo: validar cual es la mejor lógica:
				//A)
				//pausar();

				//b)
				if (pauseStatus) {
					PausarTimer ();

				} else {
					PesPausarTimer();
				}
			//}
		}

		new void FixedUpdate() {
			base.FixedUpdate ();
	    }
	}
}