namespace PukFramework {
	internal interface ITimer {

		float SegundosRestantes { get; }
		float DuracionDelCicloEnSegundos { get; }
		short CiclosPendientes { get; }
		short CiclosTranscurridos { get; }
		float SegundosAlIniciarTimer { get; }
		float SegundosAlPausar { get; }

		/// <summary>
		/// Reinicia la cuenta del contador del timer.
		/// </summary>
		void ReniciarCuenta ();

		/// <summary>
		/// Detiene y reinicializa el timer.
		/// </summary>
		void Reinicializar();

		/// <summary>
		/// Agrega ciclos al timer.
		/// </summary>
		void AgregarCiclos(short ciclos = 1);

		/// <summary>
		/// Resta ciclos pendientes del timer y aumenta los ciclos transcurridos
		/// </summary>
		/// <param name="ciclos">Ciclos.</param>
		void RestarCiclos(short ciclos = 1);

		/// <summary>
		/// Elimina ciclos al timer.
		/// </summary>
		void EliminarCiclos(short ciclos = 1);

		/// <summary>
		/// Inicia el timer si no está corriendo.
		/// </summary>
		bool Iniciar();

		/// <summary>
		/// Pausa y despausa el timer
		/// </summary>
		void Pausar() ;

		/// <summary>
		/// Quita los segundos enviados como parámetro.
		/// </summary>
		/// <param name="seconds">Seconds.</param>
		void QuitarSegundos(int seconds) ;
	}
}