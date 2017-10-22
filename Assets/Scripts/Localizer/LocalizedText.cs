using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CLocalizedText : MonoBehaviour {

    public string key;

    // Use this for initialization
    void Start()
    {
        Text text = GetComponent<Text>();
        text.text = CLocalizationManager.instance.GetLocalizedValue(key);
    }

}
