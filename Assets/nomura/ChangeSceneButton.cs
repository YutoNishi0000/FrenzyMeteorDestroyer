using UnityEngine;
using UnityEngine.UI;

public class ChangeSceneButton : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(LoadScene);
    }

    private void LoadScene()
    {
        GameManager.Instance.LoadScene(sceneName);
    }
}