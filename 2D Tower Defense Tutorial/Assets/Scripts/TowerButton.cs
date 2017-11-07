using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerButton : MonoBehaviour {

	[SerializeField]
	private GameObject towerPrefab;

	[SerializeField]
	private Sprite hoverSprite;

	public GameObject TowerPrefab {
		get{
			return towerPrefab;
		}
	}

	public Sprite HoverSprite {
		get {
			return hoverSprite;
		}
	}
}
