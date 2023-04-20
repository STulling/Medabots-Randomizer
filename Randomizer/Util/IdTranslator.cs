namespace MedabotsRandomizer.Util
{
    public static class IdTranslator
    {
        private static string[] characters = new string[]
        {
            "Ikki", //0
            "Shadow", //1
            "Erika", //2
            "Karin", //3
            "Koji", //4
            "Salty", //5
            "Samantha", //6
            "Spyke", //7
            "Sloan", //8
            "Hachiro", //9
            "Butler", //A
            "Baron", //B
            "Milky", //C
            "P. Renegade", //D
            "Henry", //E
            "ROKUSHO", //F
            "METABEE", //10
            "Phantom Lady", //11
            "Kirara", //12
            "Dr. Aki", //13
            "Nae", //14
            "Rintaro", //15
            "Boy", //16
            "Salaryman", //17
            "Lady", //18
            "Old Man", //19
            "Old Woman", //1A
            "Researcher", //1B
            "Sasuke", //1C
            "Goemon", //1D
            "Hanzo", //1E
            "Ninja", //1F
            "Pop Eye", //20
            "Hermit", //21
            "Girl", //22
            "School Girl", //23
            "Office Lady", //24
            "Escort", //25
            "Waitress", //26
            "Girl Ninja", //27
            "Air Medabot", //28
            "Air Medabot (f)", //29
            "Float Medabot", //2A
            "Float Medabot (f)", //2B
            "Multi-Leg Medabot", //2C
            "Multi-Leg Medabot (f)", //2D
            "2-Leg Medabot", //2E
            "2-Leg Medabot (f)", //2F
            "Wheels Medabot", //30
            "Wheels Medabot", //31
            "Tank Medabot", //32
            "Tank Medabot (f)", //33
            "Sea Medabot", //34
            "Sea Medabot (f)", //35
            "Rubberobo Gang", //36
            "Seaslug", //37
            "Gillgirl", //38
            "Squidguts", //39
            "Shrimplips", //3A
            "Rubberobo Kid", //3B
            "Dr. Armond", //3C
            "Select 3", //3D
            "Select Corps", //3E
            "Awamori", //3F
            "Tokkuri", //40
            "Margarita", //41
            "Cafe Ole", //42
            "Ancient Boy", //43
            "Ancient Lady", //44
            "Ancient Elder", //45
            "Kir", //46
            "Shandy", //47
            "Joe Swihan", //48
            "Spumoni", //49
            "Tequonic", //4A
            "Ryo", //4B
            "Dark Master", //4C
            "Question", //4D
            "Mr. Referee", //4E
            "Dad", //4F
            "Mom", //50
            "Rappy", //51
            "Mega-Emperor", //52
            "Man", //53
            "Woman", //54
            "Young Man", //55
            "Young Woman", //56
            "Shop Girl", //57
            "Cook", //58
            "Kawamura", //59
            "Shiratama", //5A
            "Babbyblu", //5B
            "Insane Medabot", //5C
            "Ant Medabot", //5D
            "Guardian", //5E
            "Ghost Medabot", //5F
        };
        public static string IdToCharacter(byte id)
        {
            return characters[id];
        }

        private static string[] maps = new string[]
        {
            "Riverview City",
            "Riverview Public School - Courtyard",
            "Riverview Public School - Ground Floor",
            "Riverview Public School - Ground Left Classroom",
            "Riverview Public School - Ground Middle Classroom",
            "Riverview Public School - Ground Right Upper Classroom",
            "Riverview Public School - Ground Right Lower Classroom",
            "Riverview Public School - Ground Boys Toilets",
            "Riverview Public School - Teachers Circle",
            "Riverview Public School - Second Floor",
            "Riverview Public School - Second Floor Upper Left Classroom",
            "Riverview Public School - Second Floor Upper Middle Classroom",
            "Riverview Public School - Second Floor Lower Left Classroom",
            "Riverview Public School - Second Floor Lower Middle Classroom",
            "Riverview Public School - Music Classroom",
            "Riverview Public School - Girls Toilets",
            "Ikki's house",
            "Ikki's house upstairs",
            "Erika's house",
            "Samantha's house",
            "Spyke's house",
            "Sloan's house",
            "???'s house",
            "Medabots Research Center - Entrance",
            "Medabots Research Center - Backroom",
            "Medabots Research Center - Aki's room",
            "Medabots Research Center - Nae's room",
            "Medabots Research Center - Nae's medal room",
            "Medabots Museum",
            "Old Factory",
            "Riverview City Market",
            "Riverview City Docks",
            "Docks Warehouse",
            "Mt. Odori -  Right side",
            "Mt. Odori - Left side",
            "Mt. Odori - Troop house",
            "Odoro Marsh - Down one floor",
            "Odoro Marsh - Entrance",
            "Odoro Marsh - Place where you fight Squidguts",
            "Odoro Marsh - Broken parts room",
            "Mt. Odori - Outside old woman house",
            "Mt. Odori - Old woman house",
            "Mt. Odori - Up from old woman house",
            "Mt. Odori - Below waterfall area",
            "Medabot Island - Docks",
            "Shark - Front",
            "Medabot Island - Right square",
            "Haunted House - Entrance",
            "Haunted House - Hedge Maze",
            "Haunted House - Mirror Room",
            "Haunted House - Dark Room",
            "Haunted House - Bunch of hedges",
            "Haunted House - Dark Room 2",
            "Haunted House - Candle Room",
            "Haunted House - Downstairs",
            "Haunted House - Exit",
            "Medabot Island - Arcade",
            "Medabot Island - Main square",
            "Medabot Island - Shop",
            "Medabot Island - Diner",
            "Medabot Island - Troops (left)",
            "Medabot Island - Troops (right)",
            "Medabot Island - Left square",
            "Medabot Island - Surprise site",
            "Medabot Island - Surprise site arena",
            "Medabot Island - Surprise site left lockers",
            "Medabot Island - Surprise site right lockers",
            "Medabot Island - Medacoaster",
            "Medabot Island - Castle Square",
            "Castle - Dungeon",
            "Castle - Entrance",
            "Castle - Left door",
            "Castle - Right door",
            "Castle - After Elevator",
            "Castle - Sun Room",
            "Castle - Moon Room",
            "Castle - After Elevator 2",
            "Castle - Sun Room 2",
            "Castle - Moon Room 2",
            "Castle - Elevator",
            "Castle - Downstairs",
            "Castle - Even more downstairs",
            "Castle - Inside Ventilation",
            "Castle - Downstairs even further",
            "Castle - Laboratorium Room",
            "Medaropolis Area 1",
            "Medaropolis Area 1 - Snack Bar",
            "Medaropolis Area 1 - Medashop - Floor 1",
            "Medaropolis Area 1 - Medashop - Floor 2",
            "Medaropolis Area 1 - Medashop - Floor 3",
            "Medaropolis Area 1 - Troop Building Left",
            "Medaropolis Area 1 - Troop Building Right",
            "Medaropolis Area 1 - Excellent Building Medapori",
            "Medaropolis Area 1 - Medaro Plaza",
            "Medaropolis Area 1 - The Star Building",
            "Medaropolis Area 2",
            "Medaropolis Area 2 - Medamall",
            "Medaropolis Area 2 - Medamall floor 2",
            "Medaropolis Area 2 - Sky Building",
            "Medaropolis Area 2 - Medaro Trading Company",
            "Medaropolis Area 2 - Big Land Sale Going On Now",
            "Medaropolis Area 3",
            "Rosewood Private School - Ground Floor",
            "Rosewood Private School - Ground Floor - 8 - Classroom",
            "Rosewood Private School - Ground Floor - 2 - Classroom",
            "Rosewood Private School - Ground Floor - 6 - Classroom",
            "Rosewood Private School - Ground Floor - 4 - Classroom",
            "Rosewood Private School - Ground Floor - 1 - Classroom",
            "Rosewood Private School - Ground Floor - 7 - Boys Toilet",
            "Rosewood Private School - Ground Floor - 9 - Music Class",
            "Rosewood Private School - Ground Floor - 3 - Girls toilet",
            "Rosewood Private School - Ground Floor - 5 - Courtyard",
            "Rosewood Private School - Second Floor",
            "Rosewood Private School - Second Floor - 7 - Teachers Lounge",
            "Rosewood Private School - Second Floor - Medal Room",
            "Rosewood Private School - Second Floor - 9 - Teachers Circle",
            "Rosewood Private School - Second Floor - 1 - Classroom",
            "Rosewood Private School - Second Floor - 2 - Classroom ",
            "Rosewood Private School - Second Floor - 3 - Classroom",
            "Medaropolis Area 3 - Karakuchi",
            "Medaropolis Area 3 - Karakuchi- Second Floor",
            "Medaropolis Area 3 - Jyunmai",
            "Medaropolis Area 3 - Awano",
            "Medaropolis Area 3 - Awano - Second Floor",
            "Medaropolis Area 3 - Shop",
            "Medaropolis Area 3 - \"I don't know who's house this is\" - Boy on the pc",
            "Medaropolis Area 3 - \"I don't know who's house this is\" - Girl on the pc",
            "Medaropolis Area 3 - \"I don't know who's house this is\" - Old man staring at TV",
            "Medaropolis Area 4",
            "Medaropolis Area 4 - Estate \"Risotto\" - Ground Floor",
            "Medaropolis Area 4 - Estate \"Risotto\" - Second Floor",
            "Medaropolis Area 4 - Estate \"Risotto\" - Ground Floor Left",
            "Medaropolis Area 4 - Estate \"Risotto\" - Ground Floor Right",
            "Medaropolis Area 4 - Estate \"Risotto\" - Second Floor Left",
            "Medaropolis Area 4 - Estate \"Risotto\" - Second Floor Right",
            "Medaropolis Area 4 - Medabots Corporation",
            "Medaropolis Area 4 - Medabots Corporation - Right",
            "Medaropolis Area 4 - Medabots Corporation - Left",
            "Medaropolis Area 4 - Diner",
            "Medaropolis Area 4 - Purple Tower",
            "Medaropolis Area 4 - South Medaro Building",
            "Medaropolis Area 4 - Vending Machine Door Building",
            "Rubberobo Secret Base - Entrance",
            "Rubberobo Secret Base - Dorm",
            "Rubberobo Secret Base - Medal Room",
            "Rubberobo Secret Base - Ladder to surface",
            "Rubberobo Secret Base - Down the ladder",
            "Fort Fiyun - Cave Dorm",
            "Fort Fiyun - 4 Chest Room",
            "Fort Fiyun - Big Cave Maze",
            "Kodine Kingdom - Cutscene Room",
            "Kodine Kingdom",
            "Kodine Kingdom - Entrance",
            "Kodine Kingdom - Ball Room",
            "Kodine Kingdom - Sleeping Quarters",
            "Kodine Kingdom - Bedroom",
            "Kodine Kingdom - Prison",
            "Kodine Kingdom - Shop",
            "Kodine Kingdom - House upper left",
            "Kodine Kingdom - House lower left",
            "Kodine Kingdom - House lower right",
            "Kodine Kingdom - Flower Garden",
            "Kodine Kingdom - Water Maze",
            "Kodine Kingdom - Mirage Fish",
            "Kodine Kingdom - Little island",
            "Fort Fiyun - Factory",
            "Fort Fiyun - Entrance",
            "Fort Fiyun - Farm",
            "Fort Fiyun - Flowerbeds",
            "Fort Fiyun - Dorm room",
            "Fort Fiyun - Courtyard",
            "Fort Fiyun - Controls",
            "Fort Fiyun - Hexagon Room",
            "Ninja Park - Main Area",
            "Ninja Park - Ninja House Entrance Hall (\"Ninja Hide & Seek House Tour\")",
            "Ninja Park - Ninja House Left Room",
            "Ninja Park - Ninja House Right Room",
            "Ninja Park - Ninja House Right Room Upstairs",
            "Ninja Park - Northeast House (Old Man, 3x Metabee/Rokusho)",
            "Ninja Park - West House (Master)",
            "Ninja Park - East House (Guy/kid)",
            "Ninja Park - Northwest House (Warehouse)",
            "Ninja Park - Southeast House (Tea House)",
            "Ninja Park - Southwest House (Souvenir Shop)",
            "Fort Fiyun - Maze",
            "Fort Fiyun - Maze",
            "Fort Fiyun - Before Hexagon Room",
            "Fort Fiyun - Outside",
            "Riverview City - Docks with ship",
            "Medabot Island - Docks with ship",
            "Medabot Island - Castle Square",
            "Medabot Island - Castle Square"
        };

        private static bool[] is_female = new bool[]
        {
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            false,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            false,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            true,
            true,
            false,
            false,
            true,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            false,
            false,
            false,
            true,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            true,
            true,
            false,
            true,
            true,
            false
        };

        internal static bool isFemale(byte randomBot)
        {
            return is_female[randomBot];
        }

        public static string IdToMap(byte id)
        {
            return maps[id];
        }

        public static string[] medals = new string[]
        {
            "Kuwagata",
            "Kabuto",
            "Tortoise",
            "Jellyfish",
            "Bear",
            "Spider",
            "Snake",
            "Queen",
            "Squid",
            "Phoenix",
            "Unicorn",
            "Ghost",
            "Knight",
            "Mermaid",
            "Penguin",
            "Bat",
            "Kappa",
            "Mouse",
            "Chameleon",
            "Rabbit",
            "Monkey",
            "Devil",
            "Angel",
            "Dragon",
            "Ninja",
            "Alien",
            "Cat",
            "?",
            "Botro",
            "!"
        };

        public static string IdToMedal(byte id)
        {
            return medals[id];
        }

        private static string[,] parts = new string[,]
        {
            {"Psycho Missile","Electo Missile","Sonic Missile","Grave"},
            {"Cherub Body","Cherub Hand","Cherub Arm","Cherub Leg"},
            {"Hidden Brake","Pocket Sword","Microsword","Skyleg"},
            {"Eagle Belt","Hawk Hand","Swan Arm","Turkey Leg"},
            {"Hardweighter","Heavyweighter","Veryweighter","Flyfly"},
            {"Peck Strike","Dondon Punch","Dopa Punch","Wanafly"},
            {"Phanta","Obstruct","Disruptor","Dreamer"},
            {"Changa-Head","Changa-Hand","Changa-Arm","Changa-fly"},
            {"Appealsound","Sidemuffler","Silencer","Singer"},
            {"Hexadon","Mistouch","Stinger","Insecto-Wings"},
            {"Blastgun","Fire Gun","Flame Gun","Red Tail"},
            {"Cockpit","Stabilizer","Wing","Jet Engine"},
            {"Fame Mascot","Bubble Blow","Balloon Blow","Airline"},
            {"Spreader","Smasher","Splasher","Anti-Grav"},
            {"Clay Flash","Hand Flash","Arm Flash","Teacup"},
            {"Head Shutter","Hand Shutter","Arm Shutter","Blow Up"},
            {"Drago Crystal","Curewater","Repair Spring","Dragonbed"},
            {"White Cap","White Long Arm","White Whale","White Belt"},
            {"Stonecluster","Handcluster","Armcluster","Tripod"},
            {"Full Moused","Scout","Spotter","Mover"},
            {"Majoram","Two Cotton","Boomerang","Clean Box"},
            {"Sala-Head","Sala-Hand","Sala-Arm","Sala-Tail"},
            {"Hat Trick","Globe Trick","Bangle Trick","Floater"},
            {"Void","Avoid","Exit","Panic"},
            {"Larynx","Sound Copy","Return Sound","Pipe Organ"},
            {"Field Barrier","Energy Barrier","Force Barrier","Flower Barrier"},
            {"Blank Look","Leaves","Stem","Fiyun Ball"},
            {"Fracture","Past Touch","Past Feel","Umbilical"},
            {"Spydertrap","Cheapertrap","Cheaptrap","Multi-Leg"},
            {"Hatchin","Catch","Twist","Swick"},
            {"Cobrara","Changer","Snakebite","Slither"},
            {"Sun-Beam","Go-Up","Rise-Up","Stem-Up"},
            {"Deathbreak","Deathmissile","Deathlaser","Deathcrawler"},
            {"Lionattack","Dragonattack","Goatattack","Triple Thread"},
            {"Kataki","Kamakubi","Kubikiri","Kamaki"},
            {"Scorpion Dog","Scorpion Cat","Scorpion Rat","Zigzag"},
            {"Deathblast","Deathbomb","Deathbeam","Spaghetti"},
            {"Body Attack","Saber","Mant Shield","Dash Attack"},
            {"Headband","Wire","Net","Octoleg"},
            {"Guardian","Cancellor","Recovery","Ace Hooves"},
            {"Invisi Body","Invisi Hand","Invisi Arm","Twirl"},
            {"Gun Krab","Decoy Krab","Real Krab","Sidejump"},
            {"Antenna","Sword","Pipo Hammer","Tatacker"},
            {"Missile","Revolver","Submachingun","Ochitsuka"},
            {"Head Cannon","Aim Rifle","Battle Rifle","Howzer"},
            {"Downstream","Countfall","Timefall","Pita Pata"},
            {"Broad Antenna","Shin-Sword","Bee-Bee Hammer","Tatakawa"},
            {"Reaction Bomb","Revolven","Sumachingun","Ochitsukan"},
            {"Helmet","Helmight","Helming","Helcaos"},
            {"Lightcircuit","Lightjump","Light Blow","Quick Alert"},
            {"Holy Helm","Donor","Translate","Petticoat"},
            {"Variablehair","Pateri Vulcan","Short Shot","Flaregather"},
            {"Hunter","Flexsorsword","Straw Hammer","Sharpedge"},
            {"Tension Up","Shoot Barrel","Range Shooter","Abductor"},
            {"Breathfire","Crab-Hit","Strike-Hit","Attach Leg"},
            {"Sweat Mantle","Sweat Cape","Sweat Cloak","Klip Klop"},
            {"Death Grin","Death Beckon","Death Curl","Tail End"},
            {"Charmee","Charmy Hand","Charmy Arm","Charmy Leg"},
            {"Reflect Mirror","Don't Move","Shine Shield","Temple Master"},
            {"Cover-Up","Ninja Dagger","Ninja Sword","Tiptoe"},
            {"Foxarmor","Fencing","Rapier","Hakama"},
            {"Sumo Press","Light Axe","Great Axe","Kinta Leg"},
            {"Air Bag","Spin Knuckle","Megaton Punch","Chi Leg"},
            {"Samurai Blast","Beam Saber","Samurai Saber","Samurai Kick"},
            {"Linear Cannon","Sniper Rifle","Assault","Gunner"},
            {"Acid Con","Acid Bomb","Acid Rain","Repeatastep"},
            {"Aircon","Stopwatch","Fan","Hop"},
            {"Mute Body","Mute Hand","Mute Arm","Mute Leg"},
            {"Devil Body","Devil Hand","Devil Arm","Devil Leg"},
            {"Fight Oneshot","Machinegun","Magnum","Mini-Scout"},
            {"Blizzard","Ice","Freeze","Sled"},
            {"Charm Point","Explosive Ball","Hand Grenade","Tabi"},
            {"Bodycon","Hand Whip","Arm Candy","High Leg"},
            {"Feeler","Metakiller","Thudarm","Tatakick"},
            {"Mis-Sile","Thunder Hit","Gato Ring","Falldown"},
            {"Greet Con","Zeta Complete","Zeta Perfect","Zeta Entire"},
            {"Beauty Mask","C-Right","C-Left","Tamofoot"},
            {"Clear Armor","Right Armor","Left Armor","Rocking Horse"},
            {"Sombrero","Spiky Gun","Needle Gun","Right-A-Way"},
            {"Maxima Pouch","Maxima Sword","Maxima Drill","First Wave"},
            {"Arma-Head","Arma-Hand","Arma-Arm","Side Winder"},
            {"Batori Stand","Cannonshell","Cannonball","Fortress"},
            {"Marcus","Shooter","Archer","Sagittari Base"},
            {"Toymander","Blast Rod","Yo-Yo","Kendama"},
            {"Pan","Pun","Keen","Squashbasher"},
            {"Bodshot","Anti-Missile","Long Cannon","Plasma Dash"},
            {"Plasma Laser","Track Laser","Chase Laser","Shadow"},
            {"Morningcall","Right Beam","Left Beam","Dog Race"},
            {"Finale","Carnival","Festival","Balance-Ball"},
            {"Victory Armor","Vulcan","Gatling Gun","Armor Car"},
            {"Search Radar","Point Radar","Cut Radar","Chairly"},
            {"Bat-Charge","Charger","Generator","Snail"},
            {"Mushroom","Shitake","Maitake","Fungoton"},
            {"Tyranolaser","Megalaser","Gigalaser","Rollertank"},
            {"Shotclear","Grade Hammer","Gold Hammer","Big Boots"},
            {"Anti-Skyer","Rave-Luster","Ray-Masher","Land-Leg"},
            {"Switching","Right-Ing","Leftup","Burly"},
            {"Flip","Flap","Flop","Flavor"},
            {"Blasto","Burning Pin","Burst Needle","Block Tread"},
            {"Missile Base","Intermissile","Guidemissile","Limptank"},
            {"Misstep","Slipstart","Slipend","Slipped"},
            {"Spare Head","Spare Hand","Spare Arm","Spare Tank"},
            {"Tank Missile","Homing","Grenade","Block Tank"},
            {"Trample","Right Guard","Great Shield","Chauffeur"},
            {"Coverup","Trapbuster","Barrierbuster","Shovel"},
            {"Grand Bomb","Black Hall","Dark Hall","Sit Down"},
            {"Omelette","Hamlette","Ham-Egg","Half-Egg"},
            {"Scope Head","Scope Hand","Scope Arm","Scope Leg"},
            {"Plate Beam","Underwater Flo","Undersea Flo","Anti-Stand"},
            {"Slipper","Accident","Troubler","Speartail"},
            {"Pop-N-Eye","Amphibi-Hand","Amphibi-Arm","Amphibi-Leg"},
            {"New Wave","Clinch Wave","Nibble Wave","Fishtail"},
            {"Enticed","Burst","Pro-Fence","Depth-Sole"},
            {"Hamster Bite","Starnet","Blaze Shield","Starfish"},
            {"Watergun","Shower","Klench","Luminous"},
            {"Sacrifice Head","Sacrifice Hand","Sacrifice Arm","Sacrifice Fin"},
            {"Power Driver","Plus Driver","Minus Driver","Smacker"},
            {"Allrepair","Cure Hand","Repair Arm","Purple Fins"},
            {"Reza-Rector","Free Arm","New Arm","Fin"},
            {"Concealing","Konpinch","Konplier","Duck Under"},
            {"No Head","No Right Arm","No Right Arm","No Legs"}
        };

        public static string IdToPart(byte id, int part)
        {
            return parts[id, part];
        }

        public static string[] bots = new string[]
        {
            "Noctobat",
            "Wonder Angel",
            "Air-Ptera",
            "Paradiver",
            "Dragonfly",
            "Crimson King",
            "Utopian",
            "Windsail",
            "Draken",
            "Propolis",
            "Phoenix",
            "Femjet",
            "Rappy",
            "Churlybear",
            "Haniwa",
            "Gorem",
            "Spitfire",
            "Specter",
            "Moai",
            "Jorat",
            "Sunwitch",
            "Saldron",
            "Redlace",
            "Mistyghost",
            "Volume10",
            "Botafly",
            "Rockflower",
            "Babyblu",
            "Spidar",
            "Octoclam",
            "Magdosnake",
            "Floro",
            "Mega-Emperor",
            "Chimerator",
            "Mantaprey",
            "Poison Copy",
            "Robo-Emperor",
            "Redmatador",
            "Tentaclam",
            "Acehorn",
            "Multikolor",
            "Kuraba",
            "Rokusho",
            "Metabee",
            "Krosserdog",
            "Turnmonkey",
            "Zorin",
            "Bayonet",
            "Belzelga",
            "Peppercat",
            "Neutranurse",
            "Brass",
            "Sumilidon",
            "Warbandit",
            "Attack-Tyrano",
            "Banisher",
            "Stonemirror",
            "Circulis",
            "Pretty Prime",
            "Nin-Ninja",
            "Foxuno",
            "Kintaro",
            "Agadama",
            "Samurai",
            "Cyandog",
            "Wigwamo",
            "Rabudo",
            "Cosmo-Alien",
            "Blackram",
            "Sailormate",
            "Auroraqueen",
            "Icknite",
            "Hopstar",
            "Papyrak",
            "Ambiguous",
            "Greatmotha",
            "Twinkle",
            "Armorparadeen",
            "Sabotina",
            "Spiralle",
            "Rollerman",
            "Antacker",
            "Antldier",
            "Toybox",
            "Face Lantern",
            "Landmotor",
            "Stingray",
            "Komandog",
            "Magiclown",
            "Tankar",
            "Dr.Bokchoy",
            "Snailoader",
            "Cordy",
            "Totalizer",
            "Gentleheart",
            "Landbrachio",
            "Dashbutton",
            "Snowbro",
            "Soniktank",
            "Giggly Jelly",
            "Shamen",
            "Earthkrono",
            "Gloomeg",
            "Megaphant",
            "Digmole",
            "King Pharaoh",
            "Eggy",
            "Aquamar",
            "Kappalord",
            "Flatstick",
            "Fligflag",
            "Orkamar",
            "Aviking",
            "Starpeda",
            "Fireflash",
            "Aquacrown",
            "Sharkkan",
            "Oceana",
            "Pingen",
            "Wolfeel",
            ""
        };

        public static string IdToBot(byte id)
        {
            return bots[id];
        }

        private static string[] speciality = new string[]
        {
            "Strike",
            "Berserk",
            "Shoot",
            "Aim shot",
            "Defend",
            "Heal",
            "Support",
            "Interrupt"
        };

        public static string IdToSpeciality(byte id)
        {
            return speciality[id];
        }

        private static string[] techniques = new string[]
        {
            "Sword",
            "Hammer",
            "Rifle",
            "Chain Gun",
            "Laser",
            "Beam",
            "Missile",
            "Napalm",
            "Break",
            "Press",
            "Grap Trap",
            "Shot Trap",
            "Attack Clr",
            "Bug",
            "Virus",
            "Thunder",
            "Freeze",
            "Wave",
            "Hold",
            "Fire",
            "Melt",
            "Break Form",
            "Stability",
            "Force Bind",
            "CancelOptic",
            "CancelBomb",
            "CancelGrav",
            "Defense",
            "Full Block",
            "Half Block",
            "Recovery",
            "AutoRecover",
            "Repair",
            "Reactivate",
            "Anti-Air",
            "Anti-Sea",
            "Scout",
            "Conceal",
            "Boost Chrg",
            "Rapid Chrg",
            "Accel Chrg",
            "Drain Chrg",
            "Pushover",
            "Impair",
            "Confusion",
            "Use Drain",
            "No Escape",
            "No Defense",
            "Destroy",
            "Sacrifice",
            "Forcedrain",
            "Team Form",
            "TeamAttack",
            "Strengthen",
            "CntrAttk",
            "Change",
            "ItrpChange",
            "AttkChange",
            "DEF Change",
            "HealChange"
        };

        public static string IdToTechnique(byte id)
        {
            return techniques[id];
        }

        private static byte[] bestMedal = new byte[]
        {
            15,
            22,
            0,
            22,
            4,
            21,
            20,
            25,
            10,
            6,
            9,
            15,
            22,
            4,
            11,
            11,
            13,
            20,
            11,
            17,
            19,
            25,
            5,
            12,
            3,
            5,
            19,
            10,
            5,
            8,
            6,
            19,
            4,
            9,
            0,
            9,
            1,
            0,
            8,
            10,
            18,
            0,
            0,
            1,
            1,
            20,
            0,
            1,
            21,
            7,
            13,
            1,
            0,
            1,
            0,
            24,
            21,
            19,
            24,
            0,
            0,
            0,
            0,
            0,
            1,
            9,
            19,
            25,
            21,
            1,
            7,
            3,
            19,
            0,
            1,
            14,
            1,
            12,
            1,
            0,
            12,
            23,
            23,
            4,
            20,
            15,
            2,
            2,
            3,
            1,
            17,
            19,
            20,
            2,
            5,
            15,
            12,
            7,
            3,
            3,
            20,
            25,
            3,
            12,
            10,
            4,
            25,
            16,
            2,
            6,
            18,
            8,
            21,
            0,
            16,
            25,
            4,
            13,
            14,
            24,
        };

        public static byte botMedal(byte bot)
        {
            return bestMedal[bot];
        }

        public static byte[] operationBytes = new byte[] {
        1, //0
        4, //1
        4, //2
        1, //3
        2, //4
        1, //5
        1, //6
        2, //7
        2, //8
        2, //9
        2, //A
        6, //B
        6, //C
        2, //D
        1, //E
        2, //F
        5, //10
        2, //11
        3, //12
        3, //13
        1, //14
        1, //15
        3, //16
        4, //17
        3, //18
        3, //19
        3, //1A
        1, //1B
        2, //1C
        1, //1D
        2, //1E
        6, //1F
        6, //20
        5, //21
        3, //22
        3, //23
        3, //24
        2, //25
        3, //26
        4, //27
        2, //28
        2, //29
        2, //2A
        6, //2B
        2, //2C
        3, //2D
        4, //2E
        5, //2F
        3, //30
        2, //31
        2, //32
        4, //33
        4, //34
        2, //35
        3, //36
        3, //37
        4, //38
        4, //39
        3, //3A
        3, //3B
        2, //3C
        2, //3D
        3, //3E
        3, //3F
        2, //40
        2, //41
        1, //42
        3, //43
        2, //44
        2, //45
        1, //46 
        1, //47
        2, //48
        1, //49
        3, //4A
        4, //4B
        4, //4C
        3, //4D
        4, //4E
        2, //4F
        4, //50
        3, //51
        3, //52
        3, //53
        1, //54
        1, //55
        1, //56
        1, //57
        1, //58
        2, //59
        2, //5A
        2, //5B
        3, //5C
        2, //5D
        2, //5E
        2, //5F
        4, //60
        1, //61
        1, //62
        4, //63
        1, //64
        1, //65
        3, //66
        3, //67
        3, //68
        3, //69
        1, //6A
        2, //6B
        6, //6C
        3, //6D
        6, //6E
        3, //6F
        1, //70
        1, //71
        1, //72
        1, //73
        1, //74
        1, //75
        1, //76
        1, //77
        3, //78
        2, //79
        1, //7A
        4, //7B
        1, //7C
        2, //7D
        2, //7E
        2, //7F
        1, //80
        9, //81
        1,
        3,
        2,
        1,
        9,
        1,
        3};
    }
}
