# UIManager

## Purpose

This script controls the simple text HUD, result messages, and game buttons. It updates health, objective, and enemy text, displays the correct result, and provides restart and end-game actions.

**Status:** Active.

## Attached Scene Object

- `Canvas`

## Main Responsibility

- HUD text updates
- Win panel control
- Lose panel control
- Runtime restart and end-game buttons
- Decorative canvas element cleanup
- UI reference recovery

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `healthText` | Displays player health | `HealthText` |
| `cashText` | Displays objective progress | `CashText` |
| `enemyText` | Displays enemy progress | `EnemyText` |
| `winPanel` | Victory result panel | `WinPanel` |
| `losePanel` | Defeat result panel | `LosePanel` |
| Warning flags | Prevent repeated missing-reference warnings | `false` initially |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `Awake()` | Resolves references and hides result panels | Canvas initialization |
| `UpdateHealth()` | Writes current and maximum health text | Player health changes |
| `UpdateCash()` | Writes collected and total objective text | Objective progress changes |
| `UpdateEnemies()` | Writes killed and total enemy text | Enemy progress changes |
| `ShowWin()` | Activates `WinPanel` | Win condition succeeds |
| `ShowLose()` | Activates `LosePanel` | Player loses |
| `RestartGame()` | Reloads the active scene | Restart button is pressed |
| `EndGame()` | Stops Play mode or exits the build | End Game button is pressed |
| `CreateGameButtons()` | Creates the two simple text buttons | Canvas initialization |
| `ResolveReferences()` | Finds missing UI children by name | Before UI use |
| `FindChildText()` | Finds a named text child | Reference recovery |
| `FindChildObject()` | Finds a named child object | Reference recovery |
| `WarnOnce()` | Logs one warning for a missing reference | Reference is unavailable |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `private void Awake()` | None | None |
| `public void UpdateHealth(int current, int max)` | `current`: current HP; `max`: maximum HP | None |
| `public void UpdateCash(int collected, int total)` | `collected`: obtained objectives; `total`: required objectives | None |
| `public void UpdateEnemies(int killed, int total)` | `killed`: eliminated enemies; `total`: total enemies | None |
| `public void ShowWin()` | None | None |
| `public void ShowLose()` | None | None |
| `private void ResolveReferences()` | None | None |
| `private Text FindChildText(string childName)` | `childName`: direct child name | Found `Text` or `null` |
| `private GameObject FindChildObject(string childName)` | `childName`: direct child name | Found GameObject or `null` |
| `private void WarnOnce(ref bool alreadyWarned, string message)` | Warning flag by reference and warning text | None |

The `ref` keyword in `WarnOnce()` allows the method to change the original warning flag stored by `UIManager`.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `6–10` | Public UI references | Connect the script to three HUD texts and two result panels. |
| `20` | `ResolveReferences();` | Recovers missing references before initialization. |
| `22–38` | Panel initialization | Hides win and lose panels, or warns if missing. |
| `41` | Health method signature | Receives current and maximum health separately. |
| `47` | Health text format | Produces `HP current / max`. |
| `55` | Objective method signature | Receives collected and total objectives. |
| `61` | Objective text format | Produces `CASH collected / total`. |
| `69` | Enemy method signature | Receives killed and total enemies. |
| `75` | Enemy text format | Produces `TARGETS killed / total`. |
| `87–90` | Win activation | Enables `WinPanel`. |
| `101–104` | Lose activation | Enables `LosePanel`. |
| `113–136` | Reference recovery | Searches specific direct children only when references are null. |
| `139–143` | Text lookup | Finds a child transform and returns its `Text` component. |
| `145–149` | Object lookup | Finds and returns a child GameObject. |
| `151` | `ref bool alreadyWarned` | Receives the actual stored warning flag. |
| `153–159` | Warning lock | Logs a warning once and prevents repeats. |

## Parameter Details

| Function | Parameter | Type | Meaning |
| --- | --- | --- | --- |
| `UpdateHealth` | `current` | `int` | Remaining player health |
| `UpdateHealth` | `max` | `int` | Maximum player health |
| `UpdateCash` | `collected` | `int` | Objectives already collected |
| `UpdateCash` | `total` | `int` | Total objectives required |
| `UpdateEnemies` | `killed` | `int` | Enemies already eliminated |
| `UpdateEnemies` | `total` | `int` | Total enemies in the level |
| `FindChildText` | `childName` | `string` | Exact direct child name |
| `FindChildObject` | `childName` | `string` | Exact direct child name |
| `WarnOnce` | `alreadyWarned` | `ref bool` | Persistent warning state |
| `WarnOnce` | `message` | `string` | Console warning message |

## Execution Sequence

1. `Awake()` resolves all references.
2. Result panels are hidden.
3. Health and manager scripts call public update methods.
4. Each update method resolves references again for safety.
5. The required text string is assigned.
6. Win or lose methods activate the required panel.
7. Missing references produce only one warning per field.

## How It Works In Game

The script hides decorative panels and icons, leaving a plain text HUD. It creates Restart and End Game buttons at the bottom of the canvas. Other scripts send updated values to its public methods, and win or lose activates the required text result.

## Linked Scripts

- `PlayerHealth` calls `UpdateHealth()`.
- `GameManager` calls `UpdateCash()` and `UpdateEnemies()`.
- `GameManager` calls `ShowWin()` and `ShowLose()`.

## Linked Scene Objects

- `Canvas`
- `HealthText`
- `CashText`
- `EnemyText`
- `WinPanel`
- `LosePanel`

## Why This Approach Is Used

Clean UI control

## Notes For Presentation

“UIManager keeps all HUD and result-panel updates in one script. Other systems send it values, and it displays health, objectives, enemy progress, win, and lose information.”
