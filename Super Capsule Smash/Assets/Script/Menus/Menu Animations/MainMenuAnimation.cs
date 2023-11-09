using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    [SerializeField] private GameObject mTitle;
    [SerializeField] private GameObject mExplosion;
    [SerializeField] private GameObject mPlayButton;
    [SerializeField] private GameObject mQuitButton;

    private float counter = 0.0f;
    private float maxCounter = 4.0f;
    private bool animationDone = false;

    private void Start()
    {
        Time.timeScale = 1.0f;
        mTitle.transform.LeanScale(Vector2.one, 0.8f);
        PlayButtonAnim();
        QuitButtonAnim();
    }

    private void Update()
    {
        if (!animationDone)
        {
            counter += Time.deltaTime * 5;
            if (counter >= maxCounter)
            {
                ExplosionAnim();
                animationDone = true;
                counter = 0.0f;
            }
        }
    }

    private void PlayButtonAnim()
    {
        mPlayButton.transform.LeanMoveLocal(new Vector2(0, -110), 1).setEaseOutQuart();
    }

    private void QuitButtonAnim()
    {
        mQuitButton.transform.LeanMoveLocal(new Vector2(0, -340), 1).setEaseOutQuart();
    }

    private void ExplosionAnim()
    {
        mExplosion.transform.LeanScale(Vector2.one, 0.3f);
    }
}
