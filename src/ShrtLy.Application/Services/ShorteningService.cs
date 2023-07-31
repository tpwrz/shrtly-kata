using Microsoft.AspNetCore.WebUtilities;
using Shrtly.Common;
using ShrtLy.Application.DTOs;
using ShrtLy.Contracts;
using ShrtLy.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShrtLy.Application.Services
{
    public class ShorteningService : IShorteningService
    {
        private readonly ILinksRepository repository;

        public ShorteningService(ILinksRepository repository)
        {
            this.repository = repository;
        }

        public string ProcessLink(string url)
        {// Test our URL
            if (!Uri.TryCreate(url, UriKind.Absolute, out Uri result))
            {
                return null;
            }
            
            var link = new LinkDto
            {
                Id = Guid.NewGuid(),
                ShortUrl = result.ToString(),
                Url = url
            };
            var urlChunk = link.GetUrlChunk();

            return urlChunk;
        }
    }
}
