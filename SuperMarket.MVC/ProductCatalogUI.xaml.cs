using System;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
using System.Windows;
using System.Windows.Controls;

namespace SuperMarket.MVC
{
    public partial class ProductCatalogUI : UserControl
    {
        public static readonly DependencyProperty ProductsProperty = DependencyProperty.Register(
            nameof(Products), typeof(IEnumerable<Product>), typeof(ProductCatalogUI), new PropertyMetadata(Array.Empty<Product>()));

        public IEnumerable<Product> Products
        {
            get { return (IEnumerable<Product>) GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }
        
        public ProductCatalogUI()
        {
            InitializeComponent();
        }
    }
}