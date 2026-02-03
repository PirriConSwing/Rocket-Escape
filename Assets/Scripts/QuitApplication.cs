using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    
    void Update()
    {
        CloseGame();
    }

    private static void CloseGame()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Debug.Log("ESC apretat");
            Application.Quit();
        }
    }
}
