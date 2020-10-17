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

    GameObject player;
    PlayerController playerController;

    [SerializeField] AudioSource audioSource;

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
    }

    public void KillPlayer()
    {
        Debug.Log("You died!!!");
        player.SetActive(false);
        StartCoroutine(RespawnPlayer());
    }

    public IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(spawnPlayerDelay);

        player.transform.position = playerSpawnPoint.position;
        player.SetActive(true);

        Instantiate(spawnPlayerPf, playerSpawnPoint.position, Quaternion.identity);
        audioSource.Play();
    }
}
