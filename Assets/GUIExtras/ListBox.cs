//GUIExtras for Unity3D.
//Coded by Alex ProgMan Logvinov.
//
//===ListBox===========
//ListBox is made from ScrollBox, and Buttons as ListItems.
//=====================

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//****LISTBOX CLASS****
public class ListBox {
	//Fields
	private Rect drawRect;
	private Rect visibleRect;
	
	private Vector2 scrollPos;
	
	private bool horScrollVisible;
	private bool vertScrollVisible;
	
	private List<ListItem> listItems = new List<ListItem>();
	
	protected int lastSelectedIndex = -1;
	
	protected GUISkin custom_skin; 
	protected bool custom_skin_flag = false;
	//======
	
	//Constructors
	public ListBox(Rect c_drawRect, Rect c_visibleRect)
	{
		drawRect = c_drawRect;
		visibleRect = c_visibleRect;
		
		horScrollVisible = false;
		vertScrollVisible = false;
	}
	
	public ListBox(Rect c_drawRect, Rect c_visibleRect, bool c_horScrollV, bool c_vertScrollV)
	{
		drawRect = c_drawRect;
		visibleRect = c_visibleRect;
		
		horScrollVisible = c_horScrollV;
		vertScrollVisible = c_vertScrollV;
	}
	
		//Skins added
	public ListBox(Rect c_drawRect, Rect c_visibleRect, GUISkin c_custom_skin)
	{
		drawRect = c_drawRect;
		visibleRect = c_visibleRect;
		
		horScrollVisible = false;
		vertScrollVisible = false;
		
		custom_skin = c_custom_skin;
		custom_skin_flag = true;
	}
	
	public ListBox(Rect c_drawRect, Rect c_visibleRect, bool c_horScrollV, bool c_vertScrollV, GUISkin c_custom_skin)
	{
		drawRect = c_drawRect;
		visibleRect = c_visibleRect;
		
		horScrollVisible = c_horScrollV;
		vertScrollVisible = c_vertScrollV;
		
		custom_skin = c_custom_skin;
		custom_skin_flag = true;
	}
	//=============
	
	//Methods
	//---ITEMS---
		//Text items
	public int AddItem(System.String c_ItemLabel)
	{
		int itemIndex = listItems.Count+1;
		listItems.Add(new ListItem(itemIndex, new Rect(0, 0,(int)visibleRect.width, 20), c_ItemLabel));
		return itemIndex;
	}
	public int AddItem(System.String c_ItemLabel, int c_ItemH)
	{
		int itemIndex = listItems.Count+1;
		listItems.Add(new ListItem(itemIndex, new Rect(0, 0,(int)visibleRect.width, c_ItemH), c_ItemLabel));
		return itemIndex;
	}
	
	public int InsertItem(int insertPos, System.String c_ItemLabel)
	{
		int itemIndex = listItems.Count+1;
		listItems.Insert(insertPos, new ListItem(itemIndex, new Rect(0, 0,(int)visibleRect.width, 20), c_ItemLabel));
		return itemIndex;
	}
	public int InsertItem(int insertPos,System.String c_ItemLabel, int c_ItemH)
	{
		int itemIndex = listItems.Count+1;
		listItems.Insert(insertPos, new ListItem(itemIndex, new Rect(0, 0,(int)visibleRect.width, c_ItemH), c_ItemLabel));
		return itemIndex;
	}
		//---------
		//Icon items
	public int AddItem(Texture2D c_ItemIcon)
	{
		int itemIndex = listItems.Count+1;
		listItems.Add(new ListItem(itemIndex, new Rect(0, 0,(int)visibleRect.width, 20), c_ItemIcon));
		return itemIndex;
	}
	public int AddItem(Texture2D c_ItemIcon, int c_ItemH)
	{
		int itemIndex = listItems.Count+1;
		listItems.Add(new ListItem(itemIndex, new Rect(0, 0,(int)visibleRect.width, c_ItemH), c_ItemIcon));
		return itemIndex;
	}
	
	public int InsertItem(int insertPos, Texture2D c_ItemIcon)
	{
		int itemIndex = listItems.Count+1;
		listItems.Insert(insertPos, new ListItem(itemIndex, new Rect(0, 0,(int)visibleRect.width, 20), c_ItemIcon));
		return itemIndex;
	}
	public int InsertItem(int insertPos, Texture2D c_ItemIcon, int c_ItemH)
	{
		int itemIndex = listItems.Count+1;
		listItems.Insert(insertPos, new ListItem(itemIndex, new Rect(0, 0,(int)visibleRect.width, c_ItemH), c_ItemIcon));
		return itemIndex;
	}
		//-----------
	public bool RemoveItem(int iIndex)
	{
		foreach (ListItem nextListItem in listItems)
		{
			if (nextListItem.GetID()==iIndex)
			{
				listItems.Remove(nextListItem);
				return true;
			}
		}    
		return false;	    
	}
	//-------------
	
	public bool ReDraw() 
	{
		bool clicked_button = false;
		
		GUISkin backup_skin = GUI.skin;
		if (custom_skin_flag) GUI.skin = custom_skin;
		
		GUI.Box(drawRect, ""); 
		scrollPos = GUI.BeginScrollView(drawRect, scrollPos, visibleRect, horScrollVisible, vertScrollVisible);
		
		int but_y_pos=0;
		foreach (ListItem nextListItem in listItems)
		{
			nextListItem.SetY(but_y_pos); //Put list item in right position
			if (nextListItem.DrawItem())  //Draw it, and check for click on it
			{
				clicked_button = true;
				lastSelectedIndex = nextListItem.GetID();
			}
			but_y_pos += nextListItem.GetHeight(); //Set Y position of next list item
		}
		//Set height of visible zone of ListBox
		if (but_y_pos>=drawRect.height) 
			visibleRect.height = but_y_pos;
		else
			visibleRect.height = drawRect.height;
		//---
		
		GUI.EndScrollView();
		
		if (custom_skin_flag) GUI.skin = backup_skin;
		
		return clicked_button;
	}
	//=============
	
	public int GetSelectedID()
	{
		return lastSelectedIndex;
	}

}
//*************************

//****LISTITEM CLASS****
class ListItem {
	private int id;
	
	private Rect drawRect;
	private System.String ItemLabel;
	private Texture2D ItemIcon;
	
	private bool is_iconButton;
	
	//Constructor
	public ListItem(int c_id, Rect c_drawRect, System.String c_ItemLabel)
	{
		id = c_id;
		
		drawRect = c_drawRect;
		ItemLabel = c_ItemLabel;
		
		is_iconButton = false;
	}
	
	public ListItem(int c_id, Rect c_drawRect, Texture2D c_ItemIcon)
	{
		id = c_id;
		
		drawRect = c_drawRect;
		ItemIcon = c_ItemIcon;
		
		is_iconButton = true;
	}
	//--------------
	
	public bool DrawItem()
	{
		bool is_clicked;
		
		if (is_iconButton)
		{
			is_clicked = GUI.Button(drawRect, ItemIcon);
		}
		else
		{
			is_clicked = GUI.Button(drawRect, ItemLabel);
		}
		
		return is_clicked;
	}
	
	//Setters and getters
	public void SetID(int new_id)
	{
		id = new_id;
	}
	
	public int GetID()
	{
		return id;
	}
	
	public void SetY(int set_value)
	{
		drawRect.y = set_value;
	}
	public int GetHeight()
	{
		return (int)drawRect.height;
	}
	//-------------------
}
//**********************