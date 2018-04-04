using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace SteamStatsApp
{
    public sealed class HardCodedGameFetcher : IAvailableGamesFetcher
    {        
        public async Task<IEnumerable<Game>> FetchGamesAsync()
        {
            var obj = await Task.Run(() => JsonConvert.DeserializeObject<SomeObjectDao>(gamesList));

            return obj.Values.Select(ConvertDaoToGame).ToList();            
        }

        private Game ConvertDaoToGame(GameDao dao)
        {
            return new Game()
            {
                Name = dao.Name.Value,
                Id = dao.Id
            };
        }

        private sealed class SomeObjectDao
        {
            public List<GameDao> Values { get; set; }
        }

        private sealed class GameDao
        {
            public string Id { get; set; }

            public Name Name { get; set; }
        }

        private sealed class Name
        {
            public string Value { get; set; }
        }

        private readonly string gamesList = @"{""values"":[
                        {
                            ""id"": ""578080"",
                            ""name"": {
                                ""value"": ""PLAYERUNKNOWN'S BATTLEGROUNDS""
                            }
                        },
                        {
                            ""id"": ""570"",
                            ""name"": {
                                ""value"": ""Dota 2""
                            }
                        },
                        {
                            ""id"": ""730"",
                            ""name"": {
                                ""value"": ""Counter-Strike: Global Offensive""
                            }
                        },
                        {
                            ""id"": ""271590"",
                            ""name"": {
                                ""value"": ""Grand Theft Auto V""
                            }
                        },
                        {
                            ""id"": ""346110"",
                            ""name"": {
                                ""value"": ""ARK: Survival Evolved""
                            }
                        },
                        {
                            ""id"": ""359550"",
                            ""name"": {
                                ""value"": ""Tom Clancy's Rainbow Six Siege""
                            }
                        },
                        {
                            ""id"": ""440"",
                            ""name"": {
                                ""value"": ""Team Fortress 2""
                            }
                        },
                        {
                            ""id"": ""230410"",
                            ""name"": {
                                ""value"": ""Warframe""
                            }
                        },
                        {
                            ""id"": ""252950"",
                            ""name"": {
                                ""value"": ""Rocket League""
                            }
                        },
                        {
                            ""id"": ""624090"",
                            ""name"": {
                                ""value"": ""Football Manager 2018""
                            }
                        },
                        {
                            ""id"": ""238960"",
                            ""name"": {
                                ""value"": ""Path of Exile""
                            }
                        },
                        {
                            ""id"": ""218620"",
                            ""name"": {
                                ""value"": ""PAYDAY 2""
                            }
                        },
                        {
                            ""id"": ""252490"",
                            ""name"": {
                                ""value"": ""Rust""
                            }
                        },
                        {
                            ""id"": ""4000"",
                            ""name"": {
                                ""value"": ""Garry's Mod""
                            }
                        },
                        {
                            ""id"": ""8930"",
                            ""name"": {
                                ""value"": ""Sid Meier's Civilization V""
                            }
                        },
                        {
                            ""id"": ""227300"",
                            ""name"": {
                                ""value"": ""Euro Truck Simulator 2""
                            }
                        },
                        {
                            ""id"": ""289070"",
                            ""name"": {
                                ""value"": ""Sid Meier's Civilization VI""
                            }
                        },
                        {
                            ""id"": ""377160"",
                            ""name"": {
                                ""value"": ""Fallout 4""
                            }
                        },
                        {
                            ""id"": ""107410"",
                            ""name"": {
                                ""value"": ""Arma 3""
                            }
                        },
                        {
                            ""id"": ""105600"",
                            ""name"": {
                                ""value"": ""Terraria""
                            }
                        },
                        {
                            ""id"": ""444090"",
                            ""name"": {
                                ""value"": ""Paladins""
                            }
                        },
                        {
                            ""id"": ""304930"",
                            ""name"": {
                                ""value"": ""Unturned""
                            }
                        },
                        {
                            ""id"": ""72850"",
                            ""name"": {
                                ""value"": ""The Elder Scrolls V: Skyrim""
                            }
                        },
                        {
                            ""id"": ""10"",
                            ""name"": {
                                ""value"": ""Counter-Strike""
                            }
                        },
                        {
                            ""id"": ""433850"",
                            ""name"": {
                                ""value"": ""H1Z1""
                            }
                        },
                        {
                            ""id"": ""292030"",
                            ""name"": {
                                ""value"": ""The Witcher 3: Wild Hunt""
                            }
                        },
                        {
                            ""id"": ""550"",
                            ""name"": {
                                ""value"": ""Left 4 Dead 2""
                            }
                        },
                        {
                            ""id"": ""476600"",
                            ""name"": {
                                ""value"": ""Call of Duty: WWII - Multiplayer""
                            }
                        },
                        {
                            ""id"": ""236850"",
                            ""name"": {
                                ""value"": ""Europa Universalis IV""
                            }
                        },
                        {
                            ""id"": ""435150"",
                            ""name"": {
                                ""value"": ""Divinity: Original Sin 2""
                            }
                        },
                        {
                            ""id"": ""482730"",
                            ""name"": {
                                ""value"": ""Football Manager 2017""
                            }
                        },
                        {
                            ""id"": ""394360"",
                            ""name"": {
                                ""value"": ""Hearts of Iron IV""
                            }
                        },
                        {
                            ""id"": ""221380"",
                            ""name"": {
                                ""value"": ""Age of Empires II: HD Edition""
                            }
                        },
                        {
                            ""id"": ""594570"",
                            ""name"": {
                                ""value"": ""Total War: WARHAMMER II""
                            }
                        },
                        {
                            ""id"": ""489830"",
                            ""name"": {
                                ""value"": ""The Elder Scrolls V: Skyrim Special Edition""
                            }
                        },
                        {
                            ""id"": ""236390"",
                            ""name"": {
                                ""value"": ""War Thunder""
                            }
                        },
                        {
                            ""id"": ""381210"",
                            ""name"": {
                                ""value"": ""Dead by Daylight""
                            }
                        },
                        {
                            ""id"": ""251570"",
                            ""name"": {
                                ""value"": ""7 Days to Die""
                            }
                        },
                        {
                            ""id"": ""582660"",
                            ""name"": {
                                ""value"": ""Black Desert Online""
                            }
                        },
                        {
                            ""id"": ""306130"",
                            ""name"": {
                                ""value"": ""The Elder Scrolls Online: Tamriel Unlimited""
                            }
                        },
                        {
                            ""id"": ""413150"",
                            ""name"": {
                                ""value"": ""Stardew Valley""
                            }
                        },
                        {
                            ""id"": ""374320"",
                            ""name"": {
                                ""value"": ""DARK SOULS™ III""
                            }
                        },
                        {
                            ""id"": ""281990"",
                            ""name"": {
                                ""value"": ""Stellaris""
                            }
                        },
                        {
                            ""id"": ""255710"",
                            ""name"": {
                                ""value"": ""Cities: Skylines""
                            }
                        },
                        {
                            ""id"": ""386360"",
                            ""name"": {
                                ""value"": ""SMITE""
                            }
                        },
                        {
                            ""id"": ""365590"",
                            ""name"": {
                                ""value"": ""Tom Clancy's The Division""
                            }
                        },
                        {
                            ""id"": ""427520"",
                            ""name"": {
                                ""value"": ""Factorio""
                            }
                        },
                        {
                            ""id"": ""49520"",
                            ""name"": {
                                ""value"": ""Borderlands 2""
                            }
                        },
                        {
                            ""id"": ""322330"",
                            ""name"": {
                                ""value"": ""Don't Starve Together""
                            }
                        },
                        {
                            ""id"": ""504370"",
                            ""name"": {
                                ""value"": ""Battlerite""
                            }
                        },
                        {
                            ""id"": ""644930"",
                            ""name"": {
                                ""value"": ""They Are Billions""
                            }
                        },
                        {
                            ""id"": ""447020"",
                            ""name"": {
                                ""value"": ""Farming Simulator 17""
                            }
                        },
                        {
                            ""id"": ""582160"",
                            ""name"": {
                                ""value"": ""Assassin's Creed Origins""
                            }
                        },
                        {
                            ""id"": ""359320"",
                            ""name"": {
                                ""value"": ""Elite Dangerous""
                            }
                        },
                        {
                            ""id"": ""601510"",
                            ""name"": {
                                ""value"": ""Yu-Gi-Oh! Duel Links""
                            }
                        },
                        {
                            ""id"": ""242760"",
                            ""name"": {
                                ""value"": ""The Forest""
                            }
                        },
                        {
                            ""id"": ""214950"",
                            ""name"": {
                                ""value"": ""Total War: ROME II - Emperor Edition""
                            }
                        },
                        {
                            ""id"": ""48700"",
                            ""name"": {
                                ""value"": ""Mount &amp; Blade: Warband""
                            }
                        },
                        {
                            ""id"": ""294100"",
                            ""name"": {
                                ""value"": ""RimWorld""
                            }
                        },
                        {
                            ""id"": ""363970"",
                            ""name"": {
                                ""value"": ""Clicker Heroes""
                            }
                        },
                        {
                            ""id"": ""291550"",
                            ""name"": {
                                ""value"": ""Brawlhalla""
                            }
                        },
                        {
                            ""id"": ""620"",
                            ""name"": {
                                ""value"": ""Portal 2""
                            }
                        },
                        {
                            ""id"": ""268500"",
                            ""name"": {
                                ""value"": ""XCOM 2""
                            }
                        },
                        {
                            ""id"": ""444200"",
                            ""name"": {
                                ""value"": ""World of Tanks Blitz""
                            }
                        },
                        {
                            ""id"": ""550650"",
                            ""name"": {
                                ""value"": ""Black Squad""
                            }
                        },
                        {
                            ""id"": ""231430"",
                            ""name"": {
                                ""value"": ""Company of Heroes 2""
                            }
                        },
                        {
                            ""id"": ""203770"",
                            ""name"": {
                                ""value"": ""Crusader Kings II""
                            }
                        },
                        {
                            ""id"": ""240"",
                            ""name"": {
                                ""value"": ""Counter-Strike: Source""
                            }
                        },
                        {
                            ""id"": ""239140"",
                            ""name"": {
                                ""value"": ""Dying Light""
                            }
                        },
                        {
                            ""id"": ""460930"",
                            ""name"": {
                                ""value"": ""Tom Clancy's Ghost Recon® Wildlands""
                            }
                        },
                        {
                            ""id"": ""356190"",
                            ""name"": {
                                ""value"": ""Middle-earth™: Shadow of War™""
                            }
                        },
                        {
                            ""id"": ""39210"",
                            ""name"": {
                                ""value"": ""FINAL FANTASY XIV Online""
                            }
                        },
                        {
                            ""id"": ""477160"",
                            ""name"": {
                                ""value"": ""Human: Fall Flat""
                            }
                        },
                        {
                            ""id"": ""250900"",
                            ""name"": {
                                ""value"": ""The Binding of Isaac: Rebirth""
                            }
                        },
                        {
                            ""id"": ""227940"",
                            ""name"": {
                                ""value"": ""Heroes &amp; Generals""
                            }
                        },
                        {
                            ""id"": ""232090"",
                            ""name"": {
                                ""value"": ""Killing Floor 2""
                            }
                        },
                        {
                            ""id"": ""493340"",
                            ""name"": {
                                ""value"": ""Planet Coaster""
                            }
                        },
                        {
                            ""id"": ""268910"",
                            ""name"": {
                                ""value"": ""Cuphead""
                            }
                        },
                        {
                            ""id"": ""47890"",
                            ""name"": {
                                ""value"": ""The Sims(TM) 3""
                            }
                        },
                        {
                            ""id"": ""244850"",
                            ""name"": {
                                ""value"": ""Space Engineers""
                            }
                        },
                        {
                            ""id"": ""243750"",
                            ""name"": {
                                ""value"": ""Source SDK Base 2013 Multiplayer""
                            }
                        },
                        {
                            ""id"": ""364360"",
                            ""name"": {
                                ""value"": ""Total War: WARHAMMER""
                            }
                        },
                        {
                            ""id"": ""211820"",
                            ""name"": {
                                ""value"": ""Starbound""
                            }
                        },
                        {
                            ""id"": ""291480"",
                            ""name"": {
                                ""value"": ""Warface""
                            }
                        },
                        {
                            ""id"": ""378120"",
                            ""name"": {
                                ""value"": ""Football Manager 2016""
                            }
                        },
                        {
                            ""id"": ""577800"",
                            ""name"": {
                                ""value"": ""NBA 2K18""
                            }
                        },
                        {
                            ""id"": ""22380"",
                            ""name"": {
                                ""value"": ""Fallout: New Vegas""
                            }
                        },
                        {
                            ""id"": ""698780"",
                            ""name"": {
                                ""value"": ""Doki Doki Literature Club""
                            }
                        },
                        {
                            ""id"": ""431960"",
                            ""name"": {
                                ""value"": ""Wallpaper Engine""
                            }
                        },
                        {
                            ""id"": ""220200"",
                            ""name"": {
                                ""value"": ""Kerbal Space Program""
                            }
                        },
                        {
                            ""id"": ""270880"",
                            ""name"": {
                                ""value"": ""American Truck Simulator""
                            }
                        },
                        {
                            ""id"": ""305620"",
                            ""name"": {
                                ""value"": ""The Long Dark""
                            }
                        },
                        {
                            ""id"": ""674940"",
                            ""name"": {
                                ""value"": ""Stick Fight: The Game""
                            }
                        },
                        {
                            ""id"": ""322170"",
                            ""name"": {
                                ""value"": ""Geometry Dash""
                            }
                        },
                        {
                            ""id"": ""219990"",
                            ""name"": {
                                ""value"": ""Grim Dawn""
                            }
                        },
                        {
                            ""id"": ""325610"",
                            ""name"": {
                                ""value"": ""Total War: ATTILA""
                            }
                        },
                        {
                            ""id"": ""4700"",
                            ""name"": {
                                ""value"": ""Medieval II: Total War""
                            }
                        },
                        {
                            ""id"": ""457140"",
                            ""name"": {
                                ""value"": ""Oxygen Not Included""
                            }
                        },
                        {
                            ""id"": ""440900"",
                            ""name"": {
                                ""value"": ""Conan Exiles""
                            }
                        },
                        {
                            ""id"": ""304050"",
                            ""name"": {
                                ""value"": ""Trove""
                            }
                        }
                    ]}";
    }
}
