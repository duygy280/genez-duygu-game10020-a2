using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //restart the current scene
    public void RestartGame()
    {
        //reset time
        Time.timeScale = 1f;

        //reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}