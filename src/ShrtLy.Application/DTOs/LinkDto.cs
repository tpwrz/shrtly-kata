using System;

namespace ShrtLy.Application.DTOs
{
    public class LinkDto
    {
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string ShortUrl { get; set; }
    }
}
