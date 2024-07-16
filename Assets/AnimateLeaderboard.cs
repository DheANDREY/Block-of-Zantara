using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class AnimateLeaderboard : MonoBehaviour
{
    public RectTransform boardBG;
    public GameObject[] arcNo;

    private SoundEffect sfx;
    // Start is called before the first frame update
    private void Start()
    {
        sfx = GameObject.FindWithTag("SFX").GetComponent<SoundEffect>();
    }

    public void MoveLeftMenu(int no)
    {
        sfx.PlaySFX_SwapW();
        //float posX = boardBG.transform.position.x;
        boardBG.DOLocalMoveX(-1500, .65f).OnComplete(() => boardBG.transform.position = new Vector3(5, boardBG.transform.position.y));
        arcNo[no].SetActive(false);
        StartCoroutine(openNewLeft(1.05f, no+1));
    }
    private IEnumerator openNewLeft(float delay, int a)
    {
        yield return new WaitForSeconds(delay);
        arcNo[a].SetActive(true);
        boardBG.DOLocalMoveX(-250, .35f);
    }

    public void MoveRightMenu(int no)
    {
        sfx.PlaySFX_SwapW();
        //float posX = boardBG.transform.position.x;
        boardBG.DOLocalMoveX(1500, .65f).OnComplete(() => boardBG.transform.position = new Vector3(-9, boardBG.transform.position.y));
        arcNo[no].SetActive(false);
        StartCoroutine(openNewRight(1.05f, no-1));
    }
    private IEnumerator openNewRight(float delay, int a)
    {
        yield return new WaitForSeconds(delay);
        arcNo[a].SetActive(true);
        boardBG.DOLocalMoveX(-250, .35f);
    }
}
