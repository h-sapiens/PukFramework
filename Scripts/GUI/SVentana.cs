using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PukFramework.GUI {

	// Todo: hacer la base principal heredada de los demás, o pasar a interfaz
	public class SVentana : MonoBehaviour {
		
		// implementar animaciones
		protected void Mostrar(){
			this.gameObject.SetActive (true);
		}

		/*
		// Use this for initialization
		protected virtual void Start () {
			
		}
		
		// Update is called once per frame
		protected virtual void Update () {
			// hacer animacion
			
		}*/
	}
}