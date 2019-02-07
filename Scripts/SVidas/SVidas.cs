using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PukFramework.Entidades;
using PukFramework.Componentes;

namespace PukFramework {
    public class SVidas {

		#region Propiedades

        internal static CVidasTotales VidasJuego {
            get {
                CVidasTotales vidasReturn = new CVidasTotales();
                vidasReturn.VidasActuales = SysVidasJuego.VidasActuales;
                vidasReturn.VidasIniciales = SysVidasJuego.VidasIniciales;
                vidasReturn.VidasMaximas = SysVidasJuego.VidasMaximas;
                return vidasReturn;
            }
        }

		internal static short VidasIniciales {
            get { return SysVidasJuego.VidasIniciales;}
        }

		internal static short VidasMaximas {
            get { return SysVidasJuego.VidasMaximas; }
        }

		internal static short VidasActuales {
            get {return SysVidasJuego.VidasActuales; }
        }

		private static CVidasTotales SysVidasJuego = new CVidasTotales();
		private static bool ActualizarVidasEditor = false;
		private static string FechaDeLectura = ""; // guardar la fecha porque el modelo se destruye 

		#endregion Propiedades

		#region Eventos

		public class VidasEventArgs : System.EventArgs {
			internal short Valor = 0;
		}
		// Evento Game Over
		//internal delegate void GameOverHandler(object sender, System.EventArgs args);
		internal delegate void ManejadorEventosVidas(object sender, VidasEventArgs args);
		internal static event ManejadorEventosVidas GameOver;
		internal static event ManejadorEventosVidas GanaVida;
		internal static event ManejadorEventosVidas PierdeVida;
		internal static event ManejadorEventosVidas CambiaVidasMaximas;
		internal static event ManejadorEventosVidas CambiaVidasIniciales;

		protected static void OnGameOver() {			
			SVidas xVidas = new SVidas ();

			if (GameOver != null) {
					GameOver (xVidas , new VidasEventArgs ());
			}
		}

		protected static void OnGanaVidas(short vidas = 1) {
			SVidas xVidas = new SVidas ();
			VidasEventArgs args = new VidasEventArgs ();

			args.Valor = vidas;

			if (GanaVida != null) {
				GanaVida (xVidas , args);
			}
		}

		protected static void OnPierdeVida() {
			SVidas xVidas = new SVidas ();

			if (PierdeVida != null) {
				PierdeVida (xVidas , new VidasEventArgs ());
			}
		}

		// todo: quitar de static
		protected static void OnCambiaVidasMaximas(short valor) {
			SVidas xVidas = new SVidas ();
			VidasEventArgs args = new VidasEventArgs ();

			args.Valor = valor;

			if (CambiaVidasMaximas != null) {
				CambiaVidasMaximas (xVidas , args);
			}
		}

		protected static void OnCambiaVidasIniciales(short valor) {
			SVidas xVidas = new SVidas ();
			VidasEventArgs args = new VidasEventArgs ();

			args.Valor = valor;

			if (CambiaVidasIniciales != null) {
				CambiaVidasIniciales (xVidas , args);
			}
		}

		#endregion Eventos

		// set vidas
		// get vidas
        internal static void SincronizarVidas(ref CVidasTotales vidasDelEditor)  { // sincronizar vidas
            if (ActualizarVidasEditor) {
                vidasDelEditor = SysVidasJuego;
                ActualizarVidasEditor = false;
            } else {
				SysVidasJuego = vidasDelEditor;
            }
        }

        internal static void PerderVida() {
			if (SysVidasJuego.VidasActuales > 0) {
				ActualizarVidasEditor = true; // Actualizar las vidas del editor.
				//SysVidasJuego.vidasPerdidas++;
				SysVidasJuego.VidasActuales--;

				if (SysVidasJuego.VidasActuales <= 0) {
					OnGameOver ();
				}
			}
        }

		internal static void GanarVidas(short vidas = 1) {
            ActualizarVidasEditor = true; // Actualizar las vidas del editor.
			SysVidasJuego.VidasActuales += vidas;

			Debug.Log ("Ganar vidas: " + vidas);
        }

		internal static void AumentarVidasMaximas(short vidas) { 
			SysVidasJuego.VidasMaximas += vidas;
		}

		internal static void DisminuirVidasMaximas(short vidas) { 
			SysVidasJuego.VidasMaximas -= vidas;
		}

		/// <summary>
		/// Guarda las vidas.
		/// </summary>
		/// No es estáticopara evitar que quede guardado innecesariamente en memoria.
		internal short GuardarEstadoVidas() {
			PukFramework.Modelo.MVidas mVidas = new PukFramework.Modelo.MVidas();

			mVidas.VidasIniciales = SysVidasJuego.VidasIniciales;
			mVidas.VidasMaximas = SysVidasJuego.VidasMaximas;
			mVidas.VidasActuales = SysVidasJuego.VidasActuales;
			mVidas.FechaLectura = FechaDeLectura;

			//mVidas.vidasPerdidas = SysVidasJuego.vidasPerdidas;

			if (!mVidas.GuardarEstado ("SVidas")) {
				return PukFramework.Modelo.MBase.ERROR__NO_HAY_DATOS;
			} else {
				return PukFramework.Estado.OK;
			}
		}

		internal short CargarEstadoVidas() {
			short resultado = PukFramework.Estado.OK;
			PukFramework.Modelo.MVidas mVidas = new PukFramework.Modelo.MVidas();

			mVidas.CargarEstado ("SVidas");

			//mVidas.ValidarDatosGuardados ();
			SysVidasJuego.VidasIniciales = mVidas.VidasIniciales;
			SysVidasJuego.VidasMaximas = mVidas.VidasMaximas;
			SysVidasJuego.VidasActuales = mVidas.VidasActuales;
			//SysVidasJuego.vidasPerdidas = mVidas.vidasPerdidas;

			FechaDeLectura = mVidas.FechaLectura;

			ActualizarVidasEditor = true; // todo: debuguear

			resultado = mVidas.ObtenerError ();
			return resultado;
		}

		/// <summary>
		/// Guarda las vidas.
		/// </summary>
		/// No es estáticopara evitar que quede guardado innecesariamente en memoria.
		internal short Inicializar() {
			FechaDeLectura =  System.DateTime.UtcNow.ToString ();
			return GuardarEstadoVidas ();
		}
	}
}