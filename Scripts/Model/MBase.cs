namespace PukFramework.Model{
	internal class MBase {

		internal const short ERROR__NO_HAY_DATOS = -1;
		internal const short ERROR__INCONSISTENCIA_EN_FECHAS = -2;
		internal const short OTRO = -3;

		//internal short Error { get;}
		protected short Error = PukFramework.Estado.OK;

		public string FechaLectura;
		public string FechaEscritura;

		internal bool GuardarEstado() {
			this.FechaEscritura = System.DateTime.UtcNow.ToString ();
			return true;
		}
		internal bool CargarEstado() {
			//this.FechaLectura =  System.DateTime.UtcNow.ToString (); // lo sobreescribe el toJson
			return true;
		}
		internal bool Inicializar(){
			this.FechaLectura =  System.DateTime.UtcNow.ToString (); // lo sobreescribe el toJson
			this.FechaEscritura = System.DateTime.UtcNow.ToString ();
			return true;
		}

		// mover a clase herramienta de model, para validar datos.
		protected short ValidarDatosGuardados(){

			if (Error != ERROR__NO_HAY_DATOS) {
				try {
					// todo: que pasa siii? seguir testeando
					// - si la fecha de escritura llega nula, no hay datos.
					
					System.DateTime auxFechaLectura = System.DateTime.Parse(FechaLectura);
					System.DateTime auxFechaEscritura = System.DateTime.Parse(FechaEscritura);

					if (auxFechaLectura > auxFechaEscritura ) {										
						Error = ERROR__INCONSISTENCIA_EN_FECHAS;
					}
					else {
						return PukFramework.Estado.OK;
					}
				}
				catch {
					Error = ERROR__NO_HAY_DATOS;
				}
			}
			return Error;
		}

		internal short ObtenerError(){
			return Error;
		}
	}
}