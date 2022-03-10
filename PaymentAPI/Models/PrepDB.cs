using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentAPI.Models
{
    public class PrepDB
    {
        public static void PrepPopulation(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SendData(serviceScope.ServiceProvider.GetService<PaymentDetailContext>());
            }
        }

        private static void SendData(PaymentDetailContext context)
        {
            Console.WriteLine("Appling Migration...");
            context.Database.Migrate();
            if (context.paymentDetails.Any())
            {
                context.paymentDetails.AddRange(new PaymentDetails()
                {
                    CardNumber = "1234567891234567",
                    CardOwnerName = "Steve Smith",
                    ExpirationDate = "02/24",
                    SecurityCode = "123"
                });
                context.SaveChanges();
            }
            else
            {
                Console.WriteLine("Already have data -- not sending");
            }
        }
    }
}