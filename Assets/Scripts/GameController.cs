using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public SlingShooter SlingShooter;
    public List<Bird> Birds;
    public List<Enemy> Enemies;
    public TrailController TrailController;
    public BoxCollider2D TapCollider;



    private bool _isGameEnded = false;
    private Bird _shotBird;
    private int _currentSceneIndex;


    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Birds.Count; i++)
        {
            Birds[i].OnBirdDestroyed += ChangeBird;
            Birds[i].OnBirdShot += AssignTrail;
        }

        foreach (Enemy enemy in Enemies)
        {
            enemy.OnEnemyDestroyed += CheckGameEnd;
        }

        TapCollider.enabled = false;
        _currentSceneIndex = 0;
        SlingShooter.InitiateBird(Birds[0]);
        _shotBird = Birds[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeBird()
    {
        if (_isGameEnded)
        {
            return;
        }
        TapCollider.enabled = false;


        Birds.RemoveAt(0);
        if (Birds.Count <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        if (Birds.Count > 0)
        {
            SlingShooter.InitiateBird(Birds[0]);
        }
    }

    public void CheckGameEnd(GameObject destroyedEnemy)
    {
        foreach (Enemy enemy in Enemies)
        {
            if (enemy.gameObject == destroyedEnemy)
            {
                Enemies.Remove(enemy);
                break;
            }
        }

        if (Enemies.Count <= 0)
        {
            _isGameEnded = true;
            _currentSceneIndex += 1;
            if (_currentSceneIndex == 1)
            {
                SceneManager.LoadScene("Level 2");
            }
            else
            {
                SceneManager.LoadScene("Main");
            }
        }

    }

    public void AssignTrail(Bird bird)
    {
        TrailController.SetBird(bird);
        StartCoroutine(TrailController.SpawnTrail());
        TapCollider.enabled = true;

    }

    void OnMouseUp()
    {
        if (_shotBird != null)
        {
            _shotBird.OnTap();
        }
    }
}
