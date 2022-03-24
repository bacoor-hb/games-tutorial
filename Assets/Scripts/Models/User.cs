using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    protected string address;
    protected string name;
    protected long money;
    protected Dictionary<string, Property> properties;
    protected List<CardData> cards;
    protected Token token;

    public User()
    {
        address = "";
        name = "";
        money = 0;
        properties = new Dictionary<string, Property>();
        cards = new List<CardData>();
        token = new Token();
    }

    public User(string _address, string _name, long _money, Dictionary<string, Property> _properties, List<CardData> _cards, Token _token)
    {
        address = _address;
        name = _name;
        money = _money;
        properties = _properties;
        cards = _cards;
        token = _token;
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

    public Token Token
    {
        get
        {
            return token;
        }
        set
        {
            token = value;
        }
    }

    public List<CardData> Cards
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

    public void AddProperty(Property property)
    {
        properties.Add(property.data.id, property);
    }

    public void RemoveProperty(Property property)
    {
        properties.Remove(property.data.id);
    }

    public List<Property> GetProperties()
    {
        List<Property> formatedPropertyList = new List<Property>();
        foreach (var item in properties.Values)
        {
            formatedPropertyList.Add(item);
        }
        return formatedPropertyList;
    }

    public void AddCard(CardData card)
    {
        cards.Add(card);
    }  
}