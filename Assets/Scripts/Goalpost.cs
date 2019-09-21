using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Goalpost : MonoBehaviour
{
    public int playerNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            return;
        }

        if (other.CompareTag("Ball"))
        {
            GameManager.Instance.AddScore(playerNumber, 1);
        }
    }
}
