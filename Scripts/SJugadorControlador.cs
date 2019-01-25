using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using PukFramework;

// el objeto de esta clase solo recibe la referencia el input y camara con lo que verifica el gesto y determina 
// si es valido o no le indica al controlador del jugador que tiene que hacer algo

namespace PukFramework {

	// todo: renombrar variables para una mejor descripción y lectura del código

	public class SJugadorControlador{
		public VJugador player = null;
		public bool bIzq = false; 

		public SJugadorControlador(){
			player = GameObject.Find ("player").GetComponent<VJugador> ();
			if(player == null){
				Debug.LogError("Te hace falta el componente VJugador o el objeto player no existe");
			}
		}

		public void Muevete(){
			if (bIzq) {
				player.moverDerecha ();
			} else {
				player.moverIzquierda ();
			}
			bIzq = !bIzq;
		}
	}
}