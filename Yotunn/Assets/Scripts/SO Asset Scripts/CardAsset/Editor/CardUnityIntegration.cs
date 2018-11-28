using UnityEngine;
using UnityEditor;

static class CardUnityIntegration 
{

	[MenuItem("Assets/Create/CarteRessource")]
	public static void CreateYourScriptableObject() {
		ScriptableObjectUtility2.CreateAsset<CarteRessource>();
	}

}
