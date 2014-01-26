using UnityEngine;
using System.Collections;
using ZS.Engine;
using ZS.HUD;

namespace ZS.Characters {

	// Basic unit / person that player can acquire.
	// That can be an animal or a person.
	public class Unit : Entity {

		// Override default hovering behavior.
		public override void SetHoverState(GameObject hoverObject) {
		    base.SetHoverState(hoverObject);

		    // For units we can make them move.
		    if(_currentSelection != SelectionType.NotSelected && Owner != null && Owner.playerType == PlayerType.Current) {
				 if(hoverObject.tag == Registry.GROUND_NAME) {
				 	Debug.Log("QUACK!");
		        	Registry.Instance.hudManager.SetCursorState(CursorState.Move);
		         } 
			}
		}

	}

}