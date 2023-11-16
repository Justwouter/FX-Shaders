using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SimpleNoiseGenerator : MonoBehaviour {
    [Header("Dimensions")]
    [SerializeField][Min(1)] private int width = 200;
    [SerializeField][Min(1)] private int height = 200;
    [SerializeField][Range(1, 50)] private float scale = 20f; // Bigger scale means bigger coords, Higher number is zooming-out.

    // Offset on x-axis & y-axis to create a more random result.
    // No offset = same result each time.
    [Header("Noise parameters")]
    [SerializeField] private bool usePerlin = false;
    [SerializeField] private bool randomizeOffset = false;
    [SerializeField] private float offsetX = 100f;
    [SerializeField] private float offsetY = 100f;

    [Header("Animate")]
    [SerializeField] private bool animate = false;
    [SerializeField] private float speed = 1f;

    [Header("Direction")]
    [Range(-1, 1)] public float X = 0;
    [Range(-1, 1)] public float Y = 0;

    // Mesh Renderer component in Plane GameObject.
    private Renderer _renderer;

    private void Start() {
        _renderer = GetComponent<Renderer>();

        if (randomizeOffset) {
            offsetX = Random.Range(0, 10000);
            offsetY = Random.Range(0, 10000);
        }
    }

    private void Update() {
        // Generate something using Noise.
        Texture2D texture2D = usePerlin ? Generate() : RandomGenerate();
        _renderer.sharedMaterial.mainTexture = texture2D;
        _renderer.transform.localScale = new Vector3(texture2D.width, 1, texture2D.height);


        // Make it move.
        if (animate) {
            offsetX += X * speed * Time.deltaTime;
            offsetY += Y * speed * Time.deltaTime;
        }
    }

    Texture2D Generate() {
        Texture2D texture2D = new Texture2D(width, height);

        // MAKE SOME NOISE!
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                texture2D.SetPixel(x, y, Pixel(x, y));
            }
        }

        // Don't forget.
        texture2D.Apply();

        return texture2D;
    }

    Color Pixel(int x, int y) {
        float xCoord = (float)x / width * scale;
        float yCoord = (float)y / height * scale;

        // Offset makes the coordinates random, instead of always yielding the same result.
        xCoord += offsetX;
        yCoord += offsetY;

        // The closer the value to 0.5, 'intenser' the noise. (black, compared to grey - white).
        float pixel = Mathf.PerlinNoise(xCoord, yCoord);

        return new Color(pixel, pixel, pixel);
    }

    Texture2D RandomGenerate() {
        Texture2D texture2D = new Texture2D(width, height);

        int random = 0;

        // MAKE SOME NOISE!
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                random = Random.Range(0, 99999);

                texture2D.SetPixel(x, y, random % 2 == 0 ? Color.black : Color.white);
            }
        }
        // Don't forget.
        texture2D.Apply();
        return texture2D;
    }
}