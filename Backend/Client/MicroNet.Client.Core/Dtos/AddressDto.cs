﻿namespace MicroNet.Client.Core.Dtos
{
    public class AddressDto
    {
        public string Street { get; set; } = default!;
        public string City { get; set; } = default!;
        public string State { get; set; } = default!;
        public string Country { get; set; } = default!;
        public string ZipCode { get; set; } = default!;
    }
}
