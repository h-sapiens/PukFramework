using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearPlayerPrefs : MonoBehaviour {

	void OnMouseUpAsButton()
	{
		PlayerPrefs.DeleteAll();
	}
}
