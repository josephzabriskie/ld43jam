using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnemyMovement {

    // Use this for initialization


    public static void MoveTowardsTarget(GameObject player, Rigidbody2D enemy, float speed)
    {
        Vector2 direction = (Vector2)player.transform.position - enemy.position;

        direction.Normalize();
        Vector3 difference = player.transform.position - enemy.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);

        enemy.velocity = direction * speed;
        
    }

    public static void JumpBackwards(GameObject player, Rigidbody2D enemy, float speed) {
        Vector2 direction = (Vector2)player.transform.position - enemy.position;

        direction.Normalize();
        Vector3 difference = player.transform.position - enemy.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);

        enemy.velocity = -direction * speed;

    }

    public static void Dash(Vector3 target, Rigidbody2D enemy, float speed) {
        Vector2 direction = (Vector2)target - enemy.position;
        //Vector3 vec = Vector3.MoveTowards(enemy.position, target, 2);

        direction.Normalize();
        Vector3 difference = target - enemy.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);

        enemy.velocity = direction * speed * 2;
    }

    public static void Strafe(GameObject player, Rigidbody2D enemy, float speed, float startStrafe) {
        Vector2 direction = (Vector2)player.transform.position - enemy.position;
        direction.Normalize();
        Vector3 difference = player.transform.position - enemy.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
        Vector2 newVector;
        if (Time.time - startStrafe > 4)
        {
            newVector = -enemy.transform.right * speed;
        }
        else
        {
            newVector = enemy.transform.right * speed;
        }

        enemy.velocity = newVector;

    }
}
