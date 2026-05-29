using System;
using GROUP9PoetryWebsite.Models;
using System.Linq;

namespace GROUP9PoetryWebsite.Data
{
    public static class SeedData
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Users.Any()) return;  // Check if already seeded

            context.Users.AddRange(
                new User
                {
                    Username = "Poet",
                    Email = "ordinaryPoet@gmail.com",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("ilovepoems")
                });
            context.SaveChanges();

            if (context.Anthologies.Any()) return;

            var none = new Anthology { Title = "No Anthology", Description = "No Description" };
            var resilience = new Anthology { Title = "Echoes of Resilience", Description = "Overcoming pain, rising above struggle, and the strength of the human spirit." };
            var heart = new Anthology { Title = "Whispers of the Heart", Description = "Romantic, vulnerable, and deep reflections on love and connection." };
            var reflection = new Anthology { Title = "Reflections on Existence", Description = "Philosophical musings on life, nature, death, and the way we experience the world." };
            context.Anthologies.AddRange(
                none,
                resilience,
                heart,
                reflection
            );

            context.SaveChanges();


            if (context.Poems.Any())
            {
                Console.WriteLine(">>> [Seed] Table already has poems, skipping.");
                return;
            }

            Console.WriteLine(">>> [Seed] Inserting poems...");

            context.Poems.AddRange(new Poem
            {
                Title = "Dreams",
                Text = "Hold fast to dreams\nFor if dreams die\nLife is a broken-winged bird\nThat cannot fly.",
                Author = "Langston Hughes",
                Anthology = resilience
            },
                new Poem
                {
                    Title = "The Road Not Taken",
                    Text = "Two roads diverged in a wood, and I—\nI took the one less traveled by,\nAnd that has made all the difference.",
                    Author = "Robert Frost",
                    Anthology = reflection
                },
                new Poem
                {
                    Title = "Dirge Without Music",
                    Text = "I am not resigned: in the darkness the heart will break.\nMore precious was the light in your eyes\nthan all the roses in the world.",
                    Author = "Edna St. Vincent Millay",
                    Anthology = heart
                },
                new Poem
                {
                    Title = "Do Not Go Gentle",
                    Text = "Do not go gentle into that good night.\nRage, rage against the dying of the light.",
                    Author = "Dylan Thomas",
                    Anthology = resilience
                },
                new Poem
                {
                    Title = "Because I Could Not Stop for Death",
                    Text = "Because I could not stop for Death—\nHe kindly stopped for me—\nThe Carriage held but just Ourselves—\nAnd Immortality.",
                    Author = "Emily Dickinson",
                    Anthology = reflection
                },
                new Poem
                {
                    Title = "I Carry Your Heart",
                    Text = "I carry your heart with me.\nI carry it in my heart.\nI am never without it.",
                    Author = "E. E. Cummings",
                    Anthology = heart
                },
                new Poem
                {
                    Title = "The World Is Too Much With Us",
                    Text = "The world is too much with us; late and soon,\nGetting and spending, we lay waste our powers;\nLittle we see in Nature that is ours.",
                    Author = "William Wordsworth",
                    Anthology = reflection
                },
                new Poem
                {
                    Title = "Still I Rise",
                    Text = "Still I Rise.\nOut of the huts of history's shame\nI rise\nUp from a past that's rooted in pain\nI rise.",
                    Author = "Maya Angelou",
                    Anthology = resilience
                },
                new Poem
                {
                    Title = "Sonnet 18",
                    Text = "Shall I compare thee to a summer's day?\nThou art more lovely and more temperate.",
                    Author = "William Shakespeare",
                    Anthology = heart
                },
                new Poem
                {
                    Title = "Hope Is the Thing with Feathers",
                    Text = "Hope is the thing with feathers\nThat perches in the soul,\nAnd sings the tune without the words,\nAnd never stops at all.",
                    Author = "Emily Dickinson",
                    Anthology = reflection
                },
                new Poem
                {
                    Title = "Inferno",
                    Text = "In the middle of the journey of our life\nI came to myself within a dark wood\nwhere the straight way was lost.",
                    Author = "Dante Alighieri",
                    Anthology = resilience
                },
                new Poem
                {
                    Title = "Little Gidding",
                    Text = "We shall not cease from exploration,\nAnd the end of all our exploring\nWill be to arrive where we started\nAnd know the place for the first time.",
                    Author = "T. S. Eliot",
                    Anthology = resilience
                },
                new Poem
                {
                    Title = "A Quiet Knowing",
                    Text = "Loving you\ncame without question\na quiet knowing\nsettling into my bones\nas if you were never new\njust finally here",
                    Author = "Mic Mac Property",
                    Anthology = heart
                },
                new Poem
                {
                    Title = "Begin Again",
                    Text = "Spring exists to remind us\nthat everything can\nbegin again.",
                    Author = "D.J",
                    Anthology = reflection
                },
                new Poem
                {
                    Title = "The Music",
                    Text = "And those who were\nseen dancing were thought\nto be insane by those\nwho could not\nhear the music",
                    Author = "Friedrich Nietzsche",
                    Anthology = reflection
                },
                new Poem
                {
                    Title = "Silence",
                    Text = "And in our silence\nwe hide the loudest\nof words",
                    Author = "Zahra",
                    Anthology = reflection
                },
                new Poem
                {
                    Title = "To Be Understood",
                    Text = "Perhaps one did not\nwant to be loved\nso much as to be understood.",
                    Author = "George Orwell",
                    Anthology = heart
                }
            );

            context.SaveChanges();
            Console.WriteLine(">>> [Seed] Done.");
        }
    }
}