using UnityEngine;
using System.Collections;

public class board : MonoBehaviour {

	public GameObject gameParent;
	private game bingoGame;

	void Start() {
		bingoGame = gameParent.GetComponent<game> ();
	}

	void OnMouseDown() {
		// Handle someone pressing the board
		bingoGame.BoardClicked (gameObject.name);
	}
}
