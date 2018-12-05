using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour {

    [Header("Prefabs")]
    public GameObject cardPrefab;

    [Header("Timings")]
    public float cardTransitionTime = 0.5f;
    public float cardPreviewTime = 1f;

    public static GlobalSettings instance;

	// Use this for initialization
	void Awake () {
        instance = this;

	}
	
    
}
