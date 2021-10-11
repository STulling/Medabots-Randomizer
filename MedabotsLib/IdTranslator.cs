using System.Collections.Generic;

namespace MedabotsLib
{
    public static class IdTranslator
    {
        public enum Gender
        {
            Male,
            Female
        }
        public enum Character_Id
        {
            Ikki,
            Shadow, //1
            Erika, //2
            Karin, //3
            Koji, //4
            Salty, //5
            Samantha, //6
            Spyke, //7
            Sloan, //8
            Hachiro, //9
            Butler, //A
            Baron, //B
            Milky, //C
            P_Renegade, //D
            Henry, //E
            ROKUSHO, //F
            METABEE, //10
            Phantom_Lady, //11
            Kirara, //12
            Dr_Aki, //13
            Nae, //14
            Rintaro, //15
            Boy, //16
            Salaryman, //17
            Lady, //18
            Old_Man, //19
            Old_Woman, //1A
            Researcher, //1B
            Sasuke, //1C
            Goemon, //1D
            Hanzo, //1E
            Ninja, //1F
            Pop_Eye, //20
            Hermit, //21
            Girl, //22
            School_Girl, //23
            Office_Lady, //24
            Escort, //25
            Waitress, //26
            Girl_Ninja, //27
            Air_Medabot, //28
            Air_Medabot_f, //29
            Float_Medabot, //2A
            Float_Medabot_f, //2B
            Multi_Leg_Medabot, //2C
            Multi_Leg_Medabot_f, //2D
            Two_Leg_Medabot, //2E
            Two_Leg_Medabot_f, //2F
            Wheels_Medabot, //30
            Wheels_Medabot_f, //31
            Tank_Medabot, //32
            Tank_Medabot_f, //33
            Sea_Medabot, //34
            Sea_Medabot_f, //35
            Rubberobo_Gang, //36
            Seaslug, //37
            Gillgirl, //38
            Squidguts, //39
            Shrimplips, //3A
            Rubberobo_Kid, //3B
            Dr_Armond, //3C
            Select_3, //3D
            Select_Corps, //3E
            Awamori, //3F
            Tokkuri, //40
            Margarita, //41
            Cafe_Ole, //42
            Ancient_Boy, //43
            Ancient_Lady, //44
            Ancient_Elder, //45
            Kir, //46
            Shandy, //47
            Joe_Swihan, //48
            Spumoni, //49
            Tequonic, //4A
            Ryo, //4B
            Dark_Master, //4C
            Question, //4D
            Mr_Referee, //4E
            Dad, //4F
            Mom, //50
            Rappy, //51
            Mega_Emperor, //52
            Man, //53
            Woman, //54
            Young_Man, //55
            Young_Woman, //56
            Shop_Girl, //57
            Cook, //58
            Kawamura, //59
            Shiratama, //5A
            Babbyblu, //5B
            Insane_Medabot, //5C
            Ant_Medabot, //5D
            Guardian, //5E
            Ghost_Medabot, //5F
        };

