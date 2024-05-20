using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoadPanelWithSound : MonoBehaviour
{
    public GameObject panel;
    public float delay = 5f;
    public AudioClip soundEffect;
    public float fadeDuration = 1f;

    private AudioSource audioSource;
    private CanvasGroup canvasGroup;

    void Start()
    {
        panel.SetActive(false);
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundEffect;
        canvasGroup = panel.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = panel.AddComponent<CanvasGroup>();
        }
        StartCoroutine(ActivatePanel());
    }

    IEnumerator ActivatePanel()
    {
        yield return new WaitForSeconds(delay);

        audioSource.Play();
        panel.SetActive(true);

        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        canvasGroup.alpha = 1;
    }
}
