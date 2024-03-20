using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeMenu : MonoBehaviour
{
    public void GotIt()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
        TimeAndStatusManager.timeElapsed = 0;
        TimeAndStatusManager.areCheatsUsed = false;
    }
}
