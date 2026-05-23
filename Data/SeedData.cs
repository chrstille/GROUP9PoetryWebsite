using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GROUP9PoetryWebsite.Models;
using System.Linq;

namespace GROUP9PoetryWebsite.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                if (context.Poems.Any())
                {
                    return; // DB has been seeded already
                }

                context.Poems.AddRange(
                    new Poem 
                    { 
                        Text = "Hold fast to dreams\nFor if dreams die\nLife is a broken-winged bird\nThat cannot fly.", 
                        Author = "Langston Hughes" 
                    },
                    new Poem 
                    { 
                        Text = "Two roads diverged in a wood, and I—\nI took the one less traveled by,\nAnd that has made all the difference.", 
                        Author = "Robert Frost" 
                    },
                    new Poem 
                    { 
                        Text = "I am not resigned: in the darkness the heart will break.\nMore precious was the light in your eyes\nthan all the roses in the world.", 
                        Author = "Edna St. Vincent Millay" 
                    },
                    new Poem 
                    { 
                        Text = "Do not go gentle into that good night.\nRage, rage against the dying of the light.", 
                        Author = "Dylan Thomas" 
                    },
                    new Poem 
                    { 
                        Text = "Because I could not stop for Death—\nHe kindly stopped for me—\nThe Carriage held but just Ourselves—\nAnd Immortality.", 
                        Author = "Emily Dickinson" 
                    },
                    new Poem 
                    { 
                        Text = "I carry your heart with me.\nI carry it in my heart.\nI am never without it.", 
                        Author = "E. E. Cummings" 
                    },
                    new Poem 
                    { 
                        Text = "The world is too much with us; late and soon,\nGetting and spending, we lay waste our powers;\nLittle we see in Nature that is ours.", 
                        Author = "William Wordsworth" 
                    },
                    new Poem 
                    { 
                        Text = "Still I Rise.\nOut of the huts of history's shame\nI rise\nUp from a past that's rooted in pain\nI rise.", 
                        Author = "Maya Angelou" 
                    },
                    new Poem 
                    { 
                        Text = "Shall I compare thee to a summer's day?\nThou art more lovely and more temperate.", 
                        Author = "William Shakespeare" 
                    },
                    new Poem 
                    { 
                        Text = "Hope is the thing with feathers\nThat perches in the soul,\nAnd sings the tune without the words,\nAnd never stops at all.", 
                        Author = "Emily Dickinson" 
                    },
                    new Poem 
                    { 
                        Text = "In the middle of the journey of our life\nI came to myself within a dark wood\nwhere the straight way was lost.", 
                        Author = "Dante Alighieri" 
                    },
                    new Poem 
                    { 
                        Text = "We shall not cease from exploration,\nAnd the end of all our exploring\nWill be to arrive where we started\nAnd know the place for the first time.", 
                        Author = "T. S. Eliot" 
                    },
                     new Poem
                    {
                        Text = "Loving you \ncame without question\n a quiet knowing\n settling into my bones \n as if you were never new \njust finally here",
                        Author = "Mic Mac Property"
                    },
                    new Poem
                    {
                        Text = "Spring exists to remind us \n that everything can \n begin again.",
                        Author = "D.J"
                    },
                    new Poem
                    {
                        Text = "And those who were \n seen dancing were thought \n to be insane by those \n who could not \n hear the music",
                        Author = "Fredrich Nietzsche"
                    },
                    new Poem
                    {
                        Text = "And in our silence \n we hide the loudest \n of words",
                        Author = "Zahra"
                    },
                    new Poem
                    {
                        Text = "Perhaps one did not \n want to be loved \n so much as to be understood.",
                        Author = "George Orwell"
                    }

                    // new Poem
                    // {
                    //     Text = "",
                    //     Author = ""
                    // }
                );

                context.SaveChanges();
            }
        }
    }
}