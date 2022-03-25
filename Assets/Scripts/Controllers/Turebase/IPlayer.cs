using System;

public interface IPlayer
{
    public void OnReleaseCard();
    public void OnRollDice();
    public void OnActionCell();
    public void OnPurchase();
    public void OnBuilding();
    public void OnAuctions();
}
