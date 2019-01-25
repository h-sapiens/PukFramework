using UnityEngine;

namespace PukFramework.Componentes
{
    [System.Serializable]
    internal class CVidasTotales
    {
        [SerializeField]
        internal short VidasIniciales;

        [SerializeField]
        internal short VidasMaximas;

        [SerializeField]
        internal short VidasActuales;

		//[SerializeField]
		//internal short vidasPerdidas;
    }
}