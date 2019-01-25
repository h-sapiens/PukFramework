using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Elementos del Canvas

namespace PukFramework.GUI {

	/// <summary>
	/// La ventana de aviso se manda llamar desde algun systema/controlador 
	/// todo: de preferencia envíar subscripcion de cuando el evento finalice, pero cuidando los memory leaks
	/// </summary>
	public class SDialogoController : SVentana {


		// separar como componentes ECS
		[SerializeField]
		//internal TextMesh Titulo;
		internal Text ContenedorTitulo; 

		[SerializeField]
		internal string TextoTitulo;

		[SerializeField]
		//internal TextMesh Mensaje;
		internal Text ContenedorMensaje;

		[SerializeField]
		internal string TextoMensaje;

		[SerializeField]
		internal Dialogo.Boton TipoDeBoton = Dialogo.Boton.Aceptar; 

		[SerializeField]
		internal Dialogo.SBotonesDialogo BotonesDialogo;

		[SerializeField]
		internal Dialogo.Icono IconoAMostrar = Dialogo.Icono.Niguno;

		private Dialogo.Opcion OpcionActiva = Dialogo.Opcion.Ninguna;

		internal void MostrarAviso(){
			
			// get component titulo //  si el texto tiene algo, o diferente del default, usar el que tiene?
			// get component mensaje //  si el texto tiene algo, o diferente del default, usar el que tiene?
			// get component icono

			// get component botones

			OpcionActiva = Dialogo.Opcion.Ninguna;

			BotonesDialogo.ConfigurarBotones (TipoDeBoton);

			/*if (TipoDeBoton == Dialogo.Boton.Cancelar) {
				IconoAMostrar = Dialogo.Icono.Aviso;
			}*/

			base.Mostrar();
		}

		internal Dialogo.Opcion ExtraerOpcionSeleccionada(){
			Dialogo.Opcion opcionSeleccionada = this.OpcionActiva;
			this.OpcionActiva = Dialogo.Opcion.Ninguna;
			return opcionSeleccionada;
		}

		internal void RegistrarOpcionSeleccionada(Dialogo.Opcion opcion) {
			this.OpcionActiva = opcion;
		}




		// registrar por evento de seleccion de opcion

		// Use this for initialization
		void Start () {
			// instanciar los botones según el 
			
		}
		
		// Update is called once per frame
		void Update () {
			
		}

		protected virtual void OnBecameVisible() {
			Debug.Log ("Lentro a OnBecameVisible");
			OpcionActiva = Dialogo.Opcion.Ninguna;

			if (TipoDeBoton == Dialogo.Boton.Cancelar) {
				IconoAMostrar = Dialogo.Icono.Aviso;
			}
		}
	}
}