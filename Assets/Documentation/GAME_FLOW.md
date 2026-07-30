# BullectEcho2D Game Flow

## Simple Flow Diagram

`Start → Count Objects → Play → Enemy Patrol/Shoot → Player Kill/Collect → Win or Lose`

## Complete Runtime Flow

1. **Game starts**
   - Unity loads `MainScene`.
   - `Awake()` methods prepare script references and initial values.

2. **GameManager counts objects**
   - `GameManager.Start()` finds all `EnemyHealth` components.
   - It also finds all `CashCollectible` components.
   - The current scene contains four enemies and five objectives.

3. **Player health resets to 5**
   - `PlayerHealth.Awake()` sets `currentHealth = maxHealth`.
   - `maxHealth` is configured as 5.

4. **UI initializes**
   - `UIManager.Awake()` resolves HUD and panel references.
   - `WinPanel` and `LosePanel` are hidden.
   - `PlayerHealth.Start()` updates the health display.
   - `GameManager.Start()` updates enemy and objective displays.

5. **Player moves with keyboard**
   - `PlayerController.Update()` reads WASD or arrow keys.
   - `PlayerController.FixedUpdate()` moves the `Rigidbody2D`.

6. **Camera follows player**
   - `CameraFollow.LateUpdate()` reads the player position.
   - The camera moves smoothly and remains inside map bounds.

7. **Enemies patrol fixed waypoint roads**
   - Each enemy has four assigned waypoint transforms.
   - `EnemyPatrol.FixedUpdate()` moves toward the current waypoint.
   - `AdvanceWaypoint()` selects the next point and loops the route.

8. **Enemies check range and line-of-sight**
   - `EnemyShooter.Update()` measures distance to the player.
   - It also checks the shooting cooldown.
   - `HasClearLineOfSight()` performs a `Physics2D.Linecast()`.

9. **Enemy shooting damages player by 1**
   - If the first valid linecast hit is the player, the enemy shoots.
   - `EnemyShooter` calls `PlayerHealth.TakeDamage(damage)`.
   - Each enemy is configured with `damage = 1`.

10. **Walls block enemy shooting**
    - Maze walls are on the `Wall` layer.
    - The linecast checks both `Wall` and `Player` layers.
    - If a wall is hit first, the player is not damaged.

11. **Player touches enemies to kill them**
    - `PlayerContactKill` receives collision or trigger callbacks.
    - It finds `EnemyHealth` on the contacted object or its parent.
    - It calls `EnemyHealth.KillInstantly()`.

12. **Enemy death updates enemy count**
    - `EnemyHealth.Die()` spawns death particles once.
    - It calls `GameManager.RegisterEnemyKilled()`.
    - The enemy object is destroyed.
    - `UIManager.UpdateEnemies()` refreshes the HUD.

13. **Player collects objectives**
    - A `CashCollectible` detects the player entering its trigger.
    - It calls `GameManager.CollectCash(1)`.
    - The collected object is destroyed.

14. **Objective count updates**
    - `GameManager.CollectCash()` increases the collected total.
    - `UIManager.UpdateCash()` refreshes the HUD.

15. **WinPanel appears**
    - `GameManager.CheckWinCondition()` checks both totals.
    - All enemies must be killed.
    - All objectives must be collected.
    - `UIManager.ShowWin()` displays `WinPanel`.

16. **LosePanel appears**
    - Repeated enemy shots reduce health.
    - When health reaches zero, `PlayerHealth.Die()` runs.
    - Player death particles spawn.
    - `GameManager.LoseGame()` calls `UIManager.ShowLose()`.

17. **Player controls stop after game ends**
    - `gameEnded` becomes `true`.
    - `GameManager.DisablePlayerGameplay()` disables player movement.
    - Patrol and shooter scripts also check `gameEnded` and stop acting.

## Win Flow

`Kill 4 enemies + Collect 5 objectives → CheckWinCondition() → gameEnded = true → Disable player → Show WinPanel`

## Lose Flow

`Enemy shot → TakeDamage(1) → Health reaches 0 → Die() → LoseGame() → Disable player → Show LosePanel`

## System Communication

| Source | Calls | Result |
| --- | --- | --- |
| `EnemyShooter` | `PlayerHealth.TakeDamage()` | Player loses health |
| `PlayerHealth` | `UIManager.UpdateHealth()` | Health HUD changes |
| `PlayerHealth` | `GameManager.LoseGame()` | Lose state begins |
| `PlayerContactKill` | `EnemyHealth.KillInstantly()` | Enemy dies |
| `EnemyHealth` | `GameManager.RegisterEnemyKilled()` | Enemy total changes |
| `CashCollectible` | `GameManager.CollectCash()` | Objective total changes |
| `GameManager` | `UIManager.ShowWin()` | Win panel appears |
| `GameManager` | `UIManager.ShowLose()` | Lose panel appears |