        public enum Map_Id
        {
            Riverview_City,
            Riverview_Public_School___Courtyard,
            Riverview_Public_School___Ground_Floor,
            Riverview_Public_School___Ground_Left_Classroom,
            Riverview_Public_School___Ground_Middle_Classroom,
            Riverview_Public_School___Ground_Right_Upper_Classroom,
            Riverview_Public_School___Ground_Right_Lower_Classroom,
            Riverview_Public_School___Ground_Boys_Toilets,
            Riverview_Public_School___Teachers_Circle,
            Riverview_Public_School___Second_Floor,
            Riverview_Public_School___Second_Floor_Upper_Left_Classroom,
            Riverview_Public_School___Second_Floor_Upper_Middle_Classroom,
            Riverview_Public_School___Second_Floor_Lower_Left_Classroom,
            Riverview_Public_School___Second_Floor_Lower_Middle_Classroom,
            Riverview_Public_School___Music_Classroom,
            Riverview_Public_School___Girls_Toilets,
            Ikkis_house,
            Ikkis_house_upstairs,
            Erikas_house,
            Samanthas_house,
            Spykes_house,
            Sloans_house,
            Unknowns_house,
            Medabots_Research_Center___Entrance,
            Medabots_Research_Center___Backroom,
            Medabots_Research_Center___Akis_room,
            Medabots_Research_Center___Naes_room,
            Medabots_Research_Center___Naes_medal_room,
            Medabots_Museum,
            Old_Factory,
            Riverview_City_Market,
            Riverview_City_Docks,
            Docks_Warehouse,
            Mt_Odori____Right_side,
            Mt_Odori___Left_side,
            Mt_Odori___Troop_house,
            Odoro_Marsh___Down_one_floor,
            Odoro_Marsh___Entrance,
            Odoro_Marsh___Place_where_you_fight_Squidguts,
            Odoro_Marsh___Broken_parts_room,
            Mt_Odori___Outside_old_woman_house,
            Mt_Odori___Old_woman_house,
            Mt_Odori___Up_from_old_woman_house,
            Mt_Odori___Below_waterfall_area,
            Medabot_Island___Docks,
            Shark___Front,
            Medabot_Island___Right_square,
            Haunted_House___Entrance,
            Haunted_House___Hedge_Maze,
            Haunted_House___Mirror_Room,
            Haunted_House___Dark_Room,
            Haunted_House___Bunch_of_hedges,
            Haunted_House___Dark_Room_2,
            Haunted_House___Candle_Room,
            Haunted_House___Downstairs,
            Haunted_House___Exit,
            Medabot_Island___Arcade,
            Medabot_Island___Main_square,
            Medabot_Island___Shop,
            Medabot_Island___Diner,
            Medabot_Island___Troops_left,
            Medabot_Island___Troops_right,
            Medabot_Island___Left_square,
            Medabot_Island___Surprise_site,
            Medabot_Island___Surprise_site_arena,
            Medabot_Island___Surprise_site_left_lockers,
            Medabot_Island___Surprise_site_right_lockers,
            Medabot_Island___Medacoaster,
            Medabot_Island___Castle_Square,
            Castle___Dungeon,
            Castle___Entrance,
            Castle___Left_door,
            Castle___Right_door,
            Castle___After_Elevator,
            Castle___Sun_Room,
            Castle___Moon_Room,
            Castle___After_Elevator_2,
            Castle___Sun_Room_2,
            Castle___Moon_Room_2,
            Castle___Elevator,
            Castle___Downstairs,
            Castle___Even_more_downstairs,
            Castle___Inside_Ventilation,
            Castle___Downstairs_even_further,
            Castle___Laboratorium_Room,
            Medaropolis_Area_1,
            Medaropolis_Area_1___Snack_Bar,
            Medaropolis_Area_1___Medashop___Floor_1,
            Medaropolis_Area_1___Medashop___Floor_2,
            Medaropolis_Area_1___Medashop___Floor_3,
            Medaropolis_Area_1___Troop_Building_Left,
            Medaropolis_Area_1___Troop_Building_Right,
            Medaropolis_Area_1___Excellent_Building_Medapori,
            Medaropolis_Area_1___Medaro_Plaza,
            Medaropolis_Area_1___The_Star_Building,
            Medaropolis_Area_2,
            Medaropolis_Area_2___Medamall,
            Medaropolis_Area_2___Medamall_floor_2,
            Medaropolis_Area_2___Sky_Building,
            Medaropolis_Area_2___Medaro_Trading_Company,
            Medaropolis_Area_2___Big_Land_Sale_Going_On_Now,
            Medaropolis_Area_3,
            Rosewood_Private_School___Ground_Floor,
            Rosewood_Private_School___Ground_Floor___8___Classroom,
            Rosewood_Private_School___Ground_Floor___2___Classroom,
            Rosewood_Private_School___Ground_Floor___6___Classroom,
            Rosewood_Private_School___Ground_Floor___4___Classroom,
            Rosewood_Private_School___Ground_Floor___1___Classroom,
            Rosewood_Private_School___Ground_Floor___7___Boys_Toilet,
            Rosewood_Private_School___Ground_Floor___9___Music_Class,
            Rosewood_Private_School___Ground_Floor___3___Girls_toilet,
            Rosewood_Private_School___Ground_Floor___5___Courtyard,
            Rosewood_Private_School___Second_Floor,
            Rosewood_Private_School___Second_Floor___7___Teachers_Lounge,
            Rosewood_Private_School___Second_Floor___Medal_Room,
            Rosewood_Private_School___Second_Floor___9___Teachers_Circle,
            Rosewood_Private_School___Second_Floor___1___Classroom,
            Rosewood_Private_School___Second_Floor___2___Classroom_,
            Rosewood_Private_School___Second_Floor___3___Classroom,
            Medaropolis_Area_3___Karakuchi,
            Medaropolis_Area_3___Karakuchi__Second_Floor,
            Medaropolis_Area_3___Jyunmai,
            Medaropolis_Area_3___Awano,
            Medaropolis_Area_3___Awano___Second_Floor,
            Medaropolis_Area_3___Shop,
            Medaropolis_Area_3___I_dont_know_whos_house_this_is___Boy_on_the_pc,
            Medaropolis_Area_3___I_dont_know_whos_house_this_is___Girl_on_the_pc,
            Medaropolis_Area_3___I_dont_know_whos_house_this_is___Old_man_staring_at_TV,
            Medaropolis_Area_4,
            Medaropolis_Area_4___Estate_Risotto___Ground_Floor,
            Medaropolis_Area_4___Estate_Risotto___Second_Floor,
            Medaropolis_Area_4___Estate_Risotto___Ground_Floor_Left,
            Medaropolis_Area_4___Estate_Risotto___Ground_Floor_Right,
            Medaropolis_Area_4___Estate_Risotto___Second_Floor_Left,
            Medaropolis_Area_4___Estate_Risotto___Second_Floor_Right,
            Medaropolis_Area_4___Medabots_Corporation,
            Medaropolis_Area_4___Medabots_Corporation___Right,
            Medaropolis_Area_4___Medabots_Corporation___Left,
            Medaropolis_Area_4___Diner,
            Medaropolis_Area_4___Purple_Tower,
            Medaropolis_Area_4___South_Medaro_Building,
            Medaropolis_Area_4___Vending_Machine_Door_Building,
            Rubberobo_Secret_Base___Entrance,
            Rubberobo_Secret_Base___Dorm,
            Rubberobo_Secret_Base___Medal_Room,
            Rubberobo_Secret_Base___Ladder_to_surface,
            Rubberobo_Secret_Base___Down_the_ladder,
            Fort_Fiyun___Cave_Dorm,
            Fort_Fiyun___4_Chest_Room,
            Fort_Fiyun___Big_Cave_Maze,
            Kodine_Kingdom___Cutscene_Room,
            Kodine_Kingdom,
            Kodine_Kingdom___Entrance,
            Kodine_Kingdom___Ball_Room,
            Kodine_Kingdom___Sleeping_Quarters,
            Kodine_Kingdom___Bedroom,
            Kodine_Kingdom___Prison,
            Kodine_Kingdom___Shop,
            Kodine_Kingdom___House_upper_left,
            Kodine_Kingdom___House_lower_left,
            Kodine_Kingdom___House_lower_right,
            Kodine_Kingdom___Flower_Garden,
            Kodine_Kingdom___Water_Maze,
            Kodine_Kingdom___Mirage_Fish,
            Kodine_Kingdom___Little_island,
            Fort_Fiyun___Factory,
            Fort_Fiyun___Entrance,
            Fort_Fiyun___Farm,
            Fort_Fiyun___Flowerbeds,
            Fort_Fiyun___Dorm_room,
            Fort_Fiyun___Courtyard,
            Fort_Fiyun___Controls,
            Fort_Fiyun___Hexagon_Room,
            Ninja_Park___Main_Area,
            Ninja_Park___Ninja_House_Entrance_Hall_Ninja_Hide_and_Seek_House_Tour,
            Ninja_Park___Ninja_House_Left_Room,
            Ninja_Park___Ninja_House_Right_Room,
            Ninja_Park___Ninja_House_Right_Room_Upstairs,
            Ninja_Park___Northeast_House_Old_Man,_3x_Metabee_or_Rokusho,
            Ninja_Park___West_House_Master,
            Ninja_Park___East_House_Guy_or_kid,
            Ninja_Park___Northwest_House_Warehouse,
            Ninja_Park___Southeast_House_Tea_House,
            Ninja_Park___Southwest_House_Souvenir_Shop,
            Fort_Fiyun___Maze,
            Fort_Fiyun___Maze_2,
            Fort_Fiyun___Before_Hexagon_Room,
            Fort_Fiyun___Outside,
            Riverview_City___Docks_with_ship,
            Medabot_Island___Docks_with_ship,
            Medabot_Islane___Castle_Square,
            Medabot_Island___Castle_Square_2
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

