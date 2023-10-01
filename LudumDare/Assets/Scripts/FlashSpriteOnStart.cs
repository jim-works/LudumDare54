using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class FlashSpriteOnSpawn : MonoBehaviour
{
    public float OnDuration = 0.2f;
    public float OffDuration = 0.8f;
    public float FlashCount = 2;
    public Sprite OnSprite;
    public Sprite OffSprite;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Flash());
    }

    private IEnumerator Flash()
    {
        Image img = GetComponent<Image>();
        for (int i = 0; i < FlashCount; i++)
        {
            img.sprite = OnSprite;
            yield return new WaitForSeconds(OnDuration);
            img.sprite = OffSprite;
            yield return new WaitForSeconds(OffDuration);
        }
    }
}
