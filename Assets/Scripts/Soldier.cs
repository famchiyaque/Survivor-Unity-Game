using UnityEngine;

public class Soldier : MonoBehaviour
{
    public GameObject soldier1Prefab;
    public GameObject soldier2Prefab;
    public GameObject soldier3Prefab;

    private GameObject currentSoldier;

    public GameObject gameOverUI;

    public float speed = 300.0f;
    public float horizontalInput;
    public float screenLimitLeft;
    public float screenLimitRight;

    private int soldierLevel = 1;

    public static int playerHealth = 100;

    void Start()
    {
        currentSoldier = Instantiate(soldier1Prefab, transform.position, Quaternion.identity, transform);

        float z = Mathf.Abs(Camera.main.transform.position.z); // z-distance from camera
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, z));
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, z));

        screenLimitLeft = leftEdge.x;
        screenLimitRight = rightEdge.x;
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float move = horizontalInput * speed * Time.deltaTime;

        currentSoldier.transform.Translate(Vector3.right * move);

        // Wrap around if off-screen
        Vector3 pos = currentSoldier.transform.position;

        if (pos.x < screenLimitLeft)
        {
            pos.x = screenLimitRight;
        }
        else if (pos.x > screenLimitRight)
        {
            pos.x = screenLimitLeft;
        }

        currentSoldier.transform.position = pos;

        Debug.Log("Type of Score.playerScore: " + Score.playerScore.GetType());

        Debug.Log("current player score: ");
        Debug.Log(Score.playerScore);

        if (Score.playerScore > 2000 && soldierLevel == 1) {
            Debug.Log("was in tupdate to level 2");
            SwapSoldier(2);
        }

        if (Score.playerScore > 3000 && soldierLevel == 2) {
            Debug.Log("was in tupdate to level 3");
            SwapSoldier(3);
        }
    }

    public void SwapSoldier(int level)
    {
        GameObject newPrefab = null;

        if (level == 2) {
            newPrefab = soldier2Prefab;
            soldierLevel = 2;
        }
        else if (level == 3) {
            newPrefab = soldier3Prefab;
            soldierLevel = 3;
        }

        if (newPrefab != null)
        {
            Vector3 pos = currentSoldier.transform.position;
            Destroy(currentSoldier);
            currentSoldier = Instantiate(newPrefab, pos, Quaternion.identity, transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Fireball fireball = other.GetComponent<Fireball>();

        if (fireball != null)
        {
            playerHealth -= fireball.damage;

            if (playerHealth <= 0)
            {
                gameOverUI.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }
}