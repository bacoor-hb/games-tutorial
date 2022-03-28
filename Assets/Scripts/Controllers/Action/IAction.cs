using System;

public interface IAction
{
  public ACTION GetAction();
  public void OnStartAction();
  public void OnAction();
  public void OnEndAction();
}