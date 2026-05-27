using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        string character = PlayerPrefs.GetString("SelectedCharacter", "Walter");
        int level = character == "Walter" ?
            PlayerPrefs.GetInt("WalterLevel", 1) :
            PlayerPrefs.GetInt("JesseLevel", 1);

        moveSpeed *= 1f + (level - 1) * 0.05f;

        // Aplikuj ingame upgrady z GameManageru
        if (GameManager.Instance != null)
            moveSpeed *= GameManager.Instance.moveSpeedMultiplier;
    }

    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(x, y).normalized;
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
}