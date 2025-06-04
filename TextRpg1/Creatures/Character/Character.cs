namespace Gridfall_Depths.Creatures.Character;

using System;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Gridfall_Depths.Creatures;
using Gridfall_Depths.Creatures.Enemies;
using Gridfall_Depths.Items;
using Gridfall_Depths.Locations;
using static Gridfall_Depths.UI.UI;
using static Randomizer;

internal class Character : Creature
{
    public bool IsDead;
    [JsonIgnore]
    public override double Damage
    {
        get
        {
            if (Equipment["Weapon"] is Weapon weapon) return Math.Round(_damage + weapon.Damage);
            else return Math.Round(_damage);
        }
        set
        {
            _damage = value;
        }
    }
    public override double Armor
    {
        get
        {
            if (Equipment["Armor"] is Armor armor)
                return _armor + armor.ProtectivePower;
            else return _armor;
        }
        set { _armor = value; }
    }
    [JsonProperty]
    int _hunger;
    [JsonIgnore]
    public int Hunger
    {
        get { return _hunger; }
        set
        {
            if (!IsDead) _hunger = value;
            if (_hunger < 0) _hunger = 0;
            if (_hunger >= 100)
            {
                MessagesStack.Add(("Fatal hunger.", ConsoleColor.Red));
                if(Thirst + LatestTick >= 100) MessagesStack.Add(("Fatal thirst.", ConsoleColor.Red));
                if (!IsDead) Die();
            }
        }
    }
    [JsonProperty]
    int _thirst;
    [JsonIgnore]
    public int Thirst
    {
        get { return _thirst; }
        set 
        {
            if (!IsDead) _thirst = value;
            if (_thirst < 0) _thirst = 0;
            if (_thirst >= 100)
            {
                MessagesStack.Add(("Fatal thirst.", ConsoleColor.Red));
                if (Hunger >= 100) MessagesStack.Add(("Fatal hunger.", ConsoleColor.Red));
                if (!IsDead) Die();
            }
        }
    }
    static int LatestTick = 0;

    public Dictionary<string, Item?> Equipment { get; set; } = new Dictionary<string, Item?>
    {
        {"Armor", null },
        {"Weapon", null }
    };

    [JsonConstructor]
    public Character()
    {

    }
    public Character(string name, PrimaryAttributes primaryAttributes, Location homeLocation) : base(name, primaryAttributes, homeLocation)
    {
        
    }
    
