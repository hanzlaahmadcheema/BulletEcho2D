# DeathEffectSpawner

## Purpose

This script creates a short particle effect when the player or an enemy dies. It ensures that each attached object can spawn its death effect only once.

**Status:** Active.

## Attached Scene Object

- `Player`
- `Enemy_01`
- `Enemy_02`
- `Enemy_03`

## Main Responsibility

- Death particle creation
- One-time effect protection
- Automatic particle cleanup

## Important Variables / Inspector Fields

| Field | Meaning | Example value |
| --- | --- | --- |
| `deathParticlesPrefab` | Particle prefab to create | Player or enemy death prefab |
| `spawned` | Prevents a second spawn | `false` initially |

## Main Functions

| Function | What it does | When it runs |
| --- | --- | --- |
| `SpawnDeathEffectOnce()` | Instantiates particles and schedules cleanup | Health script reports death |

## Function Signatures and Parameters

| Signature | Parameters | Return value |
| --- | --- | --- |
| `public void SpawnDeathEffectOnce()` | None | None |

The method has no parameter because it uses the attached object's current position and the assigned particle prefab.

## Important Code Lines

| Source lines | Code | Explanation |
| --- | --- | --- |
| `5` | `public ParticleSystem deathParticlesPrefab;` | Inspector reference to the correct death effect. |
| `7` | `private bool spawned;` | Stores whether this object already created its effect. |
| `11–14` | Duplicate guard | Prevents repeated particle spawning. |
| `16` | `spawned = true;` | Locks the effect before prefab validation. |
| `18–21` | Prefab guard | Safely exits if no particle prefab is assigned. |
| `23–27` | `Instantiate(...)` | Creates the effect at the dying object's world position with no rotation. |
| `29` | `particles.main` | Reads the Particle System main module. |
| `30` | Lifetime calculation | Adds system duration and maximum particle lifetime. |
| `31` | Delayed destroy | Cleans up the particle GameObject after the effect ends. |

## Execution Sequence

1. A health script calls `SpawnDeathEffectOnce()`.
2. The script checks whether it already spawned.
3. It locks the `spawned` flag.
4. It confirms that a prefab exists.
5. The prefab is instantiated at `transform.position`.
6. Effect lifetime is calculated.
7. Unity destroys the temporary effect after completion.

## How It Works In Game

The health script calls this method during death. The particle prefab is created at the dying object's position. The script calculates the effect duration and destroys the particle object after it finishes.

## Linked Scripts

- `PlayerHealth.Die()` calls `SpawnDeathEffectOnce()`.
- `EnemyHealth.Die()` calls `SpawnDeathEffectOnce()`.

## Linked Scene Objects

- `Player`
- `Enemy_01`
- `Enemy_02`
- `Enemy_03`
- `PlayerDeathParticles` prefab
- `EnemyDeathParticles` prefab

## Why This Approach Is Used

Lightweight death feedback

## Notes For Presentation

“This script creates a small particle burst when a character dies. It spawns only once and removes the effect automatically after its short lifetime.”
