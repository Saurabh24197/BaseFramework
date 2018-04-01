using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BaseFramework;

public class OnCollisionDestroy : MonoBehaviour 
{
	public List<string> destroyedGO;
	public float waitTime = 10f;

	public bool destroySelf = false;

	void OnCollisionEnter(Collision other)
	{
		#if UNITY_EDITOR
		destroyedGO.Add (other.transform.root.gameObject.name);
        #endif

        other.gameObject.SetActive(false);
        Destroy (other.gameObject);
	}

	void Start()
	{
		if (destroySelf) 
		{
			StartCoroutine (SelfDestruct ());
		}
	}
		
	IEnumerator SelfDestruct()
	{
		yield return new WaitForSeconds (waitTime);
			Destroy (gameObject);
	}
}
