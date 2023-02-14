namespace YugiOh_NonDBVersion.Constants;

#region Enums
public enum CardRace
{
    Aqua,
    Beast,
    BeastWarrior,
    Cyberse,
    Dinosaur,
    DivineBeast,
    Dragon,
    Fairy,
    Fiend,
    Fish,
    Insect,
    Machine,
    Plant,
    Psychic,
    Pyro,
    Reptile,
    Rock,
    SeaSerpent,
    SpellCaster,
    Thunder,
    Warrior,
    WingedBeast,
    Wyrm,
    Zombie,
    SpellNormal,
    Field,
    Equip,
    SpellContinuous,
    QuickPlay,
    Ritual,
    TrapNormal,
    TrapContinuous,
    Counter
}

public enum CardType
{
    EffectMonster,
    FlipEffectMonster,
    FlipTunerEffectMonster,
    GeminiMonster,
    NormalMonster,
    NormalTunerMonster,
    PendulumEffectMonster,
    PendulumEffectRitualMonster,
    PendulumFlipEffectMonster,
    PendulumNormalMonster,
    PendulumTunerEffectMonster,
    RitualEffectMonster,
    RitualMonster,
    SpellCard,
    SpiritMonster,
    ToonMonster,
    TrapCard,
    TunerMonster,
    UnionEffectMonster,
    FusionMonster,
    LinkMonster,
    PendulumEffectFusionMonster,
    SynchroMonster,
    SynchroPendulumEffectMonster,
    SynchroTunerMonster,
    XyzMonster,
    XyzPendulumEffectMonster,
    SkillCard,
    Token
}
    
public enum CardAttributes
{
    Dark,
    Divine,
    Earth,
    Wind,
    Fire,
    Light,
    Water
}

public enum CardFrameType
{
    Normal,
    Effect,
    Ritual,
    Fusion,
    Synchro,
    Xyz,
    Link,
    NormalPendulum,
    EffectPendulum,
    RitualPendulum,
    FusionPendulum,
    SynchroPendulum,
    XyzPendulum,
    Spell,
    Trap,
    Token,
    Skill
}
    
public enum SearchTerm
{
    FuzzySearch,
    NameSearch,
    TypeSearch,
    AttributeSearch,
    CardsetSearch,
    FormatSearch,
    StableSearch,
    ArhcetypeSearch
}
#endregion

public static class YugiOhEnums
{
    #region Enums for Search Terms

    public static SearchTerm ConvertStringToSearchTerm(string search)
    {
        SearchTerm searchTerm;

        switch (search.ToLower().Trim())
        {
            case "fname":
            case "fuzzy name":
            case "fuzzy search":
            case "fsearch":
                searchTerm = SearchTerm.FuzzySearch;
                break;
            case "name search":
            case "namesearch":
            case "cardname":
            case "card name":
            case "card":
            case "name":
                searchTerm = SearchTerm.NameSearch;
                break;
            case "type search":
            case "typesearch":
            case "type":
                searchTerm = SearchTerm.TypeSearch;
                break;
            case "attribute search":
            case "attributesearch":
                searchTerm = SearchTerm.AttributeSearch;
                break;
            case "card set search":
            case "cardsetsearch":
            case "cardset search":
            case "card setsearch":
            case "card set":
            case "cardset":
                searchTerm = SearchTerm.CardsetSearch;
                break;
            case "format search":
            case "format":
            case "game format":
            case "gameformat":
                searchTerm = SearchTerm.FormatSearch;
                break;
            case "stablesearch":
            case "stable search":
                searchTerm = SearchTerm.StableSearch;
                break;
            case "archetype":
            case "archetype search":
            case "arche type search":
                searchTerm = SearchTerm.ArhcetypeSearch;
                break;
            default:
                searchTerm = SearchTerm.FuzzySearch;
                break;
        }

        return searchTerm;
    }

    public static string ConvertingSearchTermToString(SearchTerm term)
    {
        switch (term)
        {
            case SearchTerm.FuzzySearch:
                return "fname";
            case SearchTerm.AttributeSearch:
                return "attribute";
            case SearchTerm.NameSearch:
                return "name";
            case SearchTerm.TypeSearch:
                return "type";
            case SearchTerm.CardsetSearch:
                return "cardset";
            case SearchTerm.FormatSearch:
                return "format";
            case SearchTerm.StableSearch:
                return "stable";
            case SearchTerm.ArhcetypeSearch:
                return "archetype";
            default:
                return "fname";
        }
    }

