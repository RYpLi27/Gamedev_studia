using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureOffsetSlider : MonoBehaviour
{
    public Material material; // Materia³ z tekstur¹
    public Slider sliderX;    // Suwak do przesuwania w osi X
    public Slider sliderY;    // Suwak do przesuwania w osi Y (opcjonalnie)

    void Start()
    {
        // Subskrybuj zmianê wartoœci suwaków
        if (sliderX != null)
            sliderX.onValueChanged.AddListener(UpdateTextureOffsetX);
        if (sliderY != null)
            sliderY.onValueChanged.AddListener(UpdateTextureOffsetY);
    }

    void UpdateTextureOffsetX(float value)
    {
        // Pobierz aktualne przesuniêcie tekstury
        Vector2 offset = material.mainTextureOffset;
        offset.x = value; // Zaktualizuj wartoœæ X
        material.mainTextureOffset = offset;
    }

    void UpdateTextureOffsetY(float value)
    {
        // Pobierz aktualne przesuniêcie tekstury
        Vector2 offset = material.mainTextureOffset;
        offset.y = value; // Zaktualizuj wartoœæ Y
        material.mainTextureOffset = offset;
    }
}
