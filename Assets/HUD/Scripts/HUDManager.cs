using UnityEngine;
using System.Collections;
using ZS.Engine;
using ZS.Engine.Peripherials;

namespace ZS.HUD {

	// State of the cursor.
	public enum CursorState { 
		Select, 
		Move,
		Attack, 
		PanLeft, 
		PanRight, 
		PanUp, 
		PanDown, 
		Harvest 
	}

	public class HUDManager : MonoBehaviour {

		#region Textures.

		public Texture2D activeCursor;
		public Texture2D selectCursor, leftCursor, rightCursor, upCursor, downCursor;
		public Texture2D[] moveCursors, attackCursors, harvestCursors;

		#endregion

		#region GUISkins.

		public GUISkin resourceSkin, ordersSkin, selectBoxSkin, mouseCursorSkin;
		public bool _insideWidth, _insideHeight;

		public int gui_ordersBarWidth = 150;
		public int gui_resourcesBarHeight = 40;
		public int gui_selectionNameHeight = 30;

		#endregion

		private CursorState _activeCursorState;
		private int _currentFrame = 0;

		// Use this for initialization
		void Start () {
			SetArcadeMode();
		}
		
		// Update is called once per frame	
		void OnGUI () {
			DrawResources();
			DrawOrders();
			DrawMouseCursor();
		}

		// Draws the resources bar.
		private void DrawResources() {
			GUI.skin = resourceSkin;
   			 GUI.BeginGroup(new Rect(0,0,Screen.width , gui_resourcesBarHeight));
  	 		 GUI.Box(new Rect(0,0,Screen.width,gui_resourcesBarHeight),"");
   			 GUI.EndGroup();
		}

		// Draws the orders bar.
		private void DrawOrders() {
			GUI.skin = ordersSkin;
	  		GUI.BeginGroup(new Rect(Screen.width - gui_ordersBarWidth , 
	  			gui_resourcesBarHeight, gui_ordersBarWidth,
	  			Screen.height - gui_resourcesBarHeight));
	  		GUI.Box(new Rect(0,0, gui_ordersBarWidth , Screen.height - gui_resourcesBarHeight),"");

			// Draw selected player.
			var selectionName = "";
			if(GameService.Instance.selectedObject != null) {
    			selectionName = GameService.Instance.selectedObject.displayName;
			}
			if(!selectionName.Equals("")) {
    			GUI.Label(new Rect(4,10, gui_ordersBarWidth, 
    				gui_selectionNameHeight), selectionName);
			}	   		

	   		GUI.EndGroup();
		}

		public bool PointInClientBounds(Vector3 point) {
			 //Screen coordinates start in the lower-left corner of the screen
   			 //not the top-right of the screen like the drawing coordinates do
   			 _insideWidth = point.x >= 0 && point.x <= Screen.width - gui_ordersBarWidth;      
   			 _insideHeight = point.y >= 0 && point.y <= Screen.height - 	gui_resourcesBarHeight;
  			 return _insideWidth && _insideHeight;
		}

		// Gets the actual PLAYING area.
		public Rect GetPlayingArea() {
    		return new Rect(0, gui_resourcesBarHeight, Screen.width - gui_ordersBarWidth, 
    			Screen.height - gui_resourcesBarHeight);
		}

		// Draw a mouse cursor.
		private void DrawMouseCursor() {
  			if(!PointInClientBounds(InputService.Instance.MousePosition)) {
	        	Screen.showCursor = true;
	    	} else {
        		Screen.showCursor = false; // dont use the regular cursor, just draw our skin.
        		GUI.skin = mouseCursorSkin;
        		GUI.BeginGroup(new Rect(0,0,Screen.width,Screen.height));
        		UpdateCursorAnimation();
        		Rect cursorPosition = GetCursorDrawPosition();
        		// Draw label with texture.
        		GUI.Label(cursorPosition, activeCursor);
        		GUI.EndGroup();
    		}
		}

		//sequence animation for cursor (based on more than one image for the cursor)
    	//change once per second, loops through array of images
    	// Time.time =  This is the time in seconds since the start of the game. 
		private void UpdateCursorAnimation() {
    		if(_activeCursorState == CursorState.Move) {
        		_currentFrame = (int)Time.time % moveCursors.Length;
        		activeCursor = moveCursors[_currentFrame];
    		} else if(_activeCursorState == CursorState.Attack) {
        		_currentFrame = (int)Time.time % attackCursors.Length;
        		activeCursor = attackCursors[_currentFrame];
    		} else if(_activeCursorState == CursorState.Harvest) {
        		_currentFrame = (int)Time.time % harvestCursors.Length;
        		activeCursor = harvestCursors[_currentFrame];
    		}
		}

		// Get position where to draw a cursror.
		private Rect GetCursorDrawPosition() {
    		//set base position for custom cursor image
    		//screen draw coordinates are inverted
  			float leftPos = InputService.Instance.MousePosition.x;
    		float topPos = Screen.height - InputService.Instance.MousePosition.y; 

    		//adjust position base on the type of cursor being shown
    		if(_activeCursorState == CursorState.PanRight)
    			 leftPos = Screen.width - activeCursor.width;
   			else if(_activeCursorState == CursorState.PanDown) 
   				topPos = Screen.height - activeCursor.height;
    		else if(_activeCursorState == CursorState.Move || 
    			_activeCursorState == CursorState.Select || 
    			_activeCursorState == CursorState.Harvest) {
        			topPos -= activeCursor.height / 2;
        			leftPos -= activeCursor.width / 2;
   				}
    		return new Rect(leftPos, topPos, activeCursor.width, activeCursor.height);
		}

		public void SetArcadeMode() {
			SetCursorState(CursorState.Attack);
		} 

		public void SetTacticalMode() {
			SetCursorState(CursorState.Select);
		}

		public void SetCursorState(CursorState newState) {
    		_activeCursorState = newState;
    		switch(newState) {
			    case CursorState.Select:
        			activeCursor = selectCursor;
        			break;
    			case CursorState.Attack:
        			_currentFrame = (int)Time.time % attackCursors.Length;
        			activeCursor = attackCursors[_currentFrame];
        			break;
    			case CursorState.Harvest:
        			_currentFrame = (int)Time.time % harvestCursors.Length;
        			activeCursor = harvestCursors[_currentFrame];
        			break;
    			case CursorState.Move:
        			_currentFrame = (int)Time.time % moveCursors.Length;
        			activeCursor = moveCursors[_currentFrame];
        			break;
    			case CursorState.PanLeft:
        			activeCursor = leftCursor;
    				break;
    			case CursorState.PanRight:
        			activeCursor = rightCursor;
        			break;
    			case CursorState.PanUp:
        			activeCursor = upCursor;
        			break;
    			case CursorState.PanDown:
        			activeCursor = downCursor;
        			break;
    			default: break;
    		}
		}
	}

}