using UnityEngine;
using System.Collections;
using ZS.Engine;

namespace ZS.HUD {

	public class HUDManager : MonoBehaviour {

		#region GUISkins.

		public GUISkin resourceSkin, ordersSkin;
		public bool _insideWidth, _insideHeight;

		#endregion

		// Use this for initialization
		void Start () {
		}
		
		// Update is called once per frame	
		void OnGUI () {
			DrawResources();
			DrawOrders();
		}

		private void DrawResources() {
			GUI.skin = resourceSkin;
   			 GUI.BeginGroup(new Rect(0,0,Screen.width , Registry.Instance.gui_resourcesBarHeight));
  	 		 GUI.Box(new Rect(0,0,Screen.width,Registry.Instance.gui_resourcesBarHeight),"");
   			 GUI.EndGroup();
		}

		private void DrawOrders() {

			GUI.skin = ordersSkin;
	  		GUI.BeginGroup(new Rect(Screen.width - Registry.Instance.gui_ordersBarWidth , 
	  			Registry.Instance.gui_resourcesBarHeight, Registry.Instance.gui_ordersBarWidth,
	  			Screen.height - Registry.Instance.gui_resourcesBarHeight));
	  		GUI.Box(new Rect(0,0, Registry.Instance.gui_ordersBarWidth , Screen.height - Registry.Instance.gui_resourcesBarHeight),"");
	   		GUI.EndGroup();
		}

		public bool PointInClientBounds(Vector3 point) {
			 //Screen coordinates start in the lower-left corner of the screen
   			 //not the top-right of the screen like the drawing coordinates do
   			 _insideWidth = point.x >= 0 && point.x <= Screen.width - Registry.Instance.gui_ordersBarWidth;      
   			 _insideHeight = point.y >= 0 && point.y <= Screen.height - Registry.Instance.gui_resourcesBarHeight;
  			 return _insideWidth && _insideHeight;
		}
	}

}