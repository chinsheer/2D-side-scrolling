using UnityEngine;
using UnityEngine.UI;

public class HearthUI : MonoBehaviour
{
    [SerializeField] Sprite _fullHearthSprite; // Sprite for a full hearth
    [SerializeField] Sprite _emptyHearthSprite; // Sprite for an empty hearth

    public void SetHearth(bool isFull)
    {
        GetComponent<Image>().sprite = isFull ? _fullHearthSprite : _emptyHearthSprite; // Set the sprite based on whether the hearth is full or empty
    }
}