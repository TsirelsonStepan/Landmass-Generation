using UnityEditor;
using UnityEngine;

[CustomEditor (typeof(GenTerrain))]
public class AutoUpdate : Editor
{
    public override void OnInspectorGUI()
    {
        GenTerrain gen = (GenTerrain)target;
        if (DrawDefaultInspector()) if (gen.autoUpdate) gen.Generate();

        if (GUILayout.Button("Generate")) gen.Generate();
    }
}