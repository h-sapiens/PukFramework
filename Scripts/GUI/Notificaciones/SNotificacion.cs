using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PukFramework.GUI {
	/// <summary>
	/// Colocar la notificación en el lugar final, y el controlador se hará cargo.
	/// Al cerrar se aplica el inverso de la animación
	/// </summary>
	public class SNotificacion : MonoBehaviour {

		[SerializeField]
		internal Animacion AnimacionMostrar = Animacion.Surgir; // al cerrar se aplica el inverso

		/*
		[SerializeField]
		internal Animacion AnimacionCerrar = Animacion.Surgir;
		*/
		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		internal enum Animacion{
			Izquierda_Derecha,
			Derecha_Izquierda,
			Arriba_Abajo,
			Abajo_Arriba,
			Surgir,
			Ninguna
		}
	}
}