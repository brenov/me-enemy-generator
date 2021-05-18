using System;
using System.Text.Json.Serialization;

namespace OverlordEnemyGenerator
{
    /// This class represents an individual.
    ///
    /// Individuals are composed of an enemy, a weapon, its fitness value, and
    /// the generation when it was created.
    ///
    /// Why individuals are represented by a class? When using MAP-Elites, some
    /// slots may be empty, then the `null` option makes easier to manage the
    /// MAP-Elites population.
    [Serializable]
    public class Individual
    {
        [JsonInclude]
        public Enemy enemy;
        [JsonInclude]
        public Weapon weapon;
        [JsonInclude]
        public float difficulty;
        [JsonInclude]
        public float fitness;
        [JsonInclude]
        public int generation;

        /// Individual contructor.
        public Individual(
            Enemy enemy,
            Weapon weapon
        ) {
            this.enemy = enemy;
            this.weapon = weapon;
        }

        /// Return a clone of the individual.
        ///
        /// We create a new individual by passing `enemy` and `weapon` by value
        /// in the Individual constructor because both are structs, therefore,
        /// we are able to copy them by value.
        public Individual Clone()
        {
            return new Individual(this.enemy, this.weapon);
        }

        /// Print the individual attributes.
        public void Debug()
        {
            Console.WriteLine("   " + this.generation);
            Console.WriteLine("   " + this.fitness);
            Console.WriteLine("   " + this.difficulty);
            Console.WriteLine("   " + this.enemy.health);
            Console.WriteLine("   " + this.enemy.strength);
            Console.WriteLine("   " + this.enemy.attackSpeed);
            Console.WriteLine("   " + this.enemy.movementType);
            Console.WriteLine("   " + this.enemy.movementSpeed);
            Console.WriteLine("   " + this.enemy.activeTime);
            Console.WriteLine("   " + this.enemy.restTime);
            Console.WriteLine("   " + this.weapon.weaponType);
            Console.WriteLine("   " + this.weapon.projectileType);
            Console.WriteLine("   " + this.weapon.projectileSpeed);
            Console.WriteLine();
        }

        /// Return a random individual.
        public static Individual GetRandom(
            Random rand,
            SearchSpace ss
        ) {
            // Create a random enemy
            Enemy e = new Enemy();
            e.health = Util.RandomInt(ss.rHealth, rand);
            e.strength = Util.RandomInt(ss.rStrength, rand);
            e.attackSpeed = Util.RandomFloat(ss.rAttackSpeed, rand);
            e.movementType = Util.RandomFromArray(ss.rMovementType, rand);
            e.movementSpeed = Util.RandomFloat(ss.rMovementSpeed, rand);
            e.activeTime = Util.RandomFloat(ss.rActiveTime, rand);
            e.restTime = Util.RandomFloat(ss.rRestTime, rand);
            // Create a random weapon
            Weapon w = new Weapon();
            w.weaponType = Util.RandomFromArray(ss.rWeaponType, rand);
            w.projectileType = Util.RandomFromArray(ss.rProjectileType, rand);
            w.projectileSpeed = Util.RandomFloat(ss.rProjectileSpeed, rand);
            // Create individual
            Individual individual = new Individual(e, w);
            individual.generation = -1;
            // Return the created individual
            return individual;
        }
    }

    /// This struct represents an enemy.
    [Serializable]
    public struct Enemy
    {
        [JsonInclude]
        public int health;
        [JsonInclude]
        public int strength;
        [JsonInclude]
        public float attackSpeed;
        [JsonInclude]
        public MovementType movementType;
        [JsonInclude]
        public float movementSpeed;
        [JsonInclude]
        public float activeTime;
        [JsonInclude]
        public float restTime;
    }

    // This struc represents a weapon.
    [Serializable]
    public struct Weapon
    {
        [JsonInclude]
        public WeaponType weaponType;
        [JsonInclude]
        public ProjectileType projectileType;
        [JsonInclude]
        public float projectileSpeed;
    }

    // This enum defines the movement types of enemies.
    [Serializable]
    public enum MovementType
    {
        None,     // The enemy stays still.
        Random,   // The enemy performs random 2D movements.
        Follow,   // The enemy follows the player.
        Flee,     // The enemy flees from the player.
        Random1D, // The enemy performs random horizontal or vertical movements.
        Follow1D, // The enemy follows the player horizontally or vertically.
        Flee1D,   // The enemy flees from the player horizontally or vertically.
    }

    /// This enum defines the types of weapons an enemy may have.
    [Serializable()]
    public enum WeaponType
    {
        None,    // The enemy attacks the player with barehands (Melee).
        Sword,   // The enemy uses a short sword to damage the player (Melee).
        Shotgun, // The enemy shots projectiles towards the player (Range).
        Cannon,  // The enemy shots bombs towards the player (Range).
        Shield,  // The enemy uses shields to defend themselves (Defense).
    }

    // This enum defines the projectile types of weapons.
    [Serializable]
    public enum ProjectileType
    {
        None,   // The weapon is not a projectile weapon.
        Bullet, // The weapon shots bullets.
        Bomb,   // The weapon shots bombs.
    }
}