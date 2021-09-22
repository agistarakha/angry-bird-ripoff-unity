using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailController : MonoBehaviour
{
    public GameObject Trail;
    private Bird TargetBird;

    private List<GameObject> _trails;
    // Start is called before the first frame update
    void Start()
    {
        _trails = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetBird(Bird bird)
    {
        TargetBird = bird;

        foreach (GameObject trail in _trails)
        {
            Destroy(trail.gameObject);
        }

        _trails.Clear();
    }

    public IEnumerator SpawnTrail()
    {
        _trails.Add(Instantiate(Trail, TargetBird.transform.position, Quaternion.identity));

        yield return new WaitForSeconds(0.1f);

        if (TargetBird != null && TargetBird.State != Bird.BirdState.HitSomething)
        {
            StartCoroutine(SpawnTrail());
        }
    }
}
