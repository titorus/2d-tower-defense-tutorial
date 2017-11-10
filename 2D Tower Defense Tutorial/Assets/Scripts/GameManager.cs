using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager> {

	private int currency;
	public int Currency {
		get {
			return currency;
		}
		set {
			currency = value;
			currencyText.text = value.ToString () + " <color=lime>$</color>";
		}
	}

	[SerializeField]
	private Text currencyText;

	public TowerButton ActiveTowerButton {
		get;
		private set;
	}

	public ObjectPool Pool {
		get;
		private set;
	}

	private void Awake(){
		Pool = GetComponent<ObjectPool> ();
	}

	// Use this for initialization
	void Start () {
		Currency = 10000;
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
		if (Currency >= towerButton.Price) {
			ActiveTowerButton = towerButton;
			Hover.Instance.Activate (towerButton.HoverSprite);
		}
	}

	public void unpickTower () {
		ActiveTowerButton = null;
		Hover.Instance.Deactivate ();
	}

	public void BuyTower(){
		if (Currency >= ActiveTowerButton.Price) {
			Currency -= ActiveTowerButton.Price;
		}

		ActiveTowerButton = null;
		Hover.Instance.Deactivate ();
	}

	public void StartWave(){
		StartCoroutine (SpawnWave ());
	}

	private IEnumerator SpawnWave(){
		yield return new WaitForSeconds (2.5f);

		GameObject newEnemey = Pool.getObject ("Crocodile");
		Debug.Log (newEnemey.name);
	}
}
