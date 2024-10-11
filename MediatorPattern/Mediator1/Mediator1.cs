namespace Patterns.MediatorPattern.Mediator1;

public class Person
{
    public Person(string name)
    {
        Name = name;
        ChatLog = [];
    }

    public string Name { get; set; }
    public ChatRoom? ChatRoom { get; set; }
    public List<string> ChatLog { get; set; }

    public void Say(string message)
    {
        ChatRoom?.Broadcast(Name, message);
    }

    public void PrivateMessage(string who, string message)
    {
        ChatRoom?.Message(Name, who, message);
    }

    public void Receive(string sender, string message)
    {
        string s = $"{sender}: '{message}'";
        ChatLog.Add(s);
        Console.WriteLine($"[{Name}'s chat session] {s}");
    }
}

public class ChatRoom
{
    public List<Person> People { get; set; } = [];
    
    public void Join(Person p)
    {
        string joinMsg = $"{p.Name} joins the chat";
        Broadcast("room", joinMsg);
    }
    
    public void Message(string source, string destination, string message)
        => People.FirstOrDefault(p => p.Name == destination)
            ?.Receive(source, message);

    public void Broadcast(string source, string message)
    {
        foreach (var person in People.Where(person => person.Name != source))
            person.Receive(source, message);
    }
}

public static class CHatManager
{
    public static void BindChatAndPerson(Person p, ChatRoom chat)
    {
        p.ChatRoom = chat;
        chat.People.Add(p);
        chat.Join(p);
    }
}

public static class Mediator1
{
    public static void LocalMain()
    {
        var room = new ChatRoom();

        var p1 = new Person("John");
        var p2 = new Person("Samanta");
        var p3 = new Person("Ashley");
        
        CHatManager.BindChatAndPerson(p1, room);
        CHatManager.BindChatAndPerson(p2, room);
        CHatManager.BindChatAndPerson(p3, room);
        
        p1.Say("Hello");
        p3.Say("Hello, j");
        p1.PrivateMessage("Samanta", "So what up?");
    }
}