using UnityEngine;

static public class Seettings
{
    // Start is called before the first frame update

    private class SeettingsMB : MonoBehaviour
    {
        [SerializeField] public static GameObject settingsMenu;
        private void Start()
        {
            settingsMenu = GameObject.FindWithTag("settingsMenu");
        }
    }

    public static void Show()
    {
        GameObject[] gameObjects = SeettingsMB.settingsMenu.GetComponentsInChildren<GameObject>();
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(true);
        }
    }
    public static void Close()
    {
        GameObject[] gameObjects = SeettingsMB.settingsMenu.GetComponentsInChildren<GameObject>();
        foreach (GameObject gameObject in gameObjects)
        {
            gameObject.SetActive(false);
        }
    }
}
