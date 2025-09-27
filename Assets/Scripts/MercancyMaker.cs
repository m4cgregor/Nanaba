using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MercancyMaker 

{
    [MenuItem("Assets/Create/Mercancy")]
    public static void CreateMercancy() {

        MercancySO asset = ScriptableObject.CreateInstance<MercancySO>();
        AssetDatabase.CreateAsset(asset, "Assets/Mercancies/NewMercany.asset");
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = asset;
    }
}
