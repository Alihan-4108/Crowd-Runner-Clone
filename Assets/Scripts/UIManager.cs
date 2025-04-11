using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI levelText;

    private void Start()
    {
        progressBar.value = 0f;

        gamePanel.SetActive(false);

        levelText.text = "Level " + (ChunkManager.instance.GetLevel() + 1);
    }

    private void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
    }

    public void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState())
            return;

        float progress = 1f - PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;
    }
}
