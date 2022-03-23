using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class User
{
    protected string address;
    protected string name;
    protected long money;
    protected Dictionary<int, Property> properties;
    protected List<Card> cards;
    protected Token token;

    public User()
    {
        address = "";
        name = "";
        money = 0;
        properties = new Dictionary<int, Property>();
        cards = new List<Card>();
        token = new Token();
    }

    public User(string _address, string _name, long _money, Dictionary<int, Property> _properties, List<Card> _cards, Token _token)
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

    public void AddProperty(Property property)
    {
        properties.Add(property.propertyId, property);
    }

    public void RemoveProperty(Property property)
    {
        properties.Remove(property.propertyId);
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

    public void AddCard(Card card)
    {
        cards.Add(card);
    }  
}