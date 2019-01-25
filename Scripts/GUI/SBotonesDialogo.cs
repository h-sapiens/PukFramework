using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // contiene los componentes del canvas
using UnityEngine.Serialization;

namespace PukFramework.GUI.Dialogo {
	public class SBotonesDialogo : SVentana {

		// meter en componente posiciones layout, y usar una interfaz para que al extender la case se sobreescriba el componente y se conserven los metodos

		[SerializeField]
		protected GameObject BotonSi;

		[SerializeField]
		protected GameObject BotonNo;

		[SerializeField]
		protected RectTransform PosicionLayoutCancelar;

		[SerializeField]
		protected RectTransform PosicionLayoutAceptar;

		//[SerializeField]
		//private RectTransform []PosicionLayoutAceptarCancelar = new RectTransform[2];

		[System.Serializable]
		protected struct PosicionAceptarCancelar{
			[SerializeField]
			private RectTransform PosicionAceptar;
			[SerializeField]
			private RectTransform PosicionCancelar;
		}

		[SerializeField]
		protected PosicionAceptarCancelar PosicionLayoutAceptarCancelar; //= new PosicionLayoutAceptarCancelarStruct();


		internal void ConfigurarBotones(Dialogo.Boton configuracion){
			switch (configuracion) {
			case Boton.Aceptar: {
					break;
				}
			case Boton.Aceptar_Cancelar: {
					break;
				}
			case Boton.Cancelar: {
					break;
				}
			case Boton.Si_No: {
					break;
				}
			default: {
					// todo.
					break;
				}
			}
		}
	}

}