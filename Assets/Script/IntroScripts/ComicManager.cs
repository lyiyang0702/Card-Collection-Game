using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ComicManager : MonoBehaviour
{

    public GameObject spaceText;
    public GameObject[] comicPages;
    private bool isLoadingComic = false;
    int comicIndex = 0;
    int currentSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        //disable comic pages
        for (int i = 0; i < comicPages.Length; i++)
        {
            comicPages[i].SetActive(false);
        }
        //comicPages[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            spaceText.SetActive(false);
            if (comicIndex < comicPages.Length)
            {
                if (isLoadingComic == false)
                    loadComic();

            }
            else
            {
                //SceneManager.LoadScene(currentSceneIndex++);
                for (int i = 0; i < comicPages.Length; i++)
                {
                    comicPages[i].SetActive(false);
                }

                loadNextScene();
            }
        }
    }

    void loadComic()
    {
        isLoadingComic = true;

        comicPages[comicIndex].SetActive(true);

        if (comicIndex - 1 >= 0)
        {
            comicPages[comicIndex - 1].SetActive(false);
        }

        comicIndex++;
        //comicPages[comicIndex-1].SetActive(false);

        isLoadingComic = false;
    }

    void loadNextScene()
    {
        //implement scene load
        Debug.Log("Loading Next Scene");
    }
}
