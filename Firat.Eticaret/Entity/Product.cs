using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Firat.Eticaret.Entity
{
    public class Product
    {
        public int Id { get; set; }

        [DisplayName("Ürün Adı")]
        public string Name { get; set; }
        [DisplayName("Ürün Açıklama")]
        public string Description { get; set; }
        [DisplayName("Fiyat")]
        public double Price { get; set; }
        public int Stock { get; set; }
        [DisplayName("Fotoğraf")]
        public string Image { get; set; }
        [DisplayName("Anasayfa")]
        public bool IsHome { get; set; }
        [DisplayName("Onaylandı")]
        public bool IsApproved { get; set; }
        [DisplayName("Kategori Id")]
        public int CategoryId { get; set; }
        [DisplayName("Kategori")]
        public Category Category { get; set; }
        public object Tarih { get; internal set; }
        [DisplayName("Ürün Kodu")]
        public object ProductCode { get; internal set; }
    }
}