using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] public float PreTime;
    [SerializeField] public float Falltime = 0.8f;
    public static int height = 20;
    public static int width = 10;
    public Vector3 rotationPoint;
    public static Transform[,] grid = new Transform[width, height];
    private GameManager GameManager;

    void Start()
    {
        GameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!IsValidMove())
                transform.position -= new Vector3(-1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!IsValidMove())
                transform.position -= new Vector3(1, 0, 0);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            if (!IsValidMove())
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }

        if (Time.time - PreTime > (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S) ? Falltime / 15 : Falltime))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!IsValidMove())
            {
                transform.position -= new Vector3(0, -1, 0);
                AddtoGrid();
                CheckForScore();
                if (IsGameOver())
                {
                    GameManager.EndGame();
                }
                else
                {
                    this.enabled = false;
                    GameManager.GetRandom();
                }
            }
            PreTime = Time.time;
        }
        SetSpeed();
    }

    void AddtoGrid()
    {
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt(child.transform.position.x);
            int roundedY = Mathf.RoundToInt(child.transform.position.y);

            if (roundedY >= height - 1)
            {
                GameManager.EndGame();
                return;
            }

            grid[roundedX, roundedY] = child;

        }
    }

    void CheckForScore()
    {
        for (int i = height - 1; i >= 0; i--)
        {
            if (YesLine(i))
            {
                RemoveLine(i);
                RowMove(i);
            }
        }
    }

    bool YesLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            if (grid[j, i] == null)
                return false;
        }
        return true;
    }

    void RemoveLine(int i)
    {
        for (int j = 0; j < width; j++)
        {
            Destroy(grid[j, i].gameObject);
            grid[j, i] = null;   
        }
        FindObjectOfType<GameManager>().AddScore(10);
    }

    void RowMove(int i)
    {
        for (int y = i; y < height; y++)
        {
            for (int j = 0; j < width; j++)
            {
                if (grid[j, y] != null)
                {
                    grid[j, y - 1] = grid[j, y];
                    grid[j, y] = null;
                    grid[j, y - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }

    bool IsValidMove()
    {
        foreach (Transform child in transform)
        {
            int roundedX = Mathf.RoundToInt(child.transform.position.x);
            int roundedY = Mathf.RoundToInt(child.transform.position.y);

            if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= height)
                return false;

            if (grid[roundedX, roundedY] != null)
                return false;
        }
        return true;
    }
    void SetSpeed()
    {
        if (GameManager.score >= 50 && GameManager.score < 100)
        {
            Falltime = 0.7f;
        }
        else if (GameManager.score >= 100 && GameManager.score < 150)
        {
            Falltime = 0.6f;
        }
        else if (GameManager.score >= 150 && GameManager.score < 200)
        {
            Falltime = 0.5f;
        }
        else if (GameManager.score >= 200 && GameManager.score < 250)
        {
            Falltime = 0.4f;
        }
        else if (GameManager.score >= 250 && GameManager.score < 300)
        {
            Falltime = 0.3f;
        }
        else if (GameManager.score >= 300)
        {
            Falltime = 0.2f;
        }
    }
    bool IsGameOver()
    {
        foreach (Transform child in transform)
        {
            if (child.transform.position.y >= height - 1)
            {
                return true;
            }
        }
        return false;
    }
}