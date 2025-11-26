using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
public class Player : MonoBehaviour
{
    [Header("Sprites / Visuals")]
    public List<Sprite> sprites = new List<Sprite>();
    public SpriteRenderer spriteRenderer;

    [Header("Bounce Settings")]
    public float bounceHeight = 0.15f;
    public float bounceDur = 0.08f;
    private Coroutine bounceRoutine;

    [Header("Walk-in Settings")]
    
    public Transform catOrigin;
    public float walkSpeed = 2f;
    private bool gameStart = false; // walk finished
    private bool isWalking = false;
    public IntroManager introManager;


    public GameObject pressStartText;
    private Animator anim;
    int index = 0;

    

    private void Awake()
    {
        anim = GetComponent<Animator>();
        
    }

    public void Update()
    {
        bool arrowPressed =
        Input.GetKeyDown(KeyCode.LeftArrow) ||
        Input.GetKeyDown(KeyCode.RightArrow) ||
        Input.GetKeyDown(KeyCode.UpArrow) ||
        Input.GetKeyDown(KeyCode.DownArrow);

        // menu arrow game hasnt started yet
        if (!gameStart && arrowPressed)
        {
            isWalking = true;
            anim.SetBool("isWalking", true);

            // hide start text
            pressStartText.SetActive(false);
            

            return;
        }

        // walking in
        if (isWalking)
        {
            WalkIn();
            return;
        }

        if (gameStart && arrowPressed)
        {
            GetJiggyWitIt();
        }

    }

    private void WalkIn()
    {
        // walk
        transform.position = Vector3.MoveTowards(transform.position, catOrigin.position, walkSpeed * Time.deltaTime);

        // arrived
        if (Vector3.Distance(transform.position, catOrigin.position) < 0.01f)
        {
            transform.position = catOrigin.position;
            anim.SetBool("isWalking", false);
            isWalking = false;

            introManager.StartIntro();
        }
    }


        
    public void GetJiggyWitIt()
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

    public void SetGameStartTrue()
    {
        gameStart = true;
    }


    private void StartBounce()
    {
        if (bounceRoutine != null)
        {
            StopCoroutine(bounceRoutine);
            transform.position = catOrigin.position;
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

            transform.position = catOrigin.position + Vector3.up * (curve * bounceHeight);

            yield return null;
        }

        transform.position = catOrigin.position;
        bounceRoutine = null;
    }
}


