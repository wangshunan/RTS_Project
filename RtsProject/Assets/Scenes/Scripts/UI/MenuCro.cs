using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCro : MonoBehaviour {

    [SerializeField]
    GameManager gameManager;

    private Animator anim;

    public GameObject test;
    public Sprite win;
    public Sprite lose;

    private void Awake()
    {
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
        anim = test.GetComponent<Animator>();
    }

    private void GameOverEvent()
    {
        if ( gameManager.gameStatus == GameManager.GameStatus.Vectory ) {
            test.GetComponent<SpriteRenderer>().sprite = win;
        }

        if ( gameManager.gameStatus == GameManager.GameStatus.Lose ) {
            test.GetComponent<SpriteRenderer>().sprite = win;
        }

        anim.SetTrigger( "Win");
    }

    public void OnPlayButton() {
        gameObject.SetActive(false);
        gameManager.gameStatus = GameManager.GameStatus.Play;
    }

    public void OnBackButton() {
        Application.Quit();
    }
}
