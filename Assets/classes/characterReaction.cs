using System;
using System.Collections.Generic;

public class characterReaction {

    readonly string[] greetings = {"hello", "what's up", "whats up", "yo", "hi", "hey", "wazamp", "wazzamp"};
    readonly string[] farewells = {"bye", "cya", "see ya", "see you", "later"};
    readonly string[] questions = {"ask", "who", "what", "how", "when", "why", "do you", "can you", "are you"};
    readonly string[] compliments = {"like", "nice", "cool", "you're welcome", "youre welcome"};
    readonly string[] insults = {"suck", "noob", "lame", "idiot", "stupid", "screw", "curse", "to hell"};
    readonly string[] sarcasms = { "deal with", "i dont care", "i don't care", "your problem", "yeah right", "i know you are but what am i"};
    readonly string[] apologies = { "sorry", "i apologize"};
    readonly string[] thanks = { "thanks", "thank you" };
    readonly string[] tradeOffers = {"trade", "wares", "items"};
    readonly string[] buyOffers = {"purchase", "buy a", "buy some", "buy a few", "buy something", "sell me", "buy your"};
    readonly string[] sellOffers = {"sell you", "buy my", "buy this"};

	public enum statementType
    {
        greeting,
        farewell,
        question,
        compliment,
        insult,
        sarcasm,
        apology,
        thank,
        tradeOffer,
        buyOffer,
        sellOffer,
        unknown
    };

    /**************************************************************/

    Dictionary<statementType, List<string>> reaction;
    Dictionary<statementType, int> reactionSequence;
    Random ran;
    Dictionary<string, string> specificReaction;

    public characterReaction()
    {
        reaction = new Dictionary<statementType, List<string>>();
        reactionSequence = new Dictionary<statementType, int>();
        specificReaction = new Dictionary<string, string>();
        ran = new Random();
    }

    public void add(statementType statement, string response)
    {
        if (!reaction.ContainsKey(statement))
        {
            reaction.Add(statement, new List<string>());
            reactionSequence.Add(statement, 0);
        }
        reaction[statement].Add(response);
    }

    public void addSpecific(string subString, string response)
    {
        specificReaction[subString] = response;
    }

    public string reactTo(string statement, bool randomPick)
    {
        if (specificReaction.ContainsKey(statement))
            return specificReaction[statement];

        statementType typ = getStatementType(statement);
        if (!reaction.ContainsKey(typ))
        {
            if (!reaction.ContainsKey(statementType.unknown))
            {
                return "I don't know how to respond to that";
            } else
            {
                typ = statementType.unknown;
            }
        }

        int indexToUse;
        if (randomPick) {
            indexToUse = ran.Next(0, reaction[typ].Count);
        } else {
            indexToUse = reactionSequence[typ];
            reactionSequence[typ] = (reactionSequence[typ] + 1) % reaction[typ].Count;
        }

        return reaction[typ][indexToUse];
    }

    public statementType getStatementType(string statement)
    {
        String toLower = statement.ToLower();
        if (substringIncludesWord(greetings, toLower)) {
            return statementType.greeting;
        } else if (substringIncludesWord(farewells, toLower)) {
            return statementType.farewell;
        } else if (substringIncludesWord(compliments, toLower)) {
            return statementType.compliment;
        } else if (substringIncludesWord(insults, toLower)) {
            return statementType.insult;
        } else if (substringIncludesWord(sarcasms, toLower)) {
            return statementType.sarcasm;
        } else if (substringIncludesWord(apologies, toLower)) {
            return statementType.apology;
        } else if (substringIncludesWord(thanks, toLower)) {
            return statementType.thank;
        } else if (substringIncludesWord(tradeOffers, toLower)) {
            return statementType.tradeOffer;
        } else if (substringIncludesWord(buyOffers, toLower)) {
            return statementType.buyOffer;
        } else if (substringIncludesWord(sellOffers, toLower)) {
            return statementType.sellOffer;
        } else if (substringIncludesWord(questions, toLower)) {
            return statementType.question;
        } else {
            return statementType.unknown;
        }
    }

    bool substringIncludesWord(string[] array, string substring)
    {
        foreach(string s in array)
        {
            if (substring.Contains(s))
                return true;
        }
        return false;
    }

    public void clear()
    {
        reaction = new Dictionary<statementType, List<string>>();
        reactionSequence = new Dictionary<statementType, int>();
    }
}
