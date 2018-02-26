using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] GameObject Player;
    [SerializeField] GameObject LightEnemy;
    [SerializeField] GameObject DarkEnemy;
    [SerializeField] Text ScoreText;
    [SerializeField] Text WaveText;
    [SerializeField] Text WaveTimeText;
    [SerializeField] LayerMask gameLayer;
    [SerializeField] int numEnemy;
    [SerializeField] float waveTime;
    int bounds = 24;
    float m_MaxWaveTime;
    int score = 0;
    int wave = 0;

    void Start()
    {
        m_MaxWaveTime = waveTime;
        waveTime = 5;
    }

    void Update()
    {
        waveTime -= Time.deltaTime;
        if (waveTime <= 0)
        {
            ++wave;
            SpawnEnemies(numEnemy + wave);
            waveTime = m_MaxWaveTime + wave;
        }

        ScoreText.text = score.ToString();
        WaveText.text = wave.ToString();
        WaveTimeText.text = waveTime.ToString("#.0");
    }

    public void addScore(int val)
    {
        score += val;
    }

    public void GameOver()
    {
        ScoreManager.instance.SaveScore(score);
        SceneManager.LoadScene(1);
    }

    void SpawnEnemies(int toSpawn)
    {
        for(int i = 0; i < toSpawn; ++i)
        {
            SpawnEnemy(LightEnemy);
            SpawnEnemy(DarkEnemy);
        }
    }

    void SpawnEnemy(GameObject enemy)
    {
        Vector3 pos = GetSpawnPos();
        GameObject e = Instantiate(enemy, pos, Quaternion.identity);
        Enemy es = e.GetComponent<Enemy>();
        es.SetPlayer(Player);
        es.SetStats(1 + (int)(wave * 0.5f));
    }

    Vector3 GetSpawnPos()
    {
        bool hit = true;
        Vector3 pos = Vector3.zero;

        while (hit)
        {
            int x = Random.Range(-bounds, bounds);
            int z = Random.Range(-bounds, bounds);
            pos = new Vector3(x, 1, z);
            hit = Physics.CheckSphere(pos, 1, gameLayer);
        }
        return pos;
    }
}
