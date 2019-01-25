using UnityEngine;

namespace PukFramework.Componentes
{
    [System.Serializable] // struct, class, enum
	internal class CPuntosDeVida
    {
		[SerializeField]
		internal short Maximos = 100;

		[SerializeField]
		internal short ParaIniciarAlarma = 40;

		[SerializeField]
		internal short fraccionDanioChoque = 3; // todo: quitar, utilizar mejor una propiedad heredada de la interfaz ICausaDaño para multiplicar
    }
}