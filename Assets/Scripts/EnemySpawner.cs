using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.5f, 10f)] [SerializeField] float secondsBetweenSpawn = 4f;
    [SerializeField] EnemyMovement enemyPrefab;
    [SerializeField] Transform enemyParentTransform;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip spawnEnemySFX;
    int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        scoreText.text = score.ToString();
    }

    IEnumerator SpawnEnemies() 
    {
        while (true)
        {
            AddScore();
            GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = enemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawn);
        }

    }

    private void AddScore()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
