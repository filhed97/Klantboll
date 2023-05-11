using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JanneSpawnPowerups : MonoBehaviour {

    [SerializeField] public GameObject superKick; // The powerup prefab to spawn
    [SerializeField] public GameObject superSpeed; // The powerup prefab to spawn
    [SerializeField] public GameObject speedDebuff; // The powerup prefab to spawn
    [SerializeField] public GameObject iceDebuff; // The powerup prefab to spawn
    //[SerializeField] public GameObject superStick; // The powerup prefab to spawn
    [SerializeField] public GameObject plane; // Spawn area
    //[SerializeField] public GameObject shieldBuff; // shield power-ups

    public float spawnDelay = 1f;
    public float spawnHeight = 1f; 
    private float spawnTimer = 0f; 
    private List<Vector3> spawnPositions = new List<Vector3>(); 
    private GameObject[] powerupsArray = new GameObject[4];
    public static int maxPowerups = 5;

    void Start() {
        MeshFilter meshFilter = plane.GetComponent<MeshFilter>();
        Mesh mesh = meshFilter.mesh;

        Vector3[] vertices = mesh.vertices;

        for (int i = 0; i < vertices.Length; i++) {
            Vector3 worldPosition = meshFilter.transform.TransformPoint(vertices[i]);
            spawnPositions.Add(worldPosition);
        }

        powerupsArray = new GameObject[] {superKick, speedDebuff, iceDebuff, superSpeed};
    }

    void Update() {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay && JannePowerup2.numOfPowerups < maxPowerups) {

            spawnTimer = 0f;

            // Choose a random spawn position from the list
            Vector3 spawnPos = spawnPositions[Random.Range(0, spawnPositions.Count)];

            int randomIndex = Random.Range(0, powerupsArray.Length);

            // Spawn the powerup prefab at the random position
            Instantiate(powerupsArray[randomIndex], spawnPos + Vector3.up * spawnHeight, Quaternion.identity);
            
            JannePowerup2.numOfPowerups++;
        }
    }      
}