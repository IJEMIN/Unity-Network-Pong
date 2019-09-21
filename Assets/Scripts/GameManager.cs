using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<GameManager>();

            return instance;
        }
    }

    private static GameManager instance;

    public Text scoreText;
    public Transform[] spawnPositions;
    public GameObject playerPrefab;
    public GameObject ballPrefab;

    private int[] playerScores;
    private Ball ball;

    private void Start()
    {
        playerScores = new[] {0, 0};
        UpdateScoreText();
        SpawnPlayer();

        if (PhotonNetwork.IsMasterClient) SpawnBall();
    }

    private void SpawnPlayer()
    {
        var localPlayerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;
        var spawnPosition = spawnPositions[localPlayerIndex % spawnPositions.Length];

        PhotonNetwork.Instantiate(playerPrefab.name, spawnPosition.position, Quaternion.identity);
    }

    private void SpawnBall()
    {
        ball = PhotonNetwork.Instantiate(ballPrefab.name, Vector2.zero, Quaternion.identity).GetComponent<Ball>();
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void AddScore(int playerNumber, int score)
    {
        playerScores[playerNumber - 1] += score;
        UpdateScoreText();

        ResetBallPosition();
    }

    private void UpdateScoreText()
    {
        scoreText.text = $"{playerScores[0]} : {playerScores[1]}";
    }

    private void ResetBallPosition()
    {
        ball.transform.position = Vector2.zero;
    }
}