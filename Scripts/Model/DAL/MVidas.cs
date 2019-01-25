namespace PukFramework.Model.DAL {
	internal class MVidas {

		/// <summary>
		/// Guarda los datos del objeto Model.MVidas enviado como parámetro. 
		/// Regresa false si ocurrió un error.
		/// </summary>
		/// <param name="nombre">Nombre como se va a guardar.</param>
		/// <param name="vidas">Vidas, objeto a guardar.</param>
		internal bool Guardar( string idNombre, Model.MVidas vidas ) {
			// probablemente meter esto en un yield, si se traba por el fixed update

			if (!(new DAL.PO.JsonPlayerPref ().Guardar<Model.MVidas> (idNombre, vidas))) {
				// registrar error.
				return false;
			}

			return true;
		}

		/// <summary>
		/// Llena el objeto Model.MVidas enviado como parámetro con los datos guardados en memoria. 
		/// Regresa false si ocurrió un error.
		/// </summary>
		/// <param name="nombre">Nombre a cargar.</param>
		/// <param name="vidas">Vidas, objeto a llenar con los datos guardados (como referencia)</param>
		internal bool Cargar(string idNombre, Model.MVidas vidas) {
			// probablemente meter esto en un yield, si se traba por el fixed update

			if ((!new DAL.PO.JsonPlayerPref ().Cargar<Model.MVidas> (idNombre, ref vidas))) {
				// registrar error.
				return false;
			}

			return true;
		}
	}
}