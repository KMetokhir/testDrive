using UnityEngine;

public abstract class AttractableGenerator<T> : MonoBehaviour // 
 where T : Attractable
{

    [SerializeField] private ObjectPool<T> _pool;

    public T Generate()
    {
        T attractable = _pool.GetObject();
        // bullet.transform.position = position;
        //  bullet.transform.rotation = rotation;

        return attractable;
    }
}

/*
    [SerializeField] private float _delay;
    [SerializeField] private float _lowerBound;
    [SerializeField] private float _upperBound;
    [SerializeField] private ObjectPool<Enemy> _pool;
    [SerializeField] private EnemyBulletGenerator _bulletGenerator;

    private Coroutine _coroutine;*/

/* public void Generate()
 {
     if (_coroutine != null)
     {
         StopCoroutine(_coroutine);
     }

     _coroutine = StartCoroutine(GenerateEnemies());
 }

 public void Restart()
 {
     _pool.Restart();
     Generate();
 }

 private IEnumerator GenerateEnemies()
 {
     WaitForSeconds wait = new WaitForSeconds(_delay);

     while (enabled)
     {
         Enemy enemy = _pool.GetObject();
         enemy.ActivateShooter(_bulletGenerator);
         Spawn(enemy);

         yield return wait;
     }
 }

 private void Spawn(Enemy enemy)
 {
     float spawnPositionY = UnityEngine.Random.Range(_upperBound, _lowerBound);
     Vector3 spawnPoint = new Vector3(transform.position.x, spawnPositionY, transform.position.z);

     enemy.transform.position = spawnPoint;
 }*/


