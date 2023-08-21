# Health and Damage System

## Damage

The DamageInstance struct represents a damage instance with its amount and critical hit status.

### Properties

Amount: The amount of damage dealt.<br>
IsCriticalHit: Indicates whether the damage is a critical hit.<br>
DamageDealer: The entity dealing the damage.<br>

### Methods

DamageInstance(IDamageDealer damageDealer): Constructor that creates a damage instance based on the provided damage dealer's properties.<br>

## DamageOverTime
The DamageOverTime class adds damage-over-time effects to a Damageable entity.

### Methods

AddDotDamage(tickInterval: float, duration: float, damageDealer: IDamageDealer): Adds damage-over-time effects to the entity.<br>
DotRoutine(tickInterval: float, duration: float, damageDealer: IDamageDealer): Coroutine that applies damage-over-time effects over a specified duration.<br>

## DamageProperties

The DamageProperties class defines properties for damage, critical hit, and damage type.

### Properties
MinAttack: The minimum damage value.<br>
MaxAttack: The maximum damage value.<br>
DamageType: The type of damage.<br>
CriticalHitChance: The chance of landing a critical hit.<br>
CriticalHitDamage: The bonus damage on a critical hit.<br>

## IDamageDealer

The IDamageDealer interface defines the methods required for a damage dealer.

### Properties
DamageProperties: Gets the damage properties of the damage dealer.<br>

## DamageReport
The DamageReport struct represents the outcome of a damage instance against a defense.

### Properties

DamageInstance: The original damage instance.<br>
Defense: The defense used to mitigate the damage.<br>
DamageAmount: The final amount of damage inflicted.<br>
Blocked: The amount of damage blocked by defense.<br>

### Methods

DamageReport(DamageInstance damageInstance, Defense defense): Constructor that calculates the damage report based on damage instance and defense.<br>

## Resistance

The Resistance class defines resistance values for different damage types.

### Properties

damageType: The type of damage.<br>
amount: The amount of resistance.<br>

## Defense

The Defense class manages the resistances and defends against incoming damage instances.

### Properties

Resistances: Dictionary of damage type and corresponding resistance.<br>
ResistanceUpdatedEvent: Event triggered when a resistance is updated.<br>

### Methods

AddResistance(resistance: Resistance): Adds or updates a resistance in the defense.<br>
GetResistancePercentageVs(type: DamageType): Gets the resistance percentage against a specific damage type.<br>
DefendAgainst(damageInstance: DamageInstance): Creates a damage report based on the incoming damage instance and defense.<br>

## Health
The Health class manages an entity's health, healing, and damage taking.

### Properties

Current: The current health value.<br>
Max: The maximum health value.<br>
IsFull: Indicates whether health is at maximum.<br>
IsDead: Indicates whether health is zero or below.<br>
LastDamaged: The time of the last damage taken.<br>

### Methods

Heal(healing: float): Heals the entity by the provided amount.<br>
TakeDamage(damage: float): Inflicts damage on the entity.<br>
SetMaxHealth(newMax: float): Sets the maximum health value.<br>

## HealthRegeneration

The HealthRegeneration class adds health regeneration to a Damageable entity.

### Properties

TickInterval: The interval at which regeneration ticks.<br>
PercentMaxHealthToHeal: The percentage of max health to heal per tick.<br>
RegenDelay: The delay before regeneration starts.<br>

### Methods

StartRegen(): Starts the health regeneration coroutine.<br>
StopRegen(): Stops the health regeneration coroutine.<br>

## DamageIndicator

The DamageIndicator class displays a damage indicator above an entity.

### Methods

SetDamage(damageReport: DamageReport): Sets the damage report to display.<br>
ShowDamageIndicator(): Displays the damage indicator with animations.<br>

## DamageIndicatorListener

The DamageIndicatorListener class listens for damage events to spawn damage indicators.

### Methods

SpawnIndicator(damage: DamageReport): Spawns a damage indicator at the specified location.<br>

## DamageIndicatorPool
The DamageIndicatorPool class manages a pool of damage indicators.

### Methods

SpawnIndicator(worldPos: Vector3, damageInstance: DamageReport): Spawns a damage indicator at the specified position.

## HealthBar
The HealthBar class displays the health of a Damageable entity as a UI slider.

### Properties

slider: The UI slider component.<br>
damageable: The Damageable entity to track.<br>

## DotStatusEffect
The DotStatusEffect class applies a damage-over-time status effect to a Damageable entity.

### Properties

tickInterval: The interval at which damage ticks.<br>
duration: The duration of the status effect.<br>
DamageProperties: The damage properties of the status effect.<br>

## StatusEffect

The StatusEffect class defines a base for status effects.

### Methods
ApplyStatus(statusTarget: IStatusAffected): Applies the status effect to a target.<br>

## IStatusEffect
The IStatusEffect interface defines the methods required for a status effect.

### Methods

ApplyStatus(statusAffected: IStatusAffected): Applies the status effect to a target.

## StatusHandler
The StatusHandler class handles status effects on an entity.

### Properties

GameObject: The game object to which the status handler is attached.

## IStatusAffected
The IStatusAffected interface defines the properties required for an entity affected by status effects.

### Properties
GameObject: The game object of the affected entity.