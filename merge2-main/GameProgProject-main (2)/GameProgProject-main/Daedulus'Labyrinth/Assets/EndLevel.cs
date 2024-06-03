using System.Collections;
using UnityEngine;

namespace DunGen.DungeonCrawler
{
    public class EndLevel : MonoBehaviour
    {
        private LoadGame loadGame;
        private bool isCooldown;

        void Start()
        {
            loadGame = GameObject.FindObjectOfType<LoadGame>();
            if (loadGame == null)
            {
                Debug.LogError("LoadGame component not found in the scene.");
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && !isCooldown)
            {
                Debug.Log("Player entered EndLevel trigger, starting regeneration.");
                if (loadGame != null)
                {
                    StartCoroutine(RegenerateLevelAfterFrame());
                }
            }
        }

        private IEnumerator RegenerateLevelAfterFrame()
        {
            yield return new WaitForEndOfFrame();
            loadGame.RegenerateLevel();
            StartCoroutine(CooldownCoroutine());
        }

        private IEnumerator CooldownCoroutine()
        {
            isCooldown = true;
            yield return new WaitForSeconds(1f); 
            isCooldown = false;
            StartCoroutine(ResetTrigger());
        }

        public IEnumerator ResetTrigger()
        {
            var boxCollider = GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                Debug.Log("Disabling EndLevel trigger.");
                boxCollider.enabled = false; 
                yield return new WaitForEndOfFrame();
                Debug.Log("Enabling EndLevel trigger.");
                boxCollider.enabled = true;
            }
        }
    }
}
