using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PukFramework.Entidades;
using PukFramework.Componentes;


namespace PukFramework {

	/// <summary>
	/// Clase Timer que continúa aun cuando la aplicación se cierra.
	/// Al abrirse, revisa los playerprefs guardados y hace los cálculos según el tiempo transcurrido.
	/// </summary>
	public class STimerPersistente : STimer, ITimer {

		#region Propiedades

		/// <summary>
		/// Propiedad para controlar el autoguardado de los playerprefs de la clase.
		/// Activado por default.
		/// </summary>
		/// <value><c>true</c> if autoguardado; otherwise, <c>false</c>.</value>
		//[SerializeField]
		//internal bool autoguardado = true; // todo << el problema es que si se desactiiva, no se guarda que se desactivó y se reactiva, sería hacer un modelo específicamente para este guardado, tal vez

		// ToDO: des serializar al terminar debugueo
		//[SerializeField]
		//private string FechaEscritura = "1959-01-01";

		[SerializeField]
		private string FechaAlPausar  = "1959-01-01";

		[SerializeField]
		private float SegundosAcumuladosEnPausa;

		private static string FechaDeLectura = ""; // guardar la fecha porque el modelo se destruye 

		#endregion Propiedades

		#region Eventos

		// Evento llega a cero y evento termina ciclo.
		internal delegate void ManejadorEventosTimer(object sender, System.EventArgs args);
		internal event ManejadorEventosTimer TimerLlegaACero;
		//internal event ManejadorEventosTimer TimerTerminaCiclo; //  todo: renombrar, por conflicto con booleano


		protected internal void OnTimerLlegaACero(object sender, System.EventArgs args){
			if (TimerLlegaACero != null) {
				TimerLlegaACero (sender, args);
			}
		}

		#endregion Eventos

		#region Métodos Base Stimer
		/// <summary>
		/// Al abrir la aplicación, si el timer se encontraba activo em una sesión previa, actualizará el estatus del timer.
		/// </summary>
		new protected void Awake() {
			base.Awake ();

			//PlayerPrefs.DeleteAll();

			float auxSegundosApagado = 0f;
			short auxCiclosTranscurridos = 0; // datos crudos
			System.DateTime auxDateTimeAlPausar; 
			PukFramework.Componentes.CTimer auxTimer;
			float contadorAperturasJuego = 0;


			// ***** ToDo: **** validar integrdad del guardado (de los prefs)
			// se guardó fecha de inicio del timer.
			// al terminar el timer, borrar la fecha de inicio.
			// en general, guardar los prefabs importantes en cada cambio que sufran.
			// si se cierra por accidente, los prefabs se quedan guardados.

			// obtener cantidad de veces que se ha abierto el juego
			contadorAperturasJuego = PlayerPrefs.GetFloat ("ContadorAperturasJuego");

			if (contadorAperturasJuego > 0) {
				CargarEstadoTimer(); // todo: preparar para en caso de error al cargar
			} else {
				// inicializar 
				FechaDeLectura =  System.DateTime.UtcNow.ToString ();
				GuardarEstadoTimer(); // todo: preparar para en caso de error al guardar
			}

			// despues de cargar o inicializar el timer, recogerlo para hacer calculos
			auxTimer = ObtenerTimer();

			if (!this.TimerPausado && this.TimerEstaCorriendo) {
				//  **** ToDo: *****
				///// Recalcular Timer /////
				// tiene que obtener la cantidad de segundos que estuvo apagado.
				// comparar fecha actual contra fecha de guardado.
				auxDateTimeAlPausar = System.DateTime.Parse(FechaAlPausar);

				// traducirlo a segundos
				auxSegundosApagado = ObtenerSegundosDeTimeSpan (System.DateTime.UtcNow.Subtract (auxDateTimeAlPausar));

				// esos segundos tiene que determinar cuantos ciclos transcurrieron  y cuantos quedan y debe guardarlos
				auxCiclosTranscurridos = (short)(auxSegundosApagado / auxTimer.DuracionDelCicloEnSegundos);

				// si se terminaron los ciclos debe parar el timer y reinicializarlo
				if (auxTimer.CiclosPendientes <= (short)auxCiclosTranscurridos) { // todo: debug
					// inicializar timer a cero
					auxTimer.CiclosTranscurridos += auxTimer.CiclosPendientes; // por qué se aumentan los ciclos? todo:
					auxTimer.CiclosPendientes = 0;
					auxTimer.SegundosRestantes = 0;
					auxTimer.SegundosAlIniciarTimer = 0;
					auxTimer.SegundosAlPausar = 0;

					//base.NuevoTimer (auxTimer); // quitar de aqui

					TimerTerminaCiclo = true;
					SegundosAcumuladosEnPausa = 0;

				} else {
					// si aun quedan segundos y ciclos, debe configirar el timer e iniciarlo

					auxTimer.CiclosTranscurridos += auxCiclosTranscurridos;
					auxTimer.CiclosPendientes -= auxCiclosTranscurridos;


					// todo: debug 
					//los segundos se estan iniciando al azar, checar si esta linea es la culpable:
					SegundosAcumuladosEnPausa += auxSegundosApagado;
					auxTimer.SegundosAlIniciarTimer = Time.unscaledTime - SegundosAcumuladosEnPausa; // todo: debug, no cuenta todos los segundos

					//base.NuevoTimer (auxTimer); // no borrar imer?

					Debug.Log ("segundosRestantes: " + auxTimer.SegundosRestantes);

					//autoGuardado();
				}

				base.NuevoTimer (auxTimer); // testear aqui
			}
		}