    public static Character CreateCharacter(Location homeLocation)
    {
        string? name;
        bool IsNameValid = false;

        PrimaryAttributes primaryAttributes = new PrimaryAttributes();

        do
        {

            Console.WriteLine("Enter your character name. From 2 to 15 letters.");
            name = Console.ReadLine();
            if (name != null && Regex.IsMatch(name, @"^[a-zA-Zа-яА-ЯёЁ]{2,15}$")) IsNameValid = true;

            else Console.WriteLine("The name format is incorrect. Please retry.");
        }
        while (!IsNameValid);

        int totalAttriburePoints = 60;
        while (totalAttriburePoints > 0)
        {
            totalAttriburePoints = 60;
            Console.WriteLine("Distribute 60 attribute points. " +
                              "You can put from 1 to 20 points into each characteristic. " +
                              "10 - represents an average person");

            do
            {
                Console.WriteLine("Enter the Strength value. Responsible for physical impact and carrying capacity.");// заменить описание
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Strength = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"{totalAttriburePoints} points left.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Input error. Please enter a number between 1 and 20.");
                else Console.WriteLine("Not enough points.");
            }
            while (primaryAttributes.Strength == 0);

            do
            {
                Console.WriteLine("Enter the Dexterity value. Responsible for speed, coordination and flexibility.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Agility = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"{totalAttriburePoints} points left.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Input error. Please enter a number between 1 and 20.");
                else Console.WriteLine("Not enough points.");
            }
            while (primaryAttributes.Agility == 0);

            do
            {
                Console.WriteLine("Enter the Endurance value. Responsible for health, stamina and resilience to negative physical effects and damage.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Endurance = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"{totalAttriburePoints} points left.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Input error. Please enter a number between 1 and 20.");
                else Console.WriteLine("Not enough points.");
            }
            while (primaryAttributes.Endurance == 0);

            do
            {
                Console.WriteLine("Enter the Intelligence value. Responsible for the experience gained, the speed of growth of skills and abilities. Also, it directly affects the character's ability to think and his memory.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Intelligence = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"{totalAttriburePoints} points left.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Input error. Please enter a number between 1 and 20.");
                else Console.WriteLine("Not enough points.");
            }
            while (primaryAttributes.Intelligence == 0);

            do
            {
                Console.WriteLine("Enter the Perception value. Responsible for the efficiency of the senses. Directly affects accuracy and reaction.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Perception = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"{totalAttriburePoints} points left.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Input error. Please enter a number between 1 and 20.");
                else Console.WriteLine("Not enough points.");
            }
            while (primaryAttributes.Perception == 0);

            do
            {
                Console.WriteLine("Enter the Willpower value. Responsible for mental fortitude and self-control. Also it influences most of your actions to varying degrees.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Willpower = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"{totalAttriburePoints} points left.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Input error. Please enter a number between 1 and 20.");
                else Console.WriteLine("Not enough points.");
            }
            while (primaryAttributes.Willpower == 0);

            if (totalAttriburePoints > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have not allocated all the attribute points. Please re-enter."); //заменить на распределение оставшихся очков. Также добавить предложение повторить распределение.
                Console.WriteLine();
                Console.ResetColor();
            }
        }

        return new Character(name, primaryAttributes, homeLocation);
        
    }

    public static Character CreateMiddleCharacter(Location homeLocation)
    {
        PrimaryAttributes primaryAttributes = new PrimaryAttributes(10, 10, 10, 10, 10, 10);
        return new Character("Vakoal", primaryAttributes, homeLocation);
    }
    public void ChangeLocation(Location targetLocation)
    {
        this.CurrentLocation = targetLocation;
        targetLocation.IsVisited = true;

        if (targetLocation.Neighbors.Count == 1 && targetLocation.Type != LocationType.HomeBase)
            targetLocation.GenerateNeighbors();

        if(!targetLocation.Habitants.Any() && targetLocation.Type != LocationType.HomeBase && !targetLocation.IsCleared)
            targetLocation.GenerateHabitants();

        MessagesStack.Add(($"You are entering location {targetLocation}", ConsoleColor.Gray));

        Tick(5);
    }
    public void LootLocation()
    {
        if (CurrentLocation.Loot.Count == 0)
        {
            MessagesStack.Add(("No items to loot in this location", ConsoleColor.Gray));
            return;
        }
        
        while (CurrentLocation.Loot.Count > 0)
        {
            int choice = ShowMenu<Item>(CurrentLocation.Loot, "You find some items:", ["Take all", "Take nothing"]) - 1;
            if (choice == CurrentLocation.Loot.Count)   //"Take all
            {
                foreach (Item item in CurrentLocation.Loot)
                    Inventory.Add(item);

                CurrentLocation.Loot.Clear();

                Tick(5);
                return;
            }
            else if (choice == CurrentLocation.Loot.Count + 1)  //Take nothing
            {
                Tick(5);
                return;
            }

            Inventory.Add(CurrentLocation.Loot[choice]);    //Particular item chosen
            CurrentLocation.Loot.RemoveAt(choice);

            Tick(5);
        }
    }
    public override void Die()
    {
        CurrentLocation = HomeLocation;
        Equipment["Armor"] = null;
        Equipment["Weapon"] = null;
        Inventory.Clear();
        IsDead = true;
        MessagesStack.Add(($"You died and were revived at your home base. You have lost all your inventory and equippment.", ConsoleColor.Yellow));
    }
    public override void ShowInfo()
    {
        base.ShowInfo();

        Console.WriteLine($"CurrentLocation: {CurrentLocation}");
        Console.WriteLine();
    }
    internal void Fight()
    {
        var Enemies = CurrentLocation.Habitants.Where(h => h is Enemy);
        if (!Enemies.Any())
        {
            MessagesStack.Add(("No enemies to fight", ConsoleColor.Gray));
            return;
        }

        MessagesStack.Add(($"You are fighting against {Enemies.Count()} enemies.", ConsoleColor.Red));

        if (Enemies.Max(c => c.PrimaryAttributes.Perception + c.PrimaryAttributes.Agility) >= (PrimaryAttributes.Perception + PrimaryAttributes.Agility))
            Enemies.ToList()[RandomInt(0, Enemies.Count() - 1)].AttackEnemy(this);

        if (Health == 0)
        {
            Tick(5);
            return;
        }

        bool fighting = true;
        while (Enemies.Any() && fighting)
        {
            ShowStatusInfo(this);
            IsDefended = false;

           int actionChoice = ShowMenu(["Attack", "Defend", "Open Inventory", "Flee"], "Your turn. Choose your action:");

            switch(actionChoice)
            {
                case 1:
                    int enemyChoice = ShowMenu(Enemies.ToList(), "Choose an enemy to attack:", ["Cancel"]) - 1;

                    if (enemyChoice == Enemies.Count()) continue;

                    else AttackEnemy(Enemies.ToList()[enemyChoice]); break;

                case 2:
                    IsDefended = true; break;

                case 3:
                    OpenInventory(); break;

                case 4:
                    var fastestEnemy = Enemies.OrderByDescending(e => e.PrimaryAttributes.Agility + e.PrimaryAttributes.Perception / 2).FirstOrDefault();
                    if (PrimaryAttributes.Agility + PrimaryAttributes.Perception / 2 + RandomInt(-5, 5) >= fastestEnemy.PrimaryAttributes.Agility + fastestEnemy.PrimaryAttributes.Perception / 2)
                    {
                        ChangeLocation(CurrentLocation.Neighbors[0]);
                        MessagesStack.Add(("You have successfully escaped from the enemy and returned to previous location.", ConsoleColor.Yellow));
                        fighting = false;

                        Tick(5); 
                        return; 
                    }
                    else
                    {
                        MessagesStack.Add(("You failed to escape. Enemy is attacking!", ConsoleColor.Red));
                        break;
                    }
            }

            foreach (var enemy in Enemies)
            {
                enemy.AttackEnemy(this);
                MessagesStack.Add(("", ConsoleColor.Gray));

                if (IsDead)
                {
                    MessagesStack.Add(($"You died and were revived at your home base. You lost all your inventory and equipment.", ConsoleColor.Yellow));
                    fighting = false;

                    Tick(5);
                    return;
                }
            }
        }
        CurrentLocation.IsCleared = true;
        MessagesStack.Add(($"You won the fight! Location cleared.", ConsoleColor.Blue));

        Tick(5);
    }
    internal void SpeakTo()
    {
        var speakableNPCs = (List<NPC>)CurrentLocation.Habitants.Where(h => h is NPC npc && npc.IsSpeakable);
        if (speakableNPCs.Count > 1)
        {
            int NPCToSpeakChoice = ShowMenu(speakableNPCs, "Select NPC you want to talk:", ["Cancel"]);

            if (NPCToSpeakChoice == speakableNPCs.Count) return;

            speakableNPCs[NPCToSpeakChoice - 1].Dialogue();
        }

        Tick(1);
    }
    internal void OpenInventory()
    {
        if (Inventory.Count == 0)
        {
            MessagesStack.Add(("Your inventory is empty", ConsoleColor.Yellow));
            return;
        }

        int categoryChoice = ShowMenu(Item.Types, "Choose category:") - 1;
        var categoryItems = Inventory.Where(i => i.GetType() == Item.Types[categoryChoice]).ToList();

        if (categoryItems.Count == 0)
        {
            MessagesStack.Add(($"You have no {Item.Types[categoryChoice].Name.ToLower()} items", ConsoleColor.Yellow));
            return;
        }

        int itemChoice = ShowMenu(categoryItems, "Choose item to interact", ["Close inventory"]) - 1;

        if (itemChoice == categoryItems.Count) return;

        List<string> itemActions = ["Throw Away"];
        if (categoryItems[itemChoice] is Food || categoryItems[itemChoice] is Medicine) itemActions.Insert(0, "Use");

        if (categoryItems[itemChoice] is Weapon || categoryItems[itemChoice] is Armor)
            if (!categoryItems[itemChoice].IsEquipped) itemActions.Insert(0, "Equip");
            else itemActions.Insert(0, "Unequip");

        int itemInteractionChoice = ShowMenu(itemActions, "") - 1;

        switch (itemInteractionChoice)
        {
            case 0:
                if (itemActions[0] == "Use")
                {
                    categoryItems[itemChoice].Use(this);
                    break;
                }
                else
                {
                    categoryItems[itemChoice].EquipOrUnequip(this);
                    break;
                }
            case 1:
                categoryItems[itemChoice].ThrowAway(this); break;

            default: MessagesStack.Add(("Something went wrong while opening inventory...",ConsoleColor.Yellow)); break;
        }
    }
    internal void Save()
    {
        string jsonSave = JsonConvert.SerializeObject(this,
            new JsonSerializerSettings
            {
                Formatting = Formatting.Indented,
                PreserveReferencesHandling = PreserveReferencesHandling.All,
                NullValueHandling = NullValueHandling.Include,
                TypeNameHandling = TypeNameHandling.Auto
            });
        File.WriteAllText("characterSave.json", jsonSave);
    }
    internal static Character Load()
    {
        string jsonLoad = File.ReadAllText("characterSave.json");
        Character character = JsonConvert.DeserializeObject<Character>(jsonLoad, new JsonSerializerSettings
        {
            Formatting = Formatting.Indented,
            PreserveReferencesHandling = PreserveReferencesHandling.All,
            NullValueHandling = NullValueHandling.Include,
            TypeNameHandling = TypeNameHandling.Auto
        });

        //reassigning values to the first neighbors neighbor, habitants home location and current location due to their missing after loading game.
        Location location = character.HomeLocation;

        location.Type = LocationType.HomeBase;
        foreach (Creature habitant in location.Habitants)
        {
            habitant.CurrentLocation = location;
            habitant.HomeLocation = location;
        }

        do
        {
            if (location.Type.Name == LocationType.HomeBase.Name)
            {
                for (int i = 0; i < location.Neighbors.Count; i++)
                {
                    location.Neighbors[i].Neighbors[0] = location;

                    foreach (Creature habitant in location.Neighbors[i].Habitants)
                    {
                        habitant.CurrentLocation = location.Neighbors[i];
                        habitant.HomeLocation = location.Neighbors[i];
                    }
                }
            }
            else
            {
                for (int i = 1; i < location.Neighbors.Count; i++)
                {
                    location.Neighbors[i].Neighbors[0] = location;

                    foreach (Creature habitant in location.Neighbors[i].Habitants)
                    {
                        habitant.CurrentLocation = location.Neighbors[i];
                        habitant.HomeLocation = location.Neighbors[i];
                    }
                }
            }

            location = location.Neighbors.First(l => l.IsDeadEnd == false && l != location.Neighbors[0]);

        } while (location.Neighbors.Count > 1);

        character.HomeLocation.Habitants.Clear();

        return character;
    }

    internal void Tick(int tickValue = 1)
    {
        LatestTick = tickValue;
        Hunger += tickValue;
        Thirst += tickValue;
    }
}
