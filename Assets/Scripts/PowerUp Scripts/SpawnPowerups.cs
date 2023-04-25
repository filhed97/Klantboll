using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPowerups : MonoBehaviour {

    [SerializeField] public GameObject superKick; // The powerup prefab to spawn
    [SerializeField] public GameObject superSpeed; // The powerup prefab to spawn
    [SerializeField] public GameObject superSpike; // The powerup prefab to spawn
    [SerializeField] public GameObject iceDebuff; // The powerup prefab to spawn


    [SerializeField] public GameObject plane; // Spawn area

    public float spawnDelay = 1f;
    public float spawnHeight = 1f; // The height at which to spawn the powerup
    private float spawnTimer = 0f; // The timer for the spawn delay
    private Bounds planeBounds;

    private GameObject[] powerupsArray = new GameObject[4];

    void Start() {
        planeBounds = plane.GetComponent<Renderer>().bounds;

        powerupsArray = new GameObject[] {superKick, superSpeed, superSpike, iceDebuff};
    }

    void Update() {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnDelay) {

            spawnTimer = 0f;

            // Calculate a random position on the plane
            Vector3 spawnPos = new Vector3(Random.Range(planeBounds.min.x + 10, planeBounds.max.x - 10), spawnHeight, Random.Range(planeBounds.min.z + 2, planeBounds.max.z - 2));

            int randomIndex = Random.Range(0, powerupsArray.Length);

            // Spawn the powerup prefab at the random position
            Instantiate(powerupsArray[randomIndex], spawnPos, Quaternion.identity);
            
        }
    }      
}