    #endregion

    #region Enum Conversion for Card Attributes

    public static CardAttributes ConvertStringToCardAttribute(string attribute)
    {
        switch (attribute.ToLower().Trim())
        {
            case "dark":
                return CardAttributes.Dark;
            case "divine":
                return CardAttributes.Divine;
            case "earth":
                return CardAttributes.Earth;
            case "wind":
                return CardAttributes.Wind;
            case "fire":
                return CardAttributes.Fire;
            case "light":
                return CardAttributes.Light;
            case "water":
                return CardAttributes.Water;
            default:
                return CardAttributes.Dark;
        }
    }

    public static string ConvertCardAttributeToString(CardAttributes attributes)
    {
        switch (attributes)
        {
            case CardAttributes.Dark:
                return "Dark";
            case CardAttributes.Divine:
                return "Divine";
            case CardAttributes.Earth:
                return "Earth";
            case CardAttributes.Wind:
                return "Wind";
            case CardAttributes.Fire:
                return "Fire";
            case CardAttributes.Light:
                return "light";
            case CardAttributes.Water:
                return "water";
            default:
                return "Dark";

        }
    }

    #endregion

    #region Enum Conversion for Card Types

    public static CardRace ConvertStringToCardRace(string types, bool isSpellCard = false)
    {
        switch (types.ToLower().Trim())
        {
            case "aqua":
                return CardRace.Aqua;
            case "beast":
                return CardRace.Beast;
            case "beastwarrior":
            case "beast-warrior":
            case "beast warrior":
                return CardRace.BeastWarrior;
            case "cyberse":
                return CardRace.Cyberse;
            case "dinosaur":
            case "dino":
                return CardRace.Dinosaur;
            case "divine-beast":
            case "divine beast":
            case "divinebeast":
                return CardRace.DivineBeast;
            case "dragon":
                return CardRace.Dragon;
            case "fairy":
                return CardRace.Fairy;
            case "fiend":
            case "demon":
                return CardRace.Fiend;
            case "fish":
                return CardRace.Fish;
            case "insect":
                return CardRace.Insect;
            case "machine":
                return CardRace.Machine;
            case "plant":
                return CardRace.Plant;
            case "psychic":
                return CardRace.Psychic;
            case "pyro":
                return CardRace.Pyro;
            case "reptile":
                return CardRace.Reptile;
            case "rock":
                return CardRace.Rock;
            case "sea serpent":
            case "seaserpent":
            case "sea serpant":
            case "seaserpant":
                return CardRace.SeaSerpent;
            case "spellcaster":
            case "spell caster":
                return CardRace.SpellCaster;
            case "thunder":
                return CardRace.Thunder;
            case "warrior":
                return CardRace.Warrior;
            case "winged-beast":
            case "wingedbeast":
            case "winged beast":
                return CardRace.WingedBeast;
            case "wyrm":
                return CardRace.Wyrm;
            case "zombie":
                return CardRace.Zombie;
            case "normal":
            {
                if (isSpellCard)
                    return CardRace.SpellNormal;
                else
                    return CardRace.TrapNormal;
            }
            case "field":
                return CardRace.Field;
            case "equip":
                return CardRace.Equip;
            case "continuous":
            {
                if (isSpellCard)
                    return CardRace.SpellContinuous;
                else
                    return CardRace.TrapContinuous;
            }
            case "quickplay":
            case "quick-play":
                return CardRace.QuickPlay;
            case "ritual":
                return CardRace.Ritual;
            case "counter":
                return CardRace.Counter;
            default:
                return CardRace.SpellCaster;
        }
    }

