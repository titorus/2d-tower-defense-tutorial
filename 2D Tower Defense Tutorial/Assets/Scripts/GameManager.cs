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
		if (Input.GetKey(KeyCode.Escape)) {
			HandleEscape ();
		}

		if (Input.GetMouseButtonDown (1)) {
			HandleRightClick ();
		}
	}

	private void HandleEscape() {
		if (ActiveTowerButton != null) {
			unpickTower ();
		}
	}

	private void HandleRightClick(){
		if(ActiveTowerButton != null ){
			unpickTower ();
		}
	}

	public void pickTower(TowerButton towerButton){
		ActiveTowerButton = towerButton;
		Hover.Instance.Activate (towerButton.HoverSprite);
	}

	public void unpickTower () {
		ActiveTowerButton = null;
		Hover.Instance.Deactivate ();
	}

	public void BuyTower(){
		ActiveTowerButton = null;
		Hover.Instance.Deactivate ();
	}
}
