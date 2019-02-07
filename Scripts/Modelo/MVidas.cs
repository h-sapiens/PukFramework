namespace PukFramework.Modelo {
	internal class MVidas : MBase {

		// **************
		// **** ToDO: ***
		// preparar para cuando una version cambie la estructura del modelo
		// según la versión que se usó, validar para cargar la versión vieja del objeto.. usar dynamic por ejemplo
		// así al cambiar el modelo y des serializar los datos, no ocurra un error
		// pero primero ver como se comporta con la solución actual
		// **************

		// CVidasTotales
		public short VidasIniciales;
		public short VidasMaximas;
		public short VidasActuales;
		//internal short vidasPerdidas;

		internal bool GuardarEstado(string idNombre) {
			base.GuardarEstado ();

			if (!(new DAL.MVidas ().Guardar (idNombre, this))) {
				// logear error
				base.Error = ERROR__NO_HAY_DATOS; // no se grabaron
				return false;
			}
			return true;
		}

		internal bool CargarEstado(string idNombre) {
			base.CargarEstado ();

			if (!(new DAL.MVidas ().Cargar (idNombre, this))) {
				// logear error
				base.Error = ERROR__NO_HAY_DATOS;

				return false;

			} else {
				short error = base.ValidarDatosGuardados ();

				if (error != PukFramework.Estado.OK) {
					return false;
				}
			}
			return true;
		}
	}
}