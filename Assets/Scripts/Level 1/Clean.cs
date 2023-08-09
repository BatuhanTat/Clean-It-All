using System;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Clean : MonoBehaviour
{
    [Header("Cleaning")]
    [SerializeField] private Camera _camera;
    [SerializeField] private Texture2D _dirtMaskBase;
    [SerializeField] private Texture2D _brush;
    [SerializeField] private float brushSizeMultiplier = 2;
    [SerializeField] private Material _material;
    [Space(2)]
    [SerializeField] private float dirtThreshold = 60;

    [Space(2)]
    [SerializeField] private TextMeshProUGUI dirtText;

    private Texture2D _templateDirtMask;
    private float dirtAmountTotal;
    private float dirtAmount;
    private int remeaningDirt;
    private Vector2Int lastPaintPixelPosition;
    private DirtFading dirtFading;

    Vector2 inputPosition;

    private void Start()
    {
        CreateTexture();
        SetBrushSize();

        dirtFading = GetComponent<DirtFading>();
        // Get the Renderer component attached to the GameObject
        Renderer renderer = GetComponent<Renderer>();

        dirtAmountTotal = 0f;
        for (int x = 0; x < _templateDirtMask.width; x++)
        {
            for (int y = 0; y < _templateDirtMask.height; y++)
            {
                dirtAmountTotal += _templateDirtMask.GetPixel(x, y).g;
            }
        }
        dirtAmount = dirtAmountTotal;
    }

    private void SetBrushSize()
    {
        // Increase the size of the brush texture
        int newWidth = (int)(_brush.width * brushSizeMultiplier);
        int newHeight = (int)(_brush.height * brushSizeMultiplier);
        Texture2D resizedBrush = new Texture2D(newWidth, newHeight);
        Color[] resizedPixels = resizedBrush.GetPixels();

        for (int x = 0; x < newWidth; x++)
        {
            for (int y = 0; y < newHeight; y++)
            {
                // Calculate the corresponding pixel coordinates in the original brush texture
                int originalX = (int)(x / brushSizeMultiplier);
                int originalY = (int)(y / brushSizeMultiplier);

                // Copy the pixel color from the original brush texture to the resized brush texture
                Color originalPixel = _brush.GetPixel(originalX, originalY);
                resizedPixels[y * newWidth + x] = originalPixel;
            }
        }

        // Set the pixels of the resized brush texture
        resizedBrush.SetPixels(resizedPixels);
        resizedBrush.Apply();

        // Assign the resized brush texture back to the _brush variable
        _brush = resizedBrush;
    }

    private void Update()
    {

        if (!GameManager.instance.inMenu)
        {
            Debug.Log("Inside ");
            Cleaning();
            remeaningDirt = Mathf.RoundToInt(GetDirtAmount() * 100f);
            dirtText.text = remeaningDirt + "%";

            if (remeaningDirt <= dirtThreshold)
            {
                Debug.Log("Dirst mask should be fading");
                // Call the StartLerping function
                if (dirtFading != null)
                {
                    dirtFading.StartLerping();
                    // Win Condition
                    Debug.Log("Level Completed");
                }
            }
        }
    }

    private void Cleaning()
    {
        // Perform a raycast from the mouse/touch position
        Ray ray = _camera.ScreenPointToRay(inputPosition);

        bool isHit = Physics.Raycast(ray, out RaycastHit raycastHit);
        if (isHit)
        {
            Vector2 textureCoord = raycastHit.textureCoord;

            int pixelX = (int)(textureCoord.x * _templateDirtMask.width);
            int pixelY = (int)(textureCoord.y * _templateDirtMask.height);

            Vector2Int paintPixelPosition = new Vector2Int(pixelX, pixelY);
            //Debug.Log("UV: " + textureCoord + "; Pixels: " + paintPixelPosition);

            int paintPixelDistance = Mathf.Abs(paintPixelPosition.x - lastPaintPixelPosition.x) + Mathf.Abs(paintPixelPosition.y - lastPaintPixelPosition.y);
            int maxPaintDistance = 20;
            if (paintPixelDistance < maxPaintDistance)
            {
                // Painting too close to last position
                return;
            }
            lastPaintPixelPosition = paintPixelPosition;

            /* 
             // Paint Square in Dirt Mask
             int squareSize = 32;
             int pixelXOffset = pixelX - (dirtBrush.width / 2);
             int pixelYOffset = pixelY - (dirtBrush.height / 2);

             for (int x = 0; x < squareSize; x++) {
                 for (int y = 0; y < squareSize; y++) {
                     dirtMaskTexture.SetPixel(
                         pixelXOffset + x,
                         pixelYOffset + y,
                         Color.black
                     );
                 }
             } */

            int pixelXOffset = pixelX - (_brush.width / 2);
            int pixelYOffset = pixelY - (_brush.height / 2);

            for (int x = 0; x < _brush.width; x++)
            {
                for (int y = 0; y < _brush.height; y++)
                {
                    Color pixelDirt = _brush.GetPixel(x, y);
                    Color pixelDirtMask = _templateDirtMask.GetPixel(pixelXOffset + x, pixelYOffset + y);

                    float removedAmount = pixelDirtMask.g - (pixelDirtMask.g * pixelDirt.g);
                    dirtAmount -= removedAmount;

                    _templateDirtMask.SetPixel(
                        pixelXOffset + x,
                        pixelYOffset + y,
                        new Color(0, pixelDirtMask.g * pixelDirt.g, 0)
                    );
                }
            }
            _templateDirtMask.Apply();
        }
    }

    private void OnSelect(InputValue value)
    {
        inputPosition = value.Get<Vector2>();
        //Debug.Log("Onselect : " + inputPosition);
    }

    private void CreateTexture()
    {
        _templateDirtMask = new Texture2D(_dirtMaskBase.width, _dirtMaskBase.height);
        _templateDirtMask.SetPixels(_dirtMaskBase.GetPixels());
        _templateDirtMask.Apply();
        _material.SetTexture("_DirtMask", _templateDirtMask);
    }

    private float GetDirtAmount()
    {
        return this.dirtAmount / dirtAmountTotal;
    }
}
