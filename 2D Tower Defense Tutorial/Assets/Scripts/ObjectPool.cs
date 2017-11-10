using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {

	[SerializeField]
	private GameObject[] objectPrefabs;

	/// <summary>
	/// Returns the requested object.	
	/// </summary>
	/// <returns>The object.</returns>
	/// <param name="type">The Type we want to get a GameObject for.</param>
	public GameObject getObject(string type){
		for (int i = 0; i < objectPrefabs.Length; i++) {
			if (objectPrefabs [i].name.Equals (type)) {
				GameObject newObject = (GameObject)Instantiate (objectPrefabs [i]);
				return newObject;
			}
		}
		return null;
	}
}