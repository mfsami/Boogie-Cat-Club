using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
public class Player : MonoBehaviour
{
    
    public List<Sprite> sprites = new List<Sprite>();
    public SpriteRenderer spriteRenderer;

    public float bounceHeight = 0.15f;
    public float bounceDur = 0.08f;

    private Vector3 originPoint;
    private Coroutine bounceRoutine;

    int index = 0;

    private void Awake()
    {
        originPoint = transform.localPosition;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) ||
            Input.GetKeyDown(KeyCode.RightArrow) ||
            Input.GetKeyDown(KeyCode.UpArrow) ||
            Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartBounce();

            // randomzies, enable this when u have more sprites

            //int randomDance = Random.Range(0, sprites.Count);
            //spriteRenderer.sprite = sprites[randomDance];

            

            spriteRenderer.sprite = sprites[index];

            index++;
            if (index >= sprites.Count)
                index = 0;
        }
    }

    private void StartBounce()
    {
        if (bounceRoutine != null)
        {
            StopCoroutine(bounceRoutine);
            transform.localPosition = originPoint;
        }

        bounceRoutine = StartCoroutine(Bounce());
    }

    public IEnumerator Bounce()
    {
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / bounceDur;

            // 0 -> 1 -> 0 curve (smooth up then down)
            float curve = Mathf.Sin(t * Mathf.PI);

            transform.localPosition = originPoint + Vector3.up * (curve * bounceHeight);

            yield return null;
        }

        transform.localPosition = originPoint;
        bounceRoutine = null;
    }
}
