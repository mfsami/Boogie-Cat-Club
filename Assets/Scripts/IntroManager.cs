using System.Collections;
using UnityEngine;
using Unity.Cinemachine;

public class IntroManager : MonoBehaviour
{
    [Header("Guard / Door")]
    public GameObject guard;
    public SpriteRenderer doorSprites;
    public Sprite doorOpen;
    public Sprite doorClose;
    public float doorOpenDelay = 2f;

    [Header("UI")]
    public GameObject clubText;
    public GameObject dialogueBox;

    [Header("Camera Shake")]
    public CinemachineImpulseSource impulseSource;

    [Header("References")]
    public Player player;   // reference back to your Player script

    // called by Player when it reaches catOrigin
    public void StartIntro()
    {
        StartCoroutine(Intro());
    }

    private IEnumerator Intro()
    {
        // wait after cat arrives 
        yield return new WaitForSeconds(doorOpenDelay);

        // open door and guard 
        clubText.SetActive(false);
        doorSprites.sprite = doorOpen;
        guard.SetActive(true);
        CameraShakeManager.instance.CameraShake(impulseSource);

        // dialogue
        yield return new WaitForSeconds(1);
        dialogueBox.SetActive(true);


        // tell Player that the game has started
        player.SetGameStartTrue();
    }
}
