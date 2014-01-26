using UnityEngine;
using System.Collections;

namespace ZS.Engine { 

	// Type of the player.
	public enum PlayerType {
		// Current player.
		Current,
		// Human opponent.
		Human,
		// Bot opponent.
		Bot
	}

	// Represents a current player.
	public class Player : MonoBehaviour {

		public string username;
		public PlayerType playerType; // Is this a current player or a different 

		public Player() {
		}

		// Use this for initialization
		void Start () {
			
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}

}