    public static string ConvertCardRaceToString(CardRace types)
    {
        switch (types)
        {
            case CardRace.Aqua:
                return "Aqua";
            case CardRace.Beast:
                return "Beast";
            case CardRace.BeastWarrior:
                return "Beast-Warrior";
            case CardRace.Cyberse:
                return "Cyberse";
            case CardRace.Dinosaur:
                return "Dinosaur";
            case CardRace.DivineBeast:
                return "Divine-Beast";
            case CardRace.Dragon:
                return "Dragon";
            case CardRace.Fairy:
                return "Fairy";
            case CardRace.Fiend:
                return "Fiend";
            case CardRace.Fish:
                return "Fish";
            case CardRace.Insect:
                return "Insect";
            case CardRace.Machine:
                return "Machine";
            case CardRace.Plant:
                return "Plant";
            case CardRace.Psychic:
                return "Psychic";
            case CardRace.Pyro:
                return "Pyro";
            case CardRace.Reptile:
                return "Reptile";
            case CardRace.SeaSerpent:
                return "Sea Serpent";
            case CardRace.SpellCaster:
                return "Spellcaster";
            case CardRace.Thunder:
                return "Thunder";
            case CardRace.Warrior:
                return "Warrior";
            case CardRace.WingedBeast:
                return "Winged-Beast";
            case CardRace.Wyrm:
                return "Wyrm";
            case CardRace.Zombie:
                return "Zombie";
            case CardRace.SpellNormal:
            case CardRace.TrapNormal:
                return "Normal";
            case CardRace.SpellContinuous:
            case CardRace.TrapContinuous:
                return "Continuous";
            case CardRace.Field:
                return "Field";
            case CardRace.Equip:
                return "Equip";
            case CardRace.QuickPlay:
                return "Quick-Play";
            case CardRace.Ritual:
                return "Ritual";
            case CardRace.Counter:
                return "Counter";
            default:
                return "Spellcaster";
        }
    }

    #endregion

    #region Card Types

    public static CardType ConvertStringToCardTypes(string types)
    {
        switch (RemoveWhiteSpace(types))
        {
            case "effectmonster":
                return CardType.EffectMonster;
            case "flipeffectmonster":
                return CardType.FlipEffectMonster;
            case "fliptunereffectmonster":
                return CardType.FlipTunerEffectMonster;
            case "geminimonster":
                return CardType.GeminiMonster;
            case "normalmonster":
                return CardType.NormalMonster;
            case "normaltunermonster":
                return CardType.NormalTunerMonster;
            case "pendulumeffectmonster":
                return CardType.PendulumEffectMonster;
            case "pendulumeffectritualmonster":
                return CardType.PendulumEffectRitualMonster;
            case "pendulumflipeffectmonster":
                return CardType.PendulumFlipEffectMonster;
            case "pendulumtunereffectmonster":
                return CardType.PendulumTunerEffectMonster;
            case "ritualeffectmonster":
                return CardType.RitualEffectMonster;
            case "ritualmonster":
                return CardType.RitualMonster;
            case "spellcard":
                return CardType.SpellCard;
            case "spiritmonster":
                return CardType.SpiritMonster;
            case "toonmonster":
                return CardType.ToonMonster;
            case "trapcard":
                return CardType.TrapCard;
            case "tunermonster":
                return CardType.TunerMonster;
            case "unioneffectmonster":
                return CardType.UnionEffectMonster;
            case "fusionmonster":
                return CardType.FusionMonster;
            case "linkmonster":
                return CardType.LinkMonster;
            case "pendulumeffectfusionmonster":
                return CardType.PendulumEffectFusionMonster;
            case "synchromonster":
                return CardType.SynchroMonster;
            case "synchropendulumeffectmonster":
                return CardType.SynchroPendulumEffectMonster;
            case "synchrotunermonster":
                return CardType.SynchroTunerMonster;
            case "xyzmonster":
                return CardType.XyzMonster;
            case "xyzpendulumeffectmonster":
                return CardType.XyzPendulumEffectMonster;
            case "skillcard":
                return CardType.SkillCard;
            case "token":
                return CardType.Token;
            default:
                return CardType.NormalMonster;
        }
    }

