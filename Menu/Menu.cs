using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadMenu()
    {
        SceneManager.LoadScene(1);
    }
}
