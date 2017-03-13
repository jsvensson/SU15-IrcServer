using System;
using System.Collections.Generic;

namespace IrcServer
{
    public class Channel
    {
        public string Name { get; set; }
        public string Topic { get; set; }
        private HashSet<User> Users { get; set; } = new HashSet<User>();

        public Channel(string name)
        {
            Name = name;
        }

        public void UserJoin(User user)
        {
            // Tell existing users about join
            foreach (User u in Users)
            {
                u.WriteLine($"CHANJOIN {Name} {user.Nickname}");
            }

            Users.Add(user);

            // Confirm join to user
            user.WriteLine($"INFO Joined channel {Name}").Wait();
            user.WriteLine($"JOIN {Name}");
        }

        public void UserPart(User user)
        {
            Users.Remove(user);
            user.WriteLine($"PART {Name}");

            // Tell existing users about part
            foreach (User u in Users)
            {
                u.WriteLine($"CHANPART {Name} {user.Nickname}");
            }
        }

        public void Message(User sender, string message)
        {
            foreach (User user in Users)
            {
                user.WriteLine($"MSG {Name} <{sender.Nickname}> {message}");
            }
        }
    }
}
