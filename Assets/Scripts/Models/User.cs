using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    protected string address;
    protected string name;
    protected long money;
    protected List<PropertyData> properties;
    protected List<Card> cards;
    protected List<Token> tokens;

    public User()
    {
        address = "";
        name = "";
        money = 0;
        properties = null;
        cards = null;
        tokens = null;
    }

    public User(string _address, string _name, long _money, List<PropertyData> _properties, List<Card> _cards, List<Token> _tokens)
    {
        address = _address;
        name = _name;
        money = _money;
        properties = _properties;
        cards = _cards;
        tokens = _tokens;
    }

    public string Address
    {
        get
        {
            return address;
        }
        set
        {
            address = value;
        }
    }

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }

    public long Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }

    public List<PropertyData> Properties
    {
        get
        {
            return properties;
        }
        set
        {
            properties = value;
        }
    }

    public List<Card> Cards
    {
        get
        {
            return cards;
        }
        set
        {
            cards = value;
        }
    }

    public List<Token> Tokens
    {
        get
        {
            return tokens;
        }
        set
        {
            tokens = value;
        }
    }


}