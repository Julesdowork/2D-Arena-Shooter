using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField] GameObject playerPf;
    [SerializeField] Transform playerSpawnPoint;
    [SerializeField] SimpleCamera simpleCam;
    [SerializeField] float spawnPlayerDelay = 4f;
    [SerializeField] GameObject spawnPlayerPf;

    public int Lives { get; private set; }

    GameObject player;
    PlayerController playerController;

    [SerializeField] string respawnSfx = "Respawn";
    [SerializeField] string respawnCountdownSfx = "RespawnCountdown";
    [SerializeField] string gameOverSfx = "GameOver";

    [SerializeField] GameObject gameOverUI;
    [SerializeField] LivesCounterUI livesCounterUI;

    private void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        player =  GameObject.FindGameObjectWithTag(TagManager.PLAYER);
        playerController = player.GetComponent<PlayerController>();
        Lives = 3;
        livesCounterUI.UpdateLives();
    }

    public void KillPlayer()
    {
        player.SetActive(false);
        Lives--;
        livesCounterUI.UpdateLives();

        if (Lives <= 0)
            EndGame();
        else
            StartCoroutine(RespawnPlayer());
    }

    public IEnumerator RespawnPlayer()
    {
        AudioManager.instance.PlaySound(respawnCountdownSfx);
        yield return new WaitForSeconds(spawnPlayerDelay);

        player.transform.position = playerSpawnPoint.position;
        player.SetActive(true);

        Instantiate(spawnPlayerPf, playerSpawnPoint.position, Quaternion.identity);
        AudioManager.instance.PlaySound(respawnSfx);
    }

    void EndGame()
    {
        AudioManager.instance.PlaySound(gameOverSfx);
        gameOverUI.SetActive(true);
    }
}
