using Book2Glow.Infrastructure.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book2Glow.Infrastructure.Data.Seeder
{
    public static class BusinessModelSeeder
    {
        public static void SeedDatabase(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DataModelContext>();

                // Appliquer les migrations si ce n'est pas déjà fait
                context.Database.Migrate();

                // Vérifier si les données existent déjà
                if (!context.Businesses.Any())
                {
                    var businesses = new[]
                    {
                        new BusinessModel
                        {
                            Id = Guid.NewGuid(),
                            Name = "Boulangerie Nantes",
                            City = "Nantes",
                            PostalCode = "44000",
                            Latitude = "47.2184",
                            Longitude = "-1.5536",
                            Country = "France",
                            Phone = "0240123456",
                            Website = "https://boulangerie-nantes.fr",
                            ApplicationUserId = "93ff1c24-284b-4b54-b3bb-93cf127a98a9"
                        },
                        new BusinessModel
                        {
                            Id = Guid.NewGuid(),
                            Name = "Café de Nantes",
                            City = "Nantes",
                            PostalCode = "44000",
                            Latitude = "47.2181",
                            Longitude = "-1.5545",
                            Country = "France",
                            Phone = "0240654321",
                            Website = "https://cafenantes.fr",
                            ApplicationUserId = "a20bedfa-c5fb-4036-9875-03e9799edfd4"
                        },
                        new BusinessModel
                        {
                            Id = Guid.NewGuid(),
                            Name = "Librairie du Château",
                            City = "Nantes",
                            PostalCode = "44000",
                            Latitude = "47.2186",
                            Longitude = "-1.5538",
                            Country = "France",
                            Phone = "0240789012",
                            Website = "https://librairiechateau.fr",
                            ApplicationUserId = "93ff1c24-284b-4b54-b3bb-93cf127a98a9"
                        }
                    };

                    context.Businesses.AddRange(businesses);
                    context.SaveChanges();
                }
            }
        }
    }
}
