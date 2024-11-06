using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiManager : MonoBehaviour
{
    [Header("Camera Scroll")]
    public float camMinSize;
    public float camMaxSize;
    public float scrollScale;

    [Header("References")]
    public Camera gameCam;
    public GameObject emojiPrefab;

    [Header("Emoji Sprites")]
    public Sprite blossom;
    public Sprite bright;
    public Sprite cactus;
    public Sprite cloud;
    public Sprite cow;
    public Sprite deciduous;
    public Sprite evergreen;
    public Sprite frog;
    public Sprite full;
    public Sprite globe;
    public Sprite glowing;

    //Our current position on the grid
    private Vector2Int gridPosition;
    //A history of what grid positions already have an emoji
    private Dictionary<Vector2Int, int> _history = new Dictionary<Vector2Int, int>();

    private Vector2 mousePos = Vector2.zero;
    //where the mouse was last frame
    private Vector2 mousePosDragStart;
    
    private void Update()
    {
        UpdateCameraZoom();

        //Save our mouse position clamped to the grid
        mousePos = gameCam.ScreenToWorldPoint(Input.mousePosition);
        Vector2Int mousePosInt = new Vector2Int(Mathf.RoundToInt(mousePos.x), Mathf.RoundToInt(mousePos.y));

        UpdateClickAndDrag();

        //If we're in a new position, spawn a new emoji and save it to the _history
        if (mousePosInt != gridPosition)
        {
            gridPosition = mousePosInt;
            if (!_history.ContainsKey(gridPosition))
            {
                _history.Add(gridPosition, 1);
                SpawnEmoji(gridPosition.x, gridPosition.y);
            }
        }
    }

    private void SpawnEmoji(int x, int y)
    {
        GameObject temp = Instantiate(emojiPrefab, new Vector3(x, y, 0), Quaternion.identity);

        float randy = Mathf.PerlinNoise(x / 10f, y / 10f);
        if(randy < 0.66f)
        {
            float gaussy = RandomGaussian(0f, 1f);
            if (gaussy < 0.1f)
                temp.GetComponent<SpriteRenderer>().sprite = frog;
            else if (gaussy < 0.5f)
                temp.GetComponent<SpriteRenderer>().sprite = evergreen;
            else if(gaussy < 0.5f)
                temp.GetComponent<SpriteRenderer>().sprite = evergreen;

        }
    }

    //gotten from https://answers.unity.com/questions/421968/normal-distribution-random.html
    private float RandomGaussian(float minValue = 0.0f, float maxValue = 1.0f)
    {
        float u, v, S;

        do
        {
            u = 2.0f * UnityEngine.Random.value - 1.0f;
            v = 2.0f * UnityEngine.Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0f);

        // Standard Normal Distribution
        float std = u * Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);

        // Normal Distribution centered between the min and max value
        // and clamped following the "three-sigma rule"
        float mean = (minValue + maxValue) / 2.0f;
        float sigma = (maxValue - mean) / 3.0f;
        return Mathf.Clamp(std * sigma + mean, minValue, maxValue);
    }

    private void UpdateCameraZoom()
    {
        gameCam.orthographicSize -= (Input.mouseScrollDelta.y * scrollScale);

        gameCam.orthographicSize = Mathf.Clamp(gameCam.orthographicSize, camMinSize, camMaxSize);
    }

    private void UpdateClickAndDrag()
    {
        if (Input.GetMouseButtonDown(0))
            mousePosDragStart = mousePos;

        if (Input.GetMouseButton(0))
        {
            Vector3 dragVec = mousePosDragStart - mousePos;
            gameCam.transform.position += dragVec;
        }
    }
}
