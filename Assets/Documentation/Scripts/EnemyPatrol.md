# EnemyPatrol

## Purpose

This script moves each enemy through an ordered list of fixed waypoint objects. The route loops after the last waypoint and does not use pathfinding or chasing.

**Status:** Active.

## Attached Scene Object

- `Enemy_01`
- `Enemy_02`
- `Enemy_03`

## Main Responsibility

- Fixed enemy movement
- Waypoint following
- Route looping

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `waypoints` | Ordered route transforms | Four per enemy |
| `moveSpeed` | Enemy patrol speed | `1.5`, `1.3`, `1.6` |
| `reachDistance` | Distance used to accept a waypoint | `0.1` |
| `stuckTimeout` | Seconds without movement before skipping a blocked segment | `1` |
| `currentWaypointIndex` | Current position in the route | Starts at `0` |
| `rb` | Enemy `Rigidbody2D` reference | Attached component |
| `enemyHealth` | Enemy health reference | Attached component |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `Awake()` | Gets rigidbody and health references | Enemy initialization |
| `FixedUpdate()` | Moves toward the current waypoint | Every physics step |
| `AdvanceWaypoint()` | Selects the next waypoint and loops | Current point is reached |
| `UpdateStuckState()` | Tracks whether the enemy has stopped making progress | Every patrol physics step |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void Awake()` | None | None |
| `private void FixedUpdate()` | None | None |
| `private void AdvanceWaypoint()` | None | None |
| `private void UpdateStuckState(Vector2 currentPosition)` | `currentPosition`: enemy's current physics position | None |

The waypoint array is supplied through the Inspector. The methods do not need parameters because they use the enemy's stored fields.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `5` | `public Transform[] waypoints;` | Stores the ordered fixed patrol route. |
| `18–20` | Component and position setup | Stores `Rigidbody2D`, `EnemyHealth`, and the initial position. |
| `25–28` | Game-end guard | Stops patrol after win or lose. |
| `30–33` | Death guard | Stops movement for a dead enemy. |
| `35–38` | Waypoint validation | Prevents errors when no route is assigned. |
| `40` | Current target selection | Reads the waypoint at `currentWaypointIndex`. |
| `42–46` | Missing waypoint handling | Skips a null waypoint instead of stopping permanently. |
| `48–49` | Position values | Reads current and target positions as `Vector2`. |
| `51–55` | Early reach check | Advances immediately when already close to the target. |
| `57–64` | Stuck recovery | Tracks movement progress and advances after the configured timeout. |
| `66–70` | `Vector2.MoveTowards()` | Calculates a fixed-speed next position without overshooting. |
| `72–80` | Movement choice | Wakes and moves the rigidbody, with transform fallback. |
| `74` | `rb.WakeUp()` | Ensures a sleeping rigidbody resumes patrol movement. |
| `82–85` | Post-movement reach check | Advances when the calculated position reaches the target. |
| `90` | Modulo waypoint loop | Moves to the next index and wraps back to zero. |
| `94–105` | Progress tracking | Measures actual movement and accumulates stuck time. |

## Execution Sequence

1. `Awake()` gets required components.
2. Every physics step checks game state and enemy health.
3. The current waypoint is selected.
4. `MoveTowards()` calculates a new position.
5. Movement progress is checked for a blocked segment.
6. A blocked segment is skipped after one second.
7. `MovePosition()` moves the enemy.
8. Distance to the target is checked.
9. `AdvanceWaypoint()` selects the next route point.
10. The last point loops back to the first.

## How It Works In Game

The script moves the enemy with `Vector2.MoveTowards()` and `Rigidbody2D.MovePosition()`. On reaching a point, it advances to the next array item. If a collider blocks one route segment for one second, it advances instead of remaining permanently stopped. It stops if the game ends or the enemy is dead.

## Linked Scripts

- Reads `GameManager.Instance.gameEnded`.
- Reads `EnemyHealth.IsDead`.
- Works beside `EnemyShooter` while the enemy moves.

## Linked Scene Objects

- `Enemy_01`
- `Enemy_02`
- `Enemy_03`
- `PatrolPoints`
- `Enemy01_WP_01` to `Enemy01_WP_04`
- `Enemy02_WP_01` to `Enemy02_WP_04`
- `Enemy03_WP_01` to `Enemy03_WP_04`

## Why This Approach Is Used

Simple fixed patrol

## Notes For Presentation

“This script moves enemies through four fixed waypoint positions. It loops the route and uses Rigidbody2D movement, so no pathfinding or advanced AI is required.”