        public enum Medal_Id
        {
            Kuwagata,
            Kabuto,
            Tortoise,
            Jellyfish,
            Bear,
            Spider,
            Snake,
            Queen,
            Squid,
            Phoenix,
            Unicorn,
            Ghost,
            Knight,
            Mermaid,
            Penguin,
            Bat,
            Kappa,
            Mouse,
            Chameleon,
            Rabbit,
            Monkey,
            Devil,
            Angel,
            Dragon,
            Ninja,
            Alien,
            Cat,
            Questionmark,
            Botro,
            Exclamationmark
        };

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

        public enum Bot
        {
            Noctobat,
            Wonder_Angel,
            Air_Ptera,
            Paradiver,
            Dragonfly,
            Crimson_King,
            Utopian,
            Windsail,
            Draken,
            Propolis,
            Phoenix,
            Femjet,
            Rappy,
            Churlybear,
            Haniwa,
            Gorem,
            Spitfire,
            Specter,
            Moai,
            Jorat,
            Sunwitch,
            Saldron,
            Redlace,
            Mistyghost,
            Volume10,
            Botafly,
            Rockflower,
            Babyblu,
            Spidar,
            Octoclam,
            Magdosnake,
            Floro,
            Mega_Emperor,
            Chimerator,
            Mantaprey,
            Poison_Copy,
            Robo_Emperor,
            Redmatador,
            Tentaclam,
            Acehorn,
            Multikolor,
            Kuraba,
            Rokusho,
            Metabee,
            Krosserdog,
            Turnmonkey,
            Zorin,
            Bayonet,
            Belzelga,
            Peppercat,
            Neutranurse,
            Brass,
            Sumilidon,
            Warbandit,
            Attack_Tyrano,
            Banisher,
            Stonemirror,
            Circulis,
            Pretty_Prime,
            Nin_Ninja,
            Foxuno,
            Kintaro,
            Agadama,
            Samurai,
            Cyandog,
            Wigwamo,
            Rabudo,
            Cosmo_Alien,
            Blackram,
            Sailormate,
            Auroraqueen,
            Icknite,
            Hopstar,
            Papyrak,
            Ambiguous,
            Greatmotha,
            Twinkle,
            Armorparadeen,
            Sabotina,
            Spiralle,
            Rollerman,
            Antacker,
            Antldier,
            Toybox,
            Face_Lantern,
            Landmotor,
            Stingray,
            Komandog,
            Magiclown,
            Tankar,
            Dr_Bokchoy,
            Snailoader,
            Cordy,
            Totalizer,
            Gentleheart,
            Landbrachio,
            Dashbutton,
            Snowbro,
            Soniktank,
            Giggly_Jelly,
            Shamen,
            Earthkrono,
            Gloomeg,
            Megaphant,
            Digmole,
            King_Pharaoh,
            Eggy,
            Aquamar,
            Kappalord,
            Flatstick,
            Fligflag,
            Orkamar,
            Aviking,
            Starpeda,
            Fireflash,
            Aquacrown,
            Sharkkan,
            Oceana,
            Pingen,
            Wolfeel,
        };

