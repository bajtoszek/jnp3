
public class WaspSpawner : AEnemySpawner<WaspMovement>
{
    protected override void SetupEnemy(WaspMovement enemy, int totalHealthPoints=0)
    {
        enemy.Reset(transform.position, totalHealthPoints);
    }
}
