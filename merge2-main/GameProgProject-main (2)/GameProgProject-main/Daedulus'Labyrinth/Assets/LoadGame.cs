using System.Collections;
using UnityEngine;

namespace DunGen.DungeonCrawler
{
    public class LoadGame : MonoBehaviour
    {
        public GameObject playerPrefab; 
        private GameObject playerInstance;
        private Transform playerSpawn;
        private RuntimeDungeon dungeonGenerator;
        public float generationDelay = 2.0f; 

        void Start()
        {
            var playerSpawnObject = GameObject.FindGameObjectWithTag("PlayerSpawn");
            if (playerSpawnObject != null)
            {
                playerSpawn = playerSpawnObject.transform;
                playerInstance = GameObject.FindWithTag("Player"); 
                if (playerInstance == null)
                {
                    playerInstance = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);
                }
            }
            else
            {
                Debug.LogError("PlayerSpawn not found!");
            }

            GameObject dungeonGeneratorObject = GameObject.FindWithTag("DungeonGenerator");
            if (dungeonGeneratorObject != null)
            {
                dungeonGenerator = dungeonGeneratorObject.GetComponent<RuntimeDungeon>();
                if (dungeonGenerator == null)
                {
                    Debug.LogError("RuntimeDungeon component not found on DungeonGenerator object!");
                }
            }
            else
            {
                Debug.LogError("DungeonGenerator object not found!");
            }
        }

        public void TeleportPlayer()
        {
            if (playerSpawn != null && playerInstance != null)
            {
                playerInstance.transform.position = playerSpawn.position;
                playerInstance.transform.rotation = playerSpawn.rotation;
                Debug.Log("Teleported player to new spawn point: " + playerSpawn.position);
            }
            else
            {
                Debug.LogError("PlayerSpawn or playerInstance is null. Cannot teleport player.");
            }
        }

        public void RegenerateLevel()
        {
            if (dungeonGenerator != null)
            {
                StartCoroutine(RegenerateLevelCoroutine());
            }
            else
            {
                Debug.LogError("Dungeon Generator not found!");
            }
        }

        private IEnumerator RegenerateLevelCoroutine()
        {
            // Destroy the current dungeon
            var existingDungeonLayout = GameObject.Find("Dungeon Layout");
            if (existingDungeonLayout != null)
            {
                Destroy(existingDungeonLayout);
            }

            yield return new WaitForEndOfFrame();

            // Generate a new dungeon
            dungeonGenerator.Generate();

            yield return new WaitForSeconds(generationDelay);

            Debug.Log("Dungeon generated. Attempting to find new PlayerSpawn...");

            // After regenerating, move the player to the new PlayerSpawn location
            var playerSpawnObject = GameObject.FindGameObjectWithTag("PlayerSpawn");
            if (playerSpawnObject != null)
            {
                playerSpawn = playerSpawnObject.transform;
                Debug.Log("New PlayerSpawn found: " + playerSpawn.position);
                TeleportPlayer();
            }
            else
            {
                Debug.LogError("PlayerSpawn not found after regeneration!");
            }

            var endLevel = FindObjectOfType<EndLevel>();
            if (endLevel != null)
            {
                Debug.Log("Resetting EndLevel trigger.");
                StartCoroutine(endLevel.ResetTrigger());
            }
        }
    }
}
