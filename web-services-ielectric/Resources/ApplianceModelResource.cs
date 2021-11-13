﻿namespace web_services_ielectric.Resources
{
    public class ApplianceModelResource
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string ImgPath { get; set; }
        public string PurchaseDate { get; set; }
        public ApplianceBrandResource ApplianceBrand { get; set; }
        public long ClientId { get; set; }
    }
}