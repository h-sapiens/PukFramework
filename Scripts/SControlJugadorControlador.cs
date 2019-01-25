using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using PukFramework;
// el objeto de esta clase solo recibe la referencia el input y camara con lo que verifica el gesto y determina 
// si es valido o no le indica al controlador del jugador que tiene que hacer algo
//dentro del codigo de la camara se debe crear el objeo de esta clase para llamar al metodo control, con las 
//referencias que se necesitan

namespace PukFramework {
	
	public class SControlJugadorControlador{
		
		private int toques = 0;
		private SJugadorControlador cJugador = new SJugadorControlador();
		private RaycastHit hit;

		private bool validandoGesto(ref Input input, ref Camera camara){

			string[] elementosInvalidos = { "Pausa", "BotonVelocidad" };

			for(int i = 0; i < Input.touchCount ; i++){
				if(Input.GetTouch(i).phase == TouchPhase.Began && toques != Input.touchCount) {	
					toques = Input.touchCount;
					if (Physics.Raycast((camara.ScreenPointToRay(Input.GetTouch(i).position)), out hit)) {
						for(int y = 0; y < elementosInvalidos.Length; y++ ){
							if(hit.collider.name.Equals(elementosInvalidos[y]))
								return false;
						}
					}
					return true;
				}
			}
			toques = Input.touchCount;

			return false;
		}

		protected void control(ref Input input, ref Camera camara){
			if(validandoGesto(ref input,ref camara)){
				//le indico al controlador del jugador que haga algo
				cJugador.Muevete();
			}
		}
	}
}