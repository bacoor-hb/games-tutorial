public class TopicAttack
{
    public string[] messages { get; set; }
    public string username { get; set; }
    public DataPet[] pets { get; set; }
    public int currentPos { get; set; }
    public DataPet[] monster { get; set; }
    public bool isYourTurn { get; set; }
    public bool isClear { get; set; }


}
public class DataUserAttack
{
    public TopicAttack topic { get; set; }


}
