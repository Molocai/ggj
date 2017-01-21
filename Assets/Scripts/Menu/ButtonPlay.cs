using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonPlay : MonoBehaviour
{
    public void OnButtonPlayClick(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }
}
