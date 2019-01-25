using UnityEngine;

namespace PukFramework.Model.DAL.PO {
	internal class JsonPlayerPref {
		
		internal bool Guardar<T>( string idNombre, T objeto) {
			try {
				string json = JsonUtility.ToJson (objeto);
				PlayerPrefs.SetString (idNombre, json);
				return true;
			}
			catch {
				return false;
			}
		}

		internal bool Cargar<T>(string idNombre, ref  T objeto) {
			try {
				string json = PlayerPrefs.GetString (idNombre);
				JsonUtility.FromJsonOverwrite (json, objeto);
				return true;
			}
			catch {
				return false;
			}
		}
	}
}