        public enum Speciality
        {
            Strike,
            Berserk,
            Shoot,
            Aim_shot,
            Defend,
            Heal,
            Support,
            Interrupt
        };

        public enum Technique
        {
            Sword,
            Hammer,
            Rifle,
            Chain_Gun,
            Laser,
            Beam,
            Missile,
            Napalm,
            Break,
            Press,
            Grap_Trap,
            Shot_Trap,
            Attack_Clr,
            Bug,
            Virus,
            Thunder,
            Freeze,
            Wave,
            Hold,
            Fire,
            Melt,
            Break_Form,
            Stability,
            Force_Bind,
            CancelOptic,
            CancelBomb,
            CancelGrav,
            Defense,
            Full_Block,
            Half_Block,
            Recovery,
            AutoRecover,
            Repair,
            Reactivate,
            Anti_Air,
            Anti_Sea,
            Scout,
            Conceal,
            Boost_Chrg,
            Rapid_Chrg,
            Accel_Chrg,
            Drain_Chrg,
            Pushover,
            Impair,
            Confusion,
            Use_Drain,
            No_Escape,
            No_Defense,
            Destroy,
            Sacrifice,
            Forcedrain,
            Team_Form,
            TeamAttack,
            Strengthen,
            CntrAttk,
            Change,
            ItrpChange,
            AttkChange,
            DEF_Change,
            HealChange
        };

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

