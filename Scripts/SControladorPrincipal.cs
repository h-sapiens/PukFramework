using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SControladorPrincipal : MonoBehaviour {
	
    // game status
    internal static bool EsGameOver {
		get { return esGameOver; }
    }
    private static bool esGameOver = false;

	/// <summary>
	/// En caso de error o que se comprometa la integridad de los datos, evitar guardar para no sobreescribir.
	/// </summary>
	private bool PuedeGuardarDatos = true; 

	[SerializeField]
	protected PukFramework.STimerPersistente TimerVidas;

	// control sobre el framework
	/// <summary>
	/// Al inicializar el juego, los valores que el Game Designer le asigne al objeto 
	/// serán con lo que se inicialize el juego.
	/// Estas vidas se usan para que el game designer controle las vidas del juego. 
	/// El controlador mandará estos valores a la entidad con los valores estáticos del framework.
	/// </summary>
	[SerializeField]
	internal PukFramework.Componentes.CVidasTotales VidasJuego = new PukFramework.Componentes.CVidasTotales();

	#if UNITY_EDITOR
	// usar lógica que solo se necesite en el editor

	private PuenteEditorStatics puenteEditor;

	#endif // UNITY_EDITOR

	private void Awake() {

		float contadorAperturasJuego = 0;
		
        DontDestroyOnLoad(this);

		#if UNITY_EDITOR
		// usar lógica que solo se necesite en el editor

//		if (Application.isEditor) { 
//			puenteEditor = gameObject.AddComponent<PuenteEditorStatics>() as PuenteEditorStatics;
//			puenteEditor.setVidasJuego( ref vidasJuego );
//		}

		#endif // UNITY_EDITOR

		//// ------ Inicializar juego y reglas de negocio -------

		// Asignar timer.
		//timerVidas = gameObject.GetComponent<PukFramework.STimerPersistente>();

		// Recuperar vidas guardadas
		// Manejar si es la primera vez que el juego se abre

		//PlayerPrefs.DeleteAll (); // para debugueo, todo: quitar

		contadorAperturasJuego = PlayerPrefs.GetFloat("ContadorAperturasJuego");

		if (contadorAperturasJuego  > 0) {
			
			short resultadoDelCargado = new PukFramework.SVidas().CargarEstadoVidas (); 

//			if (resultadoDelCargado != PukFramework.Estados.OK) {
//				
//			}
			switch (resultadoDelCargado) {
			case PukFramework.Model.MBase.ERROR__NO_HAY_DATOS:
				{
					// mostrar error de mensaje
					// de intentar más tarde
					// cuando viene nulo?

					// No permitir guardar datos
					this.PuedeGuardarDatos = false;
					break;
				}
			case PukFramework.Model.MBase.ERROR__INCONSISTENCIA_EN_FECHAS:
				{					
					// mostrar mensaje de que se perdieron datos desde la ultima sesión.
					// VALIDAR DATOS RECUPERADOS Y MOSTRAR AVISO DE ERROR EN CASO DE QUE OCURRA ERROR.

					// al guardar, restaura el sistema a su estado original
					this.PuedeGuardarDatos = true;
					break;
				}
			case PukFramework.Estado.OK:
				{
					// puede continuar

					// Flujo normal
					this.PuedeGuardarDatos = true;
					break;
				}
			default: 
				//DESCONOCIDO
				break;
			}
		} else {
			InicializarJuego ();
		}

		PukFramework.SVidas.SincronizarVidas (ref VidasJuego );

		PlayerPrefs.SetFloat ("ContadorAperturasJuego", ++contadorAperturasJuego);

		// etc

		// Subscribir eventos
		PukFramework.SVidas.GameOver += new PukFramework.SVidas.ManejadorEventosVidas(OnGameOver);
		PukFramework.SVidas.GanaVida += new PukFramework.SVidas.ManejadorEventosVidas(OnGanaVida);
		PukFramework.SVidas.PierdeVida += new PukFramework.SVidas.ManejadorEventosVidas(OnPierdeVida);
		//PukFramework.SVidas.CambiaVidasMaximas += new PukFramework.SVidas.ManejadorEventoVidas(OnActualizarVidas);
		//PukFramework.SVidas.CambiaVidasIniciales += new PukFramework.SVidas.ManejadorEventoVidas(OnActualizarVidas);

    }

	private void Start() {   // todo: debuguar y aplicar logica a esta parte
		short vidasGanadasDuranteJuegoCerrado = 0;

		// Recuperar vidas
		vidasGanadasDuranteJuegoCerrado = this.TimerVidas.ExtraerCiclosTranscurridos();

		// todo: validar logica cuando re
		if (vidasGanadasDuranteJuegoCerrado > 0) {
			PukFramework.SVidas.GanarVidas (vidasGanadasDuranteJuegoCerrado);
		}
	}

    private void Update() {

		#if UNITY_EDITOR
		// usar lógica que solo se necesite en el editor

//		if (Application.isEditor) { 
//			puenteEditor.setVidasJuego( ref vidasJuego );
//		}

		#endif // UNITY_EDITOR

		//PukFramework.SVidas.ActualizarVidas (ref vidasJuego);
		
		//PukkrFramework.STimer.SincronizarTiempo (ref tiempoSoloEditor); 

		// checar si el tiempo terminó, para agregar vida
		/*if (PukkrFramework.STimer.ChecarTimerTerminaCiclo()){
			Debug.Log ("aui: " + PukkrFramework.STimer.TiempoJuego.ciclosTranscurridos);
			PukkrFramework.SVidas.GanarVidas (PukkrFramework.STimer.TiempoJuego.ciclosTranscurridos);
		}
		if (!PukkrFramework.STimer.TimerCorriendo) {
			if (PukkrFramework.SVidas.VidasPerdidas > 0) {
				PukkrFramework.STimer.CorrerTimer ();
			}
		}*/

		//// // todo: mandar a evento o delegate // --
		// en fixed update o en update?
		// fixed: se ejecuta y puede hacer lenta la app
		// se puede brincar frames, tener lag
//		if (PukFramework.SVidas.VidasActuales <= 0) {
//			//OnGameOver ();
//
//		} else {
//			
//		}
	}
//
//	private void OnGameOver(SControladorPrincipal vidasEditor) {
//		
//		esGameOver = true;
//		//PukkrFramework.STimer.CorrerTimer ();
//
//		/*
//		if (PukkrFramework.STimer.TiempoJuego.segundosRestantes <= 0) {
//			_IsGameOver = false;
//			PukkrFramework.SVidas.GanarVidas ();
//		}
//		*/
//	}

	private void OnGameOver(object sender, PukFramework.SVidas.VidasEventArgs args){
		Debug.Log("Game over, iniciar lógica de gameover.");
		esGameOver = true;
		// proceso...
	}

	private void OnGanaVida(object sender, PukFramework.SVidas.VidasEventArgs args){
		Debug.Log("Ganó una vida, iniciar lógica de ganar vida.");
		// proceso...
	}

	private void OnPierdeVida(object sender, PukFramework.SVidas.VidasEventArgs args){
		Debug.Log("Ganó una vida, iniciar lógica de perder vida.");
		// proceso...
	}
	/*
	private void OnActualizarVida(object sender, System.EventArgs args){


		actualizarVidas ();
	}

	private void actualizarVidas (){
		PukFramework.SVidas.ActualizarVidas (ref vidasJuego );
	}*/

	private void OnApplicationQuit() {
		// guardar vidas
		if (this.PuedeGuardarDatos) {
			new PukFramework.SVidas().GuardarEstadoVidas();
		}
	
	}

	private void InicializarJuego() {
		PukFramework.SVidas.SincronizarVidas (ref VidasJuego ); // se toman las vidas del editor
		new PukFramework.SVidas().Inicializar();
	}
}