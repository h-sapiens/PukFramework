//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;

//using UnityEngine;

namespace PukFramework.Model.DAL {
	internal class MTimer {
		// todo: documentar codigo
		internal bool Guardar( string idNombre, Model.MTimer timer) {
			// probablemente meter esto en un yield, si se traba por el fixed update

			if (!(new DAL.PO.JsonPlayerPref().Guardar<Model.MTimer>(idNombre, timer))){
				// registrar error.
				return false;
			} 

			return true;
		}

		// todo: documentar
		internal bool Cargar(string idNombre, Model.MTimer timer) {
			// probablemente meter esto en un yield, si se traba por el fixed update

			if (!(new DAL.PO.JsonPlayerPref ().Cargar<Model.MTimer> (idNombre, ref timer))) {
				// registrar error.
				return false;
			}

			return true;
		}
	}
}