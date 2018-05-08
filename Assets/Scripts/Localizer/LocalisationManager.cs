using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CLocalizationManager : MonoBehaviour {

    public static CLocalizationManager instance;


    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

    // Use this for initialization
    void Awake () {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
	}

	public void LoadLocalizedFile (string fileName){
		localizedText = new Dictionary<string,string> ();
		string filePath = Path.Combine (Application.streamingAssetsPath, fileName);
		if (File.Exists (filePath)) {
			string dataAsJson = File.ReadAllText (filePath);
			CLocalizationData loadedData = JsonUtility.FromJson<CLocalizationData> (dataAsJson);
			for (int i = 0; i < loadedData.items.Length; i++) {
				localizedText.Add (loadedData.items [i].key, loadedData.items [i].value);
			}
			Debug.Log ("Data loaded successfully" );
		} else {
			Debug.LogError ("Can't find localization file!");
		}

        isReady = true;
	}

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }
        return result;
    }

    public bool GetIsReady()
    {
        return isReady;
    }
}