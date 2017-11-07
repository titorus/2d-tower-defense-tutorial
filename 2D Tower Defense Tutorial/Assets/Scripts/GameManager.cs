using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager> {
	
	public TowerButton ActiveTowerButton {
		get;
		private set;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void pickTower(TowerButton towerButton){
		ActiveTowerButton = towerButton;
		Hover.Instance.Activate (towerButton.HoverSprite);
	}

	public void BuyTower(){
		ActiveTowerButton = null;
		Hover.Instance.Deactivate ();
	}
}
