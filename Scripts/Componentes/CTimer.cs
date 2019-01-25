using UnityEngine;

namespace PukFramework.Componentes
{
	[System.Serializable]
	public class CTimer 
	{
		[SerializeField]
		public float SegundosRestantes = 0;

		[SerializeField]
		public float DuracionDelCicloEnSegundos = 0;

		[SerializeField]
		public short CiclosPendientes = 0;

		[SerializeField]
		public short CiclosTranscurridos = 0;

		// todo: quitar el serialize debug
		[SerializeField]
		public float SegundosAlIniciarTimer = 0;

		[SerializeField]
		public float SegundosAlPausar = 0;

		//[SerializeField]
		//public System.DateTime fechaAlPausar = System.DateTime.UtcNow;
	}
}
