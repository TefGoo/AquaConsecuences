using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    // Function to be called when the button is clicked to load a specific scene
    public void LoadScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
