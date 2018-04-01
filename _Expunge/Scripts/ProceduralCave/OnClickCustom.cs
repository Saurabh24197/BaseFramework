using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickCustom : MonoBehaviour 
{

    // Attach this GO onto the UI Object,
    // call it from self.UI_Parameters.

    //Each function performs a specific Operation.

    public void OpenURL(string url)
	{
		Application.OpenURL (url);
	}

	public void SetActive_StateChanged(GameObject go)
	{
		go.SetActive (!go.activeSelf);
	}
}