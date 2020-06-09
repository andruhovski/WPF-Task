using DataLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        private static readonly Random Random = new Random();
        static void Main()
        {
            var connectionString = "server=localhost;port=3306;database=demodatabase;uid=demouser;password=Start2020!";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                // Create database if not exists
                using (CardsCatalogContext contextDb = new CardsCatalogContext(connection, false))
                {
                    contextDb.Database.CreateIfNotExists();
                }

                connection.Open();
                MySqlTransaction transaction = connection.BeginTransaction();

                try
                {
                    // DbConnection that is already opened
                    using (var context = new CardsCatalogContext(connection, false))
                    {

                        // Interception/SQL logging
                        context.Database.Log = Console.WriteLine;

                        // Passing an existing transaction to the context
                        context.Database.UseTransaction(transaction);

                        // DbSet.AddRange
                        for (var i = 0; i < 10; i++)
                        {
                            var card = new Card
                            {
                                Header = $"N{i} {Faker.Lorem.Sentence(5)}",
                                CardResources = GetNewCardResource(i % 3)
                            };
                            context.Cards.Add(card);
                        }
                        context.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }

            //var context = new CardsCatalogContext();
            //for (var i = 0; i < 10; i++)
            //{
            //    var card = new Card
            //    {
            //        Header = $"N{i} {Faker.Lorem.Sentence(5)}",
            //        CardResources = GetNewCardResource(i % 3)
            //    };
            //    context.Cards.Add(card);
            //}
            //context.SaveChanges();
            //Console.WriteLine(context.Cards.Count());
        }

        private static ICollection<CardResource> GetNewCardResource(int count)
        {
            var list = new List<CardResource>();
            for (int i = 0; i < count; i++)
            {
                list.Add(new ImageResource
                {
                    Kind = 1,
                    Description = Faker.Lorem.Sentence(10),
                    Image = CreateRandomImage()
                });
                list.Add(new StringTableResource
                {
                    Kind = 2,
                    Rows = new List<Row>
                    {
                        new Row
                        {
                            Title="A"
                        },
                        new Row
                        {
                            Title="B"
                        },
                    }
                });
            }
            return list;
        }

        private static byte[] CreateRandomImage()
        {
            
            var image = Image.FromFile(@"C:\tmp\Annotation 2020-06-07 233256.png");
            var graphics = Graphics.FromImage(image);
            graphics.DrawString(Random.Next(1000).ToString(), new Font("Arial", 14), new SolidBrush(Color.DarkBlue),  20f,20f );
            var stream = new MemoryStream();
            image.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
            return stream.ToArray();
        }
    }
}