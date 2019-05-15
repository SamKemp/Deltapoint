using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PlayerWins : MonoBehaviour
{
    public GameObject[] players;
    
    private int _winner = 0;
    
    private Text _textObj;
    private RawImage _imageObj;
    
    // Start is called before the first frame update
    void Start()
    {
        players = GameObject.FindGameObjectsWithTag("Player");
        
        //Debug.Log(players.Length);
        
        _textObj = this.GetComponent<Text>();
        _imageObj = GameObject.Find("PlayerWins_BG").GetComponent<RawImage>();
    }

    // FixedUpdate is called once every few frames
    void FixedUpdate()
    {
        players = GameObject.FindGameObjectsWithTag("player");
        
        if (players.Length == 1)
        {
            _winner = players[0].gameObject.GetComponent<PlayerMove>().player;
        }

        if (_winner != 0)
        {
            _textObj.text = "Player " + _winner + " Wins";
            _textObj.enabled = true;
            _imageObj.enabled = true;
            //Time.timeScale = 0;
        }
    }
}
