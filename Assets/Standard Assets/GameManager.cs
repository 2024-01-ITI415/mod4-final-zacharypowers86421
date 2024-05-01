using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text messageText;
    public Text scoreText;
    public Transform player; // Reference to the player's transform
    public float displayDuration = 2f; // Duration to display the message in seconds

    private int pickupsCollected = 0;
    private int totalPickups = 7;
    private bool displayingMessage = false;
    private float displayTimer = 0f;

    void Start()
    {
        // Display initial message
        DisplayMessage("Make your way to the factory");
        // Initialize score text
        scoreText.text = "Pickups Collected: " + pickupsCollected + " / " + totalPickups;
    }

    void Update()
    {
        // Follow player's movement
        Vector3 screenPos = Camera.main.WorldToScreenPoint(player.position);
        messageText.rectTransform.position = screenPos;

        // Update display timer if a message is currently being displayed
        if (displayingMessage)
        {
            displayTimer += Time.deltaTime;
            if (displayTimer >= displayDuration)
            {
                // Time's up, hide the message
                HideMessage();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            // Increment pickups collected
            Debug.Log("hello");
            pickupsCollected++;
            // Update score text
            scoreText.text = "Pickups Collected: " + pickupsCollected + " / " + totalPickups;

            if (pickupsCollected == totalPickups)
            {
                // All pickups collected
                DisplayMessage("The factory will be destroyed");
                // Optional: Add code to trigger destruction of factory
            }

            // Disable collected pickup
            other.gameObject.SetActive(false);
        }
    }

    void DisplayMessage(string message)
    {
        // Display the message in the middle of the screen
        messageText.text = message;
        messageText.enabled = true;
        displayingMessage = true;
        displayTimer = 0f;
    }

    void HideMessage()
    {
        // Hide the message
        messageText.enabled = false;
        displayingMessage = false;
    }
}
