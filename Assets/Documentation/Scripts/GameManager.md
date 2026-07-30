# GameManager

## Purpose

This script controls the central game state. It counts enemies and objectives, checks the win condition, handles losing, updates UI progress, and stops player control after the game ends.

**Status:** Active.

## Attached Scene Object

- `GameManager`

## Main Responsibility

- Central game state
- Enemy and objective counters
- Win condition
- Lose condition
- Stop gameplay after result

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `Instance` | Shared manager reference | Current `GameManager` |
| `uiManager` | UI script reference | `Canvas` component |
| `totalEnemies` | Enemy count found at start | `3` |
| `killedEnemies` | Enemies eliminated by player | Starts at `0` |
| `totalCash` | Objective count found at start | `5` |
| `collectedCash` | Objectives collected | Starts at `0` |
| `gameEnded` | Prevents gameplay after win or lose | Starts `false` |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `Awake()` | Sets the shared instance and resolves UI | Manager initialization |
| `Start()` | Counts enemies/objectives and initializes HUD counters | First gameplay frame |
| `RegisterEnemyKilled()` | Increases killed enemy count | Enemy dies |
| `CollectCash(int)` | Increases objective count | Objective is collected |
| `CheckWinCondition()` | Checks whether both required totals are complete | After kill or collection |
| `LoseGame()` | Sets the lose state and shows lose UI | Player dies |
| `ResolveUIManager()` | Finds or confirms the UI reference | UI access is needed |
| `DisablePlayerGameplay()` | Disables player movement and old attack | Win or lose occurs |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void Awake()` | None | None |
| `private void Start()` | None | None |
| `public void RegisterEnemyKilled()` | None | None |
| `public void CollectCash(int amount)` | `amount`: objective progress to add | None |
| `public void CheckWinCondition()` | None | None |
| `public void LoseGame()` | None | None |
| `private bool ResolveUIManager()` | None | `true` when a `UIManager` is available |
| `private void DisablePlayerGameplay()` | None | None |

`CollectCash(int amount)` is the only method with a parameter. Each current collectible passes `1`.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `5` | `public static GameManager Instance;` | Gives other scripts a shared reference to the active manager. |
| `18` | `Instance = this;` | Registers this component as the current manager. |
| `24` | Enemy object count | Finds every active `EnemyHealth` at startup. |
| `25` | Objective object count | Finds every active `CashCollectible` at startup. |
| `27–31` | Initial UI update | Displays starting enemy and objective totals. |
| `36–39` | Kill guard | Ignores progress after the game ends. |
| `41` | `killedEnemies++;` | Adds one eliminated enemy. |
| `43–46` | Enemy HUD update | Sends current and total enemy counts to UI. |
| `48` | `CheckWinCondition();` | Rechecks victory after every enemy death. |
| `51` | `CollectCash(int amount)` | Accepts flexible objective progress from collectibles. |
| `58` | `collectedCash += amount;` | Adds the supplied amount. |
| `60–63` | Objective HUD update | Refreshes collected objective text. |
| `70` | Combined win condition | Requires both enemy and objective totals. |
| `72` | `gameEnded = true;` | Locks the game result. |
| `73` | `DisablePlayerGameplay();` | Stops player controls before showing the result. |
| `77` | `uiManager.ShowWin();` | Displays the victory panel. |
| `84–87` | Lose guard | Prevents duplicate result handling. |
| `89–94` | Lose sequence | Locks the game, disables controls, and shows the lose panel. |
| `100–103` | UI fallback search | Finds `UIManager` if the Inspector reference is missing. |
| `105–108` | Boolean success result | Lets calling methods safely know whether UI is available. |
| `110–117` | One-time warning | Reports missing UI without flooding the console. |
| `124–125` | Player script search | Finds movement and old attack components. |
| `127–135` | Component disabling | Stops player movement and old attack. |

## Parameter Details

| Parameter | Type | Used by | Meaning |
| --- | --- | --- | --- |
| `amount` | `int` | `CollectCash(int amount)` | Number of objective points to add |

## Execution Sequence

### Startup

1. `Awake()` sets `Instance`.
2. UI reference is resolved.
3. `Start()` counts enemies and collectibles.
4. Starting progress is displayed.

### Progress

1. Enemy death calls `RegisterEnemyKilled()`, or collectible calls `CollectCash(1)`.
2. The relevant counter increases.
3. HUD text updates.
4. `CheckWinCondition()` runs.

### Result

1. Both totals complete, or player death calls `LoseGame()`.
2. `gameEnded` becomes true.
3. Player components are disabled.
4. The correct result panel is shown.

## How It Works In Game

At startup, the manager finds all enemies and collectibles. Enemy deaths and objective pickups report progress to it. After every progress change, it checks whether all four enemies and all five objectives are complete. It also receives the player death event.

## Linked Scripts

- Receives calls from `EnemyHealth`.
- Receives calls from `CashCollectible`.
- Receives lose calls from `PlayerHealth`.
- Calls `UIManager` update and panel methods.
- Disables `PlayerController` and `PlayerAttack`.
- Is read by player and enemy scripts through `gameEnded`.

## Linked Scene Objects

- `GameManager`
- `Canvas`
- `Player`
- `Enemies`
- `Cash`
- `WinPanel`
- `LosePanel`

## Why This Approach Is Used

Central game state

## Notes For Presentation

“GameManager is the central controller. It counts enemies and objectives, decides when the player wins or loses, updates the UI, and stops player controls after the result.”
