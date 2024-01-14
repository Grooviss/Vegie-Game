using UnityEngine;
using UnityEngine.UI;

public class VegetableCombineGame : MonoBehaviour
{
    public GameObject[] vegetables;
    public Text nextVegetableText;
    public Text scoreText;
    private int[] vegetableScores = { 10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110 };

    private GameObject currentVegetable;
    private GameObject nextVegetable;
    private int score;

    private float moveSpeed = 5f;
    private float borderX = 5f;
    private bool canMove = true;

    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        if (canMove)
        {
            MoveVegetable();

            // Check if the left mouse button is clicked
            if (Input.GetMouseButtonDown(0))
            {
                // Drop the vegetable when left mouse button is clicked
                DropVegetable();
            }
        }
    }

    void InitializeGame()
    {
        SpawnVegetable();
        SetNextVegetable();
        score = 0;
        UpdateScoreUI();
        canMove = true;
    }

    void SpawnVegetable()
    {
        currentVegetable = Instantiate(vegetables[Random.Range(0, 5)], new Vector2(0f, 8f), Quaternion.identity);

        Rigidbody2D rigidbody2D = currentVegetable.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            rigidbody2D.gravityScale = 0f;
        }
    }

    void SetNextVegetable()
    {
        nextVegetable = Instantiate(vegetables[Random.Range(0, 5)], new Vector2(0f, 8f), Quaternion.identity);
        nextVegetable.SetActive(false); // Initially, set it inactive
        nextVegetableText.text = "Next Vegetable: " + nextVegetable.name;
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score.ToString();
    }

    void MoveVegetable()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 currentPosition = currentVegetable.transform.position;
        currentPosition.x += horizontalInput * moveSpeed * Time.deltaTime;

        currentPosition.x = Mathf.Clamp(currentPosition.x, -borderX, borderX);

        currentVegetable.transform.position = currentPosition;
    }

    void DropVegetable()
    {
        canMove = false;

        Rigidbody2D rigidbody2D = currentVegetable.GetComponent<Rigidbody2D>();
        if (rigidbody2D != null)
        {
            rigidbody2D.gravityScale = 1f;
        }

        // Spawn the next vegetable after the current one touches the ground
        Invoke("SpawnNextVegetable", 1f);
    }

    void SpawnNextVegetable()
    {
        nextVegetable.SetActive(true);
        canMove = true;
    }

    public void CombineVegetables(GameObject droppedVegetable)
    {
        Collider2D[] nearbyVegetables = Physics2D.OverlapCircleAll(droppedVegetable.transform.position, 0.5f);

        foreach (Collider2D nearbyVegetable in nearbyVegetables)
        {
            if (nearbyVegetable.gameObject != droppedVegetable && nearbyVegetable.CompareTag(droppedVegetable.tag))
            {
                Destroy(nearbyVegetable.gameObject);
                Destroy(droppedVegetable);

                int combinedVegetableIndex = System.Array.IndexOf(vegetables, nearbyVegetable.gameObject) + 1;
                if (combinedVegetableIndex < vegetables.Length)
                {
                    score += vegetableScores[combinedVegetableIndex];
                    UpdateScoreUI();
                }

                SetNextVegetable();
                nextVegetable.SetActive(false); // Set it inactive again until the current one touches the ground
                return;
            }
        }

        canMove = true;
    }
}


