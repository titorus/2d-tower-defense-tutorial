using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	private float speed;

	private Stack<AStarNode> walkingPath;

	public Point GridPosition { get; set; }

	private Vector3 currentDestination;

	public bool IsActive { get;	set; }

	private void Update(){
		Move ();
	}
		
	public void Spawn(){
		transform.position = LevelManager.Instance.SpawnPortal.transform.position;
		SetPath (LevelManager.Instance.EnemyPath);
		GetComponent<SpriteRenderer> ().sortingOrder = GridPosition.Y;

		StartCoroutine(Scale(new Vector3(0.1f, 0.1f), new Vector3(1f, 1f)));
	}

	public IEnumerator Scale(Vector3 from, Vector3 to){
		float progress = 0f;
		IsActive = false;

		while (progress <= 1) {
			transform.localScale = Vector3.Lerp (from, to, progress);
			progress += Time.deltaTime;

			yield return null;
		}
			
		transform.localScale = to;

		IsActive = true;
	}

	private void Move(){
		if (IsActive) {
			transform.position = Vector2.MoveTowards (transform.position, currentDestination, speed * Time.deltaTime);

			//did we reach the destination?
			if (transform.position == currentDestination) {
				if (walkingPath != null && walkingPath.Count > 0) {
					GridPosition = walkingPath.Peek ().gridPosition;
					currentDestination = walkingPath.Pop ().WorldPosition;
				}
			}
		}
	}

	private void SetPath(Stack<AStarNode> path){
		if (path != null) {
			walkingPath = path;
			GridPosition = walkingPath.Peek ().gridPosition;
			currentDestination = walkingPath.Pop ().WorldPosition;
		}
	}
}
