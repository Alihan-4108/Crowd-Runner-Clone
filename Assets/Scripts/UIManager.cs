using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
    }
}
