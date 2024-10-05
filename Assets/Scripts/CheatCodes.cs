using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManager namespace

public class CheatCodes : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    void Start() { }

    // Update is called once per frame
    void Update()
    {
        // Disable player collision when "C" is pressed
        if (Input.GetKeyDown(KeyCode.C))
        {
            player.GetComponent<Collider>().enabled = false;
            CollisionHandler collisionHandler = player.GetComponent<CollisionHandler>();
            if (collisionHandler != null)
            {
                collisionHandler.isCollisionHandlingEnabled = false;
            }
            Debug.Log("Player collision disabled");
        }
        else if (Input.GetKeyDown(KeyCode.V)) // Example key to re-enable collision handling
        {
            player.GetComponent<Collider>().enabled = true;
            CollisionHandler collisionHandler = player.GetComponent<CollisionHandler>();
            if (collisionHandler != null)
            {
                collisionHandler.isCollisionHandlingEnabled = true;
            }
            Debug.Log("Player collision enabled");
        }
        // Load next level when "L" is pressed
        if (Input.GetKeyDown(KeyCode.L))
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            int nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
            {
                SceneManager.LoadScene(nextSceneIndex);
                Debug.Log("Loading next level...");
            }
            else
            {
                Debug.Log("No next level available");
            }
        }
    }
}
