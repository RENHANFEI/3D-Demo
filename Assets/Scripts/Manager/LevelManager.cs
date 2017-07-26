﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using MiniJSON;
public class LevelManager : MonoBehaviour {

	public RoomBorders roomPrefab;

	public GameObject playerPrefab;

	private RoomBorders roomBorders;
	private string levelJsonFilePath = "Assets/Scripts/Json/Level.json";
	private float lengthPerUnit = Configurations.lengthPerUnit;

	// Use this for initialization
	void Start () {
		roomBorders = Instantiate(roomPrefab) as RoomBorders;
		ReadAndParse();
		GameObject player = Instantiate(playerPrefab) as GameObject;
		player.transform.position = new Vector3(15,10,15);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void ReadAndParse() {
		string jsonString = File.ReadAllText(levelJsonFilePath);
		ParseJsonString(jsonString);
	}

	void ParseJsonString(string data) {
		Dictionary<string, object> dict;
		dict = Json.Deserialize(data) as Dictionary<string,object>;

		foreach (KeyValuePair<string, object> entry in dict) {
			//entry.key should be a string
			//entry.value should be a dictionary type

			int roomId = int.Parse(entry.Key);
			float posX, posY, posZ;
			float dimX, dimY, dimZ;
			float colorR, colorG, colorB, colorA;

			Dictionary<string, object> entryValueDict = (Dictionary<string,object>)entry.Value;
			List<object> positionList = ((List<object>) entryValueDict["position"]);
			List<object> dimensionList = ((List<object>) entryValueDict["dimension"]);
			List<object> colorList = ((List<object>) entryValueDict["color"]);

			posX = System.Convert.ToSingle(positionList[0]) * lengthPerUnit;
			posY = System.Convert.ToSingle(positionList[1]) * lengthPerUnit;
			posZ = System.Convert.ToSingle(positionList[2]) * lengthPerUnit;
			dimX = System.Convert.ToSingle(dimensionList[0]);
			dimY = System.Convert.ToSingle(dimensionList[1]);
			dimZ = System.Convert.ToSingle(dimensionList[2]);
			colorR = System.Convert.ToSingle(colorList[0]);
			colorG = System.Convert.ToSingle(colorList[1]);
			colorB = System.Convert.ToSingle(colorList[2]);
			colorA = System.Convert.ToSingle(colorList[3]);
			
			Vector3 position = new Vector3(posX, posY, posZ);
			Vector3 dimension = new Vector3(dimX, dimY, dimZ);
			Color color = new Color(colorR, colorG, colorB, colorA);

			Room room = ScriptableObject.CreateInstance<Room>();
			room.Initialize(roomId, position, dimension, color);

			roomBorders.BuildRoom(position, dimension);
		}
	}
}