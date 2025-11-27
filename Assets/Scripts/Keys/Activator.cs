using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Activator : MonoBehaviour
{
    //listen for arrow press, public variable in inspector specify key
    // if pressed, dim with coroutine

    public KeyCode key;
    public bool activeKey;
    public SpriteRenderer sr;
    Color oldColor;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    public void Start()
    {
        activeKey = false;
        oldColor = sr.color;
    }

    public void Update()
    {
        keyListener();
    }

    public void keyListener()
    {
        if (Input.GetKeyDown(key))
        {
            StartCoroutine(DimKey());
        }

    }

    private IEnumerator DimKey()
    {
        // active
        activeKey = true;

        
        sr.color = new Color(1, 1, 1);

        // slight delay
        yield return new WaitForSeconds(0.2f);

        // return to old color
        sr.color = oldColor;
        activeKey = false;

        

    }
    
}
