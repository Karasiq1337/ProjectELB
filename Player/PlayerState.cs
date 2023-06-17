using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public Sprite playerPortret;
    public string playerName;
    public bool isInteracting;
    public bool canMove;

    private void Start()
    {
        canMove = true;
        isInteracting = false;
    }
}
