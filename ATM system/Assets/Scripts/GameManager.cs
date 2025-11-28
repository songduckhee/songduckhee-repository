using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;
	public List<UserData> userData;
	public UIView uIView;
	public Login login;
	public Transfer transfer;
	public UserData accessUserData;



	// Start is called before the first frame update

	private void Awake()
	{
		Instance = this;
		userData = new List<UserData>();
		LoadUserData();
		if (userData.Count == 0)
		{
			UserData user = new UserData("rtx123", "12345", "김채경", 85000, 115000);
			userData.Add(user);
			SaveUserData();
		}

	}
	private void OnApplicationQuit()
	{
		SaveUserData();
	}

	public void SaveUserData()
	{
		if (Directory.Exists(Application.persistentDataPath + "/Data"))
		{
			Debug.Log("있음!");
		}
		else
		{
			string folder = Application.persistentDataPath + "/Data";
			System.IO.Directory.CreateDirectory(folder);
			Debug.Log("없음!");
		}
		string json = JsonConvert.SerializeObject(userData, Formatting.Indented);
		string path = Path.Combine(Application.persistentDataPath + "/Data/" + "userData.json");

		File.WriteAllText(path, json);
		Debug.Log(json);

		Debug.Log("저장완료!");
	}

	public void LoadUserData()
	{
		string path = Path.Combine(Application.persistentDataPath + "/Data/" + "userData.json");
		Debug.Log(path);
		string ReadFileJson;
		ReadFileJson = File.ReadAllText(path);
		Debug.Log(ReadFileJson);
		userData = JsonConvert.DeserializeObject<List<UserData>>(ReadFileJson);
		Debug.Log(userData);

	}

}
