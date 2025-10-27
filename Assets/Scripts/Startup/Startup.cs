using UnityEngine;
using UnityEngine.SceneManagement;

namespace Startup
{
    public class Startup
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnGameStart()
        {  
            SceneManager.LoadScene("Scenes/ConstantLoad");
            SceneManager.LoadScene("Scenes/Opening");
        }
    }
}
