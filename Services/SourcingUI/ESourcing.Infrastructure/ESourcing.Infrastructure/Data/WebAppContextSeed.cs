using ESourcing.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ESourcing.Infrastructure.Data
{
    public class WebAppContextSeed
    {
        public static async Task SeedAsync(WebAppContext context,ILoggerFactory loggerFactory,int? retry=0)
        {
            int retryforAvailablity = retry.Value;
            try
            {
                context.Database.Migrate();
                if (!context.appUsers.Any())
                {
                    context.appUsers.AddRange(GetPreConfiguredOrders());
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {

                if (retryforAvailablity<50)
                {
                    retryforAvailablity++;
                    var log = loggerFactory.CreateLogger<WebAppContextSeed>();
                    log.LogError(ex.Message);
                    Thread.Sleep(2000);
                    await SeedAsync(context, loggerFactory, retryforAvailablity);
                    
                }
                throw;
            }
        }

        private static IEnumerable<AppUser> GetPreConfiguredOrders()
        {
            return new List<AppUser>()
            {
                new AppUser
                {
                    FirstName="SeedUser",
                    LastName="SeedUserLastName",
                    IsSeller=true,
                    IsBuyer=false,
                }
            };
        }
    }
}
