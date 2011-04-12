using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {
	
	public Texture2D icon;
	
	private ListBox some_list;
	private Rect window_ListBoxTest = new Rect(5, 5, 300, 175);
	public System.String lb_itemlabel_tf = "Button label"; 
	public System.String lb_insertafter_tf = "2";
	public System.String lb_removeid_tf = "3";
	public bool lb_icon_toogle = false;
	public int lb_lastselected;
	
	
	// Use this for initialization
	void Start() {
		//LISTBOX TEST
		some_list = new ListBox(new Rect(10, 20, 110, 150), new Rect(0, 0, 90, 150), false, true);
		some_list.AddItem("Text 1");
		some_list.AddItem("Text 2");
		some_list.AddItem("Text 3");
		some_list.AddItem("Text 4");
		
		some_list.InsertItem(2, icon);
		//-------------
	}
	
	void OnGUI() {
		window_ListBoxTest = GUI.Window(0, window_ListBoxTest, DrawWindow_ListBoxTest,  "ListBox Test");
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void DrawWindow_ListBoxTest(int windowID)
	{
		//Click Test
		if (some_list.ReDraw())
		{
			lb_lastselected = some_list.GetSelectedID();
		}
		//----------
		
		//Add Item Test
		lb_itemlabel_tf = GUI.TextField(new Rect(130, 20, 100, 20), lb_itemlabel_tf, 25);
		if (GUI.Button(new Rect(235, 20, 60, 20), "Add"))
		{
			if (!lb_icon_toogle) 
				some_list.AddItem(lb_itemlabel_tf);
			else
				some_list.AddItem(icon);
		}
		//----------
		//Insert Item Test
		GUI.Label(new Rect(130,45,75,20), "Insert after");
		lb_insertafter_tf = GUI.TextField(new Rect(200, 45, 30, 20), lb_insertafter_tf, 2);
		if (GUI.Button(new Rect(235, 45, 60, 20), "Insert"))
		{
			if (!lb_icon_toogle) 
				some_list.InsertItem(System.Convert.ToInt32(lb_insertafter_tf), lb_itemlabel_tf);
			else
				some_list.InsertItem(System.Convert.ToInt32(lb_insertafter_tf), icon);
		}
		//-----------
		
		//Remove Item Test
		GUI.Label(new Rect(130,70,75,20), "Remove ID");
		lb_removeid_tf = GUI.TextField(new Rect(200, 70, 30, 20), lb_removeid_tf, 2);
		if (GUI.Button(new Rect(235, 70, 60, 20), "Remove"))
		{
			some_list.RemoveItem(System.Convert.ToInt32(lb_removeid_tf));
		}
		//------------
		
		lb_icon_toogle = GUI.Toggle(new Rect(130,130,150,20), lb_icon_toogle, "Button with icon");
		
		GUI.Label(new Rect(130,150,200,20), "Last selected ID: "+lb_lastselected);
		
		GUI.DragWindow(new Rect(0, 0, 10000, 20));
	}
	
}