        public static Dictionary<byte, string> opNames = new Dictionary<byte, string>()
        {
            { 0x0, "nop"},
            { 0x01, "Show_Message"},
            { 0x04, "Wait_X_Frames"},
            { 0x21, "Initiate_NPC??"},
            { 0x23, "NPC_Schedule_Walk??"},
            { 0x31, "Yes_or_No_Box"},
            { 0x33, "Start_Battle"},
            { 0x3C, "Get_Medal"},
            { 0x3D, "Get_Tinpet"},
            { 0x48, "Play_Music"},
            { 0x59, "Open_Shop"},
            { 0x62, "Equip_Starter_Parts"}
        };


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

        public static string[] song_names = new string[] {
            "None",
            "Theme song",
            "Medaforce, Medaparts, Medafighters",
            "Medashow",
            "Ending Theme",
            "Settings",
            "Medawatch",
            "Robattle Pre-Battle",
            "Battle Theme 1",
            "Battle Theme 2",
            "Battle Theme 3",
            "Rubberobo Boss Theme",
            "Final Battle Theme",
            "Robattle Victory",
            "Robattle Defeat",
            "Medal Level Up",
            "Robattle Time's Up",
            "Game Over",
            "Medarace",
            "Protect the Select Corp.",
            "RollerCoaster Medacoaster",
            "Harbor Boat Theme",
            "Medarace, Start!",
            "Link Menu",
            "Medal Evolution",
            "Medabots Laboratory",
            "Ninja Park",
            "Robattle Victory",
            "Home Town",
            "Home Theme",
            "Research Lab",
            "Medabots Island",
            "Cave Haunted House Theme",
            "Kodine Kingdom",
            "Mt.Odoro",
            "Rubberobo Research Lab",
            "Spaceship",
            "Witch's Castle",
            "More Power",
            "Erika",
            "Karin",
            "Koji",
            "The Screws",
            "Phantom Renegade",
            "Dr. Armond",
            "Encounter Rubberobo",
            "Encounter Select. Corps",
            "Encounter Rubberobo Boss",
            "School Theme",
            "MedaMart",
            "City",
            "MedaMall MedaShop"
        };
    }
}
