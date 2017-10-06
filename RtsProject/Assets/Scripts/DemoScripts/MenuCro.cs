using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuCro : MonoBehaviour {

    [SerializeField]
    GameManager gameManager;

    public GameObject gameMenu;
    public GameObject gameOverSprites;

    private Animator anim;
    public Sprite winSprite;
    public Sprite loseSprite;

    private void Awake()
    {
        gameManager = GameObject.Find("GameSystem").GetComponent<GameManager>();
        anim = gameOverSprites.GetComponent<Animator>();
    }

    private void Update()
    {
        if (gameManager.gameStatus != GameManager.GameStatus.Stop && gameManager.gameStatus != GameManager.GameStatus.Play)
        {
            StartCoroutine(GameOverEvent());
        }
    }
   

    public void OnPlayButton() {
        gameMenu.SetActive(false);
        gameManager.gameStatus = GameManager.GameStatus.Play;       
    }

    public void OnBackButton() {
        Application.Quit();
    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(0);
    }

    IEnumerator GameOverEvent()
    {
        // 画像設定
        if (gameManager.gameStatus == GameManager.GameStatus.Vectory)
        {
            gameOverSprites.GetComponent<SpriteRenderer>().sprite = winSprite;
        }

        if (gameManager.gameStatus == GameManager.GameStatus.Lose)
        {
            gameOverSprites.GetComponent<SpriteRenderer>().sprite = loseSprite;
        }

        // sprite anim
        anim.SetBool("Win", true);

        yield return new WaitForSeconds(2);

        ReLoadScene();
    }
}
