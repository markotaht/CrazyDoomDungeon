using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Spawner))]
public class SpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Spawner spawner = (Spawner)target;

        spawner.item = (GameObject)EditorGUILayout.ObjectField("Item", spawner.item, typeof(GameObject));
        spawner.maxAmount = EditorGUILayout.IntField("Max amount", spawner.maxAmount);
        spawner.spawnOnce = EditorGUILayout.Toggle("Spawn once", true);
        spawner.spawnTimer = EditorGUILayout.FloatField("Spawn timer", spawner.spawnTimer);
    }
}
