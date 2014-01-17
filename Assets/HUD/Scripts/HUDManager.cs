using UnityEngine;
using System.Collections;
using ZS.Engine;

namespace ZS.HUD {

	public class HUDManager : MonoBehaviour {

		#region GUISkins.

		public GUISkin resourceSkin, ordersSkin, selectBoxSkin;
		public bool _insideWidth, _insideHeight;

		public int gui_ordersBarWidth = 150;
		public int gui_resourcesBarHeight = 40;
		public int gui_selectionNameHeight = 30;

		#endregion

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame	
		void OnGUI () {
			DrawResources();
			DrawOrders();
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
	}

}