using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Firat.Eticaret.Entity
{
    public class Order
    {
        public int Id { get; set; }

        [DisplayName("Sipariş numarası")]
        public string OrderNumber { get; set; }
        [DisplayName("Toplam")]
        public double Total { get; set; }
        [DisplayName("Sipariş tarihi")]
        public DateTime OrderDate { get; set; }
        [DisplayName("Sipariş Durumu")]
        public EnumOrderState OrderState { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string Username { get; set; }
        public string AdresBasligi { get; set; }
        public string Adres { get; set; }
        public string Sehir { get; set; }
        public string Semt { get; set; }
        public string Mahalle { get; set; }
        public string PostaKodu { get; set; }

        public virtual List<OrderLine> Orderlines{ get; set; }
    }

    public class OrderLine
    {
        public int Id { get; set; }
        [DisplayName("Sipariş Id")]
        public int OrderId { get; set; }
        [DisplayName("Sipariş")]
        public virtual Order Order { get; set; }
        [DisplayName("Miktar")]
        public int Quantity { get; set; }
        [DisplayName("Fiyat")]
        public double Price { get; set; }
        [DisplayName("Ürün Id")]
        public int ProductId { get; set; }
        [DisplayName("Ürün")]
        public virtual Product Product { get; set; }
    }
}