		/// <summary>
		/// Inicia el timer si no está corriendo.
		/// </summary>
		new public bool Iniciar() {
			bool seInicio = false;

			if (base.Iniciar()) {
				//AutoGuardado();
				seInicio = GuardarEstadoTimer();
			}

			return seInicio;
		}

		/// <summary>
		/// Detiene y reinicializa el timer.
		/// </summary>
		new public void Reinicializar(){
			base.Reinicializar ();

			//AutoGuardado();
			GuardarEstadoTimer();
		}

		/*
		new protected void FixedUpdate() {
			base.FixedUpdate ();
		}*/

		/// <summary>
		/// Se encarga de manejar la lógica cuando el timer llega a cero.
		/// </summary>
		protected override void TimerLLegaACero() { // todo: hacer delegate o evento.
			base.TimerLLegaACero ();
			SegundosAcumuladosEnPausa = 0;

			//AutoGuardado();
			GuardarEstadoTimer();
		}


		#endregion Métodos Base Stimer

		/// <summary>
		/// Evento application quit.
		/// Si la opcion de guardado automático de playerprefs está desactivada, éstos no se guardarán.
		/// </summary>
		private void OnApplicationQuit() {
			
			// todo: checar cómo se comporta la herencia para esta clase para los metodos privados, si no, dejarla como privada

			//autoGuardado();
			GuardarEstadoTimer();
		}

		/// <summary>
		/// Obtiene los segundos del lapso de tiempo (TimeSpan) que se le manda.
		/// </summary>
		/// <returns>The segundos de time span.</returns>
		/// <param name="TimeSpanOnPause">Time span on pause.</param>
		private float ObtenerSegundosDeTimeSpan(System.TimeSpan timeSpanOnPause) {
			return (float) (timeSpanOnPause.TotalMilliseconds / 1000);
		}

		/// <summary>
		/// Carga el estado del timer que se encuentre guardado
		/// </summary>
		/// <returns>The estado timer.</returns>
		private bool CargarEstadoTimer() {

			CTimer auxCTimer;
			PukFramework.Model.MTimer auxMTimer = new PukFramework.Model.MTimer();

			auxMTimer.CargarEstado (this.name);

			this.TimerEstaCorriendo = auxMTimer.TimerCorriendo; 
			this.TimerTerminaCiclo =  auxMTimer.TimerTerminaCiclo;
			this.TimerPausado = auxMTimer.TimerPausado;
			auxCTimer = auxMTimer.timer;

			//this.autoguardado = mTimer.autoguardado;
			this.FechaAlPausar = auxMTimer.FechaEscritura;
			FechaDeLectura = auxMTimer.FechaLectura;
			this.SegundosAcumuladosEnPausa = auxMTimer.SegundosAcumuladosEnPausa;

			base.NuevoTimer (auxCTimer);

			return true;
		}

		/// <summary>
		/// Guarda en memoria el estado del timer.
		/// </summary>
		private bool GuardarEstadoTimer() {

			PukFramework.Model.MTimer auxMTimer = new PukFramework.Model.MTimer ();

			auxMTimer.TimerCorriendo = this.TimerEstaCorriendo; 
			auxMTimer.TimerTerminaCiclo = this.TimerTerminaCiclo;
			auxMTimer.TimerPausado = this.TimerPausado;
			auxMTimer.timer = this.ObtenerTimer();

			auxMTimer.FechaLectura = FechaDeLectura;

			//mTimer.autoguardado = this.autoguardado;
			//mTimer.fechaAlPausar = System.DateTime.UtcNow.ToString(); // se omite por redundancia con fechaescritura, no olvidarlo al leer
			auxMTimer.SegundosAcumuladosEnPausa = this.SegundosAcumuladosEnPausa;
			//mTimer.fechaEscritura = System.DateTime.UtcNow.ToString();

			auxMTimer.GuardarEstado (this.name );

			return true;
		}

		/// <summary>
		/// Si el autoguardado está habilitado, guarda el timer después de ciertos eventos.
		/// </summary>
//		private void autoGuardado(){
//			if (autoguardado) {
//				guardarEstadoTimer();
//			}
//		}
	}
}