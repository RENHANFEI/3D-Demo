﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MiniJSON;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class PlaneExplorationRedirect : MonoBehaviour {

	private string jsonFilePath;
	private int level;
	private int id;

	void Start () {

//		jsonFilePath = Path.Combine(Application.streamingAssetsPath,"Puzzles.json");
		jsonFilePath = Path.Combine(Application.streamingAssetsPath, Configurations.jsonFilename);
		id = DataUtil.GetCurrentRoomId();
		ParseJson(jsonFilePath, id);

		Debug.Log(id);
		Debug.Log(level);

		switch (level) {
		case 0:
			SceneManager.LoadScene ("Q0", LoadSceneMode.Single);
			break;
		case 1:
			SceneManager.LoadScene ("Q1", LoadSceneMode.Single);
			break;
		case 2:
			SceneManager.LoadScene("Q2", LoadSceneMode.Single);
			break;
		case 3:
			SceneManager.LoadScene("Q3", LoadSceneMode.Single);
			break;
		case 4:
			SceneManager.LoadScene("Q4", LoadSceneMode.Single);
			break;
		case 5:
			SceneManager.LoadScene("Q5", LoadSceneMode.Single);
			break;
		case 6:
			SceneManager.LoadScene("Q6", LoadSceneMode.Single);
			break;
		case 7:
			SceneManager.LoadScene("Q7", LoadSceneMode.Single);
			break;
		case 8:
			SceneManager.LoadScene("Q8", LoadSceneMode.Single);
			break;
		}
	}

	private void ParseJson(string jsonFilePath, int roomId) {

		string jsonString = File.ReadAllText(jsonFilePath);
		Dictionary<string, object> dict;
		dict = Json.Deserialize(jsonString) as Dictionary<string,object>;
		dict = (Dictionary<string, object>)dict[roomId.ToString()];

		level = System.Convert.ToInt32 (dict ["level"]);
	}
}
