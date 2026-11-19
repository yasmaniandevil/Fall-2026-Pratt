using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class KaiInput : MonoBehaviour
{
    public Sprite[] hairSprites;
    public StatMeter hairMeter;
    private SpriteRenderer spriteRenderer;
    
    public GameObject sprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = sprite.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = hairSprites[0];
        hairMeter.slider.value = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float progress = hairMeter.slider.value / hairMeter.slider.maxValue;
        int sliderInt = Mathf.RoundToInt(progress * (hairSprites.Length - 1));
        switch (sliderInt)
        {
            
            case 0:
                spriteRenderer.sprite = hairSprites[1];
                break;
            case 1:
                spriteRenderer.sprite = hairSprites[2];
                break;
            case 2:
                spriteRenderer.sprite = hairSprites[3];
                break;
            case 3:
                spriteRenderer.sprite = hairSprites[4];
                break;
            default:
                spriteRenderer.sprite = hairSprites[0];
                break;
                
        }
        Debug.Log(sliderInt);
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            hairMeter.ChangeStat(10);
            Debug.Log("Clicked");
        }
    }
}
