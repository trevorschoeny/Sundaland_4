﻿using System;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Sundaland.Infrastructure;

namespace Sundaland.Models
{
    public class SessionBasket : Basket
    {
        [JsonIgnore]
        public ISession Session { get; set; }

        public static Basket GetBasket(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            SessionBasket basket = session?.GetJson<SessionBasket>("Basket") ?? new SessionBasket();
            basket.Session = session;
            return basket;
        }

        public override void AddItem(Book book, int qty)
        {
            base.AddItem(book, qty);
            Session.SetJson("Basket", this);
        }

        public override void RemoveItem(Book book)
        {
            base.RemoveItem(book);
            Session.SetJson("Basket", this);
        }

        public override void ClearBasket()
        {
            base.ClearBasket();
            Session.Remove("Basket");
        }
    }
}
