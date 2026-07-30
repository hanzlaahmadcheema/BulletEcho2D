# PlayerHealth

## Purpose

This script stores the player's health, reduces health after enemy shots, and handles player death. It also updates the health HUD and starts the lose flow.

**Status:** Active.

## Attached Scene Object

- `Player`

## Main Responsibility

- Player health
- Damage handling
- Health UI update
- Player death

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `maxHealth` | Maximum player health | `5` |
| `currentHealth` | Current health during gameplay | Starts at `5` |
| `deathHandled` | Prevents duplicate death handling | `false` initially |
| `IsDead` | Reports whether health is zero or death already ran | Runtime value |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `Awake()` | Sets current health to maximum health | Object initialization |
| `Start()` | Sends the starting health to the HUD | First gameplay frame |
| `TakeDamage(int)` | Reduces and clamps health, then checks death | Enemy successfully shoots |
| `KillInstantly()` | Sets health directly to zero | Available support method |
| `Heal(int)` | Adds health without exceeding maximum | If called by another system |
| `UpdateHealthUI()` | Sends current and maximum health to `UIManager` | After health changes |
| `Die()` | Spawns particles and tells `GameManager` to lose | Health reaches zero |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void Awake()` | None | None |
| `private void Start()` | None | None |
| `public void TakeDamage(int amount)` | `amount`: health points to remove | None |
| `public void KillInstantly()` | None | None |
| `public void Heal(int amount)` | `amount`: health points to add | None |
| `private void UpdateHealthUI()` | None | None |
| `private void Die()` | None | None |
| `public bool IsDead` | Property with no parameter | Returns `true` when dead |

`TakeDamage()` and `Heal()` accept positive integer amounts. `KillInstantly()` remains available as support code, but enemy shooting uses `TakeDamage(1)`.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `5` | `public int maxHealth = 5;` | Defines the starting and maximum health. |
| `10` | `deathHandled || currentHealth <= 0` | Treats zero health or completed death handling as dead. |
| `14` | `currentHealth = maxHealth;` | Resets health whenever the scene starts. |
| `24–27` | Damage guard | Rejects damage when dead, when the manager is missing, or after the game ends. |
| `29` | `Mathf.Max(currentHealth - amount, 0)` | Subtracts damage and prevents negative health. |
| `30` | `UpdateHealthUI();` | Refreshes the HUD immediately after damage. |
| `32–35` | Death check | Calls `Die()` only when the new health is zero. |
| `45–47` | Instant-kill path | Sets health to zero, updates UI, and handles death. |
| `57` | `Mathf.Min(..., maxHealth)` | Prevents healing above maximum health. |
| `65` | `UpdateHealth(currentHealth, maxHealth)` | Passes both values to `UIManager`. |
| `76` | `deathHandled = true;` | Prevents duplicate particles or duplicate lose calls. |
| `78–83` | Effect lookup and spawn | Uses the attached `DeathEffectSpawner` if available. |
| `87` | `GameManager.Instance.LoseGame();` | Starts the central lose flow. |

## Execution Sequence

1. `Awake()` sets health to 5.
2. `Start()` displays `5 / 5`.
3. `EnemyShooter` calls `TakeDamage(1)`.
4. Health is clamped and displayed.
5. If health remains above zero, gameplay continues.
6. At zero health, `Die()` runs once.
7. Player particles spawn.
8. `GameManager.LoseGame()` displays the lose result and stops gameplay.

## How It Works In Game

The player starts with 5 health. Each enemy shot calls `TakeDamage(1)`. The function updates the HUD after every shot. When health reaches zero, death particles appear and `LosePanel` is shown through the manager scripts.

## Linked Scripts

- `EnemyShooter` calls `TakeDamage(damage)`.
- `UIManager` receives health updates.
- `DeathEffectSpawner` creates player death particles.
- `GameManager.LoseGame()` starts the lose state.

## Linked Scene Objects

- `Player`
- `GameManager`
- `Canvas`
- `HealthText`
- `LosePanel`
- `PlayerDeathParticles` prefab

## Why This Approach Is Used

Central health control

## Notes For Presentation

“This script starts the player with five health. Enemy shots reduce health by one, update the HUD, and trigger the lose state only when health reaches zero.”
