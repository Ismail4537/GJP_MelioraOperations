using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "WavesSO", menuName = "WavesSO/WavesSONode")]
public class WavesSO : ScriptableObject
{
    public string waveName;
    
    [Header("Settings")]
    public float durationInSeconds;
    public float spawnInterval = 2f;
    [Header("Enemy Categories")]
    public List<string> categoriesToSpawn; 

    [Header("Boss Settings")]
    public bool isBossWave;
    public GameObject bossPrefab;
}
