﻿namespace Endava.TechCourse.BankApp.Shared
{
    public class CurrencyDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ChangeRate { get; set; }
        public bool CanBeRemoved { get; set; }
    }
}