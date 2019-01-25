using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

// esta clase debe ser un componente del player, en este caso el carrito

namespace PukFramework {
	
	public class VJugador: MonoBehaviour{
		
		public void moverIzquierda(){
			Debug.Log ("A la izquierda");
			this.transform.position += Vector3.left;
		}

		public void moverDerecha(){
			Debug.Log ("A la derecha");
			this.transform.position += Vector3.right;
		}
	}	
}