using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PukFramework.Entidades;
using PukFramework.Componentes;


// Todo: ¿Qué pasaría si yo quisiera tener dos timers? uno para el contador de vidas (monetizar), 
// y otro para el contador del juego.. ej: una bomba? una plataforma que se cae al n tiempo?
// separar la lógica del control del timer de la del control del stiempo, quitar las variables y métodos estáticas

namespace PukFramework {
	
	public class STimer : MonoBehaviour, ITimer {
		

		public bool TimerEstaCorriendo; // todo: proteger

		//private bool _ActualizarTiempoEditor = false;
		protected bool TimerTerminaCiclo = false;
		protected bool TimerPausado = false;

		[SerializeField]
		private CTimer Timer = new CTimer();

		public float SegundosRestantes {
			get { return Timer.SegundosRestantes; }
		}

		public float DuracionDelCicloEnSegundos {
			get { return Timer.DuracionDelCicloEnSegundos; }
		}

		public short CiclosPendientes {
			get { return Timer.CiclosPendientes; }
		}

		public short CiclosTranscurridos {
			get { return Timer.CiclosTranscurridos; }
		}

		public float SegundosAlIniciarTimer {
			get { return Timer.SegundosAlIniciarTimer; }
		}

		public float SegundosAlPausar {
			get { return Timer.SegundosAlPausar; }
		}

	    /// <summary>
	    /// Llamar este método Inicializar the objeto, desde el controlador para guardar el orden de ejecución de los eventos.
	    /// </summary>
		//public void InicializarObjeto() {
		protected void Awake(){
			Timer.SegundosAlIniciarTimer = Time.unscaledTime;
			//Debug.Log("Start() segundosAlIniciarTimer" + _timer.segundosAlIniciarTimer);
	    }

		/// <summary>
		/// Reinicia la cuenta del contador del timer.
		/// </summary>
		public void ReniciarCuenta() {
			//Debug.Log ("ResetTimer");
			float segundosDescartados = (Timer.DuracionDelCicloEnSegundos - Timer.SegundosRestantes);
			Timer.SegundosAlIniciarTimer = Time.unscaledTime; // aplica para los ciclos
			Timer.SegundosRestantes = Timer.DuracionDelCicloEnSegundos; // vista

			if (TimerPausado) { // debuguear: pausar, resetear y reanudar: el timer debe iniciar normalmente
				Timer.SegundosAlIniciarTimer -= segundosDescartados; 
			}
		}

		/// <summary>
		/// Detiene y reinicializa el timer.
		/// </summary>
		public void Reinicializar() { 
			Timer.SegundosRestantes = 0;
			Timer.CiclosPendientes = 0;
			Timer.CiclosTranscurridos = 0;
			Timer.SegundosAlPausar = 0;
			TimerPausado = false;
			TimerEstaCorriendo = false;
			TimerTerminaCiclo = false;
		}

		/// <summary>
		/// Agrega ciclos al timer.
		/// </summary>
		public void AgregarCiclos(short ciclos = 1) {
			Timer.CiclosPendientes += ciclos;
		}

		/// <summary>
		/// Resta ciclos pendientes del timer y aumenta los ciclos transcurridos
		/// </summary>
		/// <param name="ciclos">Ciclos.</param>
		public void RestarCiclos(short ciclos = 1) {
			Timer.CiclosPendientes -= ciclos;
			Timer.CiclosTranscurridos += ciclos;
		}

		/// <summary>
		/// Elimina ciclos al timer.
		/// </summary>
		public void EliminarCiclos(short ciclos = 1) {
			Timer.CiclosPendientes-= ciclos;
		}

		/// <summary>
		/// Inicia el timer si no está corriendo.
		/// </summary>
		public bool Iniciar() {
			//Debug.Log ("IniciarTimer");
			if (!TimerEstaCorriendo) {
				Timer.SegundosAlIniciarTimer = Time.unscaledTime;
				Timer.SegundosRestantes = Timer.DuracionDelCicloEnSegundos;
				if (!TimerPausado) {
					AgregarCiclos ();
				}

				TimerEstaCorriendo = true;
				TimerPausado = false;

				return TimerEstaCorriendo;
			}
			return false;
		}

		/// <summary>
		/// Pausa y despausa el timer
		/// </summary>
		public void Pausar() {
			//if (timerCorriendo) { // todo: debug
				if (!TimerPausado) {
					PausarTimer ();
				} else {
					PesPausarTimer();
				}
			//}
		}

		/// <summary>
		/// Quita los segundos enviados como parámetro.
		/// </summary>
		/// <param name="seconds">Seconds.</param>
		public void QuitarSegundos(int seconds) {
			//quitar segundos al timer
			// todo:
			// qué pasa cuando los segundos que resta lo manda a menor que cero?
			Timer.SegundosRestantes -= seconds;
		}

		public void NuevoTimer(CTimer nuevoTimer){
			Timer = nuevoTimer;
		}

		/// <summary>
		/// Regresa los ciclos transcurridos del timer y los iguala a cero.
		/// </summary>
		/// <returns>Ciclos transcurridos.</returns>
		internal short ExtraerCiclosTranscurridos() {
			short retCiclos = 0;

			retCiclos = Timer.CiclosTranscurridos;
			Timer.CiclosTranscurridos = 0;

			return retCiclos;
		}

		internal CTimer ObtenerTimer() {
			return Timer;
		}
			    
		protected void FixedUpdate() {
			if (TimerEstaCorriendo && !TimerPausado) {
				Timer.SegundosRestantes = Timer.DuracionDelCicloEnSegundos 
													- ((Time.unscaledTime - Timer.SegundosAlIniciarTimer));

				if (Timer.SegundosRestantes <= 0 && TimerEstaCorriendo) {
					TimerLLegaACero ();
				}
			}
	    }

		/// <summary>
		/// Se encarga de manejar la lógica cuando el timer llega a cero.
		/// </summary>
		protected virtual void TimerLLegaACero() {
			TimerTerminaCiclo = true;

			if (Timer.CiclosPendientes > 0) { //  en caso de eliminar ciclos de más
				RestarCiclos ();
				ReniciarCuenta ();
			}

			if (Timer.CiclosPendientes == 0) {
				DetenerTimer ();
			}
		}

		/// <summary>
		/// Detiene el timer y coloca los segundos en 0.
		/// </summary>
		protected void DetenerTimer() {
			//Debug.Log ("DetenerTimer");
			TimerEstaCorriendo = false;
			Timer.SegundosRestantes = 0;
		}

		/// <summary>
		/// Pausa el timer, respetando los segundos.
		/// </summary>
		protected void PausarTimer(){
			if (TimerEstaCorriendo) { // todo: debug
				//Debug.Log ("Pausado");	
				TimerPausado = true;
				// _timer.fechaAlPausar =  System.DateTime.UtcNow; // este es para el timer cuando s

				Timer.SegundosAlPausar = Time.unscaledTime;
			}
		}

		/// <summary>
		/// Quita la pausa del timer, es decir, reanuda la cuenta del timer.
		/// </summary>
		protected void PesPausarTimer() {
			
			//Debug.Log ("Des Pausado");

			float segundosEnPausa =  Time.unscaledTime - Timer.SegundosAlPausar;

			if (TimerEstaCorriendo) { // todo: debug
				Timer.SegundosAlIniciarTimer += segundosEnPausa;
				TimerPausado = false;
			}
		}
	}
}