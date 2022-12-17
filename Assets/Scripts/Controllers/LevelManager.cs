using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int levelsCount = 4;

    public void ReloadLevel()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadNextLevel()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            LoadFirstLevel();
            return;
        }

        string numberInString = SceneManager.GetActiveScene().name.Replace("Level", "").Replace(" ", "");
        int levelNum = 0;

        if (int.TryParse(numberInString, out levelNum))
        {
            if (levelNum + 1 <= levelsCount)
                SceneManager.LoadScene($"Level{levelNum + 1}");
            else
                LoadMenu();
        }
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void LoadTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void LoadFirstLevel()
    {
        SceneManager.LoadScene($"Level1");
    }

    public void Exit()
    {
        Application.Quit();
    }
}
