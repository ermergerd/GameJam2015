using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class game : MonoBehaviour {

	// General state
	private int currentNumber;
	private List<int> calledNumbers;
	private int updateTimes;
	private bool gameOver;

	// Granny's boards
	private int[][,] boards;
	private bool[][,] boardsStates;

	// Opponent boards
	private int[][,] opponentBoards;
	private bool[][,] opponentBoardsStates;

	// Random numbers used for the game and initialization
	private System.Random rand;

	// The number bucket (initialized to all numbers 1-75)
	private List<int> bucket;

	// Time delays for sound effects and end of game
	private int endOfGameDelay;
	private int opponentBingoDelay;

	// Constants for the game (that we could tweak)
	private const int NumberOfOpponents = 10;
	private const int NewNumberRefreshInSec = 10;
	private const int OpponentBingoDelay = 5 * 50;
	private const int EndOfGameDelay = 2 * 50;

	// Use this for initialization
	void Start () {
		// General state
		currentNumber = -1;
		calledNumbers = new List<int>();
		updateTimes = 0;
		gameOver = false;

		// Setup the delays to not be running
		endOfGameDelay = 0;
		opponentBingoDelay = 0;

		// Generate the boards
		rand = new System.Random ();

		boards = new int[4][,];
		boardsStates = new bool[4][,];
		for (int i = 0; i < 4; i++) {
			boards[i] = new int[5, 5];
			GenerateBoard(ref boards[i]);

			boardsStates[i] = new bool[5, 5]
			{
				{false,false,false,false,false},
				{false,false,false,false,false},
				{false,false,false,false,false},
				{false,false,false,false,false},
				{false,false,false,false,false},
			};

			// Write out the visible board
			for (int j = 0; j < 5; j++) {
				for (int k = 0; k < 5; k++) {
					// Don't put numbers on the free space
					if (j != 2 || k != 2) {
						PlaceNumberOnBoard(i, boards[i][j,k], j, k);
					}
				}
			}
		}

		opponentBoards = new int[NumberOfOpponents][,];
		opponentBoardsStates = new bool[NumberOfOpponents][,];
		for (int i = 0; i < NumberOfOpponents; i++) {
			opponentBoards[i] = new int[5, 5];
			GenerateBoard(ref opponentBoards[i]);

			opponentBoardsStates[i] = new bool[5, 5]
			{
				{false,false,false,false,false},
				{false,false,false,false,false},
				{false,false,true,false,false},
				{false,false,false,false,false},
				{false,false,false,false,false},
			};
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

	void PlaceNumberOnBoard(int board, int number, int i, int j) {
		// Show a circle around the number on the board
		GameObject num = Resources.Load (number.ToString()) as GameObject;
		
		Vector3 numPosition = new Vector3(0,0,0);
		switch (board) {
		case 0:
			numPosition = new Vector3(-6.31f + i*0.53f, -1.19f + j*-0.53f, 0);
			break;
		case 1:
			numPosition = new Vector3(-2.89f + i*0.53f, -1.19f + j*-0.53f, 0);
			break;
		case 2:
			numPosition = new Vector3(0.77f + i*0.53f, -1.19f + j*-0.53f, 0);
			break;
		case 3:
			numPosition = new Vector3(4.27f + i*0.53f, -1.19f + j*-0.53f, 0);
			break;
		}
		
		num = Instantiate (num, numPosition, Quaternion.identity) as GameObject;
		num.transform.localScale = new Vector3 (1.5f, 1.5f, 0);
		num.renderer.sortingOrder = 3;
	}

	void FixedUpdate() {
		// Handle end of game w/ no winner
		if (bucket.Count == 0) {
			// TODO: Handle weird no winner gameover case
			Debug.Log("Game over: Nobody won!");
			gameOver = true;
			GrannyState.instance.loadNextLevel();
		}

		// Handle the delays
		if (endOfGameDelay > 0) {
			endOfGameDelay--;
			if (endOfGameDelay == 0) {
				// TODO: Handle end of game
			}
		} else if (opponentBingoDelay > 0) {
			opponentBingoDelay--;
			if (opponentBingoDelay == 0) {
				// Handle opponent bingo
				AudioClip other_winner = Resources.Load ("bingo_other_winner") as AudioClip;
				audio.clip = other_winner;
				audio.Play ();

				gameOver = true;
				endOfGameDelay = EndOfGameDelay;
			}
		}

		if (!gameOver) {
			updateTimes++;

			// This should be called 50 times a second (default physics update)
			if (updateTimes >= 50 * NewNumberRefreshInSec) {
				updateTimes = 0;
				PickANumber ();
			}
		}
	}

	void PickANumber() {
		// TODO: Kick off animation*

		// Pick a number
		int index = rand.Next (0, bucket.Count);
		currentNumber = bucket [index];
		calledNumbers.Add(currentNumber);
		bucket.RemoveAt (index);

		// Announce the number
		string clip;
		if (currentNumber <= 15) {
			clip = "B" + currentNumber.ToString();
		} else if (currentNumber <= 30) {
			clip = "I" + currentNumber.ToString();
		} else if (currentNumber <= 45) {
			clip = "N" + currentNumber.ToString();
		} else if (currentNumber <= 60) {
			clip = "G" + currentNumber.ToString();
		} else {
			clip = "O" + currentNumber.ToString();
		}
		AudioClip number = Resources.Load (clip) as AudioClip;
		audio.clip = number;
		audio.Play ();

		// Show a circle around the number on the board
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
		circle.renderer.sortingOrder = 3;

		// Update the opponent states and handle any winners
		for (int i = 0; i < NumberOfOpponents; i++) {
			for (int j = 0; j < 5; j++) {
				for (int k = 0; k < 5; k++) {
					if (opponentBoards[i][j,k] == currentNumber) {
						opponentBoardsStates[i][j,k] = true;

						// Check for a winner
						if (CheckForWinner(ref opponentBoardsStates[i], j, k)) {
							// Handle other winner
							Debug.Log ("Game over: someone else won!");
							opponentBingoDelay = OpponentBingoDelay;
							gameOver = true;
							GrannyState.instance.loadNextLevel();
						}
					}
				}
			}
		}
	}

	// Check for winner (optimized to only check rows and columns that were just updated and diagonals)
	bool CheckForWinner(ref bool[,] board, int i, int j) {
		if (board [0, 0] && board [1, 1] && board [2, 2] && board [3, 3] && board [4, 4]) {
			return true;
		} else if (board [0, 4] && board [1, 3] && board [2, 2] && board [3, 1] && board [4, 0]) {
			return true;
		} else if (board [i, 0] && board [i, 1] && board [i, 2] && board [i, 3] && board [i, 4]) {
			return true;
		} else if (board [0, j] && board [1, j] && board [2, j] && board [3, j] && board [4, j]) {
			return true;
		}

		return false;
	}

	public void BoardClicked(string name) {
		// Handle the board being clicked (if it has a number, update the state, otherwise bad granny game over)
		Debug.Log ("Board clicked: " + name);

		// Don't process if the game is over
		if (gameOver) {
			return;
		}

		int boardNum = 0;
		if (name == "board1") {
			boardNum = 0;
		} else if (name == "board2") {
			boardNum = 1;
		} else if (name == "board3") {
			boardNum = 2;
		} else if (name == "board4") {
			boardNum = 3;
		}

		// Update the granny board states and handle any winners
		bool found = false;
		for (int j = 0; j < 5; j++) {
			for (int k = 0; k < 5; k++) {
				if (calledNumbers.Contains(boards[boardNum][j,k]) && !boardsStates[boardNum][j,k]) {
					found = true;
					boardsStates[boardNum][j,k] = true;

					// Place the piece
					PlaceToken(boardNum, j, k);
					
					// Check for a winner
					if (CheckForWinner(ref boardsStates[boardNum], j, k)) {
						// TODO: Handle granny winner (add money)
						GrannyState.instance.moneyCt += 50;

						AudioClip other_winner = Resources.Load ("grannygotbingo") as AudioClip;
						audio.clip = other_winner;
						audio.Play ();

						Debug.Log("Game over: Granny won!!");
						gameOver = true;
						endOfGameDelay = EndOfGameDelay;
						GrannyState.instance.loadNextLevel();

					}

					// Only allow granny to find one number at a time
					goto haha_goto;
				}
			}
		}

	haha_goto:

		// Handle granny cheating
		if (!found) {
			// Give her a chance to place a token on the free space
			if (!boardsStates[boardNum][2,2]) {
				boardsStates[boardNum][2,2] = true;

				PlaceToken(boardNum, 2, 2);
			} else {
				// Granny cheated! Handle loss
				AudioClip other_winner = Resources.Load ("granny_cheated") as AudioClip;
				audio.clip = other_winner;
				audio.Play ();

				Debug.Log("Game over: Granny cheated!");
				gameOver = true;
				endOfGameDelay = EndOfGameDelay;
				GrannyState.instance.moneyCt-=5;
				GrannyState.instance.loadNextLevel();
			}
		}
	}

	void PlaceToken(int board, int i, int j) {
		// Show a circle around the number on the board
		GameObject x = Resources.Load ("bingo_x") as GameObject;

		Vector3 xPosition = new Vector3(0,0,0);
		switch (board) {
		case 0:
			xPosition = new Vector3(-6.31f + i*0.53f, -1.19f + j*-0.53f, 0);
			break;
		case 1:
			xPosition = new Vector3(-2.89f + i*0.53f, -1.19f + j*-0.53f, 0);
			break;
		case 2:
			xPosition = new Vector3(0.77f + i*0.53f, -1.19f + j*-0.53f, 0);
			break;
		case 3:
			xPosition = new Vector3(4.27f + i*0.53f, -1.19f + j*-0.53f, 0);
			break;
		}

		x = Instantiate (x, xPosition, Quaternion.identity) as GameObject;
		x.transform.localScale = new Vector3 (2, 2, 0);
		x.renderer.sortingOrder = 3;
	}
}
