using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Amazon.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Amazon.Models
{
    public class SessionBasket : Basket
    {
        public static Basket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();

            basket.Session = session;

            return basket;
        }

        [JsonIgnore]
        public ISession Session { get; set; }

        public override void AddItem(Book bk, int qty, double price)
        {
            base.AddItem(bk, qty, price);
            Session.SetJson("Basket", this);
        }

        public override void RemoveItems(Book bk)
        {
            base.RemoveItems(bk);
            Session.SetJson("Basket", this);
        }


        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Basket");
        }
    }

}
