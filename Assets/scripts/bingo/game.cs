using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class game : MonoBehaviour {

	int currentNumber;
	int updateTimes;

	// Granny's boards
	private int[][,] boards;

	// Opponent boards
	private int[][,] opponentBoards;

	// Random numbers used for the game and initialization
	private System.Random rand;

	// The number bucket (initialized to all numbers 1-75)
	private List<int> bucket;

	// Use this for initialization
	void Start () {
		currentNumber = -1;
		updateTimes = 0;

		// Generate the boards
		rand = new System.Random ();

		boards = new int[4][,];
		for (int i = 0; i < 4; i++) {
			boards[i] = new int[5, 5];
			GenerateBoard(ref boards[i]);

			// TODO: Write out the visible board
		}

		opponentBoards = new int[20][,];
		for (int i = 0; i < 20; i++) {
			opponentBoards[i] = new int[5, 5];
			GenerateBoard(ref opponentBoards[i]);
		}

		// Initialize the number bucket
		bucket = new List<int> ();
		for (int i = 1; i <= 75; i++) {
			bucket.Add(i);
		}
	}

	void GenerateBoard(ref int[,] board) {
		List<int> B = new List<int>(){1, 2, 3, 4, 5, 6, 7, 8, 9, 10,11,12,13,14,15};
		List<int> I = new List<int>(){16,17,18,19,20,21,22,23,24,25,26,27,28,29,30};
		List<int> N = new List<int>(){31,32,33,34,35,36,37,38,39,40,41,42,43,44,45};
		List<int> G = new List<int>(){46,47,48,49,50,51,52,53,54,55,56,57,58,59,60};
		List<int> O = new List<int>(){61,62,63,64,65,66,67,68,69,70,71,72,73,74,75};
		List<int>[] lists = {B,I,N,G,O};
		int index = 0;
		for (int i = 0; i < 5; i++) {
			for (int j = 0; j < 5; j++) {
				index = rand.Next (0, lists[i].Count);
				board[i, j] = lists[i][index];
				lists[i].RemoveAt (index);
			}
		}
	}

	void FixedUpdate() {
		// TODO: Handle end of game
		if (bucket.Count == 0) {
			return;
		}

		updateTimes++;

		// This should be called 50 times a second (default physics update), update every 4s
		if (updateTimes >= 50 * 4) {
			updateTimes = 0;

			PickANumber();
		}
	}

	void PickANumber() {
		// TODO: Kick off animation*

		// Pick a number
		int index = rand.Next (0, bucket.Count);
		currentNumber = bucket [index];
		bucket.RemoveAt (index);

		// TODO: Announce the number

		// TODO: Show a circle around the number on the board
		GameObject circle = Resources.Load ("circle") as GameObject;

		// Hacky way to get position for circle
		Vector3 circlePosition;
		if (currentNumber <= 15) {
			circlePosition = new Vector3 (-8.75f + 0.66f * (currentNumber-1), 4.59f, 0);
		} else if (currentNumber <= 30) {
			circlePosition = new Vector3 (-8.75f + 0.66f * ((currentNumber-15)-1), 3.99f, 0);
		} else if (currentNumber <= 45) {
			circlePosition = new Vector3 (-8.75f + 0.66f * ((currentNumber-30)-1), 3.41f, 0);
		} else if (currentNumber <= 60) {
			circlePosition = new Vector3 (-8.75f + 0.66f * ((currentNumber-45)-1), 2.82f, 0);
		} else {
			circlePosition = new Vector3 (-8.75f + 0.66f * ((currentNumber-60)-1), 2.3f, 0);
		}
		circle = Instantiate (circle, circlePosition, Quaternion.identity) as GameObject;
		circle.transform.localScale = new Vector3 (2, 2, 0);

		// TODO: Update the opponent states
		// TODO: Handle any other winners
	}

	public void BoardClicked(string name) {
		// TODO: Handle the board being clicked (if it has a number, update the state, otherwise bad granny game over)
		Debug.Log ("Board clicked: " + name);
	}
}
