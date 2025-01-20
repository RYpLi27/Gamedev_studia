using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextureOffsetSlider : MonoBehaviour
{
    public Material material; // Materia� z tekstur�
    public Slider sliderX;    // Suwak do przesuwania w osi X
    public Slider sliderY;    // Suwak do przesuwania w osi Y (opcjonalnie)

    void Start()
    {
        // Subskrybuj zmian� warto�ci suwak�w
        if (sliderX != null)
            sliderX.onValueChanged.AddListener(UpdateTextureOffsetX);
        if (sliderY != null)
            sliderY.onValueChanged.AddListener(UpdateTextureOffsetY);
    }

    void UpdateTextureOffsetX(float value)
    {
        // Pobierz aktualne przesuni�cie tekstury
        Vector2 offset = material.mainTextureOffset;
        offset.x = value; // Zaktualizuj warto�� X
        material.mainTextureOffset = offset;
    }

    void UpdateTextureOffsetY(float value)
    {
        // Pobierz aktualne przesuni�cie tekstury
        Vector2 offset = material.mainTextureOffset;
        offset.y = value; // Zaktualizuj warto�� Y
        material.mainTextureOffset = offset;
    }
}
