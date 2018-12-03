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

    public static void JumpBackwards(Rigidbody2D enemy, float speed) {

        enemy.velocity = -enemy.velocity * 5;

    }

    public static void Dash(GameObject player, Rigidbody2D enemy, float speed) {
        Vector2 direction = (Vector2)player.transform.position - enemy.position;
        Vector3.MoveTowards(enemy.position, player.transform.position, 2);
        direction.Normalize();
        Vector3 difference = player.transform.position - enemy.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        enemy.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);

        enemy.velocity = direction * speed * 2;
    }

    public static void Strafe(GameObject player, Rigidbody2D enemy, float speed) { }
}