    public static string ConvertCardTypeToString(CardType type)
    {
        switch (type)
        {
            case CardType.EffectMonster:
                return "Effect Monster";
            case CardType.FlipEffectMonster:
                return "Flip Effect Monster";
            case CardType.FlipTunerEffectMonster:
                return "Flip Tuner Effect Monster";
            case CardType.GeminiMonster:
                return "Gemini Monster";
            case CardType.NormalMonster:
                return "Normal Monster";
            case CardType.PendulumEffectMonster:
                return "Pendulum Effect Monster";
            case CardType.PendulumEffectRitualMonster:
                return "Pendulum Effect Ritual Monster";
            case CardType.PendulumFlipEffectMonster:
                return "Pendulum Flip Effect Monster";
            case CardType.PendulumNormalMonster:
                return "Pendulum Normal Monster";
            case CardType.PendulumTunerEffectMonster:
                return "Pendulum Tuner Effect Monster";
            case CardType.RitualEffectMonster:
                return "Ritual Effect Monster";
            case CardType.RitualMonster:
                return "Ritual Monster";
            case CardType.SpellCard:
                return "Spell Card";
            case CardType.SpiritMonster:
                return "Spirit Monster";
            case CardType.ToonMonster:
                return "Toon Monster";
            case CardType.TrapCard:
                return "Trap Card";
            case CardType.TunerMonster:
                return "Tuner Monster";
            case CardType.UnionEffectMonster:
                return "Union Effect Monster";
            case CardType.FusionMonster:
                return "Fusion Monster";
            case CardType.LinkMonster:
                return "Link Monster";
            case CardType.PendulumEffectFusionMonster:
                return "Pendulum Effect Fusion Monster";
            case CardType.SynchroMonster:
                return "Synchro Monster";
            case CardType.SynchroPendulumEffectMonster:
                return "Synchro Pendulum Effect Monster";
            case CardType.SynchroTunerMonster:
                return "Synchro Tuner Monster";
            case CardType.XyzMonster:
                return "Xyz Monster";
            case CardType.XyzPendulumEffectMonster:
                return "Xyz Pendulum Effect Monster";
            case CardType.SkillCard:
                return "Skill Card";
            case CardType.Token:
                return "Token";
            default:
                return "Normal Monster";
        }
    }

    #endregion

    #region Card Frames

    public static CardFrameType ConvertStringToCardFrameTypes(string type)
    {
        switch (RemoveWhiteSpace(type))
        {
            case "normal":
                return CardFrameType.Normal;
            case "effect":
                return CardFrameType.Effect;
            case "ritual":
                return CardFrameType.Ritual;
            case "fusion":
                return CardFrameType.Fusion;
            case "synchro":
                return CardFrameType.Synchro;
            case "xyz":
                return CardFrameType.Xyz;
            case "link":
                return CardFrameType.Link;
            case "normalpendulum":
                return CardFrameType.NormalPendulum;
            case "effectpendulum":
                return CardFrameType.EffectPendulum;
            case "ritualpendulum":
                return CardFrameType.RitualPendulum;
            case "fusionpendulum":
                return CardFrameType.FusionPendulum;
            case "synchropendulum":
                return CardFrameType.SynchroPendulum;
            case "xyzpendulum":
                return CardFrameType.XyzPendulum;
            case "spell":
                return CardFrameType.Spell;
            case "trap":
                return CardFrameType.Trap;
            case "token":
                return CardFrameType.Token;
            case "skill":
                return CardFrameType.Skill;
            default:
                return CardFrameType.Normal;
        }
    }

    public static string ConvertCardFrameTypeToString(CardFrameType type)
    {
        switch (type)
        {
            case CardFrameType.Normal:
                return "Normal";
            case CardFrameType.Effect:
                return "Effect";
            case CardFrameType.Ritual:
                return "Ritual";
            case CardFrameType.Fusion:
                return "Fusion";
            case CardFrameType.Synchro:
                return "Synchro";
            case CardFrameType.Xyz:
                return "Xyz";
            case CardFrameType.Link:
                return "Link";
            case CardFrameType.NormalPendulum:
                return "Normal Pendulum";
            case CardFrameType.EffectPendulum:
                return "Effect Pendulum";
            case CardFrameType.RitualPendulum:
                return "Ritual Pendulum";
            case CardFrameType.FusionPendulum:
                return "Fusion Pendulum";
            case CardFrameType.SynchroPendulum:
                return "Synchro Pendulum";
            case CardFrameType.XyzPendulum:
                return "Xyz Pendulum";
            case CardFrameType.Spell:
                return "Spell";
            case CardFrameType.Trap:
                return "Trap";
            case CardFrameType.Token:
                return "Token";
            case CardFrameType.Skill:
                return "Skill";
            default:
                return "Normal";
        }
    }

    #endregion

    public static string RemoveWhiteSpace(string strToRemoveWhiteSpaces)
    {
        string final = strToRemoveWhiteSpaces.ToLower().Trim();

        return new string(final.ToCharArray().Where(x => !Char.IsWhiteSpace(x)).ToArray());
    }
}