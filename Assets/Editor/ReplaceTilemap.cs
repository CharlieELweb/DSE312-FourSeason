using UnityEngine;
using UnityEditor;
using UnityEngine.Tilemaps;

public class ReplaceTilemap : EditorWindow
{
	private Tilemap targetTilemap;
	private TileBase oldTile;
	private TileBase newTile;

	[MenuItem("Tools/Swap Tiles on Tilemap")]
	public static void ShowWindow()
	{
		GetWindow<ReplaceTilemap>("Swap Tiles");
	}

	private void OnGUI()
	{
		GUILayout.Label("Replace All Instances of a Tile", EditorStyles.boldLabel);

		targetTilemap = (Tilemap)EditorGUILayout.ObjectField("Target Tilemap", targetTilemap, typeof(Tilemap), true);
		oldTile = (TileBase)EditorGUILayout.ObjectField("Original Rule Tile", oldTile, typeof(TileBase), false);
		newTile = (TileBase)EditorGUILayout.ObjectField("Rule Override Tile", newTile, typeof(TileBase), false);

		GUI.enabled = targetTilemap != null && oldTile != null && newTile != null;

		if (GUILayout.Button("Swap All Tiles"))
		{
			Undo.RecordObject(targetTilemap, "Swap Tiles");
			targetTilemap.SwapTile(oldTile, newTile);
			EditorUtility.SetDirty(targetTilemap);
		}

		GUI.enabled = true;
	}
}
