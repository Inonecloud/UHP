using System;


[System.Serializable]
public class CLocalizationData
{
	public CLocalizationItem[] items;
}
[System.Serializable]
public class CLocalizationItem{
	public string key;
	public string value;
}

