

namespace PukFramework.Model {
	internal class MTimer : MBase {

		// **************
		// **** ToDO: ***
		// preparar con version, 
		// según la versión que se usó, validar para cargar la versión vieja del objeto.. usar dynamic por ejemplo
		// así al cambiar el modelo y des serializar los datos, no ocurra un error
		// pero primero ver como se comporta con la solución actual
		// **************

		// STimer
		public bool TimerCorriendo; 
		public bool TimerTerminaCiclo;
		public bool TimerPausado;
		public PukFramework.Componentes.CTimer timer; //

		// stimerpersistente
		//public bool autoguardado = true;
		//public string fechaAlPausar  = "1959-01-01";
		//public string fechaLectura;// = "1959-01-01";
		//public string fechaEscritura;// = "1961-01-01";
		public float SegundosAcumuladosEnPausa;

		internal void GuardarEstado(string idNombre) {
			base.GuardarEstado ();
			//fechaEscritura = System.DateTime.UtcNow.ToString ();
			new DAL.MTimer().Guardar(idNombre, this);
		}

		internal bool CargarEstado(string idNombre) {
		//internal MTimer cargarEstadoTimer(string nombre) {
			base.CargarEstado ();
		
			if (!(new DAL.MTimer ().Cargar (idNombre, this))) {
				base.Error = ERROR__NO_HAY_DATOS;

				return false;
			} else {
				short error = base.ValidarDatosGuardados ();
				//this.FechaLectura = System.DateTime.UtcNow.ToString ();
				if (error != PukFramework.Estado.OK) {
					return false;
				}
			}

			return true;
		}
	}
}