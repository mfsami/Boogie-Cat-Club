using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueAnim : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    [SerializeField] float delayChars;
    [SerializeField] float delayWords;
    public string[] stringArray;

    int i = 0;

    private void Start()
    {
        EndCheck();
    }

    void EndCheck()
    {
        if (i <= stringArray.Length - 1)
        {
            textMeshPro.text = stringArray[i];
            StartCoroutine(TextVisibile());
        }
    }

    private IEnumerator TextVisibile()
    {
        textMeshPro.ForceMeshUpdate();
        int totalChars = textMeshPro.textInfo.characterCount;
        int counter = 0;

        while (true)
        {
            int visibleCount = counter % (totalChars + 1);
            textMeshPro.maxVisibleCharacters = visibleCount;

            if (visibleCount >= totalChars)
            {
                i += 1;
                Invoke("EndCheck", delayWords);
                break;
            }

            counter += 1;
            yield return new WaitForSeconds(delayChars);
        }
    }